using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    [SerializeField] private List<Color> playerColors;
    private List<Player> players = new List<Player>();

    public int PlayerAmount => players.Count;

    public Action<Player> OnPlayerJoinedCallback;
    public Action<Player> OnPlayerLeftCallback;

    public static PlayerManagement Instance;

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
    }

    public bool IsBestPlayer(Player player)
    {
        return GetBestPlayer() == player;
    }

    public Player GetBestPlayer()
    {
        return GetSortedPlayerByScore().LastOrDefault();
    }

    public Player[] GetSortedPlayerByScore()
    {
        return players.OrderBy(p => p.Points).ToArray();
    }

public Player OnPlayerJoined(GameObject playerInstance)
    {
        var player = new Player(playerInstance, playerColors[players.Count]);
        players.Add(player);
        OnPlayerJoinedCallback?.Invoke(player);
        return player;
    }
    
    public void OnPlayerLeft(GameObject playerInstance)
    {
        var player = players.FirstOrDefault(p => p.PlayerInstance == playerInstance);
        players.Remove(player);
        OnPlayerLeftCallback?.Invoke(player);
    }
}
