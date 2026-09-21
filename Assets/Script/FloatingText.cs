using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public Text myText;
    public float floatSpeed = 1.5f;
    public float destroyTime = 0.8f;

    void Start()
    {
        // Automatically clean up the instantiated object from memory
        Destroy(transform.parent.gameObject, destroyTime);
    }

    void Update()
    {
        // Move the text gently upwards in space over time
        transform.parent.Translate(Vector3.up * floatSpeed * Time.deltaTime);
    }

    public void Setup(string textValue, Color textColor)
    {
        myText.text = textValue;
        myText.color = textColor;
    }
}
