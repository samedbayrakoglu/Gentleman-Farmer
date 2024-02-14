using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] ParticleSystem seedParticles;
    [SerializeField] ParticleSystem waterParticles;



    private void PlaySeedParticles()
    {
        seedParticles.Play();
    }

    private void PlayWaterParticles()
    {
        waterParticles.Play();
    }
}
