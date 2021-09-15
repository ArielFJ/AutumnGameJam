using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mixerSwitch : Interactible
{
    public Mixer mixer;
    public bool on;

    public override void interactFunction()
    {
        if (mixer.Bowl != null && mixer.on == false)
        {
            mixer.on = true;
            on = true;
        }
        else
        {
            mixer.on = false;
            on = false;
        }
    }
}
