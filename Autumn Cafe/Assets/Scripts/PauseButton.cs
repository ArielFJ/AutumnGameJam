using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : Interactible
{
    public musicPlayer MusicPlayer;
    public bool paused;

    public override void interactFunction()
    {
        if (!paused)
        {
            MusicPlayer.audioSource.Pause();
            paused = true;
        }
        else
        {
            MusicPlayer.audioSource.UnPause();
            paused = false;
        }
    }
}
