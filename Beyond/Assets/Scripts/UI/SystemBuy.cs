using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SystemBuy : MonoBehaviour {

    public Text lcTxt, pcTxt, starsPerTxt; //Mining and defense cost text
    public static long planetCost, lifeCost;
    public static int lifeMax;
    
	void Start () {
        if (!File.Exists(Application.dataPath + "/Player_Save.json")) {
            planetCost = 30;
            lifeCost = 100;
            lifeMax = 5;
        }
        
        starsPerTxt.gameObject.SetActive(false);
	}

	void Update () {

        if (GameObject.FindGameObjectsWithTag("Planet").Length == 9) //Planet cost text
            pcTxt.text = "MAX";
        else
            GlobalFunc.setLongAbbreviation(planetCost, pcTxt);

        //Life cost text
        if (GameObject.FindGameObjectsWithTag("Planet").Length < 3) {
            lcTxt.text = "3 PLANETS";
            lcTxt.color = new Color32(255, 0, 0, 255);
        } else if (Stats.lifeLevel < lifeMax) {
            GlobalFunc.setLongAbbreviation(lifeCost, lcTxt);
            lcTxt.color = new Color32(229, 198, 25, 255);
        } else {
            lcTxt.text = "MAX";
            lcTxt.color = new Color32(229, 198, 25, 255);
        }

        //Mine per min text
        if (Stats.mines > 0) { 
            starsPerTxt.gameObject.SetActive(true);
            long s = Stats.mines * 60;

            GlobalFunc.setLongAbbreviation(s, starsPerTxt);
            starsPerTxt.text = starsPerTxt.text + "/min";
        } else {
            starsPerTxt.gameObject.SetActive(false);
        }
    }

    //Click on mining button
    public void clickLife() { 
    
        if (Stats.stars >= lifeCost && Stats.lifeLevel < lifeMax) {
            Stats.lifeLevel += 1;
            PlaySounds.purchase = true;
            Stats.stars -= lifeCost;

            lifeCost *= 4;
            
            Stats.lifeStop += 20;
        } else {
            PlaySounds.shopClip = true;
        }
    }

    //Add planet button
    public void addPlanet() { 

        if (Stats.stars >= planetCost && GameObject.FindGameObjectsWithTag("Planet").Length != 9) {
            SystemSpawn.planetAmount += 1;
            Stats.stars -= planetCost;

            if (GameObject.FindGameObjectsWithTag("Planet").Length < 5)
                planetCost *= 2;
            else
                planetCost *= 4;

            PlaySounds.purchase = true;
        } else {
            PlaySounds.shopClip = true;
        }
    }
}
