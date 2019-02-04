using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCanvasManager : MonoBehaviour {

    public static PauseCanvasManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null) {
            instance.gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
