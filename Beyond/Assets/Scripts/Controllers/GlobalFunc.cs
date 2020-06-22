using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalFunc : MonoBehaviour {

    public static Camera cam;
    public static AnimationController shake;

    void Start() {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        shake = GameObject.FindGameObjectWithTag("MainCamera").transform.parent.GetComponent<AnimationController>();
    }

	public static void setAbbreviation(int a, Text t) {
        if (a >= 1000 && a <= 999999) //Add mine abbreviations
            t.text = (a / 1000f).ToString("#.00") + "k";
        else if (a > 999999 && a <= 999999999)
            t.text = (a / 1000000f).ToString("#.00") + "M";
        else if (a > 999999999 && System.Convert.ToInt64(a) <= 999999999999)
            t.text = (a / 1000000000f).ToString("#.00") + "B";
        else if (System.Convert.ToInt64(a) > 999999999999)
            t.text = (System.Convert.ToInt64(a) / 1000000000f).ToString("#.00") + "Q";
        else 
            t.text = a.ToString();
    }

	public static void setLongAbbreviation(long a, Text t) {
		if (a >= 1000 && a <= 999999) //Add mine abbreviations
            t.text = (a / 1000f).ToString("#.00") + "k";
        else if (a > 999999 && a <= 999999999)
            t.text = (a / 1000000f).ToString("#.00") + "M";
        else if (a > 999999999 && System.Convert.ToInt64(a) <= 999999999999)
            t.text = (a / 1000000000f).ToString("#.00") + "B";
        else if (System.Convert.ToInt64(a) > 999999999999)
            t.text = (System.Convert.ToInt64(a) / 1000000000f).ToString("#.00") + "Q";
        else 
            t.text = Mathf.RoundToInt(a).ToString();
	}

    public static GameObject FindClosest(Vector3 currentPos, string[] tags, GameObject[] objects) {
        List<GameObject> targets = new List<GameObject>();

        for (int i = 0; i < objects.Length; i++)
            targets.Add(objects[i]);

		for (int i = 0; i < tags.Length; i++) //Search for closest objects from multiple tags
        	targets.AddRange(GameObject.FindGameObjectsWithTag(tags[i]));

        GameObject closest = null;
        float dist = Mathf.Infinity;
        foreach (GameObject t in targets) {
            Vector3 diff = t.transform.position - currentPos;
            float currDist = diff.sqrMagnitude;
            if (currDist < dist) {
                closest = t;
                dist = currDist;
            }
        }

		return closest;
    }

    public static void Shake(Vector3 pos) {
        shake.CamShake();
        cam.transform.parent.transform.position = pos;
        PlaySounds.collide = true;
    }
}