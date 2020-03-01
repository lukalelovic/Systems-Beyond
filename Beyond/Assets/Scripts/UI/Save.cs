using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class Save : MonoBehaviour {

    void Start() {
        if (File.Exists(Application.dataPath + "/Player_Save.json"))
            LoadGame();
    }

    public void SaveGame() {
        JSONObject playerJson = new JSONObject(); //Set JSON Values
        playerJson.Add("Stars", Stats.stars);
        playerJson.Add("Life", Stats.life);
        playerJson.Add("Prestige", PrestigeController.prestigeLvl);
        playerJson.Add("Mines", Stats.mines);
        playerJson.Add("Life Level", Stats.lifeLevel);
        playerJson.Add("Life Min", Stats.lifeMin);
        playerJson.Add("Life Stop", Stats.lifeStop);

        playerJson.Add("Planet Cost", SystemBuy.planetCost);
        playerJson.Add("Life Cost", SystemBuy.lifeCost);
        playerJson.Add("Life Max", SystemBuy.lifeMax);
        playerJson.Add("Health Cost", FleetBuy.healthCost);
        playerJson.Add("Speed Cost", FleetBuy.speedCost);
        playerJson.Add("Planet Amount", SystemSpawn.planetAmount);

        playerJson.Add("Fleet Speed", FleetBuy.fleetSpeed);
        playerJson.Add("Fleet Health", FleetBuy.fleetHP);
        playerJson.Add("Swarm Count", AlienSpawn.swarmCount);
        playerJson.Add("Pirate Count", AlienSpawn.pirateCount);

        JSONArray mineCosts = new JSONArray();
        foreach (int mine in PlanetMines.mineCost) {
            mineCosts.Add(mine);
        }
        playerJson.Add("Mine Costs", mineCosts);

        string path = Application.dataPath + "/Player_Save.json";
        File.WriteAllText(path, playerJson.ToString());

        PlaySounds.purchase = true;
    }

    public void LoadGame() {
        string path = Application.dataPath + "/Player_Save.json";
        string jsonString = File.ReadAllText(path);
        JSONObject playerJson = (JSONObject) JSON.Parse(jsonString);

        //Player Stats
        Stats.stars = playerJson["Stars"];
        Stats.life = playerJson["Life"];
        PrestigeController.prestigeLvl = playerJson["Prestige"];
        Stats.mines = playerJson["Mines"];
        Stats.lifeLevel = playerJson["Life Level"];
        Stats.lifeMin = playerJson["Life Min"];
        Stats.lifeStop = playerJson["Life Stop"];

        //Costs
        SystemBuy.planetCost = playerJson["Planet Cost"];
        SystemBuy.lifeMax = playerJson["Life Max"];
        SystemBuy.lifeCost = playerJson["Life Cost"];
        FleetBuy.healthCost = playerJson["Health Cost"];
        FleetBuy.speedCost = playerJson["Speed Cost"];
        SystemSpawn.planetAmount = playerJson["Planet Amount"];

        for (int i = 0; i < PlanetMines.mineCost.Length; i++)
            PlanetMines.mineCost[i] = playerJson["Mine Costs"].AsArray[i];

        FleetBuy.fleetSpeed = playerJson["Fleet Speed"];
        FleetBuy.fleetHP = playerJson["Fleet Health"];
        AlienSpawn.swarmCount = playerJson["Swarm Count"];
        AlienSpawn.pirateCount = playerJson["Pirate Count"];
    }
}