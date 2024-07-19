using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed; // rotation speed
    public KeyCode resetKey = KeyCode.R; // key to press to reset rotation
    public float saturationTime = 2f; // time in seconds to display "Reaction wheels saturated"

    private Quaternion targetRotation = Quaternion.Euler(90f, 0f, 0f); // target rotation

    // Variables for key press tracking
    private int resetKeyPressCount = 0;
    private float resetKeyPressDuration = 0f;
    private bool resetKeyHeld = false;

    //Select TextMeshPro
    public TextMeshProUGUI textMeshPro;

    //Variables for desaturating reaction wheels
    public KeyCode desaturateKey = KeyCode.T; //key to press to desaturate the reaction wheels
    public float desaturateTime = 5f; //time it takes to desaturate the reaction wheels
    private float desaturateKeyPressDuration = 0f;

    void Start()
    {
        if(textMeshPro!=null)
        {
            RectTransform rectTransform = textMeshPro.rectTransform;
            rectTransform.anchorMin = new Vector2(1,1); //Top-right corner
            rectTransform.anchorMax = new Vector2(1,1); //Top-right corner
            rectTransform.pivot = new Vector2(1,1); //Top-right pivot

            //Set position relative to Canvas
            rectTransform.anchoredPosition = new Vector2(10,-10); //adjust as needed

            //Display start text
            textMeshPro.text = $"<color=red>ADCS Turned off";
        }
    }
    void Update()
    {
        if (Input.GetKey(resetKey))
        {
            if (!resetKeyHeld){
                resetKeyPressCount++;
                resetKeyHeld = true;
                //resetKeyPressDuration = 0f; // Reset duration on each new press
                //Debug.Log("ADCS Press Count: " + resetKeyPressCount);
            }

            if(resetKeyPressCount < 10 && resetKeyPressDuration < saturationTime)
            {
            //Debug.Log("turning!");
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            textMeshPro.text = $"<color=green>ADCS Turned on";
            }
        }
        else
        {
            resetKeyHeld = false;
            textMeshPro.text = $"<color=red>ADCS Turned off";
        }
        // Track the duration the reset key is held
        if (resetKeyHeld)
        {
            resetKeyPressDuration += Time.deltaTime;
            if (resetKeyPressDuration > saturationTime || resetKeyPressCount > 5)
            {
                textMeshPro.text = $"<color=white>Reaction wheels saturated /n Press T to desaturate";
            }
        }
        if(Input.GetKey(desaturateKey))
        {
            //desaturateKeyPressDuration = 0f;
            desaturateKeyPressDuration += Time.deltaTime;
            transform.rotation = Quaternion.Euler(90f, 0f, 0f); // Set rotation to (90, 0, 0)
            textMeshPro.text = $"<color=white>Desaturating...Thrusters active";
            //Debug.Log(desaturateKeyPressDuration);
            if (desaturateKeyPressDuration > desaturateTime)
            {
                textMeshPro.text = $"<color=white> Reaction wheels desaturated!";
                resetKeyPressCount = 0;
                resetKeyHeld = false;
                resetKeyPressDuration = 0f; // Reset desaturation duration   
            }
        }
        else
        {
            desaturateKeyPressDuration = 0f; // Reset duration if key is not held
        }
    }
}
