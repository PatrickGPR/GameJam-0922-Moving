using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Cloud : MonoBehaviour
{
    public CloudSystem CloudSystem;
    
    private Rigidbody rigidbody;

    [SerializeField] private float travelDistance;
    [SerializeField] private Vector3 moveDirection = Vector3.right;
    [SerializeField] private Vector2 movementSpeedRange = new Vector2(5, 15);

    private float movementSpeed;
    private Vector3 spawnPosition;
    
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rigidbody.velocity = moveDirection * movementSpeed * Time.deltaTime;

        if (Vector3.SqrMagnitude(transform.position - spawnPosition) > travelDistance * travelDistance)
        {
            CloudSystem.DeactivateMe(this);
        }
    }

    public void Spawn(Vector3 position)
    {
        transform.position = position;
        spawnPosition = transform.position;
        movementSpeed = Random.Range(movementSpeedRange.x, movementSpeedRange.y);
    }

    
}
