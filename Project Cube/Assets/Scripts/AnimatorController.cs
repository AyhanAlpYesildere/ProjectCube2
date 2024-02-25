using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{

    public Animator animator;
    
    void Start()
    {
        animator.SetBool("Running", false);
    }

    
    void Update()
    {
        if(PlayerManager.isGameStarted == true)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }
}
