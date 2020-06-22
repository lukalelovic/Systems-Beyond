using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenAsteroid : MonoBehaviour {

    GameObject closest;
    float frozenScale;
    float distToSun;
    public float deleteTime;

    void Start() {
        closest = GlobalFunc.FindClosest(this.transform.position, new string[] { "Planet" }, new GameObject[] {});
        
        if (GameObject.FindGameObjectsWithTag("Planet").Length > 0)
            if (Vector3.Distance(this.transform.position, closest.transform.position) < 6)
                Destroy(this.gameObject);

        frozenScale = Random.Range(1f, 2.7f);
        this.transform.localScale = new Vector3(frozenScale, frozenScale, 1f);

        deleteTime = 250;
    }

    void Update() {
        deleteTime -= Time.deltaTime;
        distToSun = Vector3.Distance(new Vector3(0, 0, 0), this.transform.position);

        if (distToSun > 70 || distToSun < -70)
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