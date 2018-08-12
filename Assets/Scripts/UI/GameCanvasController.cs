using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasController : MonoBehaviour {

    private static GameCanvasController sInstance;

    public static GameCanvasController GetInstance()
    {
        return sInstance;
    }

    public Transform warningPanel;
    public Transform gameOverPanel;
    public Transform victoryPanel;

    private void Awake()
    {
        if (sInstance != null && sInstance != this)
        {
            Destroy(sInstance);
        }
        sInstance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBossAppeared()
    {
        warningPanel.gameObject.SetActive(true);
    }

    public void OnVictory()
    {
        victoryPanel.gameObject.SetActive(true);
    }

    public void OnGameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
    }
}
