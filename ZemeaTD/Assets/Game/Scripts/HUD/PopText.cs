using UnityEngine;

public class PopText : MonoBehaviour {

    public TextMesh popText;
    public int alphaTime;
    public float offsetXPosition = 5f;
    private float timer;

    private void Start()
    {
        popText = GetComponentInChildren<TextMesh>();
        transform.position = transform.position + new Vector3(Random.Range(-offsetXPosition, offsetXPosition),0,0);
    }

    public void CreateCriticalText(string text)
    {
        CreateText(text, Color.red);
        popText.characterSize *= 1.2f;
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
