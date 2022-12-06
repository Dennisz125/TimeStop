using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTurn : MonoBehaviour
{
    public Animator animator;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            print("FADE Out");
            FadeToNextTurn(1);
        }
        /*else 
        if (Input.GetMouseButtonDown(0))
        {
            print("Stop Fadding");
            OnFadeComplete(1);
        }*/
    }

    public void FadeToNextTurn(int turnIndex)
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        print("FADE IN");
        animator.SetTrigger("FadeIn");
    }
}
