using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetController : MonoBehaviour {

    public GameObject target, closest;
    public Vector3 thisPos, diff;
    public static bool placing;
    public float deleteTime, travelTowards, dist, currDist;

    void Start() {
        placing = true;
        deleteTime = FleetBuy.fleetHP;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = FleetCustomize.currentFleet;
    }

    void Update() {
        if (placing == false) {
            ClosestTarget(); //Find closest swarm or asteroid
            target = closest;

            if (target != null) {
                float speed = 30f * Time.deltaTime; //Rotate towards
                Vector2 dir = target.transform.position - this.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed);
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, FleetBuy.fleetSpeed); //Move towards
            }

            deleteTime -= Time.deltaTime;
        } else { 
            //Make fleet follow mouse
            if (deleteTime > (FleetBuy.fleetHP - 25))
                this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1);
        }
        
        if (Input.GetMouseButtonDown(0)) { //Keep object on mouse cursor
            if (placing == true)
                placing = false;
        }
        if (deleteTime <= 0) { //Destroy gameobject after 20 seconds
            Destroy(this.gameObject);
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Swarm")) {
            if (placing == false) {
                PlaySounds.swarmClip = true;
                Destroy(other.gameObject);
                Stats.stars += Random.Range(75, 201);
            }
        } else if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Pirate")) {
            if (placing == false) {
                PlaySounds.mineClip = true;
                Destroy(other.gameObject);
                Stats.stars += Random.Range(15, 30);
            }
        } else if (other.gameObject.CompareTag("Red Asteroid")) {
            if (placing == false) {
                if (other.transform.parent == null) {
                    PlaySounds.mineClip = true;
                    ObjectDeflect.spawnSmalls = true;
                    ObjectDeflect.spawnPos = other.gameObject.transform.position;
                }

                Destroy(other.gameObject);
                Stats.stars += Random.Range(15, 30);
            }
        }
    }

    public void ClosestTarget() {
        List<GameObject> targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Asteroid"));
        targets.AddRange(GameObject.FindGameObjectsWithTag("Red Asteroid"));
        targets.AddRange(GameObject.FindGameObjectsWithTag("Swarm"));
        targets.AddRange(GameObject.FindGameObjectsWithTag("Pirate"));

        closest = null;
        dist = Mathf.Infinity;
        thisPos = this.transform.position;

        foreach (GameObject t in targets) {
            diff = t.transform.position - thisPos;
            currDist = diff.sqrMagnitude;
            if (currDist < dist) {
                closest = t;
                dist = currDist;
            }
        }
    }
}
