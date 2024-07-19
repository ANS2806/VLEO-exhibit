using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCubeAndCylinder : MonoBehaviour
{
    // Public rotation rate control
    public float rotationRate = 50.0f;

    // Reference to the cube object
    public GameObject cube;

    // Reference to the cylinder object
    public GameObject cylinder;

    // Called when the script is first enabled
    void Start()
    {
        // Set the initial rotation of the cube
        cube.transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the cube about the x-axis
        cube.transform.Rotate(Vector3.right, rotationRate * Time.deltaTime);

        // Move the cylinder to follow the face of the cube
        cylinder.transform.position = cube.transform.TransformPoint(new Vector3(0, 0, 0.5f));
        cylinder.transform.rotation = cube.transform.rotation;

        cylinder.transform.Rotate(Vector3.right, 90.0f);
    }
}
