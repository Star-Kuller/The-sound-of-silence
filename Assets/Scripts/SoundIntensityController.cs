using System;
using UnityEngine;

public class SoundIntensityController : MonoBehaviour
{
    [SerializeField] private int health;
    private ParticleSystem _particles;
    [SerializeField] private Transform circle;

    private void Start()
    {
        _particles = GetComponent<ParticleSystem>();
    }
}
