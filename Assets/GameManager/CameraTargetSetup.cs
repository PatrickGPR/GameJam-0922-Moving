using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineTargetGroup))]
public class CameraTargetSetup : MonoBehaviour
{
    private CinemachineTargetGroup cinemachineTargetGroup;

    public static CameraTargetSetup Instance;
    
    void Awake()
    {
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetTarget(Transform transform)
    {
        var targets = cinemachineTargetGroup.m_Targets;
        var index = targets.Length;
        cinemachineTargetGroup.AddMember(transform, 1, 5);
    }
}
