using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour {

	public string currentType;
	public float localRotate, depleteTime, travelTime;

	public GameObject pirateBullet;
	public Vector3 targetPos;
	
	void Start () {
		if (currentType.Equals("Swarm")) {
			this.transform.localScale = new Vector3(5, 5, this.transform.localScale.z);
			localRotate = 10f;
			depleteTime = 3f;
			travelTime = 0f;
		} else if (currentType.Equals("Pirate")) {
			this.transform.localScale = new Vector3(10, 10, this.transform.localScale.z);
			this.gameObject.AddComponent<PirateController>();
			this.GetComponent<PirateController>().bullet = pirateBullet;
			localRotate = 0f;
			travelTime = 0f;
		} else if (currentType.Equals("Mega")) {
			this.transform.localScale = new Vector3(2, 2, this.transform.localScale.z);
			localRotate = 0f;
			travelTime = 0f;

			this.GetComponent<BoxCollider2D>().size = new Vector2(this.transform.localScale.x, this.transform.localScale.y);
		}
	}

	void Update () {
		this.transform.Rotate(new Vector3(0, 0, localRotate));
		travelTime -= Time.deltaTime;

        if (currentType.Equals("Swarm")) {
            if (travelTime <= 0) {
				GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

				int randNum = Random.Range(0, planets.Length);
				GameObject randPlanet = planets[randNum];

                targetPos = new Vector3(randPlanet.transform.position.x + Random.Range(-10, 10), randPlanet.transform.position.y + Random.Range(-10, 10), randPlanet.transform.position.z);
				travelTime = 1.5f;

				AlienSpawn.swarmTime = 0;
			}

            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, 0.3f);

            depleteTime -= Time.deltaTime;
            if (depleteTime <= 0)
            {
                Stats.stars -= (int)(Stats.stars / 8);
                Stats.life -= (int)(Stats.life / 80);
                depleteTime = 3f;
            }
        } else if (currentType.Equals("Pirate")) {
			if (travelTime <= 0) {
				if (Stats.stars <= 0) 
					Stats.life--;

				this.GetComponent<PirateController>().randPlanet = GlobalFunc.FindClosest(this.transform.position, new string[]{ "Planet" }, new GameObject[]{});	
				travelTime = 2f;
			}
		} else if (currentType.Equals("Mega")) {
			if (Vector3.Distance(this.transform.position, targetPos) <= 1f) {
				GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

				int randNum = Random.Range(0, planets.Length);
				GameObject randPlanet = planets[randNum];

                targetPos = new Vector3(randPlanet.transform.position.x + Random.Range(-25, 25), randPlanet.transform.position.y + Random.Range(-25, 25), randPlanet.transform.position.z);
				PlaySounds.bossClip3 = true;
				
				Stats.stars -= (int)(Stats.stars / 4);

				if (Stats.stars <= 10 && Stats.life <= 110) 
					Stats.life -= 2;
				else
                	Stats.life -= (int)(Stats.life / 55);
			}

			float speed = 5f * Time.deltaTime; //Rotate towards
            Vector2 dir = targetPos - this.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, 0.45f); //Move towards
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (currentType.Equals("Mega") || currentType.Equals("Pirate")) {
			if (other.CompareTag("Asteroid") || other.CompareTag("Red Asteroid")) 
				Destroy(other.gameObject);
		}
	}
}
