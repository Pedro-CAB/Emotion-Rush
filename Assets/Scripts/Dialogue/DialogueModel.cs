using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

/// <summary>
/// Structure containing a dialog tree for a dialogue sequence, after parsing it from a JSON file.
/// This class is responsible for parsing the JSON structure into a tree of DialogueLine objects.
/// </summary>
public class DialogueModel : MonoBehaviour
{
    /// <summary>
    /// Root of the dialogue tree.
    /// </summary>
    public DialogueLine RootLine { get; private set; }

    /// <summary>
    /// Path to the JSON File containing the dialogue sequence.
    /// </summary>
    public string JsonFilePath { get; private set; }

    /// <summary>
    /// Initiates a DialogueModel object by parsing the provided JSON content.
    /// </summary>
    /// <param name="jsonContent">JSON string that represents the dialogue structure.</param>
    public DialogueModel(string jsonContent)
    {
        Debug.Log(jsonContent);
        JObject rootObj = JObject.Parse(jsonContent);
        RootLine = ParseDialogueNode(rootObj);
        //PrintDialogueTree();
    }

    /// <summary>
    /// Helper class to store intermediate node data
    /// </summary>
    private class DialogueNodeData
    {
        public DialogueLine Line;
        public DialogueNodeData Parent;
        public List<DialogueNodeData> Children = new List<DialogueNodeData>();
        public bool Visited = false;
        public JToken JsonNode;
    }

    /// <summary>
    /// Recursively parses the JSON structure to create a tree of DialogueLine objects.
    /// </summary>
    /// <param name="rootNode"> The root JSON node containing the dialogue structure.</param>
    /// <returns></returns>
    private DialogueLine ParseDialogueNode(JToken rootNode)
    {
        // Step 1: Building the tree of DialogueNodeData from JSON (bottom-up)

        Dictionary<JToken, DialogueNodeData> nodeMap = new Dictionary<JToken, DialogueNodeData>();
        BuildNodeTree(rootNode, null, nodeMap);

        // Step 2: Setting up parent relationships using addPreviousLine from DialogueLine
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

        // Step 4: Connect children to parents using addNextLine or setOptions from DialogueLine
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

    /// <summary>
    /// Recursively builds the dialogue tree and DialogueLine objects
    /// </summary>
    /// <param name="node">Node to be processed.</param>
    /// <param name="parent">Parent of the node to be processed</param>
    /// <param name="nodeMap">Node Data for handling the node.</param>
    /// <returns></returns>
    private DialogueNodeData BuildNodeTree(JToken node, DialogueNodeData parent, Dictionary<JToken, DialogueNodeData> nodeMap)
    {
        string text = node.Value<string>("text");
        string typeStr = node.Value<string>("type");
        DialogueLine.LineType type = (DialogueLine.LineType)System.Enum.Parse(typeof(DialogueLine.LineType), typeStr);

        int score = 0;
        string feedback = null;
        string answer = null;

        if (type == DialogueLine.LineType.DialogueOption)
        {
            if (node["score"] != null)
                score = node.Value<int>("score");
            if (node["feedback"] != null)
                feedback = node.Value<string>("feedback");
        }
        else if (type == DialogueLine.LineType.EmotionOption)
        {
            if (node["answer"] != null)
                answer = node.Value<string>("answer");
        }

        DialogueLine dialogueLine;
        if (type == DialogueLine.LineType.DialogueOption)
        {
            dialogueLine = new DialogueLine(text, null, null, type, score, feedback);
        }
        else if (type == DialogueLine.LineType.EmotionOption)
        {
            // Treat EmotionOption as a node with children, store answer
            dialogueLine = new DialogueLine(text, null, null, type, 0, null, answer);
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

        // Always process children for all node types, including EmotionOption
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

    /// <summary>
    /// Connects the children of a DialogueNodeData to their parent DialogueLine.
    /// </summary>
    /// <param name="nodeData">Stored Data on the node to be connected.</param>
    private void ConnectNodeChildren(DialogueNodeData nodeData)
    {
        if (nodeData.Visited) return;
        nodeData.Visited = true;

        if (nodeData.Children.Count == 0)
            return;

        var type = nodeData.Line.type;
        if (type == DialogueLine.LineType.Linear
            || type == DialogueLine.LineType.DialogueOption
            || type == DialogueLine.LineType.EmotionOption)
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

    /// <summary>
    /// Prints the dialogue tree.
    /// </summary>
    public void PrintDialogueTree()
    {
        PrintDialogueLine(RootLine, "", true);
    }

    /// <summary>
    /// Recursively prints each DialogueLine with indentation
    /// </summary>
    /// <param name="line">Specifies the next line of the tree to be printed.</param>
    /// <param name="indent">Specifies the indentation of the next line to be printed.</param>
    /// <param name="isLast">True if this is the last line to be printed.</param>
    private void PrintDialogueLine(DialogueLine line, string indent, bool isLast)
    {
        if (line == null) return;

        // Print the current line with a tree branch
        string branch = indent + (isLast ? "└─ " : "├─ ");
        //Debug.Log(branch + line.content + " [" + line.type + "]");

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