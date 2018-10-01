using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public delegate void OnAttachedToHandDelegate(PlayerHand hand);
    public delegate void OnDetachedFromHandDelegate(PlayerHand hand);

    [HideInInspector]
    public event OnAttachedToHandDelegate onAttachedToHand;

    [HideInInspector]
    public event OnDetachedFromHandDelegate onDetachedFromHand;

    private void OnAttachedToHand(PlayerHand hand)
    {
        if(onAttachedToHand != null)
        {
            onAttachedToHand.Invoke(hand);
        }
    }

    private void OnDetachedFromHand(PlayerHand hand)
    {
        if(onDetachedFromHand != null)
        {
            onDetachedFromHand.Invoke(hand);
        }
    }
}
