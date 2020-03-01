using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetColor : MonoBehaviour {

    public int r, g, b;
    public float dist;

	void Start () {
        setColor();
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color32((byte)r, (byte)g, (byte)b, 255);
	}

    //Assign color values
    public void setColor() {
        dist = Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Sun").transform.position);
        if (this.gameObject.tag.Equals("Foreground")) {
            if (dist >= 12f) {
                r = Random.Range(10, 255); g = Random.Range(0, 255); b = Random.Range(165, 200);
            } else if (dist >= 9f) {
                r = Random.Range(0, 80); g = Random.Range(0, 255); b = Random.Range(200, 255);
            } else if (dist >= 7f) {
                r = Random.Range(0, 30); g = Random.Range(0, 60); b = Random.Range(130, 255);
            } else if (dist >= 5f) {
                r = Random.Range(0, 30); g = Random.Range(90, 130); b = Random.Range(0, 55);
            } else if (dist >= 4f) {
                r = g = b = Random.Range(100, 170);
            } else {
                r = Random.Range(200, 240); g = Random.Range(0, 33); b = Random.Range(0, 33);
            }
        } 
        else if (this.gameObject.tag.Equals("Background")) {
            if (dist >= 12f) {
                r = Random.Range(0, 255); g = Random.Range(0, 255); b = Random.Range(130, 255);
            } else if (dist >= 9f) {
                r = Random.Range(0, 10); g = Random.Range(0, 255); b = Random.Range(130, 255);
            } else if (dist >= 7f) {
                r = Random.Range(0, 130); g = Random.Range(120, 255); b = Random.Range(235, 255);
            } else if (dist >= 5f) {
                r = Random.Range(0, 30); g = Random.Range(130, 240); b = Random.Range(60, 86);
            } else if (dist >= 4f) {
                r = Random.Range(225, 255); g = Random.Range(105, 250); b = Random.Range(0, 80);
            } else {
                r = g = b = Random.Range(80, 120);
            }
        } 
        else if (this.gameObject.tag.Equals("Planet")) {
            if (dist >= 12f) {
                r = Random.Range(10, 100); g = Random.Range(100, 200); b = Random.Range(0, 255);
            } else if (dist >= 9f) {
                r = Random.Range(0, 10); g = Random.Range(20, 255); b = Random.Range(130, 255);
            } else if (dist > 7f) {
                r = Random.Range(0, 130); g = Random.Range(120, 255); b = Random.Range(235, 255);
            } else if (dist >= 5f) {
                r = Random.Range(0, 70); g = Random.Range(0, 200); b = Random.Range(150, 255);
            } else if (dist >= 4f) {
                r = Random.Range(90, 255); g = Random.Range(0, 150); b = Random.Range(0, 80);
            } else {
                r = g = b = Random.Range(30, 70);
            }
        }
    }
}
