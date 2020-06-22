using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tips : MonoBehaviour {

    public Text tipTxt;

    bool[] newTip;
    float fadeTime;
    int iAlpha;

	void Start () {
        newTip = new bool[14];
        tipTxt.gameObject.SetActive(false);
        iAlpha = 175;
        fadeTime = 3f;
	}

	void Update () {

        //Make tip fade out
        if (tipTxt.gameObject.activeInHierarchy) {
            fadeTime -= Time.deltaTime;
            if (fadeTime <= 0) {
                if (iAlpha > 2) {
                    iAlpha -= 2;
                    tipTxt.color = new Color32(255, 255, 255, (byte)iAlpha);
                }
            }

            if (iAlpha <= 2)
                tipTxt.gameObject.SetActive(false);
        }
        
        //Check if tip hasn't been shown and should be
        if (Stats.lifeLevel == 1) 
            checkTip(Stats.life, 20, 0, "Your life has hit its max - Buy the next upgrade in the shop!");

        checkTip(Stats.life, 25, 1, "Farms can now be built - Click on the third planet to purchase");
        checkTip(Stats.life, 50, 2, "Fleet unlocked - Click the fleet button OR press Q to place and deploy it!");
        checkTip(Stats.life, 55, 3, "Colonies on other planets can now be established");
        checkTip(Stats.life, 100, 4, "Prestige unlocked - Click the prestige button to travel to new system!");
        checkTip(Stats.stars, 30, 6, "Planets can be added to your solar system in the shop");

        if (GameObject.FindGameObjectsWithTag("Orbit").Length >= 3)
            checkTip(Stats.stars, 100, 7, "Upgrade your life in the shop to add more %");
	}

    public void checkTip(long stat, int min, int index, string tip) {
        if (stat >= min && !newTip[index]) {
            triggerTip();
            tipTxt.text = tip;
            newTip[index] = true;
        }
    }

    public void triggerTip() {
        iAlpha = 175;
        tipTxt.color = new Color32(255, 255, 255, 255);
        tipTxt.gameObject.SetActive(true);
        fadeTime = 3f;
    }
}
