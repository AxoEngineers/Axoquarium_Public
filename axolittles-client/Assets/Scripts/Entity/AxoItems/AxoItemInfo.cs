using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxoItemInfo : MonoBehaviour
{
    public string ID;
    public string Type;
    public string Color;
    
    private void Start()
    {
        Type = gameObject.name;
        
    }

    public void SetColorName(string colorName)
    {
        Color = colorName;
    }

    public void SetNewColor()
    {
        if (Color == "default") return;
        GetComponent<MeshRenderer>().material.color = AxoItemColors.GetColor(Color);
        Debug.Log( transform.GetChild(0).name);
        if (transform.GetChild(0).TryGetComponent(out MeshRenderer renderer))
        {
            renderer.material.mainTexture = null;
            renderer.material.color = AxoItemColors.GetColor(Color);
        }
    }

    
}
