using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ItemDatabase
{
    private static readonly Dictionary<Department, List<Item>> activeSceneItems =
        new Dictionary<Department, List<Item>>();

    public static Dictionary<Department, List<Item>> ActiveSceneItems => activeSceneItems;

    public static Action<Item> OnItemAdded;
    public static Action<Item> OnItemRemoved;

    public static void Awake()
    {
        SceneManager.activeSceneChanged += (arg0, scene) => activeSceneItems.Clear();
    }
    
    public static bool AddItem(Item item)
    {
        if (item == null)
        {
            Debug.Log("Item is null");
            return false;
        }

        if (!activeSceneItems.ContainsKey(item.Department))
        {
            activeSceneItems.Add(item.Department, new List<Item>());
        }

        activeSceneItems[item.Department].Add(item);
        OnItemAdded?.Invoke(item);
        return true;
    }
    public static bool RemoveItem(Item item)
    {
        if (item == null)
        {
            Debug.Log("Item is null");
            return false;
        }

        if (activeSceneItems.ContainsKey(item.Department))
        {
            activeSceneItems[item.Department].Remove(item);
            OnItemRemoved?.Invoke(item);
            return true;
        }

        return false;
    }

    public static bool DepartmentHasAlreadyMaxValue(Department department, int maxValue)
    {
        if (activeSceneItems.ContainsKey(department))
        {
            return activeSceneItems[department].Count >= maxValue;
        }

        return false;
    }

    public static int GetAmountOfItems()
    {
        return activeSceneItems.Sum(item => item.Value.Count);
    }
    public static int GetAmountOfItems(Department department)
    {
        return activeSceneItems.ContainsKey(department) ? activeSceneItems[department].Count : -1;
    }
    
    
}