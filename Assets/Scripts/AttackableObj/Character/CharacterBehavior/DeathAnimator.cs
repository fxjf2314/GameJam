using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimator : MonoBehaviour
{
    public Animator animator;
    private void OnDisable()
    {
        animator.keepAnimatorStateOnDisable = true;
        animator.enabled = true;
    }
}
