using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    public static long stars, mines;
    public Text starTxt, lifeTxt;
    public static int life, lifeLevel, lifeMin, lifeStop;
    float countLife, mineTime;

    void Start() {
        countLife = 0;
        mineTime = 1;

        lifeTxt.text = life.ToString() + "%";
    }

    void Update () {

        if (stars < 0)
            stars = 0;

        if (life < lifeMin)
            life = lifeMin;
        
        if (life < lifeStop && lifeLevel > 0) //Add life over time
            addLife();
        else if (lifeLevel > 0)
            lifeTxt.text = life.ToString() + "% *MAX*";

        mineTime -= Time.deltaTime; //Player Mines
        if (mineTime <= 0) {
            stars += mines;
            mineTime = 1;
        }

        Abbreviation.setLongAbbreviation(stars, starTxt);
    }

    public void addLife() {
        
        countLife -= Time.deltaTime;
        if (countLife <= 0) {
            life += 1;

            if (lifeLevel == 1)
                countLife = Random.Range(1f, 4f);
            else if (lifeLevel == 2)
                countLife = Random.Range(4f, 6f);
            else
                countLife = Random.Range(6f, 8f);
        }
        lifeTxt.text = life.ToString() + "%";
    }
}
