using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    public static GameManager gameManager;

    public GameObject gameOverPanel;
    public GameObject gameWinPanel;

    [HideInInspector]
    public bool finalwave = false;
    public List<GameObject> enemies;

    void Start () {
        enemies = new List<GameObject>();
    }

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            return;
        }
        Destroy(gameObject);
    }

    void Update () {
        if (finalwave)
        {
            if (enemies.Count == 0)
            {
                Win();
            }
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        HealthManager.healthManager.Reset();
        MoneyManager.moneyManager.Reset();
        enemies = new List<GameObject>();

        StartCoroutine("ReloadLevel");
    }

    public void Die()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
    }

    public void Win()
    {
        finalwave = false;
        Time.timeScale = 0f;
        gameWinPanel.SetActive(true);
    }

    private IEnumerator ReloadLevel()
    {
        yield return LoadManager.loadManager.unloadLevel(LoadManager.loadManager.loadingLevel);
        yield return LoadManager.loadManager.LoadLevel(LoadManager.loadManager.loadingLevel);
    }
}
