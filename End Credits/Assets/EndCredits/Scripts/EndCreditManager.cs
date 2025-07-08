using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndCreditManager : MonoBehaviour
{
    public static EndCreditManager instance;

    public int rollSpeed = 5;
    public TMP_FontAsset font;

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
