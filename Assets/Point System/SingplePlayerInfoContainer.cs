using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingplePlayerInfoContainer : MonoBehaviour
{
    [SerializeField] private Image colorImage;
    [SerializeField] private TMP_Text points;
    [SerializeField] private TMP_Text addedPoints;
    private Player player;
    
    
    private Animator animator;
    private int addedPointsID;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        addedPointsID = Animator.StringToHash("AddedPoints");
    }


    public void Setup(Player player)
    {
        this.player = player;
        colorImage.color = player.Color;
        addedPoints.color = player.Color;
        points.text = player.Points.ToString();

        player.OnPointsUpdated += OnPointsUpdated;
    }

    private void OnPointsUpdated(int addedPoints, int newData)
    {
        this.addedPoints.text = "+" + addedPoints;
        animator.SetTrigger(addedPointsID);

        StartCoroutine(LerpValue(newData - addedPoints, newData));
    }

    private IEnumerator LerpValue(int oldValue, int targetValue)
    {
        var speed = 2f;
        float timer = 0;
        int start = oldValue;
        int target = targetValue;
        float value = 1.0f / 5;
        float time = value * (target - start);
        
        while (timer < time)
        {
            timer += Time.deltaTime * speed;
            points.text = ((int)Mathf.Lerp(start, target, timer/time)).ToString();

            yield return null;
        }
    }
}
