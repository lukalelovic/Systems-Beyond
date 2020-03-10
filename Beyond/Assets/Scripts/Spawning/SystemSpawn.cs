using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSpawn : MonoBehaviour {

    public Sprite[] foregrounds, backgrounds;
    public GameObject planet, planetBackground, planetForeground;
    public TextAsset namesTxt;
    
    public static int planetAmount, planetsSpawned;
    public static float previousPos;
    string[] planetNames;
    int r, g, b;

	void Start () {
        planetsSpawned = 0;
        previousPos = 2;

        planetNames = (namesTxt.text.Split('\n'));
    }

	void Update () {

        if (planetAmount < 11) { //Spawn planets
        
            while (planetsSpawned < planetAmount) {
                GameObject newPlanet = Instantiate(planet, new Vector3(0, -1.179993f, 0), Quaternion.identity);
                GameObject newBack = Instantiate(planetBackground, new Vector3(0, newPlanet.transform.GetChild(0).transform.position.y, -1), Quaternion.identity);
                GameObject newFore = Instantiate(planetForeground, new Vector3(0, newPlanet.transform.GetChild(0).transform.position.y, -1), Quaternion.identity);

                newBack.transform.parent = newPlanet.transform.GetChild(0).transform;
                newFore.transform.parent = newPlanet.transform.GetChild(0).transform;

                newPlanet.transform.GetChild(0).transform.position = new Vector2(0, previousPos);

                string planetName = planetNames[Random.Range(0, 39)];

                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Orbit")) {
                    while (g.name.Contains(planetName))
                        planetName = planetNames[Random.Range(0, 39)];
                }

                newPlanet.name = planetName + " " + (planetsSpawned + 1).ToString();

                if (planetsSpawned < 2)
                    previousPos += Random.Range(1f, 1.5f);
                else
                    previousPos += Random.Range(2f, 3f);

                planetsSpawned++;
            }
        }
    }
}
