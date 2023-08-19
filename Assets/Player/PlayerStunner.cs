using System;
using UnityEngine;

namespace StarterAssets
{
    public class PlayerStunner : MonoBehaviour
    {
        private PlayerManager playerManager;

        private void Awake()
        {
            playerManager = GetComponentInParent<PlayerManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (playerManager.enableStun)
            {
                return;
            }
            
            var item = other.GetComponent<Item>();

            if (item != null && item.canStun && item.LastOwner != playerManager.Player)
            {
                if (playerManager.Inventory.HasItem)
                {
                    Destroy(playerManager.Inventory.CurrentItem.gameObject);
                }
                
                item.Drop();
                playerManager.Inventory.DropItem();
                playerManager.Stun();
            }
        }
    }
}