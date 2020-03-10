using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Buying : MonoBehaviour {

    public GameObject fleetUI, systemUI, fleetCustomUI;
    bool custom;

	void Start () {
        fleetUI.SetActive(false);
        systemUI.SetActive(false);
        fleetCustomUI.SetActive(false);
    }

    void Update() {
        if (custom == true) {
            if (Input.GetMouseButtonDown(0))
                if (!EventSystem.current.IsPointerOverGameObject())
                    fleetCustomUI.gameObject.SetActive(false);
        }
    }

    public void systemClick() { //System button clicked
        PlaySounds.shopClip = true;
        if (systemUI.activeInHierarchy == false)
        {
            systemUI.SetActive(true);
            if (fleetUI.activeInHierarchy == true)
                fleetUI.gameObject.SetActive(false);
        }
        else
            systemUI.SetActive(false);
    }
    
    public void fleetClick() { //Fleet button clicked
        PlaySounds.shopClip = true;
        if (fleetUI.activeInHierarchy == false) {
            fleetUI.gameObject.SetActive(true);
            if (systemUI.activeInHierarchy == true)
                systemUI.gameObject.SetActive(false);
        } else {
            fleetUI.gameObject.SetActive(false);
        }
    }
    
    public void fleetCustomize() { //Customize fleet button
        PlaySounds.shopClip = true;
        if (fleetCustomUI.activeInHierarchy == false) {
            fleetCustomUI.gameObject.SetActive(true);
            custom = true;
        } else {
            fleetCustomUI.gameObject.SetActive(false);
        }
    }
}
