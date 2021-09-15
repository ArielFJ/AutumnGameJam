using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class CharacterScript : MonoBehaviour
{
    public TextAsset[] inkStories;
    public TextAsset[] importantInkStories;
    public string characterName;
    public Animator anim;
    //public Image img;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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

    public TextAsset GetRandomStory() => GetRandomStoryFrom(inkStories);

    public TextAsset GetRandomImportantStory() => GetRandomStoryFrom(importantInkStories);

    private TextAsset GetRandomStoryFrom(TextAsset[] stories)
    {
        var index = Random.Range(0, stories.Length);
        return stories[index];
    }
}