using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObjectCollisionHandler : MonoBehaviour
{
    public TranslateSatellite parentScript; // Reference to the parent script
    public DragAltitudeChange parentScriptDrag; //Reference to the parent script

    void OnParticleCollision(GameObject other)
    {
        
        // Call the parent script's method to handle the collision
        if (other.CompareTag("Particles"))
        {
            parentScript.HandleCollision(other);
            parentScriptDrag.HandleDrag(other);
            //Debug.Log("collision!");
        }       
    }
}
