using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudSystem : MonoBehaviour
{
    private Queue<Cloud> clouds = new Queue<Cloud>();

    [SerializeField] private float randomSpawnDistance;

    [Header("Spawner")] [SerializeField] private float spawnrate;

    private float timer;

    public void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var cloud = transform.GetChild(i).GetComponent<Cloud>();

            if (cloud != null)
            {
                cloud.gameObject.SetActive(false);
                clouds.Enqueue(cloud);
                cloud.CloudSystem = this;
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1 / spawnrate)
        {
            Spawn();
            timer = 0;
        }
    }

    private void Spawn()
    {
        if (clouds.Count == 0)
        {
            return;
        }

        var spawnPosition = transform.position + Random.insideUnitSphere * randomSpawnDistance;
        var cloud = clouds.Dequeue();
        cloud.gameObject.SetActive(true);
        cloud.Spawn(spawnPosition);
    }

    public void DeactivateMe(Cloud cloud)
    {
        cloud.gameObject.SetActive(false);
        clouds.Enqueue(cloud);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, randomSpawnDistance);
    }
}