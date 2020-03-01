using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

    public Canvas introCanvas;
    public Image introImg;
    public Text quoteTxt;

    public static bool currentIntro;

    private int fadeOut;
    private float fadeTime;

	void Start () {
        fadeOut = 255;
        fadeTime = 1.5f;
        introCanvas.gameObject.SetActive(true);
        currentIntro = true;
	}
	
	void Update () {
		
        if (fadeOut > 2) {
            fadeTime -= Time.deltaTime;
            if (fadeTime <= 0) {
                introImg.color = new Color32(0, 0, 0, (byte)fadeOut);
                quoteTxt.color = new Color32(255, 255, 255, (byte)fadeOut);
                fadeOut -= 1;
            }

            if (fadeOut <= 175)
                currentIntro = false;
        } else {
            introCanvas.gameObject.SetActive(false);
        }
	}
}
