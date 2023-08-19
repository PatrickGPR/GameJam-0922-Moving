using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.VFX;

public enum Department { Film, Art, Audio, Progger }
public enum Size { S = 1, M = 2, L = 3, XL = 4}
public class Item : MonoBehaviour
{
    [SerializeField] Department department;
    [SerializeField] Size size;

    [Header("Effects")]
    [SerializeField] VisualEffect chargeEffect;
    [SerializeField] VisualEffect chargeDone;

    [Header("Destruction")]
    [SerializeField] GameObject destroyedPackage;
    [SerializeField] GameObject destroyParticle;
    bool isQuitting;

    Player owner;
    Player lastOwner;

    public Department Department { get => department; set => department = value; }
    public Size Size { get => size; set => size = value; }
    public VisualEffect ChargeEffect { get => chargeEffect; set => chargeEffect = value; }
    public VisualEffect ChargeDone { get => chargeDone; set => chargeDone = value; }
    
    public Player Owner { get => owner; set => owner = value; }
    public Player LastOwner { get => lastOwner; set => lastOwner = value; }

    public bool canStun;

    private void Awake()
    {
        ItemDatabase.AddItem(this);
    }
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if(!this.gameObject.scene.isLoaded) return;
        
        // Get DestroyPackage here.
        if (!isQuitting)
        {
            GameObject tmp = Instantiate(destroyParticle, transform.position, Quaternion.identity);
            Destroy(tmp, 4f);
        }

        ItemDatabase.RemoveItem(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        canStun = false;
    }

    public static GameObject GenerateRandomItem(int maxItemsForEachDepartment)
    {
        var itemPot = new List<Department>();

        var departments = Enum.GetValues(typeof(Department)).Cast<Department>();

        foreach (var department in departments)
        {
            var difference = maxItemsForEachDepartment - ItemDatabase.GetAmountOfItems(department);
            for (int i = 0; i < difference; i++)
            {
                itemPot.Add(department);
            }
        }

        var itemDepartment = itemPot[Random.Range(0, itemPot.Count)];

        return ItemPrefabUtility.GetRandomItemPrefab(itemDepartment);
    }

    public void Drop()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }


    public void InitDestruction()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        Transform tmpPos = transform;
        tmpPos.Rotate(0, -90, 0);
        transform.gameObject.SetActive(false);
        GameObject brokenPackage = Instantiate(destroyedPackage, tmpPos.position, transform.rotation);
        brokenPackage.GetComponent<ItemDestruction>().DestroyPackage(transform.position);
    }
}
