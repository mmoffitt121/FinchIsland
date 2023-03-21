using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeraldAnimation : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;

    public float forwardThreshold = 0.1f;

    void Update()
    {
        if (Mathf.Abs(rb.velocity.magnitude) > forwardThreshold)
        {
            animator.SetBool("Forward", true);
        }
        else
        {
            animator.SetBool("Forward", false);
        }
    }
}
