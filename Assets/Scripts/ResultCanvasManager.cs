using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultCanvasManager : MonoBehaviour {

    public static ResultCanvasManager instance = null;

    public Image[] StarImages;

    public Sprite StarSprite;
    public Sprite EmptySprite;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null) {
            instance.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetStar(int numberOfStar) {

        for(int i = 0; i < 3; i++) {
            if (i < numberOfStar)
                StarImages[i].sprite = StarSprite;
            else
                StarImages[i].sprite = EmptySprite;
        }
    }
}
