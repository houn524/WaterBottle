using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour {

    public static BackgroundMusicManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != null) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
