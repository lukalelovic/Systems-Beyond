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
            Destroy(this.gameObject);
        } else if (other.gameObject.CompareTag("Asteroid")) {
            Destroy(this.gameObject);
        } else if (other.gameObject.CompareTag("Sun")) {
            Destroy(this.gameObject);
        } else if (other.gameObject.CompareTag("Planet")) {
            if (Stats.stars > 500)
                Stats.stars -= (long) Random.Range(Stats.stars / 25, Stats.stars / 15);
            else
                Stats.stars -= (long) Random.Range(Stats.stars / 15, Stats.stars / 5);
            
            Destroy(this.gameObject);
        } else if (other.gameObject.CompareTag("Fleet")) {
            Destroy(other.gameObject);
        }
    }
}
