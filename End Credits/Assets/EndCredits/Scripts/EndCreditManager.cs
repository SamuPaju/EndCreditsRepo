using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manage end credits
/// </summary>
public class EndCreditManager : MonoBehaviour
{
    public static EndCreditManager instance;

    [Header("Text modifiers")]
    public int rollSpeed = 5;
    public TMP_FontAsset font;

    [Header("Text templates")]
    [SerializeField] GameObject headlineBase;
    [SerializeField] GameObject groupLineBase;
    [SerializeField] GameObject nameLineBase;

    [Header("Credit texts")]
    [SerializeField] List<string> creditSequence;

    // Special keys
    string[] specialKeysHolder = new string[] { "/h", "/g" };
    //string headlineKey = "/h";
    //string groupLine = "/g";

    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] Transform textParent;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else { instance = this; }
    }

    private void Start()
    {
        for (int i = 0; i < creditSequence.Count; i++)
        {
            creditSequence[i].Trim();

            foreach (string key in specialKeysHolder)
            {
                if (creditSequence[i].Contains(key))
                {
                    // /h scenario
                    if (key == creditSequence[1].ToString())
                    {
                        TextMeshProUGUI text = headlineBase.GetComponent<TextMeshProUGUI>();
                        text.text = creditSequence[i].ToString();
                        CreateLine(headlineBase);
                        StartCoroutine(waitTime(5));
                    }
                    // /g scenario
                    else
                    {
                        TextMeshProUGUI text = groupLineBase.GetComponent<TextMeshProUGUI>();
                        text.text = creditSequence[i].ToString();
                        CreateLine(groupLineBase);
                        StartCoroutine(waitTime(3));
                    }
                }
                // Default scenario
                else
                {
                    TextMeshProUGUI text = nameLineBase.GetComponent<TextMeshProUGUI>();
                    text.text = creditSequence[i].ToString();
                    CreateLine(nameLineBase);
                    StartCoroutine(waitTime(2));
                }
            }
        }
        StartCoroutine(waitTime(2));
    }

    /// <summary>
    /// Instantiate the text object
    /// </summary>
    /// <param name="textObject"></param>
    void CreateLine(GameObject textObject)
    {
        Instantiate(textObject, startPoint.position, textObject.transform.rotation, textParent);
    }

    IEnumerator waitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
