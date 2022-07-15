using UnityEngine;


public class EditFuniture : MonoBehaviour
{
    [SerializeField] private GameObject paletteWindow;
    private EditMode _editMode;
    private Canvas _canvas;
    public bool IsActive { get; private set; }


    private void Start()
    {
        _editMode = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EditMode>();
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.main;
    }

    public void ShowPalette()
    {
        IsActive = !IsActive;
        _editMode.EnableOnly(gameObject);
        paletteWindow.SetActive(IsActive);
    }

    public void SwitchActiveness()
    {
        IsActive = false;
        paletteWindow.SetActive(IsActive);
    }
}