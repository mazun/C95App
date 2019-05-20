using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class PageController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] List<PageData> data;
    [SerializeField] Page pagePrefab;

    [SerializeField] RectTransform container;

    List<Page> pages = new List<Page>();

    int currentIndex = -1;
    public int CurrentIndex
    {
        get
        {
            return currentIndex;
        }
        private set
        {
            if(currentIndex != value)
            {
                if(OnPageChanged != null)
                {
                    OnPageChanged(data[value]);
                }
            }
            currentIndex = value;
        }
    }

    RectTransform trans
    {
        get
        {
            return (RectTransform)transform;
        }
    }

    Vector2 viewSize
    {
        get
        {
            Vector3[] corners = new Vector3[4];
            trans.GetLocalCorners(corners);
            return corners[2] - corners[0];
        }
    }

    public event Action<PageData> OnPageChanged;

    private void Start()
    {
        Init();
    }

    void OnRectTransformDimensionsChange()
    {
        UpdateLayout();
    }

    void Init ()
    {
        foreach(var page in pages)
        {
            Destroy(page.gameObject);
        }

        pages.Clear();

        foreach(var d in data)
        {
            var page = Instantiate(pagePrefab);
            page.transform.SetParent(container);
            page.transform.localScale = Vector3.one;
            page.InitWith(d);
            pages.Add(page);
        }

        UpdateLayout();

        // ククク、ページ数0なんて考慮してないぜ…
        CurrentIndex = 0;
    }

    [ContextMenu("Next")]
    public void MoveToNextPage()
    {
        int nextPage = (CurrentIndex + 1) % pages.Count;
        MoveToPage(nextPage);
    }

    [ContextMenu("Prev")]
    public void MoveToPrevPage()
    {
        int nextPage = (CurrentIndex - 1 + pages.Count) % pages.Count;
        MoveToPage(nextPage);
    }


    void MoveToPage(int page)
    {
        CurrentIndex = page;
        container.DOLocalMoveX(-page * viewSize.x, 0.2f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var pos = eventData.position;
        var world = Camera.main.ScreenToWorldPoint(pos);
        var local = trans.InverseTransformPoint(world);
        // Debug.Log(local);
        if (local.x > viewSize.x / 2) MoveToNextPage();
        else MoveToPrevPage();
    }

    void UpdateLayout()
    {
        var size = viewSize;
        float accX = 0;
        foreach (var page in pages)
        {
            RectTransform pageTrans = (RectTransform)page.transform;

            float height = size.y;
            var imageSize = page.Data.image.bounds.size;
            float width = height * imageSize.x / imageSize.y;

            if (width > size.x)
            {
                width = size.x;
                height = width * imageSize.y / imageSize.x;
            }

            pageTrans.sizeDelta = new Vector2(
                width,
                height
            );

            float dx = (size.x - width) / 2;
            float dy = (size.y - height) / 2;

            pageTrans.anchoredPosition = new Vector2(
                accX + dx,
                -dy
            );

            accX += size.x;
        }

        container.anchoredPosition = new Vector2(
            -CurrentIndex * viewSize.x,
            0f
        );
    }
}
