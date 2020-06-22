using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class PlanetMines : MonoBehaviour {

    public GameObject planetUI, customize, planetShield;
    public Button mineBtn;
    public Text planetNumTxt, mineTxt, mineCostTxt, shieldCostTxt, outputTxt;
    public Text[] customCostsTxt;
    
    RaycastHit2D hit;

    public static bool clickedPlanet, changeCosts; //Detect if planet was clicked, detect if costs need to be reset
    public static long[] mineCost;
    public static int presitgeMult;
    int[] customCosts; //Custom planet costs
    int planetNum;
    bool clickingUI, assignCosts; 

    void Start() {
        planetUI.SetActive(false);
        customize.gameObject.SetActive(false);

        planetNum = 1;
        presitgeMult = 1;
        changeCosts = false;

        customCosts = new int[15]; //Customize Costs
        mineCost = new long[13];

        //Assign customizable costs
        for (int i = 0; i < customCosts.Length; i++) { 
            if (i > 0)
                customCosts[i] = i * 20;
            else
                customCosts[i] = 10;
        }

        if (!File.Exists(Application.dataPath + "/Player_Save.json"))
            originalCosts();
    }

    void Update() {
        if (clickingUI) { //Deactivate planet UI
            if (Input.GetMouseButtonDown(0)) {
                if (!EventSystem.current.IsPointerOverGameObject()) {
                    planetUI.SetActive(false);
                    customize.SetActive(false);
                    clickingUI = false;
                    clickedPlanet = false;
                }
            }
        } else {
            if (Input.GetMouseButtonDown(0) && !PauseGame.paused && !BossSpawn.bossSpawned) { //Click on planet to display UI
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null && hit.transform.gameObject.tag.Equals("Planet")) { //Click on planet
                    planetClicked();
                    clickedPlanet = true;
                    PlaySounds.shopClip = true;
                }
                else if (hit.collider != null && hit.transform.parent != null && hit.transform.parent.tag == "Orbit") { //Click on planet Box
                    planetClicked();
                    clickedPlanet = true;
                    PlaySounds.shopClip = true;
                }
            }
        }

        //Return planet status + abbreviated cost
        if (planetNum == 3) {
            if (Stats.life >= 25) {
                GlobalFunc.setLongAbbreviation(mineCost[planetNum - 1], mineCostTxt);
                mineCostTxt.color = new Color32(229, 198, 25, 255);
            } else {
                mineCostTxt.text = "25%";
                mineCostTxt.color = new Color32(0, 255, 76, 255);
            }

            if (Stats.life < 41)
                mineTxt.text = "Build Farms";
            else if (Stats.life <= 100)
                mineTxt.text = "Build Factories";
            else
                mineTxt.text = "Build Colonies";
        } else {
            if (Stats.life >= 55) {
                GlobalFunc.setLongAbbreviation(mineCost[planetNum - 1], mineCostTxt);
                mineCostTxt.color = new Color32(229, 198, 25, 255);
            } else {
                mineCostTxt.text = "55%";
                mineCostTxt.color = new Color32(0, 255, 76, 255);
            }
            mineTxt.text = "Build Colonies";
        }

        //Reset and update new planet costs when prestiging
        if (changeCosts) {  
            originalCosts();
            changeCosts = false;
        }
        
        //Cheat Codes
        if (Input.GetKey(KeyCode.RightBracket) && Input.GetKey(KeyCode.LeftBracket)) {
            Stats.stars += 1000;
        } else if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.Alpha8)) {
            Intro.fadeOut = 0;
        } else if (Input.GetKeyDown(KeyCode.RightShift) && Input.GetKey(KeyCode.Alpha4)) {
            BossSpawn.bossesMined++;
        }

        outputTxt.text = "Output: x" + (((planetNum + 1) / 2f) * presitgeMult).ToString();
    }

    //Click on planet
    public void planetClicked() { 
        planetNumTxt.text = hit.transform.gameObject.transform.parent.name.Substring(0, hit.transform.gameObject.transform.parent.name.Length - 1);

        int numParse;
        if (int.TryParse(hit.transform.gameObject.transform.parent.name.Substring(hit.transform.gameObject.transform.parent.name.Length - 1), out numParse) == true)
            planetNum = int.Parse(hit.transform.gameObject.transform.parent.name.Substring(hit.transform.gameObject.transform.parent.name.Length - 1)); //Assign planet number

        planetUI.SetActive(true);

        int xPos, yPos;
        if (Input.mousePosition.x >= 1200) xPos = -200; else xPos = 325;
        if (Input.mousePosition.y >= 300) yPos = -220; else yPos = 60;

        planetUI.transform.position = new Vector2(Input.mousePosition.x + xPos, Input.mousePosition.y + yPos);
        clickingUI = true;

        mineBtn.gameObject.SetActive(true);
    }

    //Purchase mines
    public void buyMines() { 
        if (planetNum == 3) {
            if (Stats.life >= 25)
                mineCosts();
        } else {
            if (Stats.life >= 50)
                mineCosts();
        }
    }

    //Add more cost to mines
    public void mineCosts() { 

        if (Stats.stars >= mineCost[planetNum - 1]) {
            Stats.stars -= mineCost[planetNum - 1];

            Stats.mines += Mathf.RoundToInt(Mathf.Sqrt(Stats.mines + 1) * ((planetNum + 1) / 4) * presitgeMult);
            
            if (presitgeMult > 1)
                mineCost[planetNum - 1] += Mathf.RoundToInt(Mathf.Sqrt(mineCost[planetNum - 1] * presitgeMult * 9));
            else 
                mineCost[planetNum - 1] += Mathf.RoundToInt(Mathf.Sqrt(mineCost[planetNum - 1] * presitgeMult * 6));

            PlaySounds.purchase = true;
        } else {
            PlaySounds.shopClip = true;
        }
    }

    //Customize planets
    public void clickCustomize() { 
        customize.SetActive(true);
        planetUI.gameObject.SetActive(false);

        if (assignCosts == false) { //Assign custom cost text objects to custom costs
            for (int i = 0; i < customCostsTxt.Length; i++) {
                customCostsTxt[i].name = i.ToString();
                if (Stats.elements >= customCosts[i])
                    customCosts[i] = 0;
                
                if (customCosts[i] > 0)
                    customCostsTxt[i].text = customCosts[i].ToString();
                else
                    customCostsTxt[i].text = "";
            }
            assignCosts = true;
        }

        PlaySounds.shopClip = true;
    }

    //Buy custom planet
    public void buyCustom() { 
        GameObject currentPlanet = null;
        int current = int.Parse(EventSystem.current.currentSelectedGameObject.transform.GetChild(1).name); //Get custom planet clicked

        if (customCosts[current] == 0) {
            foreach(GameObject p in GameObject.FindGameObjectsWithTag("Orbit")) {
                if (p.name.Contains(planetNum.ToString()))
                    currentPlanet = p;
            }

            //Change current planet sprite to custom sprite clicked on
            currentPlanet.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite;
            currentPlanet.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

            if (currentPlanet.transform.GetChild(0).transform.GetChild(0) != null && currentPlanet.transform.GetChild(0).transform.childCount > 0) { //Get children of planet and delete them
                Destroy(currentPlanet.transform.GetChild(0).transform.GetChild(0).gameObject);
                Destroy(currentPlanet.transform.GetChild(0).transform.GetChild(1).gameObject);
            }

            PlaySounds.purchase = true;
        } else {
            PlaySounds.shopClip = true;
        }
    }
    
    //Set costs of planet purchasables
    public void originalCosts() { 
        mineCost[0] = 100; mineCost[1] = 250; mineCost[2] = 100; mineCost[3] = 500; mineCost[4] = 1000;
        mineCost[5] = 2500; mineCost[6] = 5000; mineCost[7] = 7500; mineCost[8] = 10000; mineCost[9] = 15000; mineCost[10] = 20000; 
        mineCost[11] = 25000; 

        //Assign planet mine + shield costs
        for (int i = 0; i < mineCost.Length; i++) {
            int costMult = 0;

            if (presitgeMult > 2) {
                costMult = presitgeMult * 4;
            } else {
                costMult = presitgeMult;
            }
            
            mineCost[i] *= costMult;
        }
    }
}