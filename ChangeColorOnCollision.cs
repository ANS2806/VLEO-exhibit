using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnCollision : MonoBehaviour
{
    public Camera cam;
    public Texture2D tex;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (!Input.GetMouseButton(0))
            return;

        RaycastHit hit;
        if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            return;

        Renderer rend = hit.transform.GetComponent<Renderer>();
        MeshCollider meshCollider = hit.collider as MeshCollider;
        Debug.Log(hit.transform.gameObject.name);
       

        if (rend == null || rend.sharedMaterial == null || rend.sharedMaterial.mainTexture == null || meshCollider == null)
        {
            Debug.Log("nope");
            return;
        }

        tex = rend.material.mainTexture as Texture2D;
        Debug.Log(tex.format);
        Vector2 pixelUV = hit.textureCoord;
        
        for (int i = -10; i <= 10; i++)
        {
            for (int j = -10; j <= 10; j++)
            {
                int x = (int)(pixelUV.x * tex.width) + i;
                int y = (int)(pixelUV.y * tex.height) + j;

                // Make sure we're not trying to set pixels outside the texture
                if (x >= 0 && x < tex.width && y >= 0 && y < tex.height)
                {
                    tex.SetPixel(x, y, Color.red);
                    //Debug.Log("yes");
                }
            }
        }
        tex.Apply();
        
        //Debug.Log("hello");
    }
}
