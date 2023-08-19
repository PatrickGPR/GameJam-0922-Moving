using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LKWManager : MonoBehaviour
{
    [SerializeField] private LKW[] lkws;

    public LKW[] Lkws => lkws;

    private bool setupStart;

    public static LKWManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        foreach (var lkw in lkws)
        {
            if (lkw == null)
            {
                continue;
            }
            
            lkw.OnLKWStateChanged += OnLkwStateChanged;
        }
    }

    private void OnLkwStateChanged(LKWState obj)
    {
        if (!setupStart) return;
        
        if (obj == LKWState.Waiting)
        {
            GameManager.Instance.StartGame();
            setupStart = false;
        }
    }
    
    public void StartGame()
    {
        setupStart = true;
        
        var alreadyExistsDepartments = new List<Department>(); 

        foreach (var lkw in lkws)
        {
            if (lkw == null)
            {
                continue;
            }
            
            var department = (Department)Random.Range(0,4);

            while (alreadyExistsDepartments.Contains(department))
            {
                department = (Department)Random.Range(0,4);
            }
            
            alreadyExistsDepartments.Add(department);
            
            lkw.SetupTruck(department);
        }
    }

    private bool AllLKWsWaitingToFill() => lkws.All(lkw => lkw.LkwState == LKWState.Waiting);
}
