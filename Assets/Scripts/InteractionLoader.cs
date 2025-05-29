using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class InteractionLoader : MonoBehaviour
{
    public List<DialogueSequence> interactions = new List<DialogueSequence>();

    [System.Serializable]
    public class FileListWrapper
    {
        public string[] files;
    }

    public List<DialogueSequence> GetInteractions()
    {
        return interactions;
    }

    public IEnumerator LoadInteractionFiles(string sceneName)
    {
        interactions.Clear(); // limpa antes de carregar

        string basePath = Path.Combine(Application.streamingAssetsPath, "Interactions", sceneName);
        string listPath = Path.Combine(basePath, "filelist.json");

        string listJson;

        // 1. Ler filelist.json
#if UNITY_ANDROID
        UnityWebRequest listRequest = UnityWebRequest.Get(listPath);
        yield return listRequest.SendWebRequest();

        if (listRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro ao carregar lista de ficheiros: " + listRequest.error);
            yield break;
        }

        listJson = listRequest.downloadHandler.text;
#else
        if (!File.Exists(listPath))
        {
            Debug.LogError("filelist.json não encontrado: " + listPath);
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

#if UNITY_ANDROID
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
                Debug.LogError($"Ficheiro não encontrado: {filePath}");
                continue;
            }
#endif
            DialogueSequence seq = new DialogueSequence(json); // ← assumes construtor por string JSON
            interactions.Add(seq);
        }

        Debug.Log($"[{sceneName}] Total ficheiros carregados: {interactions.Count}");
    }
}
