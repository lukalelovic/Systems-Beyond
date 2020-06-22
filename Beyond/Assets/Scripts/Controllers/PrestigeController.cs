using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrestigeController : MonoBehaviour {

	public Button prestigeBtn;
	public Text prestigeTxt;
	public GameObject prestigeConfirm;
	
	public static bool prestige;
	public static int prestigeLvl;

	void Start () {
		prestigeBtn.gameObject.SetActive(false);
		prestigeTxt.gameObject.SetActive(false);
		prestigeConfirm.SetActive(false);

		prestige = false;
		prestigeLvl = 0;
	}
	
	void Update () {
		if (Stats.life == Stats.lifeStop && Stats.lifeLevel == SystemBuy.lifeMax && prestigeLvl < 5)
			prestigeBtn.gameObject.SetActive(true);
		else
			prestigeBtn.gameObject.SetActive(false);

		if (prestige == true) { //Set new system
			List<GameObject> deletes = new List<GameObject>();
			deletes.AddRange(GameObject.FindGameObjectsWithTag("Orbit")); deletes.AddRange(GameObject.FindGameObjectsWithTag("Swarm")); 
			deletes.AddRange(GameObject.FindGameObjectsWithTag("Red Asteroid")); deletes.AddRange(GameObject.FindGameObjectsWithTag("Fleet"));
			deletes.AddRange(GameObject.FindGameObjectsWithTag("Asteroid")); deletes.AddRange(GameObject.FindGameObjectsWithTag("Pirate"));

			foreach (GameObject g in deletes)
				Destroy(g);

			foreach(GameObject p in GameObject.FindGameObjectsWithTag("Orbit"))
				Destroy(p);

			PlanetMines.presitgeMult *= 4;
			PlanetMines.changeCosts = true;
			Stats.mines = 0;
			Stats.stars = 0;
			Stats.lifeMin += 101;
			SunBuy.randSun = 1;
			SystemSpawn.previousPos = 2;
			SystemSpawn.planetAmount = 4;
			SystemBuy.planetCost = 30;

			for (int i = 0; i < SystemSpawn.planetAmount; i++) {
				if (i < 5)
					SystemBuy.planetCost *= 4;
				else
					SystemBuy.planetCost *= 8;
			}
			SystemSpawn.planetsSpawned = 0;	
			SystemBuy.lifeMax *= 2;
			prestigeLvl++;
			prestige = false;
		}

		if (prestigeLvl > 0)
			prestigeTxt.gameObject.SetActive(true);
		
		if (prestigeTxt.gameObject.activeInHierarchy == true) {
			if (prestigeLvl < 5)
				prestigeTxt.text = "Prestige: " + prestigeLvl;
			else 
				prestigeTxt.text = "Prestige: MAX";
		}
			
	}

	public void clickPrestige() {
		prestigeConfirm.gameObject.SetActive(true);
		Time.timeScale = 0;
		PlaySounds.shopClip = true;
	}
	public void clickConfirm() {
		prestige = true;
		PlaySounds.purchase = true;
		prestigeConfirm.gameObject.SetActive(false);
		Time.timeScale = 1;
		Stats.life += 1;
	}
	public void clickCancel() {
		prestigeConfirm.gameObject.SetActive(false);
		Time.timeScale = 1;
		PlaySounds.shopClip = true;
	}
}
