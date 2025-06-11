using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DialogueLoader : MonoBehaviour
{
    public List<DialogueModel> interactions = new List<DialogueModel>();

    [System.Serializable]
    public class FileListWrapper
    {
        public string[] files;
    }

    public List<DialogueModel> GetInteractions()
    {
        return interactions;
    }

    public IEnumerator LoadInteractionFiles(string sceneName)
    {
        interactions.Clear(); // Clear before loading new interactions

        string basePath = Path.Combine(Application.streamingAssetsPath, "Interactions", sceneName);
        string listPath = Path.Combine(basePath, "filelist.json");

        string listJson;

        // 1. Read filelist.json
#if UNITY_ANDROID || UNITY_WEBGL
        UnityWebRequest listRequest = UnityWebRequest.Get(listPath);
        yield return listRequest.SendWebRequest();

        if (listRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error loading file list: " + listRequest.error);
            yield break;
        }

        listJson = listRequest.downloadHandler.text;
#else
        if (!File.Exists(listPath))
        {
            Debug.LogError("filelist.json not found: " + listPath);
            yield break;
        }

        listJson = File.ReadAllText(listPath);
#endif

        FileListWrapper fileList = JsonUtility.FromJson<FileListWrapper>(listJson);

        // 2. Iterar pelos ficheiros JSON listados
        foreach (string filename in fileList.files)
        {
            string filePath = Path.Combine(basePath, filename);
            string json = null;

#if UNITY_ANDROID || UNITY_WEBGL
            UnityWebRequest fileRequest = UnityWebRequest.Get(filePath);
            yield return fileRequest.SendWebRequest();

            if (fileRequest.result == UnityWebRequest.Result.Success)
            {
                json = fileRequest.downloadHandler.text;
            }
            else
            {
                Debug.LogError($"Erro ao ler {filename}: {fileRequest.error}");
                continue;
            }
#else
            if (File.Exists(filePath))
            {
                json = File.ReadAllText(filePath);
            }
            else
            {
                Debug.LogError($"File not found: {filePath}");
                continue;
            }
#endif
            DialogueModel seq = new DialogueModel(json); // ← assumes construtor por string JSON
            interactions.Add(seq);
        }

        Debug.Log($"[{sceneName}] Total ficheiros carregados: {interactions.Count}");
    }
}
