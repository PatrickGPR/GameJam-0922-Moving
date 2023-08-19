using System;
using UnityEngine;

[Serializable]
public class Player
{
    private GameObject playerInstance;
    private Color color;
    private int points;

    public GameObject PlayerInstance => playerInstance;

    public Color Color => color;

    public int Points => points;

    public Action<int, int> OnPointsUpdated; 

    public Player(GameObject playerInstance, Color color)
    {
        this.playerInstance = playerInstance;
        this.color = color;
        points = 0;
    }

    public void AddPoints(int points)
    {
        this.points += points;
        OnPointsUpdated?.Invoke(points, this.points);
    }
}
