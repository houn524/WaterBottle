using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour {

    public static ApplicationManager instance = null;

    public int LastLevel { get; set; }
    public int[] Stars { get; set; }

    public bool IsLoggedIn = false;

    public string UserId;

    private int selectedLevel;
    public int SelectedLevel {
        set {
            selectedLevel = value;
        } get {
            return selectedLevel;
        }
    }

    private bool isDataLoaded = false;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null) {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    public void LoadDatas() {
        if(!isDataLoaded) {
            LevelsInfoData levelsInfo = BinaryDataManager.LoadLevelsInfoData();

            LastLevel = levelsInfo.LevelsInfo[0];
            Stars = new int[30];
            for (int i = 0; i < 30; i++) {
                Stars[i] = levelsInfo.LevelsInfo[i + 1];
            }

            IsLoggedIn = levelsInfo.IsLoggedIn;

            UserId = levelsInfo.UserId;

            isDataLoaded = true;
        }
    }

    public void ResetSaveData() {
        BinaryDataManager.DeleteLevelsInfoData();
        isDataLoaded = false;
    }
}
