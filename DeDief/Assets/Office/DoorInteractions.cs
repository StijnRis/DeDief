using System.Collections;
using UnityEngine;

public class DoorInteractions : MonoBehaviour
{
    public bool IsOpen = false;
    [SerializeField]
    private float Speed = 1f;
    [SerializeField]
    private Vector3 SlideDirection = Vector3.back;
    [SerializeField]
    private float SlideAmount = 1.9f;

    private Vector3 StartPosition;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;

    private void Awake()
    {
        Forward = transform.right;
        StartPosition = transform.localPosition;
    }

    public void Open(Vector3 UserPosition)
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

           
           AnimationCoroutine = StartCoroutine(DoSlidingOpen());
            
        }
    }

    private IEnumerator DoSlidingOpen()
    {
        Vector3 endPosition = StartPosition + SlideAmount * SlideDirection;
        Vector3 startPosition = transform.localPosition;

        float time = 0;
        IsOpen = true;
        while (time < 1)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }

    public void Close()
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            
            AnimationCoroutine = StartCoroutine(DoSlidingClose());
            
        }
    }

    private IEnumerator DoSlidingClose()
    {
        Vector3 endPosition = StartPosition;
        Vector3 startPosition = transform.localPosition;
        float time = 0;

        IsOpen = false;

        while (time < 1)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }
}