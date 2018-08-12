using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private static GameController sInstance;

    public enum State
    {
        PLAYING,
        GAME_OVER
    }

    public static GameController GetInstance()
    {
        return sInstance;
    }

    public float levelWidth = 27;
    public float levelHeight = 20;

    public Transform playerSpawn;
    public Transform playerPrefab;
    public Transform defaultWeaponPickupPrefab;
    public Weapon currentWeapon;
    public float spawnTime = 1f;

    public int lives = 3;
    public State state = State.PLAYING;

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
        SpawnPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPlayerDeath()
    {
        lives--;
        if (lives <= 0)
        {
            lives = 0;
            state = State.GAME_OVER;
        } else
        {
            StartCoroutine("SpawnPlayerAfterDelay");
        }
    }

    public void OnWeaponLost()
    {
        currentWeapon = null;
    }

    private IEnumerator SpawnPlayerAfterDelay()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnPlayer();
        Instantiate(defaultWeaponPickupPrefab, playerSpawn);
    }
    
    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawn);
    }
}
