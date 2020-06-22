using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetController : MonoBehaviour {

    public GameObject target;
    public static bool placing;
    public float deleteTime, travelTowards;

    void Start() {
        placing = true;
        deleteTime = FleetBuy.fleetHP;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = FleetCustomize.currentFleet;
    }

    void Update() {
        if (placing == false) {
            target = GlobalFunc.FindClosest(this.transform.position, 
            new string[]{ "Asteroid", "Red Asteroid", "Swarm", "Pirate" }, new GameObject[]{}); //Find closest swarm or asteroid

            if (target != null) {
                float speed = 30f * Time.deltaTime; //Rotate towards
                Vector2 dir = target.transform.position - this.transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed);
                this.transform.position = Vector3.MoveTowards(this.transform.position, target.transform.position, FleetBuy.fleetSpeed); //Move towards
            }

            if (Vector2.Distance(this.transform.position, target.transform.position) <= 0.3f) //Check if fleet is continuously colliding with target
                Destroy(target);

            deleteTime -= Time.deltaTime;
        } else { 
            if (deleteTime > (FleetBuy.fleetHP - 25)) //Make fleet follow mouse
                this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 1);
        }
        
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Q)) //Keep object on mouse cursor
            placing = false;

        if (deleteTime <= 0) //Destroy gameobject after 20 seconds
            Destroy(this.gameObject);
    }
    
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Swarm")) {
            fleetCollide(other.gameObject, ref PlaySounds.swarmClip, 75, 201, ref BossSpawn.swarmsMined);
        } else if (other.gameObject.CompareTag("Asteroid")) {
            fleetCollide(other.gameObject, ref PlaySounds.mineClip, 15, 30, ref BossSpawn.asteroidsMined);
        } else if (other.gameObject.CompareTag("Red Asteroid")) {
            if (!placing) {
                if (other.transform.parent == null) {
                    PlaySounds.mineClip = true;
                    ObjectDeflect.spawnSmalls = true;
                    ObjectDeflect.spawnPos = other.gameObject.transform.position;
                }

                Destroy(other.gameObject);
                Stats.stars += Random.Range(15, 30);
                BossSpawn.asteroidsMined++;
            }
        } else if (other.gameObject.CompareTag("Pirate")) {
            fleetCollide(other.gameObject, ref PlaySounds.mineClip, 30, 61, ref BossSpawn.piratesMined);
        }
    }

    public void fleetCollide(GameObject other, ref bool clip, int starMin, int starMax, ref int mine) {
        if (!placing) {
            clip = true;
            Stats.stars += Random.Range(starMin, starMax);
            mine++;
            Destroy(other.gameObject);
        }
    }
}
