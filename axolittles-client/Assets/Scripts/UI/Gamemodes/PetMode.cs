using UnityEngine;

public class PetMode : GamemodeBase
{
    [SerializeField] private GameObject pettingProgressSlider;

    private void OnEnable()
    {
        SetCursor();
        pettingProgressSlider.SetActive(true);
    }

    private void OnDisable()
    {
        pettingProgressSlider.SetActive(false);
    }

    void Update()
    {
        MouseInput();
    }
}