using System;
using System.Linq;
using Items;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public enum LKWState
{
    DrivingToPoint,
    Waiting,
    DriveAway,
    ReadyForRefill
}

public enum FillLKWResult
{
    Failed = 0,
    Accepted = 2,
    AcceptedAndCompeted = 20,
    Overfilled = 10
}

public class LKW : MonoBehaviour
{
    public LKWState LkwState
    {
        set
        {
            if (lkwState == value) return;
            lkwState = value;
            OnLKWStateChanged?.Invoke(value);
        }
        get => lkwState;
    }

    private LKWState lkwState;
    [SerializeField] private int neededFillAmount = 10;
    [SerializeField] private Transform logoFrontPosition;
    [SerializeField] private Transform logoBackPosition;
    [SerializeField] private Animator animator;
    
    [SerializeField] private AudioSource honkSource;
    
    
    [SerializeField] private AudioClip plateDownCLip;
    [SerializeField] private AudioClip plateUpCLip;
    [SerializeField] private AudioSource plateSource;

    private Department allowedDepartment;

    public Department AllowedDepartment => allowedDepartment;
    private int onItemDropID;
    private int driveAwayID;
    private int driveInSceneID;
    private int currentFillAmount = 0;

    public int NeededFillAmount => neededFillAmount;
    public int CurrentFillAmount => currentFillAmount;

    public Action<LKWState> OnLKWStateChanged;
    public Action OnLKWCompleted;

    private void Awake()
    {
        LkwState = LKWState.ReadyForRefill;
        onItemDropID = Animator.StringToHash("OnItemDrop");
        driveInSceneID = Animator.StringToHash("DriveInScene");
        driveAwayID = Animator.StringToHash("DriveAway");
        animator = GetComponent<Animator>();
        OnLKWStateChanged += OnLkwStateChanged;
    }

    private void OnLkwStateChanged(LKWState obj)
    {
        if (obj == LKWState.ReadyForRefill)
        {
            var Lkws = LKWManager.Instance.Lkws;

            var department = (Department) Random.Range(0, 4);

            var alreadyExistsDepartments = Lkws.Select(l => l.AllowedDepartment).ToArray();
            
            while (alreadyExistsDepartments.Contains(department))
            {
                department = (Department)Random.Range(0,4);
            }
            
            SetupTruck(department);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (lkwState != LKWState.Waiting) return;
        
        if (!other.CompareTag("Item")) return;

        if (!other.TryGetComponent<Item>(out var item)) return;
        if (item.LastOwner == null) return;
        
        var result = FillLKW(item);

        switch (result)
        {
            case FillLKWResult.Failed:
                return;
            case FillLKWResult.Accepted:
            case FillLKWResult.AcceptedAndCompeted:
            case FillLKWResult.Overfilled:
                
                item.LastOwner?.AddPoints((int)result);
                Destroy(item.gameObject);
                break;
        }
    }

    public void PlayHonkSound()
    {
        honkSource.Play();
    }

    public void PlayPlateDownSound()
    {
        plateSource.clip = plateDownCLip;
        plateSource.Play();
    }

    public void PlayPlateUpSound()
    {
        plateSource.clip = plateUpCLip;
        plateSource.Play();
    }

    public void OnDrivingFinished()
    {
        LkwState = LKWState.Waiting;
    }
    
    public void OnDriveAwayFinished()
    {
        LkwState = LKWState.ReadyForRefill;
    }

    public void SetupTruck(Department department)
    {
        allowedDepartment = department;
        
        var prefab = DepartmentUtility.DepartmentLogoPrefabs[allowedDepartment];
        var sound = DepartmentUtility.DepartmentSoundPrefabs[allowedDepartment];

        if (logoBackPosition.childCount > 0)
        {
            Destroy(logoBackPosition.GetChild(0).gameObject);
        }
        
        if (logoFrontPosition.childCount > 0)
        {
            Destroy(logoFrontPosition.GetChild(0).gameObject);
        }

        Instantiate(prefab, logoFrontPosition, false);
        Instantiate(prefab, logoBackPosition, false);

        honkSource.clip = sound;
        
        currentFillAmount = 0;
        animator.SetTrigger(driveInSceneID);
        LkwState = LKWState.DrivingToPoint;
    }

    private FillLKWResult FillLKW(Item item)
    {
        if (item == null || item.Department != allowedDepartment)
        {
            return FillLKWResult.Failed;
        }

        currentFillAmount += (int)item.Size;

        if (currentFillAmount < neededFillAmount)
        {
            animator.SetTrigger(onItemDropID);
            return FillLKWResult.Accepted;
        }
        
        animator.SetTrigger(driveAwayID);
        
        LkwState = LKWState.DriveAway;

        OnLKWCompleted?.Invoke();
        
        return currentFillAmount == neededFillAmount ? FillLKWResult.AcceptedAndCompeted : FillLKWResult.Overfilled;
    }
}