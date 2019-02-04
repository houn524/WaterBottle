using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDCanvasManager : MonoBehaviour {

    public static HUDCanvasManager instance = null;

    [SerializeField]
    private Text txtLevel;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null) {
            instance.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        txtLevel.text = "Level " + GameManager.instance.CurrentLevel;
    }
}
