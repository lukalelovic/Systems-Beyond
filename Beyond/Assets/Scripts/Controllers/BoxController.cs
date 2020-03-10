using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

    public GameObject boxPrefab;
    GameObject[] asteroids, swarms, planets, pirates;

    void Update () {
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        swarms = GameObject.FindGameObjectsWithTag("Swarm");
        planets = GameObject.FindGameObjectsWithTag("Orbit");
        pirates = GameObject.FindGameObjectsWithTag("Pirate");

        //Asteroid Box
        foreach (GameObject asteroid in asteroids) {
            if (asteroid.transform.childCount <= 0) {
                GameObject thisBox = Instantiate(boxPrefab, new Vector2(asteroid.transform.position.x, asteroid.transform.position.y), Quaternion.identity);
                thisBox.transform.localScale = asteroid.transform.localScale * 1.1f;
                thisBox.transform.parent = asteroid.transform;
            }
        }

        //Swarm Box
        foreach (GameObject swarm in swarms) {
            if (swarm.transform.childCount <= 0) {
                GameObject thisBox = Instantiate(boxPrefab, new Vector2(swarm.transform.position.x, swarm.transform.position.y), Quaternion.identity);
                thisBox.transform.localScale = swarm.transform.localScale * 1.4f;
                thisBox.transform.parent = swarm.transform;
            }
        }

        //Planet Box
        foreach (GameObject planet in planets) {
            if (planet.transform.childCount <= 1) {
                GameObject thisBox = Instantiate(boxPrefab, new Vector2(planet.transform.GetChild(0).transform.position.x, planet.transform.GetChild(0).transform.position.y), Quaternion.identity);
                thisBox.transform.localScale = planet.transform.localScale * 1.3f;
                thisBox.transform.parent = planet.transform;
            }
        }

        foreach (GameObject pirate in pirates) {
            if (pirate.transform.childCount <= 1) {
                GameObject thisBox = Instantiate(boxPrefab, new Vector2(pirate.transform.position.x, pirate.transform.position.y), Quaternion.identity);
                thisBox.transform.localScale = pirate.transform.localScale * 1.3f;
                thisBox.transform.parent = pirate.transform;
            }
        }
    }
}
