using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraSubscriber : MonoBehaviour
{
    void Start()
    {
        CameraTargetSetup.Instance.SetTarget(transform);
    }
}
