using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interactible
{
    public Animator doorAnim;
    public bool isOpen;

    public override void interactFunction()       
    {
        if (!isOpen)
        {
            doorAnim.SetBool("Open", true);
            isOpen = true;
        }
        else
        {
            doorAnim.SetBool("Open", false);
            isOpen = false;
        }
    }

}
