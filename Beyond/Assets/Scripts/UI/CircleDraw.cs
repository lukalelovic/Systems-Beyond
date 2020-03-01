using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CircleDraw : MonoBehaviour
{
    public float theta_scale = 0.01f, x, y;  //Set lower to add more points
    int size; //Total number of points in circle
    public float radius;
    LineRenderer lineRenderer;

    void Awake() {
        float sizeValue = (2.0f * Mathf.PI) / theta_scale;
        size = (int)sizeValue;
        size++;
        lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        lineRenderer.startColor = new Color32(25, 25, 25, 55);
        lineRenderer.endColor = new Color32(14, 14, 14, 55);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = size;
    }

    void Update() {
        radius = Vector3.Distance(this.gameObject.transform.GetChild(0).transform.position, GameObject.FindGameObjectWithTag("Sun").transform.position);

        Vector3 pos;
        float theta = 0f;
        for (int i = 0; i < size; i++) {
            theta += (2.0f * Mathf.PI * theta_scale);
            x = radius * Mathf.Cos(theta);
            y = radius * Mathf.Sin(theta);
            x += gameObject.transform.position.x;
            y += gameObject.transform.position.y;
            pos = new Vector3(x, y, 0);
            lineRenderer.SetPosition(i, pos);
        }
    }
}
