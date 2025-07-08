using UnityEngine;
using TMPro;

public class RollUp : MonoBehaviour
{
    RectTransform rectTransform;
    TextMeshProUGUI text;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        rectTransform.Translate(Vector2.up * EndCreditManager.instance.rollSpeed * Time.deltaTime);

        text.font = EndCreditManager.instance.font;
    }
}
