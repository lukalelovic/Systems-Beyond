using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FleetBuy : MonoBehaviour {

    public Text hpcTxt, scTxt; //Health and speed cost text 
    public static float fleetHP, fleetSpeed;
    public static long healthCost, speedCost;

	void Start () {
        if (!File.Exists(Application.dataPath + "/Player_Save.json")) {
            healthCost = 30;
            speedCost = 15;
            fleetSpeed = 0.08f;
            fleetHP = 25;
        }
	}

	void Update () {
        if (Stats.life >= 50) {
            hpcTxt.color = new Color32(229, 198, 25, 255);
            scTxt.color = new Color32(229, 198, 25, 255);

            if (fleetSpeed < 0.2f)
                GlobalFunc.setLongAbbreviation(speedCost, scTxt);
            else
                scTxt.text = "MAX";

            if (fleetHP < 300) 
                GlobalFunc.setLongAbbreviation(healthCost, hpcTxt);
            else
                hpcTxt.text = "MAX";
        } else {
            hpcTxt.color = new Color32(0, 255, 76, 255);
            scTxt.color = new Color32(0, 255, 76, 255);
            hpcTxt.text = "50%";
            scTxt.text = "50%";
        }
	}

    public void clickHealth() {
        if (Stats.stars >= healthCost && Stats.life >= 50 && fleetHP < 300) {
            Stats.stars -= healthCost;

            fleetHP = fleetHP * 1.2f;
            healthCost = healthCost * 3;
            PlaySounds.purchase = true;
        } else {
            PlaySounds.shopClip = true;
        }
    }

    public void clickSpeed() {
        if (Stats.stars >= speedCost && Stats.life >= 50 && fleetSpeed < 0.2f) {
            Stats.stars -= speedCost;
            
            fleetSpeed += 0.005f;
            speedCost = speedCost * 2;
            PlaySounds.purchase = true;
        } else {
            PlaySounds.shopClip = true;
        }
    }
}
