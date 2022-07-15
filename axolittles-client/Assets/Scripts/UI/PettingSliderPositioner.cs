using UnityEngine;

public class PettingSliderPositioner : MonoBehaviour
{
    
    public Transform slider;
   

    void Update()
    {


        slider.position = Input.mousePosition;
    }
}
