using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private static GameController sInstance;

    public enum State
    {
        PLAYING,
        GAME_OVER,
        WON
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
    public Transform jetpackPickupPrefab;
    public Weapon currentWeapon;
    public PlayerController player;
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
        if (state == State.WON)
        {
            return;
        }

        lives--;
        if (lives <= 0)
        {
            lives = 0;
            state = State.GAME_OVER;
            StartCoroutine("GameOverAfterDelay");
        } else
        {
            StartCoroutine("SpawnPlayerAfterDelay");
        }
    }

    public void OnWeaponLost()
    {
        currentWeapon = null;
    }

    public void OnExtraLife(int lives)
    {
        this.lives += lives;
        if (this.lives > 9)
        {
            this.lives = 9;
        }
    }

    public void OnBossSpawned()
    {
        GameCanvasController.GetInstance().OnBossAppeared();
    }

    public void OnBossKilled()
    {
        state = State.WON;
        StartCoroutine("VictoryAfterDelay");
    }

    private IEnumerator SpawnPlayerAfterDelay()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnPlayer();
        Instantiate(defaultWeaponPickupPrefab, playerSpawn);

        if (BurnController.GetInstance().burningZone.radius < 9)
        {
            Instantiate(jetpackPickupPrefab, playerSpawn);
        }
    }
    
    private void SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, playerSpawn.transform.position, Quaternion.identity);
        this.player = player.GetComponent<PlayerController>();
    }

    private IEnumerator GameOverAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        GameCanvasController.GetInstance().OnGameOver();
    }

    private IEnumerator VictoryAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        GameCanvasController.GetInstance().OnVictory();
    }
}
