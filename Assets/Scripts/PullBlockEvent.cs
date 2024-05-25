using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBlockEvent : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public void OnPullEnter() {
        animator.SetBool("activate", true);
    }

    public void OnPullExit() {
        animator.SetBool("activate", false);
    }
}
