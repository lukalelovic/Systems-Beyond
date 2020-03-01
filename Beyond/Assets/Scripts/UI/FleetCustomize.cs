using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FleetCustomize : MonoBehaviour {

    public Sprite[] fleets;
    public Text[] fleetCostsTxt;
    public static Sprite currentFleet;
    private int[] fleetCosts;

	void Start () {
        fleetCosts = new int[10];
        currentFleet = fleets[0];

        for (int i = 1; i < fleetCosts.Length; i++) //Assign custom costs
            fleetCosts[i - 1] = i * 5000;

        clickCustomize();
    }

    //Customize planets
    public void clickCustomize() {
        for (int i = 0; i < fleetCostsTxt.Length; i++) {
            fleetCostsTxt[i].name = i.ToString();
            fleetCostsTxt[i].text = fleetCosts[int.Parse(fleetCostsTxt[i].name)].ToString();
        }

        PlaySounds.shopClip = true;
    }

    public void buyCustom() { //Buy custom planet
    
        int current = int.Parse(EventSystem.current.currentSelectedGameObject.transform.GetChild(1).name);
        if (Stats.stars >= fleetCosts[current]) {
            currentFleet = fleets[current + 1];
            
            Stats.stars -= fleetCosts[current];
            fleetCosts[current] = 0;
            fleetCostsTxt[current].text = "";

            PlaySounds.purchase = true;
        } else {
            PlaySounds.shopClip = true;
        }
    }
}
