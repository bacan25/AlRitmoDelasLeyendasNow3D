using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmEngine : MonoBehaviour
{
    public AudioSource audioSource;
    public float bpm;
    private float beatInterval;
    private float nextBeatTime;

    // Definir un evento que se dispare en cada beat
    public event Action OnBeat;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        beatInterval = 60f / bpm;
        nextBeatTime = audioSource.time + beatInterval;
        audioSource.Play();
    }

    void Update()
    {
        if (audioSource.time >= nextBeatTime)
        {
            nextBeatTime += beatInterval;
            OnBeat?.Invoke(); // Disparar el evento en cada beat
        }
    }
}
