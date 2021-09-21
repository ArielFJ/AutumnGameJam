using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class CharacterScript : MonoBehaviour
{
    public TextAsset[] inkStories;
    public TextAsset[] importantInkStories;
    public AudioClip[] characterConversationSounds;
    public string characterName;
    
    private Animator anim;
    private AudioSource audioSource;
    //public Image img;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAnimation(string _name)
    {
        switch (_name)
        {
            case "neutral":
                anim.SetTrigger("toNeutral");
                break;
            case "smile":
                anim.SetTrigger("toSmile");
                break;
            case "frown":
                anim.SetTrigger("toFrown");
                break;
        }
    }

    public void PlayRandomConversationSound()
    {
        var audioClip = GetRandomElementFrom(characterConversationSounds);
        audioSource.PlayOneShot(audioClip);
    }

    public TextAsset GetRandomStory() => GetRandomElementFrom(inkStories);

    public TextAsset GetRandomImportantStory() => GetRandomElementFrom(importantInkStories);

    private TElement GetRandomElementFrom<TElement>(TElement[] elements)
    {
        var index = Random.Range(0, elements.Length);
        return elements[index];
    }
}