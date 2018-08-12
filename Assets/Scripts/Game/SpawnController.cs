using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    [System.Serializable]
    public struct SpawnConfig
    {
        public Spawner[] spawners;
        public float time;
        public float timeDiff;
    }

    public SpawnConfig[] configs;
    public SpawnConfig[] loopingConfigs;
    public float timeMultiplier = 1f;
    public bool limitIfLowRadius = false;

    private int index = 0;
    private float time = 0.0f;
    private bool looping = false;
    private float currentTimeDiff;

	// Use this for initialization
	void Start () {
        if (index >= configs.Length)
        {
            looping = true;
        }
        UpdateTimeDiff(configs);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        SpawnConfig[] spawns;
        if (looping)
        {
            spawns = loopingConfigs;
        }
        else
        {
            spawns = configs;
        }
        CheckShouldSpawn(spawns);
    }

    private void CheckShouldSpawn(SpawnConfig[] spawns)
    {
        if (index >= spawns.Length)
        {
            return;
        }
        if (time / timeMultiplier < spawns[index].time + currentTimeDiff)
        {
            return;
        }
        time -= (spawns[index].time + currentTimeDiff) * timeMultiplier;
        var spawner = GetSpawner(spawns[index]);
        if (spawner == null)
        {
            return;
        }

        spawner.Spawn();
        index++;

        if (index >= spawns.Length)
        {
            looping = true;
            index = 0;
        }
        var newSpawns = looping ? loopingConfigs : configs;
        UpdateTimeDiff(newSpawns);
        CheckShouldSpawn(newSpawns);
    }

    private Spawner GetSpawner(SpawnConfig config)
    {
        if (limitIfLowRadius && BurnController.GetInstance().burningZone.radius < 10)
        {
            return config.spawners[0];
        }

        var spawnerIndex = Random.Range(0, config.spawners.Length);
        var spawner = config.spawners[spawnerIndex];
        var corner1 = spawner.transform.position + new Vector3(-.5f, .5f, 0);
        var corner2 = spawner.transform.position + new Vector3(.5f, -.5f, 0);
        int layerMask = 1 << (int)Layers.PLAYER;
        var overlapCollider = Physics2D.OverlapArea(corner1, corner2, layerMask);
        if (overlapCollider != null && overlapCollider.GetComponent<Player>() != null)
        {
            if (spawnerIndex > 0)
            {
                return config.spawners[spawnerIndex - 1];
            }
            else if (spawnerIndex < config.spawners.Length)
            {
                return config.spawners[spawnerIndex + 1];
            }
            return null;
        }

        return spawner;
    }

    private void UpdateTimeDiff(SpawnConfig[] spawns)
    {
        if (index >= spawns.Length)
        {
            return;
        }
        currentTimeDiff = Random.Range(0, spawns[index].timeDiff);
    }
}
