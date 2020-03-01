using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseGame : MonoBehaviour {

    public Canvas pause;
    public GameObject[] menus; //Menu[0] is quit screen, menu[1] is help
    public static bool paused;

    void Start() {
        if (pause.gameObject.activeInHierarchy == true)
            pause.gameObject.SetActive(false);

    }

    void Update () {
        if (Input.GetMouseButtonDown(0)) { //Check if clicked out of pause menu
            if (!EventSystem.current.IsPointerOverGameObject()) {
                pause.gameObject.SetActive(false);
                Time.timeScale = 1;
                paused = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) { //Pause keybind

            if (pause.gameObject.activeInHierarchy == false)
                pause.gameObject.SetActive(true);
            else
                pause.gameObject.SetActive(false);

            if (Time.timeScale > 0)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;

            PlaySounds.shopClip = true;  
            paused = true;
        }
    }

    public void clickPause() {
        if (pause.gameObject.activeInHierarchy == false)
            pause.gameObject.SetActive(true);
        else
            pause.gameObject.SetActive(false);

        PlaySounds.shopClip = true;
        Time.timeScale = 0;
        paused = true;

    }
    public void clickResume() {
        pause.gameObject.SetActive(false);
        Time.timeScale = 1;
        PlaySounds.shopClip = true;
        paused = false;
    }
    public void clickHelp() {
        if (menus[1].gameObject.activeInHierarchy == false)
            menus[1].gameObject.SetActive(true);
        else
            menus[1].gameObject.SetActive(false);

        PlaySounds.shopClip = true;
    }
    public void confirm() { //Leave to Menu
        menus[0].gameObject.SetActive(true);
        PlaySounds.shopClip = true;
    }
    public void cancel() {
        menus[0].gameObject.SetActive(false);
        PlaySounds.shopClip = true;
    }
    public void clickQuit() {
        Application.Quit();
    }
}
