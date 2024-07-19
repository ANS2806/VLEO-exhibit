using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Thruster : MonoBehaviour
{
    public float thrustForce; // force magnitude
    public KeyCode thrusterKey = KeyCode.Space; // key to press to activate thruster
    public KeyCode ThrustUpKey = KeyCode.U; //key to press to thrust upwards
    public TranslateSatellite parentScript; //Reference to the parent script
    private Rigidbody rb; // satellite's Rigidbody component
    public float heightDecreaseAmount = 10f;
    public Image targetImage;
    //Select TextMeshPro
    public TextMeshProUGUI textMeshPro;
    public ParticleSystem desaturationThruster1;
    public ParticleSystem desaturationThruster2;

    public KeyCode desaturateKey = KeyCode.T; //key to press to desaturate the reaction wheels

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKey(ThrustUpKey))
        {
            //Adding a Propulsion Power or Fuel left UI
            if(targetImage!=null)
            {
                // Get the current size of the image
                RectTransform rectTransform = targetImage.GetComponent<RectTransform>();

                // Decrease the height of the image
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - heightDecreaseAmount);
            }
        }
        if (Input.GetKey(thrusterKey))
        {
            //Adding a Propulsion Power or Fuel left UI
            if(targetImage!=null)
            {
                // Get the current size of the image
                RectTransform rectTransform = targetImage.GetComponent<RectTransform>();

                // Decrease the height of the image
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - heightDecreaseAmount);

                if(rectTransform.sizeDelta.y > 0)
                {
                    textMeshPro.text = $"<color=green>Increasing thrust force!";
                    Vector3 thrustDirection = -transform.up; // direction parallel to satellite's orientation
                    
                    rb.AddForce(thrustDirection * thrustForce, ForceMode.Force);
                    
                    Debug.Log("thrust activated!");
                    Vector3 globalVelocity = rb.velocity;
                    //Vector3 localVelocity = transform.InverseTransformDirection(globalVelocity);
            
                    parentScript.DisplaySpeed(globalVelocity.magnitude);
                }
                else
                {
                    textMeshPro.text = $"<color=red>No fuel left!";
                }
            }
        }
        else
        {
            textMeshPro.text = $"<color=blue>No additional thrust force!";
        }
        if(Input.GetKey(desaturateKey))
        {
            desaturationThruster1.Play();
            desaturationThruster2.Play();
            // Get the current size of the image
            RectTransform rectTransform = targetImage.GetComponent<RectTransform>();

            // Decrease the height of the image
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - heightDecreaseAmount);
        }
        else
        {
             desaturationThruster1.Stop();
             desaturationThruster2.Stop();
        }
    }
}
