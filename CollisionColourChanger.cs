using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionColourChanger : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Color hitColor = Color.red; // Set the hit color in the Inspector
    public GameObject collisionTarget; // Assign this in the Inspector
    public Camera mainCamera; // Assign the main camera in the Inspector
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other == collisionTarget)
        {
            int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

            // Access the material of the collision target
            Material targetMaterial = other.GetComponent<Renderer>().material;
            Texture2D tex = targetMaterial.mainTexture as Texture2D;

            if (tex == null)
            {
                Debug.LogError("No texture found on collision target.");
                return;
            }

            // Loop through collision events and modify texture around collision points
            foreach (var collision in collisionEvents)
            {
                // Determine world space position of the collision
                Vector3 collisionPosition = collision.intersection;

                // Perform a raycast from the camera to the collision position
                Ray ray = mainCamera.ScreenPointToRay(mainCamera.WorldToScreenPoint(collisionPosition));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == collisionTarget)
                    {
                        // Get the UV coordinates from the hit
                        Vector2 pixelUV = hit.textureCoord;

                        //Debug.Log($"Collision at UV: {pixelUV}");

                        // Convert UV coordinates to pixel coordinates
                        int centerX = Mathf.RoundToInt(pixelUV.x * tex.width);
                        int centerY = Mathf.RoundToInt(pixelUV.y * tex.height);

                        // Define the size of the square area to color around the collision point
                        int squareSize = 10;

                        // Color the square area around the collision point
                        for (int i = -squareSize; i <= squareSize; i++)
                        {
                            for (int j = -squareSize; j <= squareSize; j++)
                            {
                                int x = centerX + i;
                                int y = centerY + j;

                                // Make sure we're not trying to set pixels outside the texture
                                if (x >= 0 && x < tex.width && y >= 0 && y < tex.height)
                                {
                                    tex.SetPixel(x, y, hitColor);
                                }
                            }
                        }
                    }
                }
            }

            // Apply all changes to the texture at once
            tex.Apply();
            Debug.Log("Texture pixels modified around collision points.");
        }
    }
}

