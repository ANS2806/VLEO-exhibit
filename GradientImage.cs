using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class GradientImage : MonoBehaviour
{
    public Color topColor = Color.red;
    public Color middleColor = Color.green;
    public Color bottomColor = Color.blue;

    private Image image;
    private Material material;

    void Awake()
    {
        image = GetComponent<Image>();

        if (image == null)
        {
            Debug.LogError("GradientImage script must be attached to a GameObject with an Image component.");
            return;
        }

        // Create a new material with a shader that supports gradients
        Shader shader = Shader.Find("UI/Default");
        material = new Material(shader);
        image.material = material;

        // Set the material's shader properties to create the gradient
        UpdateGradient();
    }

    void UpdateGradient()
    {
        if (material == null) return;

        Texture2D texture = new Texture2D(1, 3);
        texture.wrapMode = TextureWrapMode.Clamp;

        // Set the colors for the gradient
        texture.SetPixel(0, 0, bottomColor);
        texture.SetPixel(0, 1, middleColor);
        texture.SetPixel(0, 2, topColor);
        texture.Apply();

        // Assign the texture to the material
        material.SetTexture("_MainTex", texture);
    }

    // Update the gradient when any color is changed in the Inspector
    void OnValidate()
    {
        UpdateGradient();
    }
}

