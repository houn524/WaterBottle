using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

    private bool isGet = false;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Bottle") && !isGet) {
            isGet = true;
            GameManager.instance.GetStar();
            StartCoroutine(PlaySound());
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Hide";
        }
    }

    IEnumerator PlaySound() {
        GetComponent<AudioSource>().Play();

        while(GetComponent<AudioSource>().isPlaying) {
            yield return null;
        }

        Destroy(gameObject);
    }
}
