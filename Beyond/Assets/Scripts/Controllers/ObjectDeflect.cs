using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeflect : MonoBehaviour {

    private GameObject objHit;
    private int resourceNum;
    private float objHP, regenTime;
    public static bool spawnSmalls;
    public static Vector2 spawnPos;

    void Start () {
		regenTime = 2f;
	}
    void Update() {
        RaycastHit2D hit;

        if (Input.GetMouseButton(0)) {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //Check for click on objects
            if (hit.collider != null && hit.transform.gameObject.tag == "Asteroid") {
                objHit = hit.transform.gameObject;
                PlaySounds.mineClip = true;
                hitObj();
            }
            if (hit.collider != null && hit.transform.gameObject.tag == "Swarm") {
                objHit = hit.transform.gameObject;
                PlaySounds.swarmClip = true;
                hitObj();
            }
            if (hit.collider != null && hit.transform.gameObject.tag == "Pirate") {
                objHit = hit.transform.gameObject;
                PlaySounds.mineClip = true;
                hitObj();
            }
            if (hit.collider != null && hit.transform.gameObject.tag == "Red Asteroid") {
                if (hit.transform.parent == null)
                    spawnSmalls = true;

                hitObj();
                PlaySounds.mineClip = true;
                spawnPos = hit.transform.gameObject.transform.position;
                Destroy(hit.transform.gameObject);
                resourceNum = 1;
            }


            //Hit collision boxes
            if (hit.collider != null && hit.transform.parent != null && hit.transform.parent.tag == "Asteroid") {
                objHit = hit.transform.parent.gameObject;
                hitObj();
                PlaySounds.mineClip = true;
            }
            if (hit.collider != null && hit.transform.parent != null && hit.transform.parent.tag == "Swarm") {
                objHit = hit.transform.gameObject;
                hitObj();
                PlaySounds.swarmClip = true;
            }
            if (hit.collider != null && hit.transform.parent != null && hit.transform.parent.tag == "Pirate") {
                objHit = hit.transform.gameObject;
                hitObj();
                PlaySounds.mineClip = true;
            }
        } else {
            if (objHP < 100) {
                regenTime -= Time.deltaTime;
                if (regenTime <= 0) {
                    objHP = 100;
                    regenTime = 2f;
                }
            }
        }

        //Get random resource from asteroid
        if (resourceNum != 0) {
            Stats.stars += Random.Range(3, 7) * PlanetMines.presitgeMult;
            resourceNum = 0;
        }
    }


    public void hitObj() {
        objHP -= 100;

        if (objHP <= 0) {
            Destroy(objHit);
            resourceNum = 1;
            objHP = 100;
        }
    }
}
