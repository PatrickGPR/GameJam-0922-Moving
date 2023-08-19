using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUIDisplayer : MonoBehaviour
{
    private Dictionary<Player, GameObject> uiInstances;
    [SerializeField] private GameObject singplePlayerContainerPrefab;

    private void Start()
    {
        PlayerManagement.Instance.OnPlayerJoinedCallback += OnPlayerJoinedCallback;
    }

    private void OnPlayerJoinedCallback(Player player)
    {
        var instance = Instantiate(singplePlayerContainerPrefab, transform);
        instance.GetComponent<SingplePlayerInfoContainer>().Setup(player);
    }
}
