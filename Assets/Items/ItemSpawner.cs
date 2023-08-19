using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] Vector2 spawnBorder;
    [SerializeField] float spawnHeight;
    [SerializeField] float upThrowStrength;
    [SerializeField] float spawnInterval;
    float spawnTimer;

    List<GameObject> currentItemsInScene;
    [SerializeField] int maxItems;
    [SerializeField] int maxItemsPerDepartment;


    [SerializeField] GameObject[] items;

    private AudioSource source;
    private Animator _animator;
    private int throwPackageID;

    [SerializeField] AudioClip[] markusSounds;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        throwPackageID = Animator.StringToHash("ThrowPackage");
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager.Instance.GameState != GameState.Playing) return;

        if (spawnTimer < spawnInterval && ItemDatabase.GetAmountOfItems() < maxItems)
        {
            spawnTimer += Time.deltaTime;
        }
        else if (spawnTimer >= spawnInterval)
        {          
            spawnTimer = 0f;
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        var tmp = Instantiate(Item.GenerateRandomItem(maxItemsPerDepartment), spawnPosition.position, Quaternion.identity, transform);
        tmp.GetComponent<Rigidbody>().AddForce(Vector3.up * upThrowStrength, ForceMode.Impulse);

        // Launch the Falling Countdown.
        StartCoroutine(WaitForFalling(tmp));

        _animator.SetTrigger(throwPackageID);

        // Play sound.
        int r = Random.Range(0, 100);
        if (r < 20)
            source.PlayOneShot(markusSounds[Random.Range(0, markusSounds.Length)]);
        else
            source.Play();
    }

    IEnumerator WaitForFalling(GameObject item)
    {
        yield return new WaitForSeconds(3f);
        
        item.GetComponent<Rigidbody>().velocity = Vector3.zero;
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.transform.position = new Vector3(Random.Range(spawnBorder.x, spawnBorder.y), spawnHeight, 0f);
        item.layer = 7;
        // ToDo: Fix
        item.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1f,1f), 0, Random.Range(-1f, 1f)), ForceMode.Impulse);
    }
}
