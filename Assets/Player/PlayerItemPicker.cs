using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerItemPicker : MonoBehaviour
{
    [SerializeField] InputActionAsset playerControls;
    InputAction pickUp;

    PlayerInventory playerInventory;

    bool tryToPickItemUp;
    bool itemInRange;

    GameObject currentItem;
    [SerializeField] Transform itemHolderPosition;
    [SerializeField] StarterAssetsInputs inputs;

    public GameObject CurrentItem { get => currentItem; set => currentItem = value; }

    Collider other;
    [SerializeField] ThrowingSystem throwingSystem;

    public Action OnItemPickedUp;

    private void Awake()
    {
        playerInventory = GetComponentInParent<PlayerInventory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the input action assets.
        // var actionmap = playerControls.FindActionMap("Player");
        // pickUp = actionmap.FindAction(("Throw"));
        // pickUp.performed += InteractionStarted;

        //spickUp.Enable();
    }

    private void Update()
    {
        if (inputs.onThrow)
        {
            if (playerInventory.HasItem)
            {
                return;
            }

            if (other == null)
            {
                itemInRange = false;
                return;
            }
        
            if (itemInRange && !playerInventory.HasItem)
            {
                StartCoroutine(StartPickUpDelay());
                playerInventory.PickupItem(other.GetComponent<Item>());

                // Set the new parent and itemposition.
                playerInventory.CurrentItem.Owner = GetComponentInParent<PlayerManager>().Player;

                var o = playerInventory.CurrentItem.gameObject;
                o.transform.parent = itemHolderPosition;
                o.transform.position = itemHolderPosition.position;
                o.transform.localRotation = Quaternion.identity;
                playerInventory.CurrentItem.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                OnItemPickedUp?.Invoke();
            }
        }
    }

    private void InteractionStarted(InputAction.CallbackContext ctx)
    {
        if (playerInventory.HasItem || GameManager.Instance.GameState == GameState.TimeIsUp)
        {
            return;
        }

        if (other == null)
        {
            itemInRange = false;
            return;
        }
        
        if (itemInRange && !playerInventory.HasItem)
        {
            StartCoroutine(StartPickUpDelay());
            playerInventory.PickupItem(other.GetComponent<Item>());

            // Set the new parent and itemposition.
            playerInventory.CurrentItem.Owner = GetComponentInParent<PlayerManager>().Player;

            var o = playerInventory.CurrentItem.gameObject;
            o.transform.parent = itemHolderPosition;
            o.transform.position = itemHolderPosition.position;
            o.transform.localRotation = Quaternion.identity;
            playerInventory.CurrentItem.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Item"))
        {
            itemInRange = true;
            this.other = other;
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            itemInRange = false;
        }
    }

    IEnumerator StartPickUpDelay()
    {
        yield return new WaitForSeconds(0.1f);
        throwingSystem.justPickedUp = true;
    }
}
