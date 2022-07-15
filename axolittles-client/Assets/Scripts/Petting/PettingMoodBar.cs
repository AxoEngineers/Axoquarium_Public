using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PettingMoodBar : MonoBehaviour
{
    public Slider moodIndicatorSlider;
    public Image fill;
    public Gradient gradient;
   

    public void SetMaxMood(float mood)
    {
        moodIndicatorSlider.maxValue = mood;
        moodIndicatorSlider.value = mood;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetMood(float mood)
    {
        moodIndicatorSlider.value = mood;

        fill.color = gradient.Evaluate(moodIndicatorSlider.normalizedValue);
    }

}
