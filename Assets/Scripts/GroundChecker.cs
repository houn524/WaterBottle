using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {

    //void update() {
    //    ContactFilter2D contactFilter = new ContactFilter2D();
    //    Collider2D[] colliders = new Collider2D[5];
    //    int colliderCount = GetComponent<BoxCollider2D>().OverlapCollider(contactFilter, colliders);

    //    if (colliderCount > 0) {
    //        foreach(Collider2D col in colliders) {
    //            if(col.gameObject.tag != "basket") {
    //                gameObject.GetComponentInParent<Bottle>().IsGrounded = true;
    //                Debug.Log("true");
    //                return;
    //            }
    //        }
    //        gameObject.GetComponentInParent<Bottle>().IsGrounded = false;
    //    } else {
    //        gameObject.GetComponentInParent<Bottle>().IsGrounded = false;
    //    }
    //}

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag != "basket") {
            gameObject.GetComponentInParent<Bottle>().IsGrounded = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.tag != "basket") {
            gameObject.GetComponentInParent<Bottle>().IsGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D colllider) {
        gameObject.GetComponentInParent<Bottle>().IsGrounded = false;
    }
}
