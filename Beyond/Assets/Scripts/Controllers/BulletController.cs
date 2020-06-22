using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float deleteTime;

	void Start () {
        deleteTime = 5f;
    }
	
	void Update () {
        
        deleteTime -= Time.deltaTime;
        if (deleteTime <= 0)
            Destroy(this.gameObject);

        if (Vector2.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Sun").transform.position) > 100)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Swarm")) {
            Destroy(other.gameObject);

            if (this.transform.localScale.x < 5)
                Destroy(this.gameObject);
        } else if (other.gameObject.CompareTag("Asteroid")) {
            Destroy(other.gameObject);

            if (this.transform.localScale.x < 5)
                Destroy(this.gameObject);
        } else if (other.gameObject.CompareTag("Sun")) {
            Destroy(this.gameObject);
        } else if (other.gameObject.CompareTag("Planet")) {
            Stats.stars -= (long) (Random.Range(Stats.stars / 15, Stats.stars / 5) * (PrestigeController.prestigeLvl + 1) * (int)this.transform.localScale.x);
            SystemSpawn.hitObj = other.gameObject;

            Destroy(this.gameObject);
        } else if (other.gameObject.CompareTag("Fleet")) {
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("Shield")) {
            Destroy(this.gameObject);
        }
    }
}
