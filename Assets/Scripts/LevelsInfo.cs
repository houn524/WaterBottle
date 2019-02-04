using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsInfo {

    public string lastLevel;
    public List<string> stars;

    public LevelsInfo() {
        lastLevel = "1";
        stars = new List<string>();
    }

    public LevelsInfo(int _lastLevel, int[] _stars) {
        lastLevel = _lastLevel.ToString();
        stars = new List<string>();
        foreach(int star in _stars) {
            stars.Add(star.ToString());
        }
    }

}