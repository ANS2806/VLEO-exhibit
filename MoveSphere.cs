using UnityEngine;

public class MoveSphere : MonoBehaviour
{
    // Public variables for the cube and sphere objects
    public GameObject cube;
    public GameObject sphere;

    // Public variables for the initial and reflected angles
    public float initialAngle = 45f;
    public float reflectedAngle = 135f;

    // Public variable for the speed of the sphere
    public float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction vector from the sphere to the cube
        Vector3 direction = cube.transform.position - sphere.transform.position;

        // Rotate the direction vector by the initial angle
        direction = Quaternion.AngleAxis(initialAngle, Vector3.up) * direction;

        // Move the sphere towards the cube along the rotated direction
        sphere.transform.position += direction.normalized * speed * Time.deltaTime;

        // Calculate the reflection direction vector
        Vector3 reflectionDirection = Vector3.Reflect(direction, cube.transform.forward);

        // Rotate the reflection direction vector by the reflected angle
        reflectionDirection = Quaternion.AngleAxis(reflectedAngle, Vector3.up) * reflectionDirection;

        // Move the sphere away from the cube along the reflected direction
        sphere.transform.position += reflectionDirection.normalized * speed * Time.deltaTime;
    }
}
