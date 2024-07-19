using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSetup : MonoBehaviour
{
  // Public variable for the translation speed
    public float translationSpeed = 5f;


    // Update is called once per frame
    void Update()
    {
        // Translate the satellite in the negative z-axis direction in global space
        transform.position += -Vector3.forward * translationSpeed * Time.deltaTime;

        // Log the current translation speed to the console
        //Debug.Log("Translation Speed: " + translationSpeed);
    }
}
