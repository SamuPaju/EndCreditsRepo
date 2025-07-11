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
    string headlineKey = "/h";
    string groupLineKey = "/g";

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
        StartCoroutine(PlayCredits());
    }

    /// <summary>
    /// Instantiate the text object
    /// </summary>
    /// <param name="textObject">Formatted text object</param>
    void CreateLine(GameObject textObject)
    {
        Instantiate(textObject, startPoint.position, textObject.transform.rotation, textParent);
    }

    /// <summary>
    /// Plays credits in order and with pauses
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayCredits()
    {
        for (int i = 0; i < creditSequence.Count; i++)
        {
            string line = creditSequence[i].Trim();

            // /h scenario
            if (line.StartsWith(headlineKey))
            {
                StyleText(headlineBase, line, headlineKey);
                yield return new WaitForSeconds(5);
            }
            // /g scenario
            else if (line.StartsWith(groupLineKey))
            {
                StyleText(groupLineBase, line, groupLineKey);
                yield return new WaitForSeconds(2f);
            }
            // Default scenario
            else
            {
                StyleText(nameLineBase, line);
                yield return new WaitForSeconds(1f);
            }
        }        
    }

    /// <summary>
    /// Style and create text
    /// </summary>
    /// <param name="textTemplate">The text GameObject</param>
    /// <param name="lineText">The current line in credits</param>
    /// <param name="specialKey">Special key for detecting special lines. 
    /// Default value is for basic nameLine</param>
    void StyleText(GameObject textTemplate, string lineText, string specialKey = "")
    {
        TextMeshProUGUI text;
        text = textTemplate.GetComponent<TextMeshProUGUI>();

        if (specialKey == "") { text.text = lineText.ToString(); }
        else
        {
            text.text = lineText.Remove(0, specialKey.Length).Trim().ToString();
        }
        CreateLine(textTemplate);
    }
}
