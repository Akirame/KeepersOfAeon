using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILight : MonoBehaviour {
    
    public Text lightText;
    public Slider lightSlider;
    private int currentLightValue = 0;

    private void Start() {
        lightText.text = currentLightValue + "%";
        lightSlider.value = 0f;
    }    
    public void UpdateTexts(int _currentLight) {
        if(currentLightValue != _currentLight) {
            currentLightValue = _currentLight;
            lightText.text = currentLightValue + "%";
            lightSlider.value = currentLightValue * 0.1f;
        }
    }
}
