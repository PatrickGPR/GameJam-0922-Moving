using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ColorButtonTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // enum HoverState
    // {
    //     None,
    //     MovingToTarget,
    //     MovingBack
    // }
    //
    // [SerializeField] private float length;
    // [SerializeField] private float movingSpeed;
    // [SerializeField] private AnimationCurve toTargetCurve;
    // [SerializeField] private AnimationCurve moveBackCurve;
    //
    // private Vector2 defaultPosition;
    // private Vector2 targetPosition;
    //
    // private HoverState state;
    // private float timer;

    [SerializeField] private TMP_Text text;
    [SerializeField] private Color highlightColor;
    
    private Color defaultColor;

    private void Awake()
    {
        // defaultPosition = transform.position;
        // targetPosition = defaultPosition;
        // targetPosition.x += length;

        defaultColor = text.color;
    }

    private void Update()
    {
        // if (state is HoverState.MovingBack)
        // {
        //     DoLerp(targetPosition, defaultPosition, moveBackCurve);
        // }
        // else if (state is HoverState.MovingToTarget)
        // {
        //     DoLerp(defaultPosition, targetPosition, toTargetCurve);
        // }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // state = HoverState.MovingToTarget;
        text.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // state = HoverState.MovingBack;
        text.color = defaultColor;
    }

    // private void DoLerp(Vector2 from, Vector2 to, AnimationCurve curve)
    // {
    //     timer += Time.deltaTime * movingSpeed;
    //         
    //     transform.position = Vector2.Lerp(from, to, curve.Evaluate(timer));
    //
    //     if (timer >= 1)
    //     {
    //         state = HoverState.None;
    //         timer = 0;
    //     }
    // }
}