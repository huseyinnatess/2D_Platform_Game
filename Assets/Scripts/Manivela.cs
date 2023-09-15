using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manivela : MonoBehaviour
{
    BoxCollider2D manivelaCollider;
    Animator animator;

    public Animator doorAnimator;
    private void Awake()
    {
        manivelaCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(manivelaCollider.IsTouchingLayers(LayerMask.GetMask("Weapon")))
        {
            animator.SetTrigger("acilma");
        }
    }


    public void KapiAcilma()
    {
        doorAnimator.SetBool("acilsinMi", true);
    }
}
