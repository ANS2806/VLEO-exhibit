using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTexture : MonoBehaviour
{
    void Start()
    {
        Texture originalTexture = GetComponent<Renderer>().material.mainTexture;
        Texture2D originalTexture2D = (Texture2D)originalTexture; // Explicit cast
        Texture2D copyTexture = new Texture2D(originalTexture2D.width, originalTexture2D.height);
        copyTexture.SetPixels(originalTexture2D.GetPixels());
        copyTexture.Apply();
        GetComponent<Renderer>().material.mainTexture = copyTexture;
        Debug.Log("done!");
    }
}
