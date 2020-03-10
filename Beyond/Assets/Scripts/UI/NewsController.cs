using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewsController : MonoBehaviour {

    public TextAsset notificationsTxt;
    public Text newsTxt;
    
    public static bool asteroidHit;
    public static bool planetSpawned;
    int[] lifeValues = { 1,5,10,15,21,25,27,30,33,41,42,50,55,59,61,65,70,80,90,100 };
    bool[] used = new bool[30];
    string[] notifications;
    
	void Start () {
        notifications = (notificationsTxt.text.Split('\n'));
        newsTxt.text = "";
        asteroidHit = false;
        planetSpawned = false;
	}

	void Update () {
        if (asteroidHit == true) { //Asteroid notification
            newsTxt.text += AsteroidNews().ToString();
            asteroidHit = false;

            PlaySounds.notification = true;
        }

        if (planetSpawned == true) { //Planet notification
            newsTxt.text += PlanetNews().ToString();
            planetSpawned = false;

            PlaySounds.notification = true;
        }
        
        newsTxt.text += LifeNews().ToString();

        if (AlienSpawn.spawnSwarm == true)
            newsTxt.text += SwarmNews();

        if (AlienSpawn.spawnPirate == true)
            newsTxt.text += PirateNews();
    }

    private string AsteroidNews() {
        return "\n\n*ALERT* An asteroid just collided with a planet in the solar system!";
    }

    private string PlanetNews() {
        return "\n\n*ALERT* A planet has been created in the solar system!";
    }

    private string SwarmNews() {
        if (used[19] == false) {
            used[19] = true; PlaySounds.notification = true;
            return "\n\n*ALERT* An alien parasite known as 'The Swarm' has rapidly multiplied and seeks to harness your light!"; 
        } else {
            return "";
        }
    }

    private string PirateNews() {
        if (used[22] == false) {
            used[22] = true; PlaySounds.notification = true;
            return "\n\n*ALERT* Alien 'Space Pirates' are on approach to attack your planets!"; 
        } else {
            return "";
        }
    }

    private string LifeNews() { //Add notifications as life increases
        for (int i = 0; i < lifeValues.Length; i++) {
            if (Stats.life >= lifeValues[i] && used[i] == false && i != 22 && i != 19) {
                used[i] = true; PlaySounds.notification = true;
                return "\n\n" + notifications[i];
            }
        }

        return "";
    }
}
