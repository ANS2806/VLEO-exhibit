using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DragAltitudeChange : MonoBehaviour
{
    //Public variable for the altitude
    public float altitude = 400.0f;
    private float prevAltitude = 400.0f;

    //Select TextMeshPro to display altitude
    public TextMeshProUGUI textMeshProAltitude;
    public TextMeshProUGUI textMeshProDeltaV;
    public TextMeshProUGUI textMeshProAltitudeWarning;

    private Rigidbody rb; // satellite's Rigidbody component

    //Reduce altitude drag Force
    private float dragForce = 2.5f;
    //public float density;
    //public float dragCoeff;
    //public float area;
    //private float speed;

    //Display delta V needed to manoeuvre to original orbit 
    private float deltaV = 0f;

    //Adding thrust to increase velocity and enter original orbit
    public float thrustForce; // force magnitude
    public KeyCode thrustUpKey = KeyCode.U; // key to press to activate thruster

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(textMeshProAltitude!=null)
        {
            RectTransform rectTransform = textMeshProAltitude.rectTransform;
            rectTransform.anchorMin = new Vector2(0,1); //Top-left corner
            rectTransform.anchorMax = new Vector2(0,1); //Top-left corner
            rectTransform.pivot = new Vector2(0,1); //Top-left pivot

            //Set position relative to Canvas
            rectTransform.anchoredPosition = new Vector2(10,-10); //adjust as needed

            //Display start text
           
            textMeshProAltitude.text = $"<color=yellow>Satellite Altitude = {altitude} km";
        }
        if(textMeshProDeltaV!=null)
        {
            RectTransform rectTransform = textMeshProDeltaV.rectTransform;
            rectTransform.anchorMin = new Vector2(1,1); //Top-right corner
            rectTransform.anchorMax = new Vector2(1,1); //Top-right corner
            rectTransform.pivot = new Vector2(1,1); //Top-right pivot

            //Set position relative to Canvas
            rectTransform.anchoredPosition = new Vector2(-10,-10); //adjust as needed

            //Display start text
           
            textMeshProDeltaV.text = $"<color=yellow>Delta V needed to manoeuvre = {deltaV}";
        }
        textMeshProAltitudeWarning.text = $"<color=green> Maintaining 400 km altitude";
    }

    void Update()
    {
        if (Input.GetKey(thrustUpKey))
        {
            Vector3 upwardsDirection = -transform.forward; //direction perpendicular to satellite's orientation
            rb.AddForce(upwardsDirection * thrustForce, ForceMode.Impulse);
            prevAltitude += 0.5f;
            textMeshProAltitude.text = $"<color=yellow>Satellite Altitude = {prevAltitude} km";
            CalculateDeltaV(prevAltitude);
            AltitudeIndicator(prevAltitude);
        }
    }

    // This method is called when the Collider other collides with this GameObject's Collider
    void OnParticleCollision(GameObject other)
    {
        //Debug.Log("collision with main body!");
        // Check if the collided object's tag is "particles"
        if (other.CompareTag("Particles"))
        {
            HandleDrag(other);
        }
    }

    public void HandleDrag(GameObject other)
    {
        //speed = rb.velocity.y;
        //dragForce = 0.5f*density*speed*speed*dragCoeff*area;
        //Debug.Log(dragForce);
        //Debug.Log("Previous Altitude is: " + prevAltitude);
        Vector3 altitudeDirection = transform.forward; // direction perpendicular to satellite's orientation
        rb.AddForce(altitudeDirection*dragForce, ForceMode.Force);
        float newAltitude = prevAltitude - 0.5f;
        //Debug.Log("new altitude is: " + newAltitude);
        textMeshProAltitude.text = $"<color=yellow>Satellite Altitude = {newAltitude} km";
        CalculateDeltaV(newAltitude);
        AltitudeIndicator(newAltitude);
        prevAltitude = newAltitude;
    }

    public void AltitudeIndicator(float currentAltitude)
    {
        if(currentAltitude == 400f){
            textMeshProAltitudeWarning.text = $"<color=green> Original Altitude attained";
        }
        else if (currentAltitude < 395f)
        {
            textMeshProAltitudeWarning.text = $"<color=red> Altitude is too low!";
        }
        else if (currentAltitude > 405f)
        {
            textMeshProAltitudeWarning.text = $"<color=red> Altitude is too high!";
        }
    }

    public void CalculateDeltaV(float newAltitude)
    {
        //Calculating delta V needed
        float radiusEarth = 6371; //km
        float gravitationalParameter = 398000; //km3/s2
        float a = (newAltitude + altitude + (2*radiusEarth))/2;
        float r_p = radiusEarth + newAltitude;
        float r_a = radiusEarth + altitude;
        deltaV = Mathf.Abs(Mathf.Sqrt(((2*gravitationalParameter)/r_p)-(gravitationalParameter/a))-Mathf.Sqrt(gravitationalParameter/r_p)) + Mathf.Abs(Mathf.Sqrt(gravitationalParameter/r_a)-Mathf.Sqrt(((2*gravitationalParameter)/r_a)-(gravitationalParameter/a)));
        textMeshProDeltaV.text = $"<color=yellow>Delta V needed to manoeuvre = {deltaV} km/s";
       
    }
}
