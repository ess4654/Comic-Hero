using System;
using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private AnimatorController player1;
    [SerializeField] private AnimatorOverrideController player2;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimation(int index)
    {
        animator.runtimeAnimatorController = index == 0 ? player1 : player2;
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
