using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SatelliteVelocityMonitor : MonoBehaviour
{
    public Rigidbody rigidbody;
    public string csvFileSatelliteVelocity; //set the file to store data
    private bool headersWritten = false; // flag to check if headers have been written
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        string tableData_velocitySat;
        string csvData_velocitySat;

        if (!headersWritten)
        {
            tableData_velocitySat = "<table><tr><th>Satellite Velocity</th></tr>";
            csvData_velocitySat = "Velocity X,Velocity Y,Velocity Z, Rotation X, Rotation Y, Rotation Z\n";
            headersWritten = true;
        }
        else
        {
            tableData_velocitySat = "";
            csvData_velocitySat = "";
        }

        Vector3 velocitySat = rigidbody.velocity;
        Vector3 rotationSat = rigidbody.rotation.eulerAngles;
        Debug.Log("Satellite Velocity: " + velocitySat);
        Debug.Log("Satellite Rotation: " + rotationSat);
        tableData_velocitySat += "<tr><td>(" + velocitySat.x + ", " + velocitySat.y + ", " + velocitySat.z + ")</tr></td>";
        csvData_velocitySat += velocitySat.x + "," + velocitySat.y + "," + velocitySat.z + "," + rotationSat.x + "," + rotationSat.y + "," + rotationSat.z + "\n";
        
        tableData_velocitySat += "</table>";

        File.AppendAllText(csvFileSatelliteVelocity, csvData_velocitySat);
        Debug.Log("CSV file updated: " + csvFileSatelliteVelocity);
    }
}
