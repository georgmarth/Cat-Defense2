using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Wave[] waves;
    public GameObject path;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(spawnWave());
	}

    private IEnumerator spawnWave()
    {
        foreach(Wave wave in waves)
        {
            WaitForSeconds startDelay = new WaitForSeconds(wave.startDelay);
            yield return startDelay;

            WaitForSeconds waitForSpawn = new WaitForSeconds(wave.SpawnRate);

            for (int i = 0; i < wave.waveSize; i++)
            {

                GameObject spawnedEnemy = Instantiate(wave.enemy, transform.position, transform.rotation);
                GameManager.gameManager.enemies.Add(spawnedEnemy);
                spawnedEnemy.GetComponent<EnemyMovement>().path = path;
                yield return waitForSpawn;
            }
        }
        GameManager.gameManager.finalwave = true;
    }
}
