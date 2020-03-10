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

    public static int mineMult, randSun;
    RaycastHit2D hit;
    int[] sunCosts;
    bool buying;
    
    void Start() {
        sunCosts = new int[sunBtns.Length + 1];
        sunCosts[0] = 0; sunCosts[1] = 25000; sunCosts[2] = 50000;
        sunCosts[3] = 100000; sunCosts[4] = 250000; sunCosts[5] = 500000;

        randSun = -1;
        mineMult = 1;
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

        if (Input.GetMouseButtonDown(0) && GameObject.FindGameObjectsWithTag("Intro").Length == 0 && PauseGame.paused == false && PlanetMines.clickedPlanet == false) {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.transform.gameObject.tag == "Sun" && buying == false) {
                sunUI.gameObject.SetActive(true);
                sunUI.transform.position = new Vector2(Input.mousePosition.x + 325, Input.mousePosition.y - 120);
                buying = true;
                PlaySounds.shopClip = true;
            }
        }

        if (randSun > -1) { //Set new random sun when prestiging
        
            sunCosts = new int[sunBtns.Length + 1];
            sunCosts[0] = 0; sunCosts[1] = 25000; sunCosts[2] = 50000;
            sunCosts[3] = 100000; sunCosts[4] = 250000; sunCosts[5] = 500000;

            for (int i = 0; i < sunCosts.Length; i++)
                sunCosts[i] *= 4;

            for (int i = 0; i < costTxts.Length; i++) //Reset bought stars
                setAbbreviation(sunCosts[i], costTxts[i]);

            MainSequence();
            randSun = -1;
        }
    }

    public void buySun() {
        GameObject currentBtn = EventSystem.current.currentSelectedGameObject;
        ParticleSystem.MainModule module = sunEffect.main;

        if (currentBtn.transform.name == "Main Sequence" && Stats.stars >= sunCosts[0]) {
            MainSequence();
            Stats.stars -= sunCosts[0];
            PlaySounds.purchase = true;
        } else if (currentBtn.transform.name == "Red Giant" && Stats.stars >= sunCosts[1]) {
            RedGiant();
            Stats.stars -= sunCosts[1];
            PlaySounds.purchase = true;
        } else if (currentBtn.transform.name == "Blue Giant" && Stats.stars >= sunCosts[2]) {
            BlueGiant();
            Stats.stars -= sunCosts[2];
            PlaySounds.purchase = true;
        } else if (currentBtn.transform.name == "White Dwarf" && Stats.stars >= sunCosts[3]) {
            sun.transform.localScale = new Vector3(1, 1, 1);
            sun.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
            module.startColor = new ParticleSystem.MinMaxGradient(new Color32(255, 255, 255, 34));
            module.startSize = 5.45f;
            Stats.stars -= sunCosts[3];
            sunCosts[3] = 0;
            costTxts[3].text = "";
            PlaySounds.purchase = true;
            mineMult = 4;
        } else if (currentBtn.transform.name == "Neutron Star" && Stats.stars >= sunCosts[4]) {
            sun.transform.localScale = new Vector3(2, 2, 1);
            sun.GetComponent<SpriteRenderer>().color = new Color32(214, 0, 181, 255);
            module.startColor = new ParticleSystem.MinMaxGradient(new Color32(214, 0, 181, 34));
            module.startSize = 10.8f;
            Stats.stars -= sunCosts[4];
            sunCosts[4] = 0;
            costTxts[4].text = "";
            PlaySounds.purchase = true;
            mineMult = 5;
        } else if (currentBtn.transform.name == "Black Dwarf" && Stats.stars >= sunCosts[5]) {
            sun.transform.localScale = new Vector3(2, 2, 1);
            sun.GetComponent<SpriteRenderer>().color = new Color32(10, 10, 10, 255);
            module.startColor = new ParticleSystem.MinMaxGradient(new Color32(75, 75, 75, 34));
            module.startSize = 11f;
            Stats.stars -= sunCosts[5];
            sunCosts[5] = 0;
            costTxts[5].text = "";
            PlaySounds.purchase = true;
            mineMult = 6;
        }
    }

    public void MainSequence() {
        ParticleSystem.MainModule module = sunEffect.main;

        sun.transform.localScale = new Vector3(2, 2, 1);
        sun.GetComponent<SpriteRenderer>().color = new Color32(251, 255, 0, 255);
        module.startColor = new ParticleSystem.MinMaxGradient(new Color32(255, 227, 0, 34));
        module.startSize = 10.9f;
        costTxts[0].text = "";
        mineMult = 1;
    }
    public void RedGiant() {
        ParticleSystem.MainModule module = sunEffect.main;

        sun.transform.localScale = new Vector3(2.3f, 2.3f, 1);
        sun.GetComponent<SpriteRenderer>().color = new Color32(208, 0, 0, 255);
        module.startColor = new ParticleSystem.MinMaxGradient(new Color32(208, 0, 0, 34));
        module.startSize = 13f;
        sunCosts[1] = 0;
        costTxts[1].text = "";
        mineMult = 2;
    }
    public void BlueGiant() {
        ParticleSystem.MainModule module = sunEffect.main;

        sun.transform.localScale = new Vector3(2.3f, 2.3f, 1);
        sun.GetComponent<SpriteRenderer>().color = new Color32(0, 70, 208, 255);
        module.startColor = new ParticleSystem.MinMaxGradient(new Color32(0, 70, 208, 34));
        module.startSize = 13f;
        sunCosts[2] = 0;
        costTxts[2].text = "";
        mineMult = 3;
    }

    public void setAbbreviation(int a, Text t) {
        if (a >= 1000 && a <= 999999) //Add mine abbreviations
            t.text = (a / 1000f).ToString("#.00") + "k";
        else if (a > 999999 && a <= 999999999)
            t.text = (a / 1000000f).ToString("#.00") + "M";
        else if (a > 999999999 && System.Convert.ToInt64(a) <= 999999999999)
            t.text = (a / 1000000000f).ToString("#.00") + "B";
        else if (System.Convert.ToInt64(a) > 999999999999)
            t.text = (System.Convert.ToInt64(a) / 1000000000f).ToString("#.00") + "Q";
        else 
            t.text = a.ToString();
    }
}
