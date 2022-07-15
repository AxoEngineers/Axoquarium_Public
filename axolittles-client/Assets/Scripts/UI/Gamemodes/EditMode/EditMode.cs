using System.Collections.Generic;
using System.Linq;
using System.Security;
using UnityEngine;

public class EditMode : GamemodeBase
{
    public GameObject[] EditableObjects;
    public GameObject[] colorPalettes;
    public List<ApplyColor> colorAppliers;
    public GameObject EditButton;
    
    private void Start()
    {
        
        EditableObjects = GameObject.FindGameObjectsWithTag("EditTag");
        
        colorPalettes = GameObject.FindGameObjectsWithTag("Palette");
        for (int i = 0; i < colorPalettes.Length; i++)
        {
            colorAppliers.Add(colorPalettes[i].GetComponent<ApplyColor>()); 
            colorAppliers[i].gameObject.SetActive(false);
        }
        OnDisable();
        GetComponent<EditMode>().enabled = false;
    }

    private void OnEnable()
    {
        SetCursor();
        if (EditableObjects == null) return;
        
        foreach (GameObject canvas in EditableObjects)
        {
            canvas.SetActive(true);
        }
    }

    private void OnDisable()
    {
        if (EditableObjects == null) return;
        foreach (var canvas in EditableObjects)
        {
            canvas.SetActive(false);
        }
    }

    
    public void Update()
    {
        MouseInput();
    }


    public void DiscardChanges()
    {
        foreach (var palette in colorAppliers)
        {
            palette.DiscardChanges();
        }
        GamemodeSwitcher.MakeChanges(false);
        GetComponent<AquariumManager>().OpenModalWindow(false);
        GetComponent<AquariumManager>().EnableEditMode();
    }

    public void EnableOnly(GameObject currentCanvas) // Leaves only one button turned on
    {
        foreach (var canvas in EditableObjects)
        {
            if (canvas != currentCanvas)
                canvas.GetComponent<EditFuniture>().SwitchActiveness();
        }
    }
    
}