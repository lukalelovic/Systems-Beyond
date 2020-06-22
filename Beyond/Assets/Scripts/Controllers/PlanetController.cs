using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

    float cooldown;
    
    void Start() {
        NewsController.planetSpawned = true;

        cooldown = 0f;
    }

    void Update() {
        if (cooldown > 0f) {
            cooldown -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Red Asteroid")) {
            Stats.stars -= (long) Mathf.RoundToInt(Random.Range(Stats.stars / 4, Stats.stars / 2));

            if (other.gameObject.name.Contains("3") && cooldown <= 0f) {
                Stats.life -= Random.Range(1, Stats.life / 3);
                Stats.mines -= (long) Mathf.RoundToInt(Random.Range(Stats.mines / 40, Stats.mines / 8));
            } else {
                Stats.life -= Random.Range(1, Stats.life / 6);
            }

            NewsController.asteroidHit = true;
            cooldown = 3f;

            GlobalFunc.Shake(new Vector3(this.transform.position.x, this.transform.position.y, -10));
        }
    }
}
