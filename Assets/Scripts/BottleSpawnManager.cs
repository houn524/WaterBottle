using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleSpawnManager : MonoBehaviour {

    public Object[] BottlePrefab;

    public GameObject SpawnBottle() {

        ArrayList laundryList = new ArrayList();

        int BottleType = Random.Range(0, BottlePrefab.Length);

        GameObject newBottleObj = Instantiate(BottlePrefab[BottleType], transform.position, transform.rotation) as GameObject;

        return newBottleObj;
    }
}
