using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaundrySpawnManager : MonoBehaviour {

    public Object[] laundryPrefab;

    public GameObject SpawnLaundry() {

        ArrayList laundryList = new ArrayList();

        int laundryType = Random.Range(0, laundryPrefab.Length);

        GameObject newLaundryObj = Instantiate(laundryPrefab[laundryType], transform.position, transform.rotation) as GameObject;

        return newLaundryObj;
    }
}
