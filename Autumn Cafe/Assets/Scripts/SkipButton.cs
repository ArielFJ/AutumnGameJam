using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : Interactible
{
    public musicPlayer MusicPlayer;

    public override void interactFunction()
    {
        MusicPlayer.skip = true;
    }
}
