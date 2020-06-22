using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FleetSpawn : MonoBehaviour {

    public GameObject playerFleet;
    public Image coolImg;
    public Text coolTxt;
    
    int shownCool;
    float cooldown;
    bool activated;

    void Start() {
        cooldown = 1f;
    }


    void Update() {
        if (Stats.life >= 50) {
           
            if (cooldown > 0) { //Allow fleet to be used if cooldown is zero
                cooldown -= Time.deltaTime;
            } else {
                coolImg.gameObject.SetActive(false);
                coolTxt.gameObject.SetActive(false);
            }

            if (activated) {  //Activate cooldown after fleet is placed
                coolImg.gameObject.SetActive(true);
                coolTxt.gameObject.SetActive(true);

                
                activated = false;
                cooldown = 51f;
            }

            coolTxt.color = Color.white;
            shownCool = (int)cooldown;
            coolTxt.text = shownCool.ToString();
        } else {
            coolTxt.color = new Color32(0, 255, 76, 255);
            coolTxt.text = "50%";
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            clickFleet();
        }
    }

    public void clickFleet() {
        if (cooldown <= 0) {
            Instantiate(playerFleet, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Quaternion.identity);
            activated = true;
            PlaySounds.shopClip = true;  
        }
    }
}
