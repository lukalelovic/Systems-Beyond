using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmController : MonoBehaviour {

    public Rigidbody2D rb;
    public float swarmScale, distToSun;
    private float subtractTime;
    public bool travelTowards;

	void Start () {
        swarmScale = Random.Range(0.8f, 1.5f);

        this.gameObject.transform.localScale = new Vector3(swarmScale, swarmScale, 1f);

        subtractTime = 2;

        rb = this.gameObject.GetComponent<Rigidbody2D>();
        GameObject randPlanet = GameObject.FindGameObjectWithTag("Planet");
        GameObject sun = GameObject.FindGameObjectWithTag("Sun");

        if (GameObject.FindGameObjectsWithTag("Planet").Length > 0)
            rb.velocity = ((randPlanet.transform.position - this.transform.position) * Random.Range(5f, 10f)) * Time.deltaTime;
        else
            rb.velocity = ((new Vector2(sun.transform.position.x - this.transform.position.x, sun.transform.position.y - this.transform.position.y) * Random.Range(5f, 10f)) * Time.deltaTime);
    }

	void Update () {
        distToSun = Vector3.Distance(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Sun").transform.position);

        if (distToSun < 20 && distToSun > 6) {//Swarm break off when close to sun
            travelTowards = true;
        } else if (distToSun < 6) {
            travelTowards = false;

            subtractTime -= Time.deltaTime;
            if (subtractTime <= 0) //Take life and light
            {
                Stats.stars -= (int)Stats.stars / 40;
                subtractTime = 2;
            }
        } else if (distToSun > 75) {
            Destroy(this.gameObject);
        } else if (this.gameObject.transform.parent == null && distToSun > 6) {
            travelTowards = true;
        }

        if (travelTowards == true) {
            float travel = Random.Range(4f, 10f) * Time.deltaTime;
            this.gameObject.transform.parent = null;
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Sun").transform.position, travel);
        }
	}

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Red Asteroid")) //Make swarm break off when hit by asteroid
            travelTowards = true;
    }
}
