using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseGame : MonoBehaviour {

    public GameObject[] menus; //Menu[0] is quit screen, menu[1] is help
    public static bool paused;

    void Start() {
        disableMenus();
    }

    void Update () {
        if (Input.GetMouseButtonDown(0)) { //Check if clicked out of pause menu
            if (!EventSystem.current.IsPointerOverGameObject()) {
                disableMenus();

                Time.timeScale = 1;
                paused = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) //Pause keybind
            menuInteract(2);
    }

    public void clickPause() {
        menuInteract(2);
    }

    public void clickHelp() {
        menuInteract(1);
    }

    public void confirm() {
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

    public void clickStats() {
        menuInteract(3);
    }

    public void menuInteract(int index) {
        if (!menus[index].gameObject.activeInHierarchy)
            menus[index].gameObject.SetActive(true);
        else
            menus[index].gameObject.SetActive(false);

        if (Time.timeScale > 0) {
            Time.timeScale = 0;
            paused = true;
        } else {
            Time.timeScale = 1;
            paused = false;
        }

        PlaySounds.shopClip = true;  
    }

    public void disableMenus() {
        foreach (GameObject menu in menus)
            menu.SetActive(false);
    }
}
