using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Jump()
    {
        animator.SetBool("Moving", false);
        StartCoroutine(JumpRoutine());
    }

    private IEnumerator JumpRoutine()
    {
        animator.SetBool("Jumping", true);
        yield return new WaitForSeconds(1);
        animator.SetBool("Jumping", false);
    }

    public void Moving()
    {
        animator.SetBool("Jumping", false);
        animator.SetBool("Moving", true);
    }

    public void SetIdle()
    {
        animator.SetBool("Jumping", false);
        animator.SetBool("Moving", false);
    }
}
