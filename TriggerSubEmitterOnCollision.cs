using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSubEmitterOnCollision : MonoBehaviour
{
    public ParticleSystem subEmitter;
    public string collisionTag = "Collider"; // tag of the GameObject to collide with

    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(collisionTag))
        {
            TriggerSubEmitter(other.transform.position);
        }
    }

    private void TriggerSubEmitter(Vector3 position)
    {
        // Set the sub-emitter's position to the collision position
        subEmitter.transform.position = position;

        // Emit one particle from the sub-emitter
        subEmitter.Emit(1);
    }
}
