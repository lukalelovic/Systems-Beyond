using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandom : MonoBehaviour {

    public GameObject rocket, backgroundStar;
    private GameObject starParent, planet;
    private int starAmount, count;

    void Start() {
        starParent = new GameObject("Star Parent");
        starParent.transform.position = new Vector2(0, 0);

        starAmount = Random.Range(120, 200);
    }

    void Update () {
        
		if (Stats.life >= 53 && Stats.life <= 100) { //Rocket

            if (GameObject.FindGameObjectsWithTag("Rocket").Length != 1) {
                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Orbit")) {
                    if (p.name.Contains("3"))
                        planet = p.transform.GetChild(0).gameObject;
                }

                if (GameObject.FindGameObjectsWithTag("Orbit").Length >= 3) {
                    GameObject myRocket = Instantiate(rocket, new Vector2(planet.transform.position.x + 0.5f, planet.transform.position.y), Quaternion.identity);
                    myRocket.transform.parent = planet.transform;
                }
            }
        }

        while (count < starAmount) { //Background stars
            float randX = Random.Range(-60, 60);
            float randY = Random.Range(-60, 60);

            GameObject newStar = Instantiate(backgroundStar, new Vector2(randX, randY), Quaternion.identity);

            if (Vector2.Distance(newStar.transform.position, GameObject.FindGameObjectWithTag("Sun").transform.position) < 20)
                Destroy(newStar);
            else
                newStar.transform.parent = starParent.transform;

            count++;
        }
    }
}
