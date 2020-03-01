using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject camFollow;
    public Camera cam;
    private float moveSpeed;

    void Update () {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && cam.orthographicSize > 6 && Intro.currentIntro == false) //Zoom in
            cam.orthographicSize -= 1;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && cam.orthographicSize <= 22 && Intro.currentIntro == false) //Zoom out
            cam.orthographicSize += 1;

        while (cam.orthographicSize > 10 && moveSpeed < ((cam.orthographicSize / 2))) //Change movement speed of camera depending on zoom
            moveSpeed++;
        
        while (cam.orthographicSize <= 10 && moveSpeed < 5)
            moveSpeed++;
        
        while (cam.orthographicSize > 10 && moveSpeed > ((cam.orthographicSize / 2)))
            moveSpeed--;

        while (cam.orthographicSize <= 10 && moveSpeed > 5) 
            moveSpeed--;
        
        if (Intro.currentIntro == false) { //Move the camera

            if ((Input.GetKey(KeyCode.D) || (Input.mousePosition.x >= Screen.width - 40)) && cam.transform.position.x < 15)
                camFollow.transform.Translate((Vector3.right * moveSpeed) * Time.deltaTime, Space.World);
            else if ((Input.GetKey(KeyCode.A) || (Input.mousePosition.x <= 40)) && cam.transform.position.x > -15)
                camFollow.transform.Translate((Vector3.left * moveSpeed) * Time.deltaTime, Space.World);
            else if ((Input.GetKey(KeyCode.W) || (Input.mousePosition.y >= Screen.height - 40)) && cam.transform.position.y < 15)
                camFollow.transform.Translate((Vector3.up * moveSpeed) * Time.deltaTime, Space.World);
            else if ((Input.GetKey(KeyCode.S) || (Input.mousePosition.y <= 40)) && cam.transform.position.y > -15)
                camFollow.transform.Translate((Vector3.down * moveSpeed) * Time.deltaTime, Space.World);
        }
    }
}
