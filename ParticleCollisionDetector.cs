using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // Add this namespace for file I/O

public class ParticleCollisionDetector : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;
    public Rigidbody rigidbody;
    public string csvFilePath = "collision_data_1.csv"; // Set the CSV file path
    public string csvFilePathVelocity = "velocityOfParticles.csv";
    private bool headersWritten_velocity = false; // flag to check if the headers are written
    private bool headersWritten = false; // flag to check if the headers are written
    int particleCounter = 0;
    List<int> particleIDs = new List<int>();

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        particleIDs.Clear();
    }

    void Update()
    {
        int numParticlesAlive = particleSystem.GetParticles(particles);

        //Display the particle Velocity in a table
        string tableData_velocity;
        string csvData_velocity;

        if (!headersWritten_velocity)
        {
            tableData_velocity = "<table><tr><th>Particle Velocity</th><th>";
            csvData_velocity = "Particle Number, Velocity X,Velocity Y,Velocity Z\n";
            headersWritten_velocity = true;
        }
        else
        {
            tableData_velocity = "";
            csvData_velocity = "";
        }

        for (int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 localVelocity = particles[i].velocity;
            Vector3 velocity = transform.TransformDirection(localVelocity);
            
            // Assign a unique particle ID
            int particleID;
            if (i >= particleIDs.Count)
            {
                particleID = particleCounter++;
                particleIDs.Add(particleID);
            }
            else
            {
                particleID = particleIDs[i];
            }

            tableData_velocity += "<tr><td>(" + velocity.x + ", " + velocity.y + ", " + velocity.z + ")</td><td>z";
            csvData_velocity += particleID + "," + velocity.x + "," + velocity.y + "," + velocity.z + "\n";
        }
        tableData_velocity += "</table>";

        File.AppendAllText(csvFilePathVelocity, csvData_velocity);
        Debug.Log("CSV file updated: " + csvFilePathVelocity);
    }
    private void OnParticleCollision(GameObject other)
    {
        // Create a list to store the collision events
        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

        // Get the collision events
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

        // Display the collision data in a table
        string tableData;
        string csvData;

        if (!headersWritten)
        {
            tableData = "<table><tr><th>Collision Location</th><th>Collision Velocity</th><th>";
            csvData = "Location X,Location Y,Location Z,Velocity X,Velocity Y,Velocity Z, Normal X, Normal Y, Normal Z\n";
            headersWritten = true;
        }
        else
        {
            tableData = "";
            csvData = "";
        }
        for (int i = 0; i < numCollisionEvents; i++)
        {
            ParticleCollisionEvent collisionEvent = collisionEvents[i];
            tableData += "<tr><td>(" + collisionEvent.intersection.x + ", " + collisionEvent.intersection.y + ", " + collisionEvent.intersection.z + ")</td><td>(" + collisionEvent.velocity.x + ", " + collisionEvent.velocity.y + ", " + collisionEvent.velocity.z + ")</td><td>";
            csvData += collisionEvent.intersection.x + "," + collisionEvent.intersection.y + "," + collisionEvent.intersection.z + "," + collisionEvent.velocity.x + "," + collisionEvent.velocity.y + "," + collisionEvent.velocity.z + "," + collisionEvent.normal.x + "," + collisionEvent.normal.y + "," + collisionEvent.normal.z + "\n";
        }
        tableData += "</table>";

        // Display the table data
        //Debug.Log(tableData);

        // Export to CSV file
        File.AppendAllText(csvFilePath, csvData);
        Debug.Log("CSV file updated: " + csvFilePath);
    }
}