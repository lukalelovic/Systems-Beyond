using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour {

    public Texture2D defaultTxt, selectTxt;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    bool showHover;

    void Start() {
        UnityEngine.Cursor.SetCursor(defaultTxt, hotSpot, cursorMode);
    }

    void Update() {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (checkIntro()) 
            showHover = false;
        else if (hit.collider != null && hit.transform.parent != null && hit.transform.parent.gameObject.transform.tag == "Orbit")
            showHover = true;
        else if (hit.collider != null && hit.transform.gameObject.tag == "Sun")
            showHover = true;
        else if (EventSystem.current.IsPointerOverGameObject() && !EventSystem.current.tag.Equals("Resource"))
            showHover = true;
        else
            showHover = false;
        

        if (showHover)
            UnityEngine.Cursor.SetCursor(selectTxt, hotSpot, cursorMode);
        else
            UnityEngine.Cursor.SetCursor(defaultTxt, hotSpot, cursorMode);
    }
    
    public bool checkIntro() {
        if (Intro.fadeOut > 2)
            return true;
        return false;
    }
}
