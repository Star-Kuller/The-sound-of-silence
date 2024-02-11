using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEmitter : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    [SerializeField] private int emitCount;
    [SerializeField] private AudioClip sound;
    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnMouseEnter()
    {
        _particleSystem.Emit(emitCount);
        GetComponent<AudioSource>().PlayOneShot(sound);
    }
}
