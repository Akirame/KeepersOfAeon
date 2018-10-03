using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopText : MonoBehaviour {

    public TextMesh popText;

    private void Start()
    {
        popText = GetComponent<TextMesh>();
    }

    public void CreateText(string text, Color colorText)
    {
        popText.text = text;
        popText.color = colorText;
    }

    public void DestroyText()
    {
        Destroy(this.gameObject);
    }

}
