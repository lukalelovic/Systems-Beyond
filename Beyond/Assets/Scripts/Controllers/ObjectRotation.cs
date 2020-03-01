using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour {

    public float rotateSpeed;

    void Start() {
        if (this.transform.parent != null && this.transform.parent.tag == "Orbit")
            rotateSpeed = Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Sun").transform.position) * 10;

    }

    void Update () {
        this.gameObject.transform.Rotate((Vector3.forward * rotateSpeed) * Time.deltaTime);
    }
}
