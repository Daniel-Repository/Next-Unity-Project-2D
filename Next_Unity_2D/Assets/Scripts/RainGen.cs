using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainGen : MonoBehaviour {

    private ParticleSystem rainSystem;
    private Camera mainCam;

    // Start is called before the first frame update
    void Update() {
        setPSWidth();
    }

    private void setPSWidth() {
        //Getting main cam width, converting to pixels, and setting the width of the PS to the same as the cam.
        mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        float camWidth = mainCam.orthographicSize * mainCam.aspect * 100; //Multi by 100 as 1 unit = 100 pixels

        rainSystem = this.GetComponent<ParticleSystem>();
        var rsShape = rainSystem.shape;
        rsShape.scale = new Vector3(camWidth + 200, 0, 0);
    }
}
