using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILight : MonoBehaviour {
    
    public Image lightImage;
    public float maxLightValue;
    private float currentLightValue = 0;

    private void Start() {
        lightImage.fillAmount = 0f;
    }    

    public void UpdateTexts(float _currentLight) {
        if(currentLightValue != _currentLight) {
            currentLightValue = _currentLight;
            lightImage.fillAmount = currentLightValue / maxLightValue;
        }
    }
}
