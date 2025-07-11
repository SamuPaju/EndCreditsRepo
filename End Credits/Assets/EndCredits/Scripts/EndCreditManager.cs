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
    /// <param name="textObject"></param>
    void CreateLine(GameObject textObject)
    {
        Instantiate(textObject, startPoint.position, textObject.transform.rotation, textParent);
    }

    /// <summary>
    /// Plays credits over time
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
                TextMeshProUGUI text = headlineBase.GetComponent<TextMeshProUGUI>();
                text.text = line.Remove(0, headlineKey.Length).ToString();
                CreateLine(headlineBase);
                yield return new WaitForSeconds(5);
            }
            // /g scenario
            else if (line.StartsWith(groupLineKey))
            {
                TextMeshProUGUI text = groupLineBase.GetComponent<TextMeshProUGUI>();
                text.text = line.Remove(0, groupLineKey.Length).ToString();
                CreateLine(groupLineBase);
                yield return new WaitForSeconds(2f);
            }
            // Default scenario
            else
            {
                TextMeshProUGUI text = nameLineBase.GetComponent<TextMeshProUGUI>();
                text.text = line.ToString();
                CreateLine(nameLineBase);
                yield return new WaitForSeconds(1f);
            }
        }        
    }
}
