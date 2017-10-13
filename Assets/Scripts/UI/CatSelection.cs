using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSelection : MonoBehaviour {

    public CatCost cat;
    public AudioClip selectClip;

    public void OnSelect()
    {
        BuildManager.buildManager.cat = cat;
        SoundManager.soundManager.sfx.clip = selectClip;
        SoundManager.soundManager.sfx.Play();
    }

}
