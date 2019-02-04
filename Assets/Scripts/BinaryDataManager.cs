using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class BinaryDataManager {

    private const string FILENAME = "/levelsinfo.sav";

    public static void SaveLevelsInfoData(int lastLevel, int[] stars, bool isLoggedIn, string userId) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + FILENAME, FileMode.Create);

        LevelsInfoData data = new LevelsInfoData(lastLevel, stars, isLoggedIn, userId);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static LevelsInfoData LoadLevelsInfoData() {
        if(File.Exists(Application.persistentDataPath + FILENAME)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + FILENAME, FileMode.Open);

            LevelsInfoData data = bf.Deserialize(stream) as LevelsInfoData;

            Debug.Log("id : " + data.UserId);

            stream.Close();
            return data;
        } else {
            Debug.LogError("File does not exist.");
            int[] defaultStars = new int[30];

            return new LevelsInfoData(1, defaultStars, false, "");
        }
    }

    public static void DeleteLevelsInfoData() {
        if (File.Exists(Application.persistentDataPath + FILENAME)) {
            Debug.Log("Delete");
            File.Delete(Application.persistentDataPath + FILENAME);
        }
    }
}

[Serializable]
public class LevelsInfoData {

    public int[] LevelsInfo;
    public bool IsLoggedIn;
    public string UserId;

    public LevelsInfoData(int lastLevel, int[] stars, bool isLoggedIn, string userId) {
        LevelsInfo = new int[31];

        LevelsInfo[0] = lastLevel;
        
        for(int i = 1; i < 31; i++) {
            LevelsInfo[i] = stars[i - 1];
        }

        IsLoggedIn = isLoggedIn;

        UserId = userId;
    }

}