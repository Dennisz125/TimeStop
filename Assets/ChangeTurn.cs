using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTurn : MonoBehaviour
{
    public Animator animator;
    
    public void FadeToNextTurn()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        animator.SetTrigger("FadeIn");
    }
}
