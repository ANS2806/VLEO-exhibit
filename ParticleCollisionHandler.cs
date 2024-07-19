using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ParticleCollisionHandler : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public float dampingFraction = 1.0f;
    private bool[] particlesCollided;
    public TextMeshProUGUI textMeshPro;
    public Color initialVelocityColour = Color.yellow;
    public Color finalVelocityColour = Color.red;

    void Start()
    {
        particlesCollided = new bool[particleSystem.main.maxParticles];
        if(textMeshPro!=null)
        {
            RectTransform rectTransform = textMeshPro.rectTransform;
            rectTransform.anchorMin = new Vector2(0,1); //Top-left corner
            rectTransform.anchorMax = new Vector2(0,1); //Top-left corner
            rectTransform.pivot = new Vector2(0,1); //Top-left pivot

            //Set position relative to Canvas
            rectTransform.anchoredPosition = new Vector2(10,-10); //adjust as needed

            //Display start text
            textMeshPro.text = $"<color=green>Launching particles!";
        }
        else
        {
            Debug.Log("not found!");
        }
    }
 

    void OnParticleCollision(GameObject other)
    {
        // Get the Renderer component of the collided GameObject
        Renderer renderer = other.GetComponent<Renderer>();
        if (renderer != null)
        {
            // Get the material of the collided GameObject
            Material material = renderer.material;
            if (material != null)
            {
                // Check the material name
                string materialName = material.name;
                //Debug.Log("Particle collided with object having material: " + materialName);

                // Get the particles that collided with the other game object
                ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
                int numParticles = particleSystem.GetParticles(particles);

                // Loop through the particles that collided
                for (int i = 0; i < numParticles; i++)
                {
                    if (!particlesCollided[i]) // Check if the particle has already collided
                    {
                        Vector3 initialVelocity_local = particles[i].velocity;
                        Vector3 initialVelocity_global = transform.TransformDirection(initialVelocity_local);

                        // Perform actions based on the material name
                        // Replace "YourMaterialName" with the actual name of the material
                        // Handle specific material collision
                        //Debug.Log("Collided with the specific material!");
                            
                        if (materialName == "Satellite_Mat_2 (Instance)" || materialName == "Ball_Mat (Instance)")
                        {
                            // Calculate magnitude of initial velocity
                            float initialMagnitude = Mathf.Sqrt(initialVelocity_global.x * initialVelocity_global.x + initialVelocity_global.z * initialVelocity_global.z);

                            // Calculating x and y components based on cosine curve
                            float minValue = -(dampingFraction * initialMagnitude * Mathf.PI) / 2;
                            float maxValue = (dampingFraction * initialMagnitude * Mathf.PI) / 2;
                            float vx = Random.Range(minValue, maxValue);
                            float vy = -dampingFraction * initialMagnitude * Mathf.Cos(vx / (dampingFraction * initialMagnitude));
                            float vz = 0;

                            Vector3 finalVelocity_global = new Vector3(vx, vy, vz);
                            Vector3 finalVelocity_local = transform.InverseTransformDirection(finalVelocity_global);
                            // Change the particle velocity in the x, y, and z directions
                            particles[i].velocity = finalVelocity_local;
                            string initialVelocityHtmlColor = ColorUtility.ToHtmlStringRGB(initialVelocityColour);
                            string finalVelocityHtmlColor = ColorUtility.ToHtmlStringRGB(finalVelocityColour);
                            textMeshPro.text = $"<color=#{initialVelocityHtmlColor}>Initial Velocity = {initialVelocity_global.magnitude:F2}\n <color=#{finalVelocityHtmlColor}>Final Velocity = {finalVelocity_global.magnitude:F2}";

                        }
                        else
                        {
                            Vector3 finalVelocity_global = new Vector3(initialVelocity_global.x,-initialVelocity_global.y,initialVelocity_global.z);
                            Vector3 finalVelocity_local = transform.InverseTransformDirection(finalVelocity_global);
                            // Change the particle velocity in the x, y, and z directions
                            particles[i].velocity = finalVelocity_local;
                            string initialVelocityHtmlColor = ColorUtility.ToHtmlStringRGB(initialVelocityColour);
                            string finalVelocityHtmlColor = ColorUtility.ToHtmlStringRGB(finalVelocityColour);
                            textMeshPro.text = $"<color=#{initialVelocityHtmlColor}>Initial Velocity = {initialVelocity_global.magnitude:F2}\n <color=#{finalVelocityHtmlColor}>Final Velocity = {finalVelocity_global.magnitude:F2}";

                        }
                        //Debug.Log(finalVelocity_global);
                        particlesCollided[i] = true; // Set the flag to true to mark it as collided
                    }
                }

                // Apply the modified particles back to the particle system
                particleSystem.SetParticles(particles, numParticles);
                //Debug.Log("diffuse emission!");
            }
        }
    }
}
