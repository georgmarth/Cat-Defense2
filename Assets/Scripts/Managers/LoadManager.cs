using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {

    public string loadingLevel = "Testing Scene";

    public static LoadManager loadManager;
    public GameObject[] persistentObjects;

    public void Awake()
    {
        if (loadManager == null)
        {
            loadManager = this;

            return;
        }
        Destroy(this);
    }

    private void Start()
    {
        foreach (GameObject gameObject in persistentObjects)
        {
            DontDestroyOnLoad(gameObject);
        }
        StartCoroutine("LoadLevel", loadingLevel);
    }


    public IEnumerator LoadLevel(string levelName)
    {
        yield return SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        Scene newScene = SceneManager.GetSceneByName(levelName);
        SceneManager.SetActiveScene(newScene);
        yield return null;
    }

    public IEnumerator unloadLevel(string levelName)
    {
        yield return SceneManager.UnloadSceneAsync(levelName);
        yield return null;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
