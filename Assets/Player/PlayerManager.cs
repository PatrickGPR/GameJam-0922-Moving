using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Unity.Mathematics;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [field: SerializeField] public PlayerInventory Inventory { get; private set; }
    [field: SerializeField] public SideScrollerController Controller { get; private set; }
    [field: SerializeField] public Player Player { get; private set; }

    [SerializeField] private MeshRenderer playerBodyMeshRenderer;
    [SerializeField] private GameObject stunVfx;

    [SerializeField] public bool enableStun = true;
    [SerializeField] private Transform stunParticle;
    [SerializeField] private float stunTime = 2;
    
    public bool isStunned;

    private float timer;

    public Action OnPlayerFinishInitialized;

    private void Start()
    {
        Inventory = GetComponent<PlayerInventory>();
        Controller = GetComponent<SideScrollerController>();
        Player = PlayerManagement.Instance.OnPlayerJoined(gameObject);
        playerBodyMeshRenderer.materials[0].color = Player.Color;
        
        OnPlayerFinishInitialized?.Invoke();
        
    }

    private void Update()
    {
        if (isStunned)
        {
            timer += Time.deltaTime;

            if (timer >= stunTime)
            {
                isStunned = false;
            }
        }
    }

    public void Stun()
    {
        if (!enableStun)
        {
            return;
        }
        
        isStunned = true;
        timer = 0;
        var particle = Instantiate(stunVfx, stunParticle.position, stunParticle.rotation);
        Destroy(particle, 2);
    }

}