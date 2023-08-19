using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class ThrowingSystem : MonoBehaviour
{
    [SerializeField] InputActionAsset playerControls;
    InputAction throwChargeAction;

    [Header("Throwing related:")]
    [SerializeField] AnimationCurve throwStrengthAnimationCurve;
    [SerializeField] float maxThrowStrength;
    [SerializeField] float maxChargeTime;
    [SerializeField] Transform throwAngle;
    [SerializeField] AudioSource source;

    [SerializeField] PlayerItemPicker itemPicker;

    [SerializeField] private StarterAssetsInputs inputs;
    
    float chargeTimer;
    float chargePercentage;
    bool chargeEnabled;

    Rigidbody currentItemRB;
    Item currentItem;

    PlayerInventory playerInventory;

    VisualEffect chargeEffect;
    VisualEffect chargeDone;
    float chargeSpeed;
    float chargeRadius;

    public bool justPickedUp;
    private bool oldValue;

    private void Awake()
    {
        playerInventory = GetComponentInParent<PlayerInventory>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the input action assets.
        // var actionmap = playerControls.FindActionMap("Player");
        // throwChargeAction = actionmap.FindAction(("Throw"));
        // throwChargeAction.performed += OnThrowChargeStarted;
        // throwChargeAction.canceled += OnThrowChargeEnded;
        // throwChargeAction.Enable();

        
    }

    

    private void OnThrowChargeEnded(InputAction.CallbackContext ctx)
    {
        if (chargeEnabled)
        {
            EvaluateChargePercentage();

            ThrowItem();
        }
        
    }

    private void OnThrowChargeStarted(InputAction.CallbackContext ctx)
    {

        if (playerInventory.HasItem && justPickedUp)
        {
            justPickedUp = false;
            chargeEnabled = true;
            currentItem = playerInventory.CurrentItem;
            currentItemRB = playerInventory.CurrentItem.GetComponent<Rigidbody>();
            // Vfx.
            chargeEffect = currentItem.ChargeEffect;
            chargeDone = currentItem.ChargeDone;
            chargeSpeed = chargeEffect.GetFloat("ChargeSpeed");
            chargeRadius = chargeDone.GetFloat("MaxRadius");
        }

        // ToDO: Get the actual item.

        //currentItem.InitDestruction();
    }


    // Update is called once per frame
    void Update()
    {
        if (inputs.onThrow)
        {
            if (playerInventory.HasItem && justPickedUp)
            {
                oldValue = true;
                justPickedUp = false;
                chargeEnabled = true;
                currentItem = playerInventory.CurrentItem;
                currentItemRB = playerInventory.CurrentItem.GetComponent<Rigidbody>();
                // Vfx.
                chargeEffect = currentItem.ChargeEffect;
                chargeDone = currentItem.ChargeDone;
                chargeSpeed = chargeEffect.GetFloat("ChargeSpeed");
                chargeRadius = chargeDone.GetFloat("MaxRadius");
            }
        }
        
        if (oldValue && oldValue != inputs.onThrow)
        {
            if (chargeEnabled)
            {
                EvaluateChargePercentage();

                ThrowItem();
            }

            oldValue = false;
        }
        
        if (chargeEnabled)
        {
            if (chargeEffect == null)
            {
                chargeEnabled = false;
                return;
            }
            
            chargeEffect.enabled = true;
            chargeEffect.SetFloat("ChargeSpeed", chargeSpeed * (chargeTimer / maxChargeTime));
            
            if (chargeTimer < maxChargeTime)
                chargeTimer += Time.deltaTime;
            //else
            //{
            //    // Calculate throw strength.
            //    EvaluateChargePercentage();

            //    ThrowItem();

            //    // ToDO: make charge appear.
            //}
            
        }
    }

    /// <summary>
    /// Calculates the charge percentage. Resets the charge timer.
    /// </summary>
    private void EvaluateChargePercentage() 
    {
        chargePercentage = chargeTimer / maxChargeTime;
        if(chargeDone) chargeDone.SetFloat("MaxRadius", chargeRadius * chargePercentage);
        chargePercentage = throwStrengthAnimationCurve.Evaluate(chargePercentage);
        chargeTimer = 0f;
        chargeEnabled = false;
        chargeEffect.enabled = false;
        if(chargeDone) chargeDone?.Play();
    }

       
    private void ThrowItem()
    {
        // Throw the actual item.
        currentItemRB.isKinematic = false;
        currentItemRB.AddForce(throwAngle.forward * (maxThrowStrength * chargePercentage), ForceMode.Impulse);

        //playerInventory.CurrentItem.Owner = null;
        playerInventory.CurrentItem.gameObject.transform.SetParent(null);
        playerInventory.CurrentItem.LastOwner = playerInventory.CurrentItem.Owner;
        playerInventory.CurrentItem.Owner = null;
        playerInventory.CurrentItem.canStun = true;
        playerInventory.DropItem();
        
        source.Play();
    }

    //public void SetCurrentItem()
    //{
    //    if (playerInventory.HasItem)
    //    {
    //        chargeEnabled = true;
    //        currentItem = playerInventory.CurrentItem;
    //        currentItemRB = playerInventory.CurrentItem.GetComponent<Rigidbody>();
    //        // Vfx.
    //        chargeEffect = currentItem.ChargeEffect;
    //        chargeDone = currentItem.ChargeDone;
    //        chargeSpeed = chargeEffect.GetFloat("ChargeSpeed");
    //        chargeRadius = chargeDone.GetFloat("MaxRadius");
    //    }
    //}

}
