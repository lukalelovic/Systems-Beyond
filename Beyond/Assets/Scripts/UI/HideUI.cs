using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideUI : MonoBehaviour {

    public Canvas[] canvasUI;
    private bool hide;

	void Update () {

        //Press f1 to hide the UI
		if (Input.GetKeyDown(KeyCode.F1)) {
            if (hide == false)
                hide = true;
            else
                hide = false;
        }

        //Hide the UI elements
        if (hide == true) {
            UnityEngine.Cursor.visible = false;
            for (int i = 0; i < canvasUI.Length; i++)
                canvasUI[i].gameObject.SetActive(false);
        } else {
            UnityEngine.Cursor.visible = true;
            for (int i = 0; i < canvasUI.Length; i++)
                canvasUI[i].gameObject.SetActive(true);
        }
	}
}
