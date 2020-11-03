using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private float targetTime = 1.0f;

    public TileManager tileManager;

    private void Start() {
        
    }

    // Start is called before the first frame update
    void Update() {
        targetTime -= Time.deltaTime;

        if(targetTime <= 0.0f) {
            timerEnded();
        }
    }

    void timerEnded() {
        tileManager.timerUpdate();
        //print("Timer tick");
        targetTime = 1.0f;
    }
}
