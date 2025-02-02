using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SettingSaveData
{
    public int msgSpeed = 2;
    public int bgmVolume = 1;
    public int seVolume = 1;
}

[System.Serializable]
public class RankingSaveData
{
    public List<float> rankingData;
}

public static class SaveAndLoader
{
    private static string SETTINGS_FILE_NAME = "/settingSavefile.json";
    private static string RANKING_FILE_NAME = "/RankingSavefile.json";

    public static void Save<T>(T saveData)
    {
        string jsonStr = JsonUtility.ToJson(saveData);

        File.WriteAllText(Application.persistentDataPath + GetSaveFileName<T>(), jsonStr);
    }

    public static T Load<T>() where T : class, new()
    {
        string path = Application.persistentDataPath + GetSaveFileName<T>();

        T data = GetSaveData<T>();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<T>(json);
        }

        return data;
    }

    private static string GetSaveFileName<T>()
    {
        if (typeof(T) == typeof(SettingSaveData))
        {
            return SETTINGS_FILE_NAME;
        }
        else
        {
            return RANKING_FILE_NAME;
        }
    }

    private static T GetSaveData<T>() where T : class, new()
    {
        if (typeof(T) == typeof(SettingSaveData))
        {
            return new SettingSaveData() as T;
        }
        else
        {
            return new RankingSaveData() as T;
        }

    }

}
