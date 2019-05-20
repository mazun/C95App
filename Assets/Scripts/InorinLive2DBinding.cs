using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InorinLive2DBinding : MonoBehaviour
{
    [SerializeField] Animator animator;

    const string isReadingKey = "IsReading";

    public bool IsReading
    {
        get
        {
            return animator.GetBool(isReadingKey);
        }

        set
        {
            animator.SetBool(isReadingKey, value);
        }
    }

    const string greetKey = "Greet";
    const string winkPracticeKey = "WinkPractice";
    const string winkKey = "Wink";

    public void StartGreeting()
    {
        animator.SetTrigger(greetKey);
    }

    public void StartWinkPractice()
    {
        animator.SetTrigger(winkPracticeKey);
    }

    public void StartWink()
    {
        animator.SetTrigger(winkKey);
    }

    public void RandomAction()
    {
        float p = Random.Range(0f, 1f);
        if (p < 1f / 3f) StartGreeting();
        else if (p < 2f / 3f) StartWinkPractice();
        else StartWink();
    }
}
