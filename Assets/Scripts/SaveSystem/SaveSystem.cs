
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Application = UnityEngine.Device.Application;

public static class SaveSystem 
{
    public static void Save(Progress progress)
    {
        BinaryFormatter binaryFormatter = new();
        string path = Application.persistentDataPath + "/progress.pr";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        ProgressData progressData = new ProgressData(progress);
        binaryFormatter.Serialize(fileStream, progressData);
        fileStream.Close();
    }

    public static ProgressData Load()
    {
        string path = Application.persistentDataPath + "/progress.pr";

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            ProgressData progressData = binaryFormatter.Deserialize(fileStream) as ProgressData;
            fileStream.Close();
            return progressData;
        }
        
        Debug.Log("No SavedFile");
        return null;
    }

    public static void DeleteFile()
    {
        string path = Application.persistentDataPath + "/progress.pr";
        
        File.Delete(path);
    }
}
