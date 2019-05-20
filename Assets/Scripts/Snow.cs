using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct SnowDropSettings
{
    public float scale;
    public float xDiff;
    public float yDiff;

    public float yBaseSpeed;

    public float baseX;
    public float baseY;

    public float rotateZ;

    public float timeScale;

    public Sprite sprite;
}

[RequireComponent(typeof(RectTransform))]
public class Snow : MonoBehaviour
{
    [SerializeField] Image image;

    public SnowDropSettings Settings
    {
        get;
        set;
    }

    float startTime;

    public void Reset()
    {
        startTime = Time.time;
    }

    private void Start()
    {
        startTime = Time.time;

        RectTransform transform = (RectTransform)this.transform; 
        transform.localScale = Vector3.one * Settings.scale;
        transform.anchoredPosition = new Vector2(Settings.baseX, Settings.baseY);

        image.sprite = Settings.sprite;
    }

    public void Update()
    {
        float elapsed = Time.time - startTime;

        float x = Settings.baseX + Settings.xDiff * Mathf.Sin(elapsed * Settings.timeScale);
        float y = Settings.yBaseSpeed * elapsed + Settings.baseY + Settings.yDiff * Mathf.Cos(elapsed * Settings.timeScale);

        float z = Settings.rotateZ * elapsed * Settings.timeScale;

        RectTransform transform = (RectTransform)this.transform;
        transform.anchoredPosition = new Vector2(x, y);

        transform.eulerAngles = new Vector3(0, 0, z);
    }
}
