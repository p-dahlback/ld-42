using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [System.Serializable]
    public struct SpawnConfig
    {
        public Transform prefab;
        public float likelihood;
    }

    public SpawnConfig[] spawns;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Spawn()
    {
        var random = Random.value;
        var value = 0f;
        foreach (var spawn in spawns)
        {
            value += spawn.likelihood;
            if (random <= value)
            {
                var positionDiff = Random.Range(-1, 1);
                var position = transform.position;
                position.y += positionDiff;
                Instantiate(spawn.prefab, position, Quaternion.identity);
                break;
            }
        }
    }
}
