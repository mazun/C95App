using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingController : MonoBehaviour
{
    [SerializeField] InorinLive2DBinding inorin;
    [SerializeField] PageController pageController;
    [SerializeField] MessageController messageController;

    PageData currentPage;
    int currentPageMessageIndex = 0;

    float lastMessageChangeTime = 0f;

    private void Awake()
    {
        pageController.OnPageChanged += OnPageChanged;
    }

    private void Update()
    {
        const float autoMessageDuration = 5f;

        if (currentPage == null) return;

        if(lastMessageChangeTime + autoMessageDuration < Time.time)
        {
            if(currentPageMessageIndex == currentPage.descriptions.Length - 1 || currentPage.descriptions.Length == 0)
            {
                pageController.MoveToNextPage();
            }
            else
            {
                currentPageMessageIndex++;
                ShowMessage(currentPage.descriptions[currentPageMessageIndex]);
            }
        }
    }

    public void ShowMessage(string text)
    {
        messageController.PopupMessage(text);
        lastMessageChangeTime = Time.time;
    }

    public void OnPageChanged(PageData page)
    {
        currentPage = page;
        currentPageMessageIndex = 0;
        if (currentPage.descriptions.Length > 0) {
            ShowMessage(currentPage.descriptions[currentPageMessageIndex]);
        }
        else
        {
            lastMessageChangeTime = Time.time;
        }
    }

    public void OnInorinTouch()
    {
        float p = Random.Range(0f, 1f);
        if (p < 1f / 3f)
        {
            inorin.StartGreeting();
            ShowMessage("こんにちは！\nここは東H-14b あいすまぐねっとだよ！");
        }
        else if (p < 2f / 3f)
        {
            inorin.StartWinkPractice();
        }
        else
        {
            inorin.StartWink();
        }
    }
}
