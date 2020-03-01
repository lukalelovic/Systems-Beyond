using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    public Sprite[] backgroundSprites;
    public int randSprite;

	void Start () {
        randSprite = Random.Range(0, backgroundSprites.Length);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = backgroundSprites[randSprite];
	}
}
