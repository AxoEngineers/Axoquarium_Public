using UnityEngine;
using UnityEngine.UI;


public class PettingSliderController : MonoBehaviour
{
    private PettingController _pettingController;
    [SerializeField] public Image radialSliderFirst;
    
    
    public void SetNewSlider(PettingController hitObjectController)
    {
        
        _pettingController = hitObjectController;
       
    }

    private void Update()
    {
        if (_pettingController == null) return;
        if (_pettingController.GetCurrentSlider() == PettingController.CurrentSlider.First)
        {
           
            radialSliderFirst.fillAmount = _pettingController.lerpedValue;
            radialSliderFirst.color = new Color(0.8773585f, 0.8773585f, 0.128293f, 1f);
        }
        else if (_pettingController.GetCurrentSlider() == PettingController.CurrentSlider.Second)
        {
            
            radialSliderFirst.fillAmount = _pettingController.lerpedValue;
            radialSliderFirst.color = new Color(0.1294117f, 0.8862745f, 0.2358871f, 1f);
        }
        else if(_pettingController.GetCurrentSlider()==PettingController.CurrentSlider.None)
            radialSliderFirst.fillAmount = 0f;
       
    }

   


}
