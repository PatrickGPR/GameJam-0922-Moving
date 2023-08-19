using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    
    [SerializeField] private AudioSource source;

    private List<AudioClip> clips = new List<AudioClip>();

    public void PlaySound()
    {
        if (clips.Count == 0)
        {
            Refill();
        }

        var randomNumber = Random.Range(0, clips.Count);
        source.clip = clips[randomNumber];
        clips.RemoveAt(randomNumber);
        source.Play();
    }

    private void Refill()
    {
        foreach (var audioClip in audioClips)
        {
            clips.Add(audioClip);
        }
    }
}
