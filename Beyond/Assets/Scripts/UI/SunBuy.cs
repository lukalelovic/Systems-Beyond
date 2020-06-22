using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SunBuy : MonoBehaviour {

    public GameObject sunUI, sun;
    public Text[] costTxts;
    public Button[] sunBtns;
    public ParticleSystem sunEffect;

    public static int randSun;
    RaycastHit2D hit;
    long[] sunCosts;
    bool buying;
    
    void Start() {
        sunCosts = new long[sunBtns.Length + 1];
        sunCosts[0] = 0; sunCosts[1] = 25000; sunCosts[2] = 50000;
        sunCosts[3] = 100000; sunCosts[4] = 250000; sunCosts[5] = 500000;

        for(int i = 0; i < sunCosts.Length; i++)
            sunCosts[i] *= (1 + PrestigeController.prestigeLvl);

        randSun = -1;
    }

    void Update () {
        if (buying == true) {
            if (Input.GetMouseButtonDown(0)) {
                if (!EventSystem.current.IsPointerOverGameObject()) {
                    sunUI.SetActive(false);
                    buying = false;
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && GameObject.FindGameObjectsWithTag("Intro").Length == 0 && !PauseGame.paused && !PlanetMines.clickedPlanet && !BossSpawn.bossSpawned) {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.transform.gameObject.tag == "Sun" && !buying) {
                sunUI.gameObject.SetActive(true);
                sunUI.transform.position = new Vector2(Input.mousePosition.x + 325, Input.mousePosition.y - 120);
                buying = true;
                PlaySounds.shopClip = true;
            }
        }

        if (randSun > -1) { //Set new random sun when prestiging
        
            sunCosts = new long[sunBtns.Length + 1];
            sunCosts[0] = 0; sunCosts[1] = 25000; sunCosts[2] = 50000;
            sunCosts[3] = 100000; sunCosts[4] = 250000; sunCosts[5] = 500000;

            for (int i = 0; i < sunCosts.Length; i++)
                sunCosts[i] *= 4;

            for (int i = 0; i < costTxts.Length; i++) //Reset bought stars
                GlobalFunc.setLongAbbreviation(sunCosts[i], costTxts[i]);

            checkSun("Main Sequence", new Vector3(2, 2, 1), new Color32(251, 255, 0, 255), 
            new Color32(255, 227, 0, 34), 10.9f, 0);

            randSun = -1;
        }
    }

    public void buySun() {

        checkSun("Main Sequence", new Vector3(2, 2, 1), new Color32(251, 255, 0, 255), 
        new Color32(255, 227, 0, 34), 10.9f, 0);

        checkSun("Red Giant", new Vector3(2.3f, 2.3f, 1), new Color32(208, 0, 0, 255), 
        new Color32(208, 0, 0, 34), 13f, 1);

        checkSun("Blue Giant", new Vector3(2.3f, 2.3f, 1), new Color32(0, 70, 208, 255), 
        new Color32(0, 70, 208, 34), 13f, 2);

        checkSun("White Dwarf", new Vector3(1, 1, 1), new Color32(255, 255, 255, 255), 
        new Color32(255, 255, 255, 34), 5.45f, 3);

        checkSun("Neutron Star", new Vector3(2, 2, 1), new Color32(214, 0, 181, 255), 
        new Color32(214, 0, 181, 34), 10.8f, 4);

        checkSun("Black Dwarf", new Vector3(2, 2, 1), new Color32(10, 10, 10, 255), 
        new Color32(75, 75, 75, 34), 11f, 5);
    }

    public void checkSun(string type, Vector3 scale, Color32 spriteColor, Color32 partSystColor, float startSize, int index) {
        GameObject currentBtn = EventSystem.current.currentSelectedGameObject;
        ParticleSystem.MainModule module = sunEffect.main;
        
        if ((currentBtn.transform.name.Equals(type) && Stats.stars >= sunCosts[index]) || randSun > -1) {
            sun.transform.localScale = scale;
            sun.GetComponent<SpriteRenderer>().color = spriteColor;
            module.startColor = new ParticleSystem.MinMaxGradient(partSystColor);

            module.startSize = startSize;
            costTxts[index].text = "";

            Stats.stars -= sunCosts[index];
            sunCosts[index] = 0;
            PlaySounds.purchase = true;
        }
    }
}
