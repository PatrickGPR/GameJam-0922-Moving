using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicHandler : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip startClip;
    [SerializeField] private AudioClip loopClip;
    
    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.clip = startClip;
        source.Play();
    }

    void Update()
    {
        if (source.isPlaying) return;
        
        source.clip = loopClip;
        source.loop = true;
        source.Play();
    }
}
