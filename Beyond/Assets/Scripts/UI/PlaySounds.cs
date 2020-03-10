using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour {

    public AudioSource[] AudSources;

    public AudioClip[] mineAsteroids, soundClips;

    public static bool mineClip, shopClip, purchase, collide, notification, swarmClip, shootClip;

    float waitToPlay;

    void Start() {
        waitToPlay = 1f;
    }
	
	void Update () {

        if (waitToPlay > 0f) {
            mineClip = shopClip = shootClip = purchase = collide = notification = swarmClip = false;
            waitToPlay -= Time.deltaTime;
        }

		if (mineClip == true) {
            randClip();
            mineClip = false;
        }
        if (shopClip == true) {
            playShop();
            shopClip = false;
        }
        if (purchase == true) {
            playBuy();
            purchase = false;
        }
        if (collide == true) {
            playCollide();
            collide = false;
        }
        if (notification == true) {
            playNotif();
            notification = false;
        }
        if (swarmClip == true) {
            playSwarm();
            swarmClip = false;
        }
        if (shootClip == true) {
            playShoot();
            shootClip = false;
        }
	}


    public void randClip() {
        int num = Random.Range(0, mineAsteroids.Length);
        AudSources[0].clip = mineAsteroids[num];
        if (!AudSources[0].isPlaying)
            AudSources[0].Play();
    }
    public void playSwarm() {
        AudSources[1].clip = soundClips[4];
        if (!AudSources[1].isPlaying)
            AudSources[1].Play();
    }
    public void playShop() {
        AudSources[0].clip = soundClips[0];
        AudSources[0].Play();
    }
    public void playBuy() {
        AudSources[0].clip = soundClips[1];
        AudSources[0].Play();
    }
    public void playCollide() {
        AudSources[1].clip = soundClips[2];
        AudSources[1].Play();
    }
    public void playNotif() {
        AudSources[2].clip = soundClips[3];
        AudSources[2].Play();
    }
    public void playShoot() {
        AudSources[3].clip = soundClips[5];
        AudSources[3].Play();
    }
}
