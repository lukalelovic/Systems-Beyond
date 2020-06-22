using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{

    public GameObject[] asteroids;
    
    Vector3 randPos;
    int randomObj, spawnPoint, fleetExists;
    float randTime, frozenTime, greenTime, redTime;

    void Start() {
        randTime = 3f; greenTime = 7f; frozenTime = 3f; redTime = 6f;
    }

    void Update() {
        fleetExists = GameObject.FindGameObjectsWithTag("Fleet").Length;
        
        spawnAsteroid("normal", ref randTime, false, randPos);
        spawnAsteroid("frozen", ref frozenTime, true, new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 0f));
        spawnAsteroid("green", ref greenTime, false, randPos);
        spawnAsteroid("red", ref redTime, true, new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 0f));
        Spawns(); //Random pos for moving asteroids
    }

    public void spawnAsteroid(string clr, ref float spawnTime, bool frozen, Vector3 pos) {
        if (spawnTime <= 0) {
            randomObj = Random.Range(0, 3);

            if (!frozen)
                spawnPoint = Random.Range(1, 5); //Allow random spawn point

            GameObject newAsteroid = Instantiate(asteroids[randomObj], pos, Quaternion.identity);

            if (clr.Equals("red")) { //Red Asteroids
                newAsteroid.GetComponent<SpriteRenderer>().color = new Color32(249, 92, 92, 255);
                newAsteroid.tag = "Red Asteroid";
                spawnTime = Random.Range(4f, 8f) / (PrestigeController.prestigeLvl + 1);
                newAsteroid.AddComponent<RedAsteroid>();
            } else if (clr.Equals("green")) { //Green Asteroids
                newAsteroid.GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0, 255);
                newAsteroid.AddComponent<GreenAsteroid>();
                spawnTime = Random.Range(7f, 14f) / (PrestigeController.prestigeLvl + 1);
            } else if (frozen) { //Frozen Asteroids
                spawnTime = Random.Range(1.5f, 3.5f) / (PrestigeController.prestigeLvl + 1);
                newAsteroid.AddComponent<FrozenAsteroid>();
            } else { //Normal Asteroids
                spawnTime = Random.Range(15f, 30f) / (PrestigeController.prestigeLvl + 1);
                newAsteroid.AddComponent<AsteroidController>();
            }

            fleetCheck(spawnTime);
        } else {
            spawnTime -= Time.deltaTime;
        }

        if (ObjectDeflect.spawnSmalls == true) { //Spawn small red asteroids
            GameObject redHolder = null;
            float offsetX = -0.6f, offsetY = 0.6f;

            for (int i = 0; i < 5; i++) {
                if (i == 0) {
                    redHolder = new GameObject("Holder");
                    redHolder.transform.position = new Vector3(ObjectDeflect.spawnPos.x, ObjectDeflect.spawnPos.y, 0f);
                } else {
                    GameObject newRed = Instantiate(asteroids[randomObj], new Vector3(ObjectDeflect.spawnPos.x + offsetX, ObjectDeflect.spawnPos.y + offsetY, 0f), Quaternion.identity);
                    newRed.transform.parent = redHolder.transform;
                    newRed.AddComponent<RedAsteroid>();
                    newRed.GetComponent<SpriteRenderer>().color = new Color32(249, 92, 92, 255);
                    newRed.tag = "Red Asteroid";

                    if (i != 2) {
                        offsetX /= -1f;
                        offsetY /= -1f;
                    } else {
                        offsetY /= -1f;
                    }
                }
                randomObj = Random.Range(0, 3);
            }
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
