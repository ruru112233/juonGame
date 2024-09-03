using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public int msgSpeed = 2;
}

public static class SaveAndLoader
{
    private static string FILE_NAME = "/savefile.json";


    public static void Save(SaveData saveData)
    {
        string jsonStr = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + FILE_NAME, jsonStr);
    }

    public static SaveData Load()
    {
        string path = Application.persistentDataPath + FILE_NAME;

        SaveData data = new SaveData();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
        }

        return data;
    }

}
