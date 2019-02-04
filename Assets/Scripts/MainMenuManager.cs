using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainMenuManager : MonoBehaviour {

    private bool isDialogShowing = false;

    public Canvas LevelSelectCanvas;
    public Canvas SettingsMenuCanvas;
    public Canvas LoginMenuCanvas;
    public Canvas SaveMenuCanvas;
    public Canvas RegisterMenuCanvas;
    public Canvas OKDialogCanvas;
    public Canvas YesOrNoDialogCanvas;
    public Canvas LoadingDialogCanvas;

    void Start() {
        Time.timeScale = 1f;
        LoadDatasAsync();
    }

    public void ShowLevelSelect() {
        if (!isDialogShowing)
            LevelSelectCanvas.sortingLayerName = "MenuUI";
    }

    public void HideLevelSelect() {
        if (!isDialogShowing)
            LevelSelectCanvas.sortingLayerName = "Hide";
    }

    public void ShowSettingsMenu() {
        if (!isDialogShowing)
            SettingsMenuCanvas.sortingLayerName = "MenuUI";
    }

    public void HideSettingsMenu() {
        if (!isDialogShowing)
            SettingsMenuCanvas.sortingLayerName = "Hide";
    }

    public void ShowLoginMenu() {
        if (!isDialogShowing) {
            if (ApplicationManager.instance.IsLoggedIn)
                SaveMenuCanvas.sortingLayerName = "MenuUI";
            else
                LoginMenuCanvas.sortingLayerName = "MenuUI";
        }
    }

    public void HideLoginMenu() {
        if (!isDialogShowing)
            LoginMenuCanvas.sortingLayerName = "Hide";
    }

    public void ShowSaveMenu() {
        if (!isDialogShowing)
            SaveMenuCanvas.sortingLayerName = "MenuUI";
    }

    public void HideSaveMenu() {
        if (!isDialogShowing)
            SaveMenuCanvas.sortingLayerName = "Hide";
    }

    public void ShowRegisterMenu() {
        if (!isDialogShowing)
            RegisterMenuCanvas.sortingLayerName = "MenuUI";
    }

    public void HideRegisterMenu() {
        if (!isDialogShowing)
            RegisterMenuCanvas.sortingLayerName = "Hide";
    }

    public void ShowOKDialog(string text) {
        GameObject.Find("txtOkDialog").GetComponent<Text>().text = text;
        OKDialogCanvas.sortingLayerName = "MenuUI";
        isDialogShowing = true;
        
    }

    public void HideOKDialog() {
        OKDialogCanvas.sortingLayerName = "Hide";
        isDialogShowing = false;
    }

    public void ShowYesOrNoDialog(string text, UnityAction yesAction) {
        GameObject.Find("txtYesOrNoDialog").GetComponent<Text>().text = text;
        YesOrNoDialogCanvas.sortingLayerName = "MenuUI";
        isDialogShowing = true;

        Button btn = GameObject.Find("btnYes").GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(yesAction);
    }

    public void HideYesOrNoDialog() {
        YesOrNoDialogCanvas.sortingLayerName = "Hide";
        isDialogShowing = false;
    }

    public void ShowLoadingDialog() {
        LoadingDialogCanvas.sortingLayerName = "MenuUI";
    }

    public void HideLoadingDialog() {
        LoadingDialogCanvas.sortingLayerName = "Hide";
    }

    public void Save() {
        ShowYesOrNoDialog("Do you want overwrite save data on the server?", () => {
            HideYesOrNoDialog();
            FirebaseManager.instance.Save();
            ShowLoadingDialog();
        });
    }

    public void Load() {
        ShowYesOrNoDialog("Do you want overwrite save data on the device?", () => {
            HideYesOrNoDialog();
            FirebaseManager.instance.Load();
            ShowLoadingDialog();
        });
    }

    public void Logout() {
        ShowYesOrNoDialog("Logout will delete all save data on this device.\nDo you want to logout?", () => {
            HideYesOrNoDialog();
            ShowLoadingDialog();
            ApplicationManager.instance.ResetSaveData();
            LoadDatasAsync();
            HideLoadingDialog();

            HideSaveMenu();
            ShowLoginMenu();
        });
    }

    public void Quit() {
        Application.Quit();
    }

    public void LoadDatasAsync() {
        StartCoroutine(LoadDatas());
    }

    IEnumerator LoadDatas() {
        ApplicationManager.instance.LoadDatas();

        LevelSelectButton[] levelSelectButtons = Resources.FindObjectsOfTypeAll<LevelSelectButton>() as LevelSelectButton[];

        foreach (LevelSelectButton levelSelectButton in levelSelectButtons) {
            yield return null;

            if (levelSelectButton.gameObject.scene.name == null)
                continue;

            levelSelectButton.UpdateUI();
        }
    }

}
