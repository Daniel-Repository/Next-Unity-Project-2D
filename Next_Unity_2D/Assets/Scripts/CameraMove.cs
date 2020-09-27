using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    public float speed = 0.1f;
    public float maxY = 4.0f;
    public float minY = -4.0f;
    public float maxX = 10.0f;
    public float minX = -10.0f;

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

    //Position the camera through the WASD keys
    void Move() {
        if (Input.GetKey(KeyCode.W)) {
            if (camPos.y < maxY) {
                camPos.y += speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.S)) {
            if (camPos.y > minY) {
                camPos.y -= speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.A)) {
            if (camPos.x > minX) {
                camPos.x -= speed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.D)) {
            if (camPos.x < maxX) {
                camPos.x += speed * Time.deltaTime;
            }
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
