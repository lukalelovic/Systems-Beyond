using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour {

    public float speed;

    void Start() {
        if (!this.gameObject.tag.Equals("Gold"))
            speed = (1 / (Vector3.Distance(this.transform.GetChild(0).transform.position, GameObject.FindGameObjectWithTag("Sun").transform.position))) * 100;
        else
            speed = ((1 / (Vector3.Distance(GameObject.Find("Planet 3").transform.GetChild(0).transform.position, GameObject.FindGameObjectWithTag("Sun").transform.position))) * 100) + Random.Range(-1, 4);
    }

    void Update () {
        this.gameObject.transform.Rotate((Vector3.forward * speed) * Time.deltaTime);
    }
}
