using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Manage end credits
/// </summary>
public class EndCreditManager : MonoBehaviour
{
    public static EndCreditManager instance;

    [Header("Text modifiers")]
    public float rollSpeed = 5;
    public TMP_FontAsset font;
    
    [Header("Credit texts")]
    [SerializeField, 
        Tooltip("Put here all the names. " +
        "If you have a headline put in front of it /h. " +
        "If you have a group or job title put in front of it /g. " +
        "Put nothing in front of the text to have just a name. " +
        "You can also use basic string changes like (forward slash)t. " +
        "Make sure you put /l in front of the last line to stop the credits.")] 
    List<string> creditSequence;
    
    [Header("Scene management")]
    [SerializeField, 
        Tooltip("Put here a scenes scene number that you want to go after this scene")] int wantedScene;

    [Space(20), Header("DO NOT TOUCH!")]

    [Header("Text templates")]
    [SerializeField] GameObject headlineBase;
    [SerializeField] GameObject groupLineBase;
    [SerializeField] GameObject nameLineBase;
    [SerializeField] GameObject lastLineBase;

    // Special keys
    string headlineKey = "/h";
    string groupLineKey = "/g";
    string lastLineKey = "/l";

    [Header("Required objects")]
    [SerializeField] Transform startPoint;
    public Transform middlePoint;
    public Transform endPoint;
    [SerializeField] Transform canvas;

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
        Instantiate(textObject, startPoint.position, textObject.transform.rotation, canvas);
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
                HandleLine(headlineBase, line, headlineKey);
                yield return new WaitForSeconds(5);
            }
            // /g scenario
            else if (line.StartsWith(groupLineKey))
            {
                HandleLine(groupLineBase, line, groupLineKey);
                yield return new WaitForSeconds(2f);
            }
            // /l scenario
            else if (line.StartsWith(lastLineKey))
            {
                yield return new WaitForSeconds(7f);
                HandleLine(lastLineBase, line, lastLineKey);
                yield return new WaitForSeconds(2f);
            }
            // Default scenario
            else
            {
                HandleLine(nameLineBase, line);
                yield return new WaitForSeconds(1f);
            }
        }        
    }

    /// <summary>
    /// Style and create text line
    /// </summary>
    /// <param name="textTemplate">The text GameObject</param>
    /// <param name="lineText">The current line in credits</param>
    /// <param name="specialKey">Special key for detecting special lines. 
    /// Default value is for basic nameLine</param>
    void HandleLine(GameObject textTemplate, string lineText, string specialKey = "")
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

    /// <summary>
    /// Stops credits from rolling and sends back to wanted scene
    /// </summary>
    /// <returns></returns>
    public IEnumerator StopCredits()
    {
        rollSpeed = 0f;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(wantedScene);        
    }
}
