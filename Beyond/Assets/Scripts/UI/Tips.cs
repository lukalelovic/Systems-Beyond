using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour {

    public GameObject tipObj;
    public Text tipTxt;
    bool[] newTip;
    float fadeTime, tipTime;
    int iAlpha;

	void Start () {
        newTip = new bool[14];
        tipObj.gameObject.SetActive(false);
        iAlpha = 175;
        fadeTime = 3f;
        tipTime = 3f;
	}

	void Update () {

        //Make tip fade out
        if (tipObj.gameObject.activeInHierarchy == true) {
            fadeTime -= Time.deltaTime;
            if (fadeTime <= 0) {
                if (iAlpha > 2) {
                    iAlpha -= 2;
                    tipTxt.color = new Color32(255, 255, 255, (byte)iAlpha);
                }
            }

            if (iAlpha <= 2)
                tipObj.gameObject.SetActive(false);
        }
        
        if (Stats.life >= 20 && newTip[0] == false && Stats.lifeLevel == 1) { //Check for new tips
            triggerTip();
            tipTxt.text = "Your life has hit its max - Buy the next upgrade in the shop!";
            newTip[0] = true;
        } else if (Stats.life >= 25 && newTip[1] == false) {
            triggerTip();
            tipTxt.text = "Farms can now be built - Click on the third planet to purchase";
            newTip[1] = true;
        } else if (Stats.life >= 50 && newTip[2] == false) {
            triggerTip();
            tipTxt.text = "Fleet unlocked - Click the fleet button and place the fleet to deploy it!";
            newTip[2] = true;
        } else if (Stats.life >= 55 && newTip[3] == false) {
            triggerTip();
            tipTxt.text = "Colonies on other planets can now be established";
            newTip[3] = true;
        } else if (Stats.life >= 100 && newTip[4] == false) {
            triggerTip();
            tipTxt.text = "Prestige unlocked - Click the prestige button to travel to new system!";
            newTip[4] = true;
        } else if (Stats.life >= 101 && newTip[5] == false) {
            tipTime -= Time.deltaTime;
            if (tipTime <= 0) {
                triggerTip();
                tipTxt.text = "Your life has hit its max - Buy the next upgrade to increase it";
            }
            newTip[5] = true;
        } else if (Stats.stars >= 30 && newTip[6] == false) { //Show tips if condition is met
            triggerTip();
            tipTxt.text = "Planets can be added to your solar system in the shop";
            newTip[6] = true;
        } else if (Stats.stars >= 100 && newTip[7] == false && GameObject.FindGameObjectsWithTag("Orbit").Length >= 3) {
            triggerTip();
            tipTxt.text = "Upgrade your life in the shop to add more %";
            newTip[7] = true;
        }
	}

    public void triggerTip() {
        iAlpha = 175;
        tipTxt.color = new Color32(255, 255, 255, 255);
        tipObj.gameObject.SetActive(true);
        fadeTime = 3f;
    }
}
