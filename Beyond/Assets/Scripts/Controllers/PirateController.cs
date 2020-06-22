using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour {

	public GameObject randPlanet, bullet, target;

	public bool isClose;
    public float dist, moveTime, bulletTime;
	public int randNum;
	

	void Start () {
		randNum = Random.Range(3, GameObject.FindGameObjectsWithTag("Orbit").Length + 1);

		newPlanet();
		
		moveTime = 1f;
		bulletTime = 0f;
		isClose = false;
	}
	
	void Update () {
		if (randPlanet != null) {
			dist = Vector2.Distance(this.transform.position, randPlanet.transform.position);
			if (this.tag.Equals("Boss")) {
				if (dist > 10f && moveTime <= 0)
					this.transform.position = Vector2.MoveTowards(this.transform.position, randPlanet.transform.position, 0.3f);
				else if (dist <= 12f) 
					moveTime = 4f;
				else if (dist <= 15f)
					isClose = true;
				else 
					moveTime -= Time.deltaTime;
			} else {
				if (dist > 3 && moveTime <= 0) //Travel towards planet if distance hits limit
					this.transform.position = Vector2.MoveTowards(this.transform.position, randPlanet.transform.position, 0.3f);
				else if (dist <= 3.8f)
					moveTime = 1f;
				else if (dist <= 15f && !isClose)
					isClose = true;
				else
					moveTime -= Time.deltaTime;
			}
		} else {
			newPlanet();
		}

		target = GlobalFunc.FindClosest(this.transform.position, new string[]{ "Fleet" }, new GameObject[]{ randPlanet });

		float speed = 30f * Time.deltaTime; //Rotate towards planet
        Vector2 dir = target.transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed);

		if (isClose) { //Fire bullets if close enough to planet
			bulletTime -= Time.deltaTime;
			if (bulletTime <= 0)
			{
				GameObject thisBullet = Instantiate(bullet, this.transform.position, this.transform.rotation);
				thisBullet.GetComponent<Rigidbody2D>().velocity = (target.transform.position - this.transform.position) * 5f;
				bulletTime = 0.7f;
				
				if (this.tag.Equals("Boss")) {
					thisBullet.transform.localScale = new Vector3(10f, 10f, this.transform.localScale.z);
					PlaySounds.bossClip2 = true;
				} else {
					PlaySounds.shootClip = true;
				}
			}
		}

		if (Vector2.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Sun").transform.position) > 80)
			Destroy(this.gameObject);
	}

	public void newPlanet() {
		foreach (GameObject p in GameObject.FindGameObjectsWithTag("Orbit"))
			if (p.name.Contains(randNum.ToString()))
				randPlanet = p.transform.GetChild(0).gameObject;
	}
}
