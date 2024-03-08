using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueDisplay : MonoBehaviour
{
    private TextMeshProUGUI valueDisplay;
    private Slider slider;

    public bool valueIsFloat = true;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        valueDisplay = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    void Update()
    {
        valueDisplay.text = valueIsFloat? slider.value.ToString("0.00") : slider.value.ToString();
    }
}
