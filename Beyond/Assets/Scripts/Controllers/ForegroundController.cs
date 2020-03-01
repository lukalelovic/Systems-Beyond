using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundController : MonoBehaviour {

    public Sprite[] foregroundSprites;
    public int randSprite;
	
	void Start () {
        randSprite = Random.Range(0, foregroundSprites.Length);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = foregroundSprites[randSprite];
    }
}
