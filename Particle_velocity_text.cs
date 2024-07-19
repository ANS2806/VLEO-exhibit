using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayParticleVelocities : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public TextMeshProUGUI textMeshPro;

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
        }
    }

    void Update()
    {
        textMeshPro.text = $"<color=red>Velocity = 20";
    }
}


