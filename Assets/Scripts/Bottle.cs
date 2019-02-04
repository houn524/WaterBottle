using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour {

    public float throwPower = 1000f;

    private bool isThrowed = false;
    public bool IsThrowed {
        get {
            return isThrowed;
        }
    }

    public bool IsGrounded { get; set; }

    void Start() {
        IsGrounded = false;
    }

    void Update() {
        if (transform.position.y <= -10.0f) {
            GameManager.instance.SpawnBottle();
            Destroy(gameObject);
        }
    }

    public void Throw(Vector2 dir) {
        //centerLaundryObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //centerLaundryObj.GetComponent<Rigidbody2D>().AddForce(dir * throwPower);

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().AddForce(dir * throwPower);
        GetComponent<Rigidbody2D>().AddTorque(-90f);
        isThrowed = true;
    }

    public void JumpSpring(Vector2 vector) {
        Debug.Log("Jump");
        GetComponent<Rigidbody2D>().AddForce(vector);
    }

    //void OnCollisionStay2D(Collision2D col) {
    //    Debug.Log("ENter");
    //    if (col.gameObject.tag == "MovingBlock") {
            
    //        transform.parent = col.gameObject.transform;
    //    }
    //}

    //void OnCollisionExit2D(Collision2D col) {
    //    if (col.gameObject.tag == "MovingBlock") {
    //        transform.parent = null;
    //    }
    //}
}
