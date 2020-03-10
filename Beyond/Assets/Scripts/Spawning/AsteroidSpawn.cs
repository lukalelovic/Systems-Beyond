using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour {

    public GameObject[] asteroids;
    public GameObject[] frozenAsteroids;
    public GameObject[] greenAsteroids;
    public GameObject[] redAsteroids;

    Vector3 randPos, frozenPos, redPos;
    bool canSpawn, greenSpawn;
    int randomObj, spawnPoint, fleetExists;

    float randTime;
    float frozenTime, greenTime, redTime;

    void Start () {
        randTime = 3f; greenTime = 7f; frozenTime = 3f; redTime = 6f;
	}

    void Update() {
        fleetExists = GameObject.FindGameObjectsWithTag("Fleet").Length;
        nAsteroid();
        fAsteroid();
        gAsteroid();
        rAsteroid();
        Spawns(); //Random pos for moving asteroids
    }


    public void nAsteroid() { //Normal asteroids
    
        if (canSpawn == true)
        {
            randomObj = Random.Range(0, 3);
            spawnPoint = Random.Range(1, 5); //Allow random spawn point

            Instantiate(asteroids[randomObj], randPos, Quaternion.identity);

            randTime = Random.Range(15f, 30f) / (PrestigeController.prestigeLvl + 1); //Set the timer (makes the if false)

            fleetCheck(randTime); //Change the time spawning takes depending on amount of fleet
        }
        else
            randTime -= Time.deltaTime; //Countdown timer to spawn asteroid

        if (randTime <= 0)
            canSpawn = true;
        else
            canSpawn = false;
    }


    public void fAsteroid() { //Frozen asteroids
    
        if (frozenTime <= 0) {
            randomObj = Random.Range(0, 3);
            frozenPos = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 0f);
            Instantiate(frozenAsteroids[randomObj], frozenPos, Quaternion.identity);
            frozenTime = Random.Range(1.5f, 3.5f) / (PrestigeController.prestigeLvl + 1);

            fleetCheck(frozenTime);
        } else {
            frozenTime -= Time.deltaTime;
        }
    }

    public void gAsteroid() { //Green asteroids
    
        if (greenSpawn == true) {
            randomObj = Random.Range(0, 3);
            spawnPoint = Random.Range(1, 5); 

            Instantiate(greenAsteroids[randomObj], randPos, Quaternion.identity);
            greenTime = Random.Range(7f, 14f) / (PrestigeController.prestigeLvl + 1);

            fleetCheck(greenTime);
        }
        else
            greenTime -= Time.deltaTime; 

        if (greenTime <= 0)
            greenSpawn = true;
        else
            greenSpawn = false;
    }

    public void rAsteroid() { //Red asteroids
    
        if (redTime <= 0) {
            randomObj = Random.Range(0, 3);
            redPos = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 0f);
            Instantiate(redAsteroids[randomObj], redPos, Quaternion.identity);
            redTime = Random.Range(4f, 8f) / (PrestigeController.prestigeLvl + 1);

            fleetCheck(redTime);
        }
        else
            redTime -= Time.deltaTime;
        
        if (ObjectDeflect.spawnSmalls == true) { //Spawn small red asteroids
        
            GameObject redHolder = new GameObject("Holder"); redHolder.transform.position = new Vector3(ObjectDeflect.spawnPos.x, ObjectDeflect.spawnPos.y, 0f); randomObj = Random.Range(0, 3);
            GameObject redOne = Instantiate(redAsteroids[randomObj], new Vector3(ObjectDeflect.spawnPos.x - 0.6f, ObjectDeflect.spawnPos.y + 0.6f, 0f), Quaternion.identity); randomObj = Random.Range(0, 3);
            GameObject redTwo = Instantiate(redAsteroids[randomObj], new Vector3(ObjectDeflect.spawnPos.x + 0.6f, ObjectDeflect.spawnPos.y + 0.6f, 0f), Quaternion.identity); randomObj = Random.Range(0, 3);
            GameObject redThree = Instantiate(redAsteroids[randomObj], new Vector3(ObjectDeflect.spawnPos.x - 0.6f, ObjectDeflect.spawnPos.y - 0.6f, 0f), Quaternion.identity); randomObj = Random.Range(0, 3);
            GameObject redFour = Instantiate(redAsteroids[randomObj], new Vector3(ObjectDeflect.spawnPos.x + 0.6f, ObjectDeflect.spawnPos.y - 0.6f, 0f), Quaternion.identity);

            redOne.transform.parent = redHolder.transform; redTwo.transform.parent = redHolder.transform; redThree.transform.parent = redHolder.transform; redFour.transform.parent = redHolder.transform;
            redHolder.tag = "Holder";

            ObjectDeflect.spawnSmalls = false;
        } else {
            GameObject[] holders = GameObject.FindGameObjectsWithTag("Holder");
            foreach (GameObject holder in holders) {
                if (holder.transform.childCount == 0)
                    Destroy(holder);
            }
        }
    }

    public void Spawns() {
        if (spawnPoint > 0) {
            if (spawnPoint == 1)
                randPos = new Vector3(Random.Range(-70, 70), 50, 0f);
            else if (spawnPoint == 2)
                randPos = new Vector3(Random.Range(-70, 70), -50, 0f);
            else if (spawnPoint == 3)
                randPos = new Vector3(-50, Random.Range(-70, 70), 0f);
            else if (spawnPoint == 4)
                randPos = new Vector3(50, Random.Range(-70, 70), 0f);
        }
    }

    public void fleetCheck(float t) {
        if (fleetExists >= 2)
            t = t / 2;
    }
}
