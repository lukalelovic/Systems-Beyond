using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideUI : MonoBehaviour {

    public GameObject UI;
    bool hide;

	void Update () {

        //Press f1 to hide the UI
		if (Input.GetKeyDown(KeyCode.F1)) {
            if (!hide)
                hide = true;
            else
                hide = false;
        }

        //Hide the UI elements
        if (hide) {
            UnityEngine.Cursor.visible = false;
            UI.SetActive(false);
        } else {
            UnityEngine.Cursor.visible = true;
            UI.SetActive(true);
        }
	}
}
