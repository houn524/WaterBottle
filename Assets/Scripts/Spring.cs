using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpringDirection {
    Horizontal,
    Vertical
}

public class Spring : MonoBehaviour {

    public float power = 700f;

    public SpringDirection direction;

    private float delayTimer = 0f;

    private bool isDelay = false;

    void Update() {
        if(isDelay) {
            delayTimer += Time.deltaTime;
            if(delayTimer >= 1.0f) {
                isDelay = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Bottle" && !isDelay) {
            GetComponent<Animator>().SetTrigger("trigSpring");
            if(direction == SpringDirection.Horizontal) {
                col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, col.gameObject.GetComponent<Rigidbody2D>().velocity.y);
            } else {
                col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(col.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            }
            
            col.gameObject.GetComponent<Bottle>().JumpSpring(transform.up * power);
            delayTimer = 0f;
            isDelay = true;
        }
    }
}
