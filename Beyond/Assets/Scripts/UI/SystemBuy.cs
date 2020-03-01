using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SystemBuy : MonoBehaviour {

    public Text lcTxt, pcTxt, starsPerTxt; //Mining and defense cost text
    public static int lifeMax, planetCost, lifeCost;
    
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
            setAbbreviation(planetCost, pcTxt);

        if (Stats.lifeLevel < lifeMax) //Life cost text
            setAbbreviation(lifeCost, lcTxt);
        else
            lcTxt.text = "MAX";

        if (Stats.mines > 0) { //Mine per min text
            starsPerTxt.gameObject.SetActive(true);
            long s = Stats.mines * 60;
            setLongAbbreviation(s, starsPerTxt);
            starsPerTxt.text = starsPerTxt.text + "/min";
        } else {
            starsPerTxt.gameObject.SetActive(false);
        }
    }

    public void clickLife() { //Click on mining button
    
        if (Stats.stars >= lifeCost && Stats.lifeLevel < lifeMax) {
            Stats.lifeLevel += 1;
            PlaySounds.purchase = true;
            Stats.stars -= lifeCost;
            if (Stats.lifeLevel < 5)
                lifeCost *= 4;
            else
                lifeCost *= 8;
            
            Stats.lifeStop += 20;
        } else {
            PlaySounds.shopClip = true;
        }
    }

    public void clickPlanet() { //Add planet button
    
        if (Stats.stars >= planetCost && GameObject.FindGameObjectsWithTag("Planet").Length != 9) {
            SystemSpawn.planetAmount += 1;
            Stats.stars -= planetCost;

            if (GameObject.FindGameObjectsWithTag("Planet").Length < 5)
                planetCost *= 2;
            else
                planetCost *= 4;

            PlaySounds.purchase = true;
        }
    }

    public void setAbbreviation(int a, Text t) {
        if (a >= 1000 && a <= 999999) //Add mine abbreviations
            t.text = (a / 1000f).ToString("#.00") + "k";
        else if (a > 999999 && a <= 999999999)
            t.text = (a / 1000000f).ToString("#.00") + "M";
        else if (a > 999999999 && System.Convert.ToInt64(a) <= 999999999999)
            t.text = (System.Convert.ToInt64(a) / 1000000000f).ToString("#.00") + "B";
        else if (System.Convert.ToInt64(a) > 999999999999)
            t.text = (System.Convert.ToInt64(a) / 1000000000f).ToString("#.00") + "Q";
        else 
            t.text = a.ToString();
    }
    
    public void setLongAbbreviation(long a, Text t) {
        if (a >= 1000 && a <= 999999) //Add mine abbreviations
            t.text = (a / 1000f).ToString("#.00") + "k";
        else if (a > 999999 && a <= 999999999)
            t.text = (a / 1000000f).ToString("#.00") + "M";
        else if (a > 999999999 && a <= 999999999999)
            t.text = (a / 1000000000f).ToString("#.00") + "B";
        else if (a > 999999999999)
            t.text = (a / 1000000000f).ToString("#.00") + "Q";
        else 
            t.text = a.ToString();
    }
}
