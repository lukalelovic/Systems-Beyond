using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour {

    private Camera cam;
    private AnimationController shake;
    private float starsAsteroids;
    void Start() {
        NewsController.planetSpawned = true;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        shake = GameObject.FindGameObjectWithTag("MainCamera").transform.parent.GetComponent<AnimationController>();

        starsAsteroids = 4;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Asteroid") || other.gameObject.CompareTag("Red Asteroid"))
        {
            Stats.stars -= (long) Mathf.RoundToInt(Random.Range(Stats.stars / starsAsteroids, Stats.stars / (starsAsteroids / 2)));

            if (other.gameObject.name.Contains("3"))
            {
                Stats.life -= Random.Range(1, Stats.life / 2);
                Stats.mines -= (long) Mathf.RoundToInt(Random.Range(Stats.mines / 40, Stats.mines / 8));
            }
            else
                Stats.life -= Random.Range(1, Stats.life / 6);

            NewsController.asteroidHit = true;

            shake.CamShake();
            cam.transform.parent.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);

            PlaySounds.collide = true;
        }
    }
}
