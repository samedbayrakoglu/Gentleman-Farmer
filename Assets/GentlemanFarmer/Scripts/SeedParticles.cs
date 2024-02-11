using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SeedParticles : MonoBehaviour
{
    public static Action<Vector3[]> OnSeedCollied;


    private void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        List<ParticleCollisionEvent> particleCollisionEvents = new List<ParticleCollisionEvent>();
        int collisionCount = ps.GetCollisionEvents(other, particleCollisionEvents);

        Vector3[] collisionPositions = new Vector3[collisionCount];


        for (int i = 0; i < collisionCount; i++)
        {
            collisionPositions[i] = particleCollisionEvents[i].intersection;
        }

        OnSeedCollied?.Invoke(collisionPositions);
    }
}
