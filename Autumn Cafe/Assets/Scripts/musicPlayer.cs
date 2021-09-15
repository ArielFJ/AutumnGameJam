using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class musicPlayer : Interactible
{
    public MusicMenu MusicMenu;
    public GameObject canvas;

    private AudioSource audioSource;
    public List<AudioClip> songs = new List<AudioClip>();
    public float volume;
    private float trackTimer;
    private float songsPlayed;
    private bool[] beenPlayed;

    public bool shuffle;

    public bool skip;

    [SerializeField]
    AudioClip current;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        beenPlayed = new bool[songs.Count];
        if (!audioSource.isPlaying)
        {
            ChangeSong(Random.Range(0,songs.Count));
        }
    }

    private void Update()
    {
        current = audioSource.clip;

        audioSource.volume = volume;
        if (audioSource.isPlaying)
            trackTimer += 1 * Time.deltaTime;

        if(!audioSource.isPlaying || trackTimer >= audioSource.clip.length || skip == true)
        {
            if (shuffle)
            {
                ChangeSong(Random.Range(0, songs.Count));
            }
            else
            {
                ChangeSong(songs.IndexOf(audioSource.clip)+1);
            }
            skip = false;
        }
    }

    public override void interactFunction()
    {
        if (!MusicMenu.gameObject.activeSelf)
        {
            canvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            player.GetComponent<FirstPersonController>().enabled = false;
        }
    }

    void ChangeSong(int songPicked)
    {
        if (!beenPlayed[songPicked])
        {
            trackTimer = 0;
            songsPlayed++;
            beenPlayed[songPicked] = true;
            audioSource.clip = songs[songPicked];
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ResetSuffle()
    {
        if(songsPlayed == songs.Count)
        {
            songsPlayed = 0;
            for(int i = 0; i < songs.Count; i++)
            {
                if (i == songs.Count)
                    break;
                else
                    beenPlayed[i] = false;
            }
        }
    }
}
