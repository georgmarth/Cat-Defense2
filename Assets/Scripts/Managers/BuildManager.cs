using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    public CatCost cat { get; set;}
    public static BuildManager buildManager;

    private void Awake()
    {
        if (buildManager == null)
        {
            buildManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
