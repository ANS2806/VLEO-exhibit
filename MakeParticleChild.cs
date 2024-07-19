using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeParticleChild : MonoBehaviour
{
    public ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        MakeParticleChildOfObject(other);
        //Debug.Log("particle is child of parent");
    }

    private void MakeParticleChildOfObject(GameObject objectToCollideWith)
    {
        // Get the particles from the particle system
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        int numParticles = particleSystem.GetParticles(particles);

        // Make each particle a child of the object
        for (int i = 0; i < numParticles; i++)
        {
            GameObject particleObject = new GameObject("Particle " + i);
            particleObject.transform.position = particles[i].position;
            particleObject.transform.parent = objectToCollideWith.transform;
        }
    }
}
