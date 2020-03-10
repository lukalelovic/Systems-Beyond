using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawn : MonoBehaviour {

    public GameObject[] swarms;
    public GameObject pirate;
    
    public static int swarmCount, pirateCount;
    public static bool spawnSwarm, spawnPirate;
    float randX, randY, swarmTime, pirateTime;
    int swarmNum, spawnPt;

    void Start() {
        swarmTime = pirateTime = 1f;
        spawnSwarm = false;
        spawnPirate = false;
    }

    void Update () {

        if (Stats.life >= 57)
            spawnSwarm = true;

        if (Stats.life >= 75)
            spawnPirate = true;

        if (Stats.life <= 25 && spawnSwarm == true)
            spawnSwarm = false;

        if (spawnSwarm == true) { //Spawn Swarm
            swarmNum = Random.Range(0, 3); //Get random swarm object
            spawnPt = Random.Range(1, 5); //Get random spawn point

            swarmTime -= Time.deltaTime;
            if (swarmTime <= 0) {
                randPt();
                int spawnAmount = Random.Range(15, 45);
                GameObject newHive = new GameObject("Hive");
                newHive.tag = "Hive";
                for (int i = 0; i < spawnAmount; i++)
                {
                    GameObject newSwarm = Instantiate(swarms[swarmNum], new Vector2(randX, randY), Quaternion.identity);
                    newSwarm.transform.SetParent(newHive.transform);
                }

                swarmCount += 1;

                if (swarmCount < 4) //Set amount of time it takes to spawn swarm
                    swarmTime = Random.Range(5, 20);
                else if (swarmCount < 12)
                    swarmTime = Random.Range(20, 60);
                else
                    swarmTime = Random.Range(60, 120);
            }
        }

        if (spawnPirate == true) { //Spawn Pirate

            spawnPt = Random.Range(1, 5);
            pirateTime -= Time.deltaTime;
            if (pirateTime <= 0) {
                randPt();
                for (int i = 0; i < Random.Range(1, 5); i++)
                    Instantiate(pirate, new Vector2(randX, randY), Quaternion.identity);

                pirateCount += 1;

                if (pirateCount < 10)
                    pirateTime = Random.Range(5, 20);
                else
                    pirateTime = Random.Range(25, 60);
            }
        }

        foreach(GameObject h in GameObject.FindGameObjectsWithTag("Hive"))
            if (h.transform.childCount <= 0)
                Destroy(h);
    }

    public void randPt() {
        if (spawnPt == 1) {
            randX = Random.Range(-70, 70);
            randY = 50;
        } else if (spawnPt == 2) {
            randX = Random.Range(-70, 70);
            randY = -50;
        } else if (spawnPt == 3) {
            randX = -50;
            randY = Random.Range(-70, 70);
        } else if (spawnPt == 4) {
            randX = 50;
            randY = Random.Range(-70, 70);
        }
        spawnPt = 0;
    }
}
