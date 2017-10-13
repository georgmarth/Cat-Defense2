using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatPlacement : MonoBehaviour {

    public Transform spawnPoint;

    private GameObject cat;
    public float clickTime = 0.2f;
    public AudioClip defaultSound;

    private float startHoldTime;

    public void onButtonDown(BaseEventData e)
    {
        if (e.GetType() != typeof(PointerEventData))
            return;

        startHoldTime = Time.time;
    }

    public void onButtonUp(BaseEventData e)
    {
        if (e.GetType() != typeof(PointerEventData))
            return;

        if (Time.time - startHoldTime < clickTime)
        {
            SpawnCat();
        }
    }

    private void SpawnCat()
    {
        
        CatCost spawnCat = BuildManager.buildManager.cat;

        if (spawnCat == null)
        {
            Debug.Log("No Cat selected.");
            return;
        }
        if (cat != null)
        {
            Debug.Log("Cat already placed");
            return;
        }

        MoneyManager moneyManager = MoneyManager.moneyManager;

        if (moneyManager.money >= spawnCat.cost)
        {
            cat = Instantiate(spawnCat.gameObject, spawnPoint.position, spawnPoint.rotation);
            moneyManager.SpendMoney(spawnCat.cost);

            // play audio
            AudioSource src = SoundManager.soundManager.sfx;

            AudioClip clip = cat.GetComponent<AudioClip>();
            if (clip != null)
                src.clip = clip;
            else
                src.clip = defaultSound;

            src.Play();
        }
           
    }

}