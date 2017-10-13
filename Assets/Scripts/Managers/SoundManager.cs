using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager soundManager;

    public AudioSource sfx;
    public AudioSource music;

    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
            return;
        }
        Destroy(gameObject);
    }
}
