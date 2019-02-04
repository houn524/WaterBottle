using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laundry : MonoBehaviour {

    public GameObject centerLaundryObj;
    public GameObject[] subObjs;

    public float throwPower = 1000f;
    public float rotatePower = 100f;

    private bool isThrowed = false;
    public bool IsThrowed {
        get {
            return isThrowed;
        }
    }

    void Update() {
        if(!isThrowed) {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * rotatePower), Space.World);
        } else if(centerLaundryObj.transform.position.y <= -10.0f) {
            GameManager.instance.SpawnBottle();
            Destroy(gameObject);
        }
    }

    public void Throw(Vector2 dir) {
        //centerLaundryObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //centerLaundryObj.GetComponent<Rigidbody2D>().AddForce(dir * throwPower);

        foreach(GameObject obj in subObjs) {
            obj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            obj.GetComponent<Rigidbody2D>().AddForce(dir * throwPower);
        }
        isThrowed = true;
    }
}
