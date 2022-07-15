using System.ComponentModel;
using UnityEngine;


public abstract class GamemodeBase : MonoBehaviour
{
    [SerializeField] private Texture2D cursorImage;
    protected RaycastHit Hit;

    protected void SetCursor()
    {
        if (cursorImage == null)

            throw new WarningException($"Cursor image of {this} was not initialized");


        Cursor.SetCursor(cursorImage, new Vector2(13, 11), CursorMode.ForceSoftware);
    }
    

    protected void MouseInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

       
        if (Physics.Raycast(ray, out Hit))
        {
            // Debug.Log($"{hit.collider.name} - is selected");
        }


        Debug.DrawRay(ray.origin, ray.direction * 20f);

        
    }


 
}