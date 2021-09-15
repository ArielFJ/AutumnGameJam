using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using TMPro;
using UnityEngine.UI;

public class MusicMenu : MonoBehaviour
{
    public musicPlayer music;
    public GameObject canvas;
    public GameObject player;

    public GameObject songSelectionMenu;

    public int changingIndex;

    public List<AudioClip> allSongs = new List<AudioClip>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<FirstPersonController>().enabled = true;
        canvas.SetActive(false);
    }

    public void changeChangingIndex(int indexOfSongToChange)
    {
        changingIndex = indexOfSongToChange;
    }

    public void ChangeSongAtIndex(int newClipIndex)
    {
        music.songs[changingIndex] = allSongs[newClipIndex];
    }

    public void OpenSongSelection()
    {
        songSelectionMenu.SetActive(true);
    }

    public void CloseSongSelection()
    {
        songSelectionMenu.SetActive(false);
    }
}
