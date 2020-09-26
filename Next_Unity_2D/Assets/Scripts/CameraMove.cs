using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    public float speed = 0.1f;

    Vector3 camPos = new Vector3(0, 0, -1);

    
    public float minZoom = 0.0f;
    public float maxZoom = 1250.0f;

    float zoom;

    void Update() {
        Move();

        transform.position = camPos;
        camPos = transform.position;

        //Zoom();

        //zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        //GetComponent<Camera>().orthographicSize = zoom;
    }

    void Move() {
        if (Input.GetKey(KeyCode.W)) {
            camPos.y += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S)) {
            camPos.y -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A)) {
            camPos.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D)) {
            camPos.x += speed * Time.deltaTime;
        }
    }

    void Zoom() {
        if (Input.GetKey(KeyCode.KeypadMinus)) {
            zoom -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.KeypadPlus)) {
            zoom += speed * Time.deltaTime;
        }

        if (Input.mouseScrollDelta.y > 0) {
            zoom -= speed * Time.deltaTime * 10f;
        }
        if (Input.mouseScrollDelta.y < 0) {
            zoom += speed * Time.deltaTime * 10f;
        }
    }
}
