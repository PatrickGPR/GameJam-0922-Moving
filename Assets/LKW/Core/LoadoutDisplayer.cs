using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadoutDisplayer : MonoBehaviour
{
    [SerializeField] private LKW lkw;
    [SerializeField] private float fadeDuration = 1;
    private TMP_Text amountText;

    private void Awake()
    {
        amountText = GetComponent<TMP_Text>();
        lkw.OnLKWStateChanged += OnLkwStateChanged;
    }

    private void OnLkwStateChanged(LKWState obj)
    {
        if (obj == LKWState.DriveAway)
        {
            StartCoroutine(nameof(FadeOut));
        }
        else if (obj == LKWState.DrivingToPoint)
        {
            StartCoroutine(nameof(FadeIn));
        }
    }

    void Update()
    {
        if (lkw && amountText)
        {
            amountText.text = $"{lkw.CurrentFillAmount}/{lkw.NeededFillAmount}";
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            amountText.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }
    }
    
    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            amountText.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            yield return null;
        }
    }
}
