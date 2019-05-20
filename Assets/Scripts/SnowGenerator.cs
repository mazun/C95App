using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RandomSnowDropSettings
{
    public float minScale;
    public float maxScale;

    public float minXdiff;
    public float maxXdiff;


    public float minYDiff;
    public float maxYDiff;

    public float minYBaseSpeed;
    public float maxYBaseSpeed;

    public float minRotateZ;
    public float maxRotateZ;

    public float minTimeScale;
    public float maxTimeScale;

    public int count;

    public Sprite[] sprites;

    public SnowDropSettings GetSnowDropSettings(float baseX, float baseY)
    {
        return new SnowDropSettings
        {
            scale = Random.Range(minScale, maxScale),
            xDiff = Random.Range(minXdiff, maxXdiff),
            yDiff = Random.Range(minYDiff, maxYDiff),
            yBaseSpeed = -Random.Range(minYBaseSpeed, maxYBaseSpeed),
            baseX = baseX,
            baseY = baseY,
            rotateZ = Random.Range(minRotateZ, maxRotateZ),
            timeScale = Random.Range(minTimeScale, maxTimeScale),
            sprite = sprites[Random.Range(0, sprites.Length)]
        };
    }
}

public class SnowGenerator : MonoBehaviour
{
    [SerializeField] RandomSnowDropSettings settings;
    [SerializeField] RectTransform bounds;

    [SerializeField] Snow snowPrefab;

    List<Snow> snows = new List<Snow>();

    Snow CreateRandomSnow()
    {
        Snow snow = Instantiate(snowPrefab);

        Vector3[] corners = new Vector3[4];
        bounds.GetLocalCorners(corners);

        float topY = corners[2].y;
        float minX = corners[0].x;
        float maxX = corners[2].x;

        snow.transform.SetParent(bounds);
        RectTransform rect = (RectTransform)snow.transform;
        rect.pivot = rect.anchorMin = rect.anchorMax = Vector2.one * 0.5f;
        snow.transform.localScale = Vector3.one;
        snow.Settings = settings.GetSnowDropSettings(UnityEngine.Random.Range(minX, maxX), topY + 100);

        return snow;
    }

    IEnumerator Start()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.2f); // TODO

            Vector3[] corners = new Vector3[4];
            bounds.GetLocalCorners(corners);

            float bottomY = corners[0].y;

            List<int> removes = new List<int>();
            for(int i = 0; i < snows.Count; i++)
            {
                Snow snow = snows[i];
                if(snow.transform.localPosition.y < bottomY - 100)
                {
                    removes.Add(i);
                }
            }

            removes.Reverse();
            foreach(int i in removes)
            {
                Snow snow = snows[i];
                Destroy(snow.gameObject);
                snows.RemoveAt(i);
            }

            if(snows.Count < settings.count)
            {
                snows.Add(CreateRandomSnow());
            }
        }
    }
}
