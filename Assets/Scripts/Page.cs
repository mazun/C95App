using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PageData
{
    [TextArea(1, 10)]
    public string[] descriptions;
    public Sprite image;
}

public class Page : MonoBehaviour
{
    [SerializeField] Image image;

    public PageData Data
    {
        get;
        private set;
    }

    public void InitWith(PageData data)
    {
        Data = data;
        image.sprite = data.image;
    }
}
