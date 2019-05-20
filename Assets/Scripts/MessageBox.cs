using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] CanvasGroup canvasGroup;

    public CanvasGroup CanvasGroup
    {
        get
        {
            return canvasGroup;
        }
    }


    public void SetText(string s)
    {
        text.text = s;
    }
}
