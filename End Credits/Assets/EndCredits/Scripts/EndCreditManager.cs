using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndCreditManager : MonoBehaviour
{
    public static EndCreditManager instance;

    [Header("Text modifiers")]
    public int rollSpeed = 5;
    public TMP_FontAsset font;

    [Header("Text templates")]
    [SerializeField] GameObject headlineBase;
    [SerializeField] GameObject groupLineBase;
    [SerializeField] GameObject nameLine;

    [Header("Credit texts")]
    [SerializeField] List<string> creditSequence;

    [SerializeField] GameObject startPoint;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance);
        }
        else { instance = this; }
    }

    private void Update()
    {
        
    }
}
