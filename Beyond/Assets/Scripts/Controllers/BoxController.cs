using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

    public GameObject boxPrefab;
    GameObject[] asteroids, planets;

    void Update () {
        asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
        planets = GameObject.FindGameObjectsWithTag("Orbit");

        //Asteroid Box
        foreach (GameObject asteroid in asteroids) {
            if (asteroid.transform.childCount <= 0) {
                GameObject thisBox = Instantiate(boxPrefab, new Vector2(asteroid.transform.position.x, asteroid.transform.position.y), Quaternion.identity);
                thisBox.transform.localScale = asteroid.transform.localScale * 1.1f;
                thisBox.transform.parent = asteroid.transform;
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
    }
}
