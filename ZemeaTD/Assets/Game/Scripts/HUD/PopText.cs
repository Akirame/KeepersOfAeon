using UnityEngine;

public class PopText : MonoBehaviour {

    public TextMesh popText;
    public int alphaTime;
    private Vector3 finalPos;
    private float timer;
    private TextMesh text;
    private float alphaDecrementer = 1;

    private void Start()
    {
        popText = GetComponent<TextMesh>();
        text = GetComponentInChildren<TextMesh>();
        finalPos.x = Random.Range(-5, 5);
        finalPos.y = 10;
        finalPos += transform.position;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, finalPos, 1);
        timer += Time.deltaTime;
        if (timer > alphaTime)
        {
            alphaDecrementer -= Time.deltaTime;
            text.color = new Color(1, 1, 1, alphaDecrementer);
            transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime;
        }
        if (text.color.a <= 0.1)
        {
            DestroyText();
        }
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
