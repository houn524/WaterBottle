using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoadingManager : MonoBehaviour {

    public Slider ProgressBar;

	// Use this for initialization
	void Start () {
        StartCoroutine(loadLevel());
	}

    IEnumerator loadLevel() {

        yield return null;

        AsyncOperation async = SceneManager.LoadSceneAsync("Level" + ApplicationManager.instance.SelectedLevel);
        async.allowSceneActivation = false;

        float timer = 0f;

        while(!async.isDone) {
            yield return null;

            timer += Time.deltaTime;

            if (async.progress >= 0.9f) {
                ProgressBar.value = Mathf.Lerp(ProgressBar.value, 1f, timer);

                if (ProgressBar.value == 1.0f)
                    async.allowSceneActivation = true;
            } else {
                ProgressBar.value = Mathf.Lerp(ProgressBar.value, async.progress, timer);
                if (ProgressBar.value >= async.progress) {
                    timer = 0f;
                }
            }
        }
    }
}
