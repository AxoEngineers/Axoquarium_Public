using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    [SerializeField] private Texture2D defaultCursor;
    [SerializeField] private Texture2D clickCursor;

    public void SetDefaultCursor()
    {
        if (defaultCursor == null) return;
        Cursor.SetCursor(defaultCursor, new Vector2(13, 11), CursorMode.Auto);
    }

    public void SetCursorClick()
    {
        if (clickCursor == null) return;
        Cursor.SetCursor(clickCursor, new Vector2(25, 7), CursorMode.Auto);
    }
}