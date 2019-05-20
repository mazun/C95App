using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MessageController : MonoBehaviour
{
    [SerializeField] MessageBox messageBoxPrefab;
    [SerializeField] RectTransform container;

    MessageBox currentMessage;

    public void PopupMessage(string text)
    {
        if(currentMessage != null)
        {
            var message = currentMessage;
            RemoveAnimation(message)
                .OnComplete(() => Destroy(message.gameObject));
        }

        currentMessage = Instantiate(messageBoxPrefab);
        currentMessage.SetText(text);
        var trans = currentMessage.transform as RectTransform;
        trans.SetParent(container);
        trans.localScale = Vector3.one;
        trans.anchoredPosition = Vector2.zero;
        trans.sizeDelta = Vector2.zero;
        AddAnimation(currentMessage);
    }

    Tween AddAnimation(MessageBox box)
    {
        const float delay = 0.2f;
        const float duration = 0.5f;
        var trans = box.transform as RectTransform;
        box.CanvasGroup.alpha = 0f;
        trans.anchoredPosition = new Vector3(0f, -20f, 0f);
        return DOTween.Sequence()
            .Insert(delay, box.CanvasGroup.DOFade(1f, duration))
            .Insert(delay, trans.DOAnchorPosY(0, duration));
    }

    Tween RemoveAnimation(MessageBox box)
    {
        const float duration = 0.5f;
        var trans = box.transform as RectTransform;
        return DOTween.Sequence()
            .Insert(0f, box.CanvasGroup.DOFade(0f, duration))
            .Insert(0f, trans.DOAnchorPosY(20f, duration));
    }
}
