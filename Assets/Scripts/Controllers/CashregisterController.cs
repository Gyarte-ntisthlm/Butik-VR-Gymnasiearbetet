using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashregisterController : MonoBehaviour
{
    Animator animator;

    // Register to the purchase completed event from the game manager
    void Start()
    {
        GameManager.instance.onPurchaseCompleted += OnPurchaseCompleted;
        GameManager.instance.onPurchaseBegin += OnPurchaseBegin;

        animator = GetComponent<Animator>();
    }

    void OnDestroy()
    {
        GameManager.instance.onPurchaseCompleted -= OnPurchaseCompleted;
        GameManager.instance.onPurchaseBegin -= OnPurchaseBegin;
    }

    // When the purchase is completed, play the "Done" animation clip from the cash register
    void OnPurchaseCompleted()
    {
        animator.Play("Armature|Done");
    }

    void OnPurchaseBegin()
    {
        animator.Play("Armature|AddedItem");
    }
}
