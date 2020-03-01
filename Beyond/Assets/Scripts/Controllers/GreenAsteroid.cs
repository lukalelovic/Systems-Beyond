using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAsteroid : MonoBehaviour {

    private Rigidbody2D rb;
    private float distToSun;
    private float asteroidScale;

    void Start () {
        asteroidScale = Random.Range(0.8f, 1.5f);
        this.gameObject.transform.localScale = new Vector3(asteroidScale, asteroidScale, 1f);

        rb = this.gameObject.GetComponent<Rigidbody2D>();

        GameObject randPlanet = GameObject.FindGameObjectWithTag("Planet");
        GameObject sun = GameObject.FindGameObjectWithTag("Sun");

        if (GameObject.FindGameObjectsWithTag("Planet").Length > 0)
            rb.velocity = ((randPlanet.transform.position - this.transform.position) * Random.Range(7f, 14f)) * Time.deltaTime;
        else
            rb.velocity = ((new Vector2(sun.transform.position.x - this.transform.position.x, sun.transform.position.y - this.transform.position.y) * Random.Range(2f, 5f)) * Time.deltaTime);
    }

	void Update () {
        distToSun = Vector3.Distance(new Vector3(0, 0, 0), this.transform.position);

        if (distToSun > 70 || distToSun < -70) {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Sun")) {
            Destroy(this.gameObject);
        }
    }
}
