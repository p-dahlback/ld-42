using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    [System.Serializable]
    public struct Drop
    {
        public Transform prefab;
        public float likelihood;
    }

    public float radiusIncrease = 0.8f;
    public Drop[] drops;

    private void Update()
    {
        if (health <= 0)
        {
            GenerateDrops();
        }
    }

    private void GenerateDrops()
    {
        foreach (var drop in drops)
        {
            var random = Random.value;
            if (random <= drop.likelihood)
            {
                Instantiate(drop.prefab, transform.position, Quaternion.identity);
                break;
            }
        }
    }

}
