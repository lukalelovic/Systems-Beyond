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
            if (dist >= 12f) 
                assignColor(0,255,0,255,240,255);
            else if (dist >= 9f) 
                assignColor(0,80,0,255,200,255);
            else if (dist >= 7f) 
                assignColor(0,30,0,60,130,255);
            else if (dist >= 5f) 
                assignColor(0,30,90,130,0,55);
            else if (dist >= 4f) 
                assignColor(160,255,100,170,0,70);
            else 
                assignColor(200,240,0,33,0,33);
        } 
        else if (this.gameObject.tag.Equals("Background")) {
            if (dist >= 12f) 
                assignColor(0,255,0,165,175,255);
            else if (dist >= 9f) 
                assignColor(0,10,0,255,130,255);
            else if (dist >= 7f) 
                assignColor(0,130,120,255,235,255);
            else if (dist >= 5f) 
                assignColor(0,30,130,240,60,86);
            else if (dist >= 4f)
                assignColor(255,255,105,250,0,80);
            else 
                assignColor(80,120,0,0,0,0);
        } 
        else if (this.gameObject.tag.Equals("Planet")) {
            if (dist >= 12f) 
                assignColor(10,100,100,200,130,255);
            else if (dist >= 9f) 
                assignColor(0,10,20,255,130,255);
            else if (dist > 7f) 
                assignColor(0,130,120,255,235,255);
            else if (dist >= 5f) 
                assignColor(0,70,0,200,150,255);
            else if (dist >= 4f) 
                assignColor(90,130,60,100,0,80);
            else 
                assignColor(30,70,0,0,0,0);
        }
    }

    void assignColor(int rMin, int rMax, int gMin, int gMax, int bMin, int bMax) {
        if (gMin == gMax) {
            r = g = b = Random.Range(rMin, rMax);
        } else {
            r = Random.Range(rMin, rMax); g = Random.Range(gMin, gMax); b = Random.Range(bMin, bMax);
        }
    }
}
