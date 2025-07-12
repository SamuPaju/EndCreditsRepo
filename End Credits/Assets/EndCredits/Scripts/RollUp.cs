using UnityEngine;
using TMPro;

public class RollUp : MonoBehaviour
{
    RectTransform rectTransform;
    TextMeshProUGUI text;
    [SerializeField] bool lastLine;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        rectTransform.Translate(Vector2.up * EndCreditManager.instance.rollSpeed * Time.deltaTime);

        text.font = EndCreditManager.instance.font;

        if (rectTransform.position.y >= EndCreditManager.instance.endPoint.position.y)
        {
            Destroy(gameObject);
        }

        if (lastLine && rectTransform.position.y >= EndCreditManager.instance.middlePoint.position.y)
        {
            StartCoroutine(EndCreditManager.instance.StopCredits());
        }
    }
}
