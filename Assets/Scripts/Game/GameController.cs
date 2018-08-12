using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private static GameController sInstance;

    public static GameController GetInstance()
    {
        return sInstance;
    }

    public float levelWidth = 27;
    public float levelHeight = 20;


    private void Awake()
    {
        if (sInstance != null && sInstance != this)
        {
            Destroy(sInstance.gameObject);
        }
        sInstance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
