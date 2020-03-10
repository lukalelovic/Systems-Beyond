using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

public class PlanetMines : MonoBehaviour {

    public GameObject planetUI, customize;
    public Button addLifeBtn, mineBtn;
    public Text planetNumTxt, mineTxt, mineCostTxt, outputTxt;
    public Text[] customCostsTxt;
    public Sprite[] customs;
    
    GameObject currentPlanet;
    RaycastHit2D hit;

    public static bool clickedPlanet;
    public static int[] mineCost;
    public static int presitgeMult, changeCosts;
    int[] customCosts;
    int planetNum;
    bool clickingUI, assignCosts; 

    void Start()
    {
        planetUI.SetActive(false);
        addLifeBtn.gameObject.SetActive(false);
        customize.gameObject.SetActive(false);
        planetNum = 1;
        customCosts = new int[15]; //Customize Costs
        mineCost = new int[13];
        changeCosts = 0;
        presitgeMult = 1;
 
        for (int i = 1; i < customCosts.Length; i++) //Assign custom costs
            customCosts[i] = i * 2500; customCosts[0] = 1000;

        if (!File.Exists(Application.dataPath + "/Player_Save.json"))
            originalCosts();
    }

    void Update()
    {
        if (clickingUI == true) { //Deactivate planet UI
            if (Input.GetMouseButtonDown(0)) {
                if (!EventSystem.current.IsPointerOverGameObject()) {
                    planetUI.SetActive(false);
                    customize.SetActive(false);
                    clickingUI = false;
                    clickedPlanet = false;
                }
            }
        } else {
            if (Input.GetMouseButtonDown(0) && PauseGame.paused == false) { //Click on planet to display UI
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null && hit.transform.gameObject.tag.Equals("Planet")) { //Click on planet
                    planetClicked();
                    clickedPlanet = true;
                    PlaySounds.shopClip = true;
                }
                else if (hit.collider != null && hit.transform.parent != null && hit.transform.parent.tag == "Orbit") { //Planet Box
                    planetClicked();
                    clickedPlanet = true;
                    PlaySounds.shopClip = true;
                }
            }
        }

        if (GameObject.FindGameObjectsWithTag("Planet").Length >= 3) //Show upgrade life
            addLifeBtn.gameObject.SetActive(true);
        
        if (planetNum == 3) { //Assign cost text
            if (Stats.life >= 25) {
                Abbreviation.setAbbreviation(mineCost[planetNum - 1], mineCostTxt);
                mineCostTxt.color = new Color32(229, 198, 25, 255);
            }
            else {
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
                Abbreviation.setAbbreviation(mineCost[planetNum - 1], mineCostTxt);
                mineCostTxt.color = new Color32(229, 198, 25, 255);
            } else {
                mineCostTxt.text = "55%";
                mineCostTxt.color = new Color32(0, 255, 76, 255);
            }
            mineTxt.text = "Build Colonies";
        }

        outputTxt.text = "Output: x" + (((planetNum + 1) / 2f) * SunBuy.mineMult * presitgeMult).ToString();

        if (changeCosts != 0) {  //Prestige new planet costs
            originalCosts();
            changeCosts = 0;
        }

        /* Cheat key bind
        if (Input.GetKey(KeyCode.RightBracket) && Input.GetKey(KeyCode.LeftBracket)) {
            Stats.stars += 1000;
            if (Stats.life < 100)
                Stats.life += 1;
        }
        */
    }


    public void planetClicked() { //Click on planet
        planetNumTxt.text = hit.transform.gameObject.transform.parent.name.Substring(0, hit.transform.gameObject.transform.parent.name.Length - 1);

        int numParse;
        if (int.TryParse(hit.transform.gameObject.transform.parent.name.Substring(hit.transform.gameObject.transform.parent.name.Length - 1), out numParse) == true)
            planetNum = int.Parse(hit.transform.gameObject.transform.parent.name.Substring(hit.transform.gameObject.transform.parent.name.Length - 1)); //Assign planet number

        planetUI.SetActive(true);

        planetUI.transform.position = new Vector2(Input.mousePosition.x + 325, Input.mousePosition.y - 220);
        clickingUI = true;

        mineBtn.gameObject.SetActive(true);
    }

    public void buyMines() { //Purchase mines
        if (planetNum == 3) {
            if (Stats.life >= 25)
                mineCosts();
        } else {
            if (Stats.life >= 50)
                mineCosts();
        }
    }
    public void mineCosts() { //Add more cost to mines

        if (Stats.stars >= mineCost[planetNum - 1]) {
            Stats.stars -= mineCost[planetNum - 1];

            if (Stats.mines < 200) {
                Stats.mines += Mathf.RoundToInt(Mathf.Sqrt(Stats.mines + 1) * ((planetNum + 1) / 4) * SunBuy.mineMult * presitgeMult);
                mineCost[planetNum - 1] += Mathf.RoundToInt(Mathf.Sqrt(mineCost[planetNum - 1] * 6));
            } else {
                Stats.mines += Mathf.RoundToInt(Mathf.Sqrt(Stats.mines / 4) * ((planetNum + 1) / 4) * SunBuy.mineMult * presitgeMult);
                mineCost[planetNum - 1] += Mathf.RoundToInt(Mathf.Sqrt(mineCost[planetNum - 1] * 9));
            }

            PlaySounds.purchase = true;
        } else {
            PlaySounds.shopClip = true;
        }
    }

    public void clickCustomize() { //Customize planets
        customize.SetActive(true);
        planetUI.gameObject.SetActive(false);

        if (assignCosts == false) {
            for (int i = 0; i < customCostsTxt.Length; i++) {
                customCostsTxt[i].name = i.ToString();

                customCostsTxt[i].text = customCosts[int.Parse(customCostsTxt[i].name)].ToString();
            }
            assignCosts = true;
        }

        PlaySounds.shopClip = true;
    }

    public void buyCustom() { //Buy custom planet
    
        int current = int.Parse(EventSystem.current.currentSelectedGameObject.transform.GetChild(1).name);
        if (Stats.stars >= customCosts[current]) {
            foreach(GameObject p in GameObject.FindGameObjectsWithTag("Orbit")) {
                if (p.name.Contains(planetNum.ToString()))
                    currentPlanet = p;
            }

            //Change current planet sprite to sprite clicked on
            currentPlanet.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite;
            currentPlanet.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

            if (currentPlanet.transform.GetChild(0).transform.childCount > 0 && currentPlanet.transform.GetChild(0).transform.GetChild(0) != null) //Get children of planet and delete them
            {
                Destroy(currentPlanet.transform.GetChild(0).transform.GetChild(0).gameObject);
                Destroy(currentPlanet.transform.GetChild(0).transform.GetChild(1).gameObject);
            }

            Stats.stars -= customCosts[current];
            customCosts[current] = 0;
            customCostsTxt[current].text = "";

            PlaySounds.purchase = true;
        }
        else
            PlaySounds.shopClip = true;
    }

    public void originalCosts() { //Set cost of mines
        mineCost[0] = 100; mineCost[1] = 250; mineCost[2] = 100; mineCost[3] = 500; mineCost[4] = 1000;
        mineCost[5] = 2500; mineCost[6] = 5000; mineCost[7] = 7500; mineCost[8] = 10000; mineCost[9] = 15000; mineCost[10] = 20000; 
        mineCost[11] = 25000; 

        for (int i = 0; i < mineCost.Length; i++) {
            int costMult = 0;
            if (presitgeMult > 2)
                costMult = presitgeMult * 4;
            else
                costMult = presitgeMult;
            
            mineCost[i] *= costMult;
        }
    }
}
