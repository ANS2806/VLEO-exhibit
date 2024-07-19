using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TranslateSatellite : MonoBehaviour
{
    // Public variable for the translation speed
    public float translationSpeed = 5f;


    // Public variable for the fraction of kinetic energy to reduce by on collision
    public float collisionEnergyReductionFraction = 0.05f;
    
    //Select TextMeshPro
    public TextMeshProUGUI textMeshPro;


    //Display initial Speed in start
    void Start()
    {
       
        if(textMeshPro!=null)
        {
            RectTransform rectTransform = textMeshPro.rectTransform;
            rectTransform.anchorMin = new Vector2(0,1); //Top-left corner
            rectTransform.anchorMax = new Vector2(0,1); //Top-left corner
            rectTransform.pivot = new Vector2(0,1); //Top-left pivot

            //Set position relative to Canvas
            rectTransform.anchoredPosition = new Vector2(10,-10); //adjust as needed

            //Display start text
            textMeshPro.text = $"<color=green>Satellite Speed = {translationSpeed}";
            
        }
    }


    // Update is called once per frame
    void Update()
    {     
        // Translate the satellite in the negative z-axis direction in global space
        transform.position += -Vector3.forward * translationSpeed * Time.deltaTime;

        // Log the current translation speed to the console
        //Debug.Log("Translation Speed: " + translationSpeed);
    }

    // This method is called when the Collider other collides with this GameObject's Collider
    void OnParticleCollision(GameObject other)
    {
        //Debug.Log("collision with main body!");
        // Check if the collided object's tag is "particles"
        if (other.CompareTag("Particles"))
        {
            HandleCollision(other);

        }
    }

    public void HandleCollision(GameObject other)
    {
        // Reduce the translation speed by the fraction specified
        translationSpeed -= translationSpeed * collisionEnergyReductionFraction;  
        textMeshPro.text = $"<color=green>Satellite Speed = {translationSpeed}";
    }

    public void DisplaySpeed(float addedSpeed)
    {
        translationSpeed += addedSpeed;
        //Display new Speed after thrusters
        textMeshPro.text = $"<color=green>Satellite Speed = {translationSpeed}";
    }
}
