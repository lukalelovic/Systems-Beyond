using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawn : MonoBehaviour {

	public GameObject bossUI, bossPrefab;
	public Image healthUI;
	public Text healthTxt;

	public Text[] mineTexts;
	public Sprite[] bossSprites;

	public Material pixelLight;

	GameObject newBoss;

	public static bool bossSpawned;
	public static int swarmsMined, asteroidsMined, piratesMined, bossesMined;
	public static float bossHealth;

	float bossMax;
	int defeatBonus, sPrevious, pPrevious, bPrevious;

	void Start () {
		bossUI.SetActive(false);
		bossSpawned = false;
	
		sPrevious = 175 + (swarmsMined * 15); 
		pPrevious = 50 + (piratesMined * 3);
		bPrevious = 5 + (bossesMined * 2);
	}

	void Update () {

		if (!bossSpawned) {
			if (swarmsMined >= sPrevious) {
				int x = Random.Range(-20, 20), y = Random.Range(-12, 10);
				createBoss(new Vector3(x, y, 0), bossSprites[0], "Swarm", "Hive Mind", 40f, 10000);

				sPrevious *= 15;
			} else if (piratesMined >= pPrevious) {
				Vector3 thisPos = cornerPos();

				createBoss(thisPos, bossSprites[1], "Pirate", "Quarter Master", 100f, 50000);
				pPrevious *= 3;
			} else if (bossesMined >= bPrevious) {
				Vector3 thisPos = cornerPos();
				
				createBoss(thisPos, bossSprites[2], "Mega", "World Eater", 150f, 100000);
				bPrevious *= 2;
			}

			bossUI.SetActive(false);
		} else {
			bossUI.SetActive(true);
		}

		if (bossHealth <= 0 && bossSpawned) {
			Stats.stars += defeatBonus * (PlanetMines.presitgeMult);
			Destroy(newBoss.gameObject);
			BossSpawn.bossSpawned = false;
			PlaySounds.collide = true;
		}

		healthUI.fillAmount = bossHealth / bossMax;

		//Set stats menu text
		GlobalFunc.setAbbreviation(asteroidsMined, mineTexts[0]);
		GlobalFunc.setAbbreviation(swarmsMined, mineTexts[1]);
		GlobalFunc.setAbbreviation(piratesMined, mineTexts[2]);
		GlobalFunc.setAbbreviation(bossesMined, mineTexts[3]);
		GlobalFunc.setAbbreviation(Stats.elements, mineTexts[4]);
	}

	public void createBoss(Vector3 pos, Sprite spr, string type, string bossName, float HP, int bonus) {
		newBoss = Instantiate(bossPrefab, pos, Quaternion.identity);
		newBoss.GetComponent<SpriteRenderer>().sprite = spr;
		newBoss.GetComponent<BossController>().currentType = type;

		if (type.Equals("Mega")) //Give 2D lighting to Mega boss
			newBoss.GetComponent<SpriteRenderer>().material = pixelLight;
		
		healthTxt.text = bossName;
		bossHealth = bossMax = HP * (PrestigeController.prestigeLvl + 1);

		defeatBonus = bonus;
		bossSpawned = true;
	}

	public Vector2 cornerPos() {
		int choosePos = Random.Range(0, 4);

		int x = 0, y = 0;
		switch (choosePos) {
			case 0: 
				x = -38;
				y = -38;
				break;
			case 1:
				x = -38;
				y = 38;
				break;
			case 2:
				x = 38;
				y = 38;
				break;
			case 3:
				x = 38;
				y = -38;
				break;
		}

		return new Vector3(x, y, 0);
	}
}
