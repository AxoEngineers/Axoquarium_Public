using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class GamomodeBase : MonoBehaviour
{
    [SerializeField] protected Texture2D cursorImage;

    protected void SetCursor(Texture2D image)
    {
       Cursor.SetCursor(image, Vector2.zero, CursorMode.Auto);
    }


    protected void MouseInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log($"{hit.collider.name} - is selected");
        }
        Debug.DrawRay(ray.origin,ray.direction*20f);

        Interact(hit);
    }

    public abstract void Interact(RaycastHit hit);
}

