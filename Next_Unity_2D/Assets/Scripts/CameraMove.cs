using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    
    public float moveSpeed = 3f;
    public float scrollSpeed = 2f;
    public float maxY = 4.0f;
    public float minY = -4.0f;
    public float maxX = 10.0f;
    public float minX = -10.0f;

    Vector3 camPos = new Vector3(0, 0, -1);

    float minZoom = 1.68f;
    float maxZoom = 3.5f;

    float zoom = 1.68f;

    void Update() {

        Move();
        transform.position = camPos;
        camPos = transform.position;

        Zoom();
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        GetComponent<Camera>().orthographicSize = zoom;
    }

    //Position the camera through the WASD keys
    void Move() {
        if (Input.GetKey(KeyCode.W)) {
            if (camPos.y < maxY) {
                camPos.y += moveSpeed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.S)) {
            if (camPos.y > minY) {
                camPos.y -= moveSpeed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.A)) {
            if (camPos.x > minX) {
                camPos.x -= moveSpeed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.D)) {
            if (camPos.x < maxX) {
                camPos.x += moveSpeed * Time.deltaTime;
            }
        }
    }

    void Zoom() {
        if (Input.GetKey(KeyCode.KeypadMinus)) {
            zoom -= scrollSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.KeypadPlus)) {
            zoom += scrollSpeed * Time.deltaTime;
        }

        if (Input.mouseScrollDelta.y > 0) {
            zoom -= scrollSpeed * Time.deltaTime * 10f;
        }
        if (Input.mouseScrollDelta.y < 0) {
            zoom += scrollSpeed * Time.deltaTime * 10f;
        }
    }
}
