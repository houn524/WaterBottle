using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour {

    private int numberOfStar = 0;

    private int levelNumber;

    public GameObject LockPanel;
    public GameObject[] StarObjs;

    private void OnClick() {
        string levelNumber = transform.Find("Text").gameObject.GetComponent<Text>().text;

        ApplicationManager.instance.SelectedLevel = int.Parse(levelNumber);

        SceneManager.LoadScene("LevelLoading");
    }

    public void UpdateUI() {
        levelNumber = int.Parse(transform.Find("Text").gameObject.GetComponent<Text>().text);

        for (int i = 0; i < 3; i++) {
            StarObjs[i].SetActive(false);
        }
        LockPanel.SetActive(false);

        if (ApplicationManager.instance.LastLevel < levelNumber) {
            LockPanel.SetActive(true);
            
        } else {
            numberOfStar = ApplicationManager.instance.Stars[levelNumber - 1];

            for (int i = 0; i < numberOfStar; i++) {
                StarObjs[i].SetActive(true);
            }

            GetComponent<Button>().onClick.AddListener(OnClick);
        }
    }
}
