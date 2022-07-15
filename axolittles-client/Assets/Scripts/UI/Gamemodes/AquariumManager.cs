using System;
using UnityEngine;

public class AquariumManager : Mingleton<AquariumManager>
{
    private static GamemodesTypes _mode;
    [SerializeField] private GameObject modalWindow;
    [SerializeField] private PetMode petMode;
    [SerializeField] private PlayMode playMode;
    [SerializeField] private EditMode editMode;
    
    public static GamemodesTypes Mode
    {
        get => _mode;
        set => _mode = value;
    }

   

    public void OpenModalWindow(bool state)
    {
        modalWindow.SetActive(state);
    }

    public void LoadChanges(AxoItems[] items)
    {
        AxoItemInfo[] axoItemInfos = FindObjectsOfType<AxoItemInfo>();
        for (int i = 0; i < axoItemInfos.Length; i++)
        {
            for (int j = 0; j < axoItemInfos.Length; j++)
            {
                Debug.Log($"{axoItemInfos[i].Type} =? {items[j].type}");
                if (axoItemInfos[i].Type == items[j].type)
                {
                    axoItemInfos[i].ID = items[j].id;
                    axoItemInfos[i].Type = items[j].type;
                    axoItemInfos[i].Color = items[j].color;
                    axoItemInfos[i].SetNewColor();
                    Debug.Log("SET!");
                    break;
                }
            }
        }
    }

    public void SaveChanges()
    {
        GamemodeSwitcher.MakeChanges(false);
        var itemSize = editMode.EditableObjects.Length;
        AxoItemInfo[] axoItemInfos = new AxoItemInfo[itemSize];
       
            axoItemInfos = FindObjectsOfType<AxoItemInfo>();
        
        AxoItems[] item = new AxoItems[itemSize];
        for (int i = 0; i < itemSize; i++)
        {
            item[i] = new AxoItems()
            {
                id = axoItemInfos[i].ID,
                type = axoItemInfos[i].Type,
                color = axoItemInfos[i].Color
                
            };
            Debug.Log(item[i].type + "is saved");
        }

        StartCoroutine(SaveApiAxoquariumConfig.Instance.SendRequest(item));
        OpenModalWindow(false);
        EnableEditMode();
    }

    public void EnablePlayMode()
    {
        Debug.Log("PlayMode");
        editMode.enabled = false;
        petMode.enabled = false;
        playMode.enabled = true;

        Mode = GamemodesTypes.Play;
    }

    public void EnableEditMode()
    {
        foreach (var colorApplier in editMode.colorAppliers)
        {
            colorApplier.OnEnableEditMode();
        }
      
        if (editMode.enabled)
        {
            Debug.Log("EditMode disable");

            editMode.enabled = false;
            EnablePlayMode();
        }
        else
        {
            Debug.Log("EditMode enable");

            playMode.enabled = false;
            petMode.enabled = false;
            editMode.enabled = true;
            Mode = GamemodesTypes.Build;
        }
    }

    public void EnablePetMode()
    {
        if (petMode.enabled)
        {
            Debug.Log("PetMode disable");
            petMode.enabled = false;
            EnablePlayMode();
        }
        else
        {
            Debug.Log("PetMode enable");
            playMode.enabled = false;
            editMode.enabled = false;
            petMode.enabled = true;
            Mode = GamemodesTypes.Petting;
        }
    }
}