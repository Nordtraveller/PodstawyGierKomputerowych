using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour {

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeFacingToRight()
    {
        animator.SetBool("IsFacingLeft", false);
        animator.SetBool("IsFacingRight", true);
    }

    public void ChangeFacingToLeft()
    {
        animator.SetBool("IsFacingLeft", true);
        animator.SetBool("IsFacingRight", false);
    }
}
