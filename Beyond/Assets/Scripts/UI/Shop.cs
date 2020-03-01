using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public Canvas shop;
    private bool canShop;
    
	void Start () {
        shop.gameObject.SetActive(false);
	}

    void Update() {
        if (canShop == true) {
            shop.gameObject.SetActive(true);

            if (Input.GetMouseButton(0)) {
                if (!EventSystem.current.IsPointerOverGameObject())
                    canShop = false;
            }
        } else {
            shop.gameObject.SetActive(false);
        }
    }

    public void showShop() {
        if (canShop == false) {
            canShop = true;
            PlaySounds.shopClip = true;
        } else {
            canShop = false;
            PlaySounds.shopClip = true;
        }
    }
}
