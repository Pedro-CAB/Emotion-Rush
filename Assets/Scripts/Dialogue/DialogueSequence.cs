using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

public class DialogueSequence : MonoBehaviour
{
    // The root DialogueLine of the interaction (first line of the JSON)
    public DialogueLine RootLine { get; private set; }

    // Constructor that loads and parses a dialogue JSON file, setting the RootLine
    public DialogueSequence(string jsonFilePath)
    {
        string jsonText = File.ReadAllText(jsonFilePath);
        JObject rootObj = JObject.Parse(jsonText);
        RootLine = ParseDialogueNode(rootObj);
        PrintDialogueTree();
    }

    // Loads and parses a dialogue JSON file, saving the root DialogueLine
    public void LoadFromJson(string jsonFilePath)
    {
        string jsonText = File.ReadAllText(jsonFilePath);
        JObject rootObj = JObject.Parse(jsonText);
        RootLine = ParseDialogueNode(rootObj);
    }

    // Recursively parses a JSON node and its children into DialogueLine objects
    // Helper class to store intermediate node data
    private class DialogueNodeData
    {
        public DialogueLine Line;
        public DialogueNodeData Parent;
        public List<DialogueNodeData> Children = new List<DialogueNodeData>();
        public bool Visited = false;
        public JToken JsonNode;
    }

    // Entry point for parsing the JSON tree
    private DialogueLine ParseDialogueNode(JToken rootNode)
    {
        // Step 1: Build the tree of DialogueNodeData from JSON (bottom-up)
        Dictionary<JToken, DialogueNodeData> nodeMap = new Dictionary<JToken, DialogueNodeData>();
        BuildNodeTree(rootNode, null, nodeMap);

        // Step 2: Set up parent relationships (addPreviousLine)
        foreach (var nodeData in nodeMap.Values)
        {
            if (nodeData.Parent != null)
            {
                nodeData.Line.addPreviousLine(nodeData.Parent.Line);
            }
        }

        // Step 3: For option nodes, connect each option to its following line (if any)
        foreach (var nodeData in nodeMap.Values)
        {
            var type = nodeData.Line.type;
            if ((type == DialogueLine.LineType.TwoOption || type == DialogueLine.LineType.ThreeOption) && nodeData.Children.Count > 0)
            {
                foreach (var optionData in nodeData.Children)
                {
                    // If the option has a child, connect as next line
                    if (optionData.Children.Count == 1)
                    {
                        optionData.Line.addNextLine(optionData.Children[0].Line);
                        optionData.Children[0].Line.addPreviousLine(optionData.Line);
                    }
                }
            }
        }

        // Step 4: Connect children to parents (addNextLine or setOptions)
        foreach (var nodeData in nodeMap.Values)
        {
            if (nodeData.Visited) continue;
            ConnectNodeChildren(nodeData);
        }

        // The root node is the one with no parent
        foreach (var nodeData in nodeMap.Values)
        {
            if (nodeData.Parent == null)
                return nodeData.Line;
        }
        return null;
    }

    // Recursively build the node tree and DialogueLine objects
    private DialogueNodeData BuildNodeTree(JToken node, DialogueNodeData parent, Dictionary<JToken, DialogueNodeData> nodeMap)
    {
        string text = node.Value<string>("text");
        string typeStr = node.Value<string>("type");
        DialogueLine.LineType type = (DialogueLine.LineType)System.Enum.Parse(typeof(DialogueLine.LineType), typeStr);

        // Get score and feedback if present and type is DialogueOption
        int score = 0;
        string feedback = null;
        if (type == DialogueLine.LineType.DialogueOption)
        {
            if (node["score"] != null)
                score = node.Value<int>("score");
            if (node["feedback"] != null)
                feedback = node.Value<string>("feedback");
        }

        DialogueLine dialogueLine;
        if (type == DialogueLine.LineType.DialogueOption)
        {
            dialogueLine = new DialogueLine(text, null, null, type, score, feedback);
        }
        else
        {
            dialogueLine = new DialogueLine(text, null, null, type);
        }

        DialogueNodeData nodeData = new DialogueNodeData
        {
            Line = dialogueLine,
            Parent = parent,
            JsonNode = node
        };
        nodeMap[node] = nodeData;

        JToken childrenToken = node["children"];
        if (childrenToken != null && childrenToken.HasValues)
        {
            foreach (var child in childrenToken)
            {
                DialogueNodeData childData = BuildNodeTree(child, nodeData, nodeMap);
                nodeData.Children.Add(childData);
            }
        }
        return nodeData;
    }

    // Connect children to their parent node using addNextLine or setOptions
    private void ConnectNodeChildren(DialogueNodeData nodeData)
    {
        if (nodeData.Visited) return;
        nodeData.Visited = true;

        if (nodeData.Children.Count == 0)
            return;

        var type = nodeData.Line.type;
        if (type == DialogueLine.LineType.Linear || type == DialogueLine.LineType.DialogueOption)
        {
            // Only one child, connect as next line
            if (nodeData.Children.Count == 1)
            {
                nodeData.Line.addNextLine(nodeData.Children[0].Line);
                nodeData.Children[0].Line.addPreviousLine(nodeData.Line);
            }
        }
        else if (type == DialogueLine.LineType.TwoOption || type == DialogueLine.LineType.ThreeOption)
        {
            // Multiple children, connect as options
            DialogueLine[] options = new DialogueLine[nodeData.Children.Count];
            for (int i = 0; i < nodeData.Children.Count; i++)
            {
                options[i] = nodeData.Children[i].Line;
                nodeData.Children[i].Line.addPreviousLine(nodeData.Line);
            }
            nodeData.Line.setOptions(options);
        }

        // Recursively connect children
        foreach (var child in nodeData.Children)
        {
            ConnectNodeChildren(child);
        }
    }
    
    // Prints the dialogue tree starting from the RootLine
    public void PrintDialogueTree()
    {
        PrintDialogueLine(RootLine, "", true);
    }

    // Helper recursive function to print each DialogueLine with indentation
    private void PrintDialogueLine(DialogueLine line, string indent, bool isLast)
    {
        if (line == null) return;

        // Print the current line with a tree branch
        string branch = indent + (isLast ? "└─ " : "├─ ");
        Debug.Log(branch + line.content + " [" + line.type + "]");

        // Prepare indentation for children
        string childIndent = indent + (isLast ? "   " : "│  ");

        // Print options if any
        var options = line.dialogueOptions;
        if (options != null && options.Length > 0)
        {
            for (int i = 0; i < options.Length; i++)
            {
                bool lastOption = (i == options.Length - 1) && (line.nextLine == null);
                PrintDialogueLine(options[i], childIndent, lastOption);
            }
        }

        // Print next line if any (for linear/dialogue option)
        var next = line.nextLine;
        if (next != null)
        {
            PrintDialogueLine(next, childIndent, true);
        }
    }
}