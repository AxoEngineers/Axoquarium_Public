using System.ComponentModel;
using UnityEngine;

public class ApplyColor : MonoBehaviour
{
    public MeshRenderer objectMaterial;
    public MeshRenderer additionalMaterial;
    private bool _isPickerOpened;
    private Color _oldColor;
    private Color _oldAdditionalMaterial;
    private Texture _objectTexture;
    public void OnEnableEditMode()
    {
      
        _oldColor = objectMaterial.material.color;
        if (additionalMaterial == null) return;
        _oldAdditionalMaterial = additionalMaterial.material.color;
        _objectTexture = additionalMaterial.material.mainTexture;
    }
    
   

    public void SetColor(string color)
    {
        
        GamemodeSwitcher.MakeChanges(true);
        objectMaterial.material.color = AxoItemColors.GetColor(color);
        transform.parent.parent.GetComponent<AxoItemInfo>().SetColorName(color);
        _oldColor = objectMaterial.material.color;
        if (additionalMaterial == null) return;
           
        
        additionalMaterial.material.color = AxoItemColors.GetColor(color);
        additionalMaterial.material.mainTexture = null;
        _oldAdditionalMaterial = additionalMaterial.material.color;
        
        
    }
    public void DiscardChanges()
    {
        objectMaterial.material.color = _oldColor;
        
        if (additionalMaterial == null) {return;}
        additionalMaterial.material.color = _oldAdditionalMaterial;
        additionalMaterial.material.mainTexture = _objectTexture;
    }
}