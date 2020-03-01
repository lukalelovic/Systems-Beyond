using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollow : MonoBehaviour {

    public GameObject planetLabel;

    private bool clicked;

    void Start() {
        planetLabel.SetActive(false);
    }

    void Update () {

        Vector2 planetPos = Camera.main.WorldToScreenPoint(this.transform.position);
        planetLabel.transform.position = new Vector2(planetPos.x, planetPos.y + 20);

        RaycastHit2D hit;
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (Input.GetMouseButtonDown(0)) {
            if (hit.collider != null && hit.transform.gameObject.tag == "Planet" && clicked == false) {
                planetLabel.SetActive(true);
                clicked = true;
            }
            else {
                planetLabel.SetActive(false);
                clicked = false;
            }
        }
    }
}
