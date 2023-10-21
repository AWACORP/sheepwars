using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SlimeGenerator : NetworkBehaviour
{
    [Header("Ball Settings")]
    public GameObject slime;
    public float spawnRadius = 0.2f;

    private void Start()
    {
        InvokeRepeating("SpawnRandomBall", 6.0f, 12.0f); // 1 seconds after script start, spawn each seconds
    }

    // Method to spawn a ball at a random position around the generator
    public void SpawnRandomBall()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        Vector3 spawnPosition = transform.position;

        Runner.Spawn(slime,
            spawnPosition,
            Quaternion.identity,
            Object.InputAuthority,
            (runner, o) =>
            {
                o.GetComponent<FlamingSlime>().Init(randomDirection); // You might want to adjust this line based on your needs
            });
    }
}
