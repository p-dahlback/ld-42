using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour {

    public Text[] textFields;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        int lives = GameController.GetInstance().lives;
        SetLives(lives);
	}

    private void SetLives(int lives)
    {
        var livesText = lives.ToString();
        foreach (var text in textFields)
        {
            text.text = livesText;
        }
    }
}
