using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour {

	public GameObject randPlanet, bullet, target;
	public int randNum;
	public float moveTime, bulletTime;
	public bool isClose;
    public Vector3 thisPos, diff;
    public float dist, currDist;
	void Start () {
		randNum = Random.Range(2, GameObject.FindGameObjectsWithTag("Orbit").Length + 1);

		foreach (GameObject p in GameObject.FindGameObjectsWithTag("Orbit"))
			if (p.name.Contains(randNum.ToString()))
				randPlanet = p.transform.GetChild(0).gameObject;
		
		moveTime = 1f;
		bulletTime = 0f;
		isClose = false;
	}
	
	void Update () {
		if (Vector2.Distance(this.transform.position, randPlanet.transform.position) > 3 && moveTime <= 0)
			this.transform.position = Vector2.MoveTowards(this.transform.position, randPlanet.transform.position, 0.2f);
		else if (Vector2.Distance(this.transform.position, randPlanet.transform.position) <= 3.8f)
			moveTime = 1f;
		else if (Vector2.Distance(this.transform.position, randPlanet.transform.position) <= 15f && isClose == false)
			isClose = true;
		else
			moveTime -= Time.deltaTime;

		ClosestTarget();

		float speed = 30f * Time.deltaTime; //Rotate towards planet
        Vector2 dir = target.transform.position - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed);

		if (isClose == true) { //Fire bullets if close enough to planet
			bulletTime -= Time.deltaTime;
			if (bulletTime <= 0)
			{
				GameObject thisBullet = Instantiate(bullet, this.transform.position, this.transform.rotation);
				thisBullet.GetComponent<Rigidbody2D>().velocity = (target.transform.position - this.transform.position) * 5f;
				PlaySounds.shootClip = true;
				bulletTime = 0.7f;
			}
		}

		if (Vector2.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Sun").transform.position) > 80)
			Destroy(this.gameObject);
	}

	public void ClosestTarget() {
        List<GameObject> targets = new List<GameObject>();
        targets.Add(randPlanet);
        targets.AddRange(GameObject.FindGameObjectsWithTag("Fleet"));

        target = null;
        dist = Mathf.Infinity;
        thisPos = this.transform.position;

        foreach (GameObject t in targets) {
            diff = t.transform.position - thisPos;
            currDist = diff.sqrMagnitude;
            if (currDist < dist) {
                target = t;
                dist = currDist;
            }
        }
    }
}
