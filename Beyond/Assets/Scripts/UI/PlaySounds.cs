using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySounds : MonoBehaviour {

    public AudioSource[] AudSources;

    public AudioClip[] soundClips;
    
    public static bool mineClip, shopClip, purchase, collide, 
    notification, swarmClip, shootClip, bossClip2, bossClip3;

    float waitToPlay;

    void Start() {
        waitToPlay = 1f;
    }
	
	void Update () {

        if (waitToPlay > 0f) {
            mineClip = shopClip = shootClip = purchase = collide = notification = swarmClip = false;
            waitToPlay -= Time.deltaTime;
        }

        play(ref shopClip, 0, 0, false);
        play(ref purchase, 0, 1, false);
        play(ref collide, 1, 2, false);
        play(ref notification, 2, 3, false);
        play(ref shootClip, 3, 5, false);
        
        play(ref swarmClip, 1, 4, true);
        play(ref mineClip, 0, 6, true);

        play(ref bossClip2, 4, 7, false);
        play(ref bossClip3, 4, 8, true);
	}

    public void play(ref bool clipBool, int sourceIndex, int clipindex, bool checkOverlap) {
        if (clipBool) {
            AudSources[sourceIndex].clip = soundClips[clipindex];

            if (checkOverlap) {
                if (!AudSources[sourceIndex].isPlaying)
                    AudSources[sourceIndex].Play();
            } else {
                AudSources[sourceIndex].Play();
            }

            clipBool = false;
        }
    }
}
