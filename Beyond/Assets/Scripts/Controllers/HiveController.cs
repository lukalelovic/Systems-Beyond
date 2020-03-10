using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveController : MonoBehaviour {

    float distToSun;
    float hiveScale;
    public float deleteTime;

    void Start() {
        hiveScale = Random.Range(1f, 2.5f);
        this.gameObject.transform.localScale = new Vector3(hiveScale, hiveScale, 1f);

        deleteTime = 250;
    }

    void Update() {
        distToSun = Vector3.Distance(new Vector3(0, 0, 0), this.transform.position);

        if (distToSun > 65 || distToSun < -65)
            Destroy(this.gameObject);
            
        if (deleteTime <= 0)
            Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Sun"))
            Destroy(this.gameObject);
    }
}
