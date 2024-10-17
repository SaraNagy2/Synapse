using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Instance;
    public AudioSource audio;
    [SerializeField] AudioClip FlipSound;
    [SerializeField] AudioClip MismatchingSound;
    [SerializeField] AudioClip MatchingSound;
    [SerializeField] AudioClip WinSound;
    [SerializeField] AudioClip GameOverSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayFlipSound() 
    {
        audio.PlayOneShot(FlipSound);
    }
    public void PlayMismatchingSound()
    {
        audio.PlayOneShot(MismatchingSound);
    }
    public void PlayMatchingSound()
    {
        audio.PlayOneShot(MatchingSound);
    }

    public void PlayWinSound()
    {
        audio.PlayOneShot(WinSound);
    }

    public void PlayGameOverSound()
    {
        audio.PlayOneShot(GameOverSound);
    }
}
