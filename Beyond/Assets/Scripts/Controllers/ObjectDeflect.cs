using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeflect : MonoBehaviour {

    GameObject objHit;
    public static bool spawnSmalls;
    public static Vector2 spawnPos;

    float objHP;

    void Update() {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (Input.GetMouseButton(0)) {
            detectHit(hit, "Asteroid", ref PlaySounds.mineClip, ref objHP, 100f, ref BossSpawn.asteroidsMined, 10);
            detectHit(hit, "Swarm", ref PlaySounds.swarmClip, ref objHP, 100f, ref BossSpawn.swarmsMined, 15);
            detectHit(hit, "Pirate", ref PlaySounds.mineClip, ref objHP, 10f, ref BossSpawn.piratesMined, 12);
            detectHit(hit, "Red Asteroid", ref PlaySounds.mineClip, ref objHP, 100f, ref BossSpawn.asteroidsMined, 12);
        }

        if (Input.GetMouseButtonDown(0)) {
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            if (boss != null) {
                string bossType = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>().currentType;

                if (bossType.Equals("Swarm"))
                    detectHit(hit, "Boss", ref PlaySounds.swarmClip, ref BossSpawn.bossHealth, 1f, ref BossSpawn.bossesMined, 10);
                else if (bossType.Equals("Pirate") || bossType.Equals("Mega"))
                    detectHit(hit, "Boss", ref PlaySounds.mineClip, ref BossSpawn.bossHealth, 1f, ref BossSpawn.bossesMined, 10);
            }
        }
    }

    //Check for click on objects or collision box
    public void detectHit(RaycastHit2D hit, string tag, ref bool audioClip, ref float objHP, float hitAmnt, ref int bossIncrement, int elementChance) {
        if (hit.collider != null && (hit.transform.gameObject.tag.Equals(tag) || (hit.transform.parent != null && hit.transform.parent.tag.Equals(tag)))) {

            if (objHit != hit.transform.gameObject && !tag.Equals("Boss")) {
                objHit = hit.transform.gameObject;
                objHP = 100f;
            }

            objHP -= hitAmnt;

            if (objHP <= 0 && !tag.Equals("Boss")) {
                if (tag.Equals("Red Asteroid")) {
                    if (hit.transform.parent == null)
                        spawnSmalls = true;

                    spawnPos = hit.transform.position;
                    Destroy(objHit);
                } else if (hit.transform.parent != null && hit.transform.parent.tag.Equals(tag)) {
                    Destroy(objHit.transform.parent.gameObject);
                } else {
                    Destroy(objHit);
                }

                if (PrestigeController.prestigeLvl > 1)
                    Stats.stars += Random.Range(30, 70) * PlanetMines.presitgeMult * PrestigeController.prestigeLvl;
                else
                    Stats.stars += Random.Range(3, 7);

                objHP = 100;
                bossIncrement++;
            }

            //Possibly mine a rare element
            int randChance = Random.Range(elementChance, 101);
            if (randChance <= elementChance) 
                Stats.elements++;

            audioClip = true;
        }
    }
}
