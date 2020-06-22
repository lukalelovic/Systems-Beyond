using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

    Rigidbody2D rb;
    float distToSun;
    float asteroidScale;
    float deleteTime;

    void Start() {
        asteroidScale = Random.Range(1f, 2.5f);
        this.gameObject.transform.localScale = new Vector3(asteroidScale, asteroidScale, 1f);

        rb = this.gameObject.GetComponent<Rigidbody2D>();
        GameObject randPlanet = GameObject.FindGameObjectWithTag("Planet");
        GameObject sun = GameObject.FindGameObjectWithTag("Sun");

        if (GameObject.FindGameObjectsWithTag("Planet").Length > 0)
            rb.velocity = ((randPlanet.transform.position - this.transform.position) * Random.Range(5f, 10f)) * Time.deltaTime;
        else
            rb.velocity = ((new Vector2(sun.transform.position.x - this.transform.position.x, sun.transform.position.y - this.transform.position.y) * Random.Range(5f, 10f)) * Time.deltaTime);

        deleteTime = 300;
    }

	void Update () {

        distToSun = Vector3.Distance(new Vector3(0, 0, 0), this.transform.position);

        if (distToSun > 65 || distToSun < -65)
            Destroy(this.gameObject);
        
        if (deleteTime <= 0)
            Destroy(this.gameObject);
        
	}

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Sun"))
            Destroy(this.gameObject);
        if (other.gameObject.CompareTag("Shield")) {
            Destroy(other.gameObject);

            GlobalFunc.Shake(new Vector3(this.transform.position.x, this.transform.position.y, -10));
        }
    }
}