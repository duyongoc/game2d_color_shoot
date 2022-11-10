using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public class TweenScale : MonoBehaviour
{

    public float ScaleFrom;
    public float ScaleTo;
    public float Duration;
    public bool PlayOnAwake;
    public bool PingPong;

    [Tooltip("Set as -1 to loop forever")]
    public int LoopTimes;
    public float DelayTime;
    public Ease Ease = Ease.OutQuad;

    void Start()
    {
        if (PlayOnAwake)
            Play();
    }

    public void Play(Action callback = null)
    {
        Stop();
        transform.localScale = new Vector3(ScaleFrom, ScaleFrom, ScaleFrom);
        
        if (PingPong)
        {
            transform.DOScale(ScaleTo, Duration).SetDelay(DelayTime).SetLoops(LoopTimes, LoopType.Yoyo)
                .SetEase(Ease).OnStart(
                    () => transform.gameObject.SetActive(true))
                    .OnComplete(() => { callback?.Invoke(); });
        }
        else
        {
            transform.DOScale(ScaleTo, Duration).SetDelay(DelayTime).SetLoops(LoopTimes)
                .OnStart(() => transform.gameObject.SetActive(true))
                .OnComplete(() => { callback?.Invoke(); });
        }
    }
    
    public void PlayReverse(Action callback = null)
    {
        Stop();
        transform.localScale = new Vector3(ScaleTo, ScaleTo, ScaleTo);
        
        if (PingPong)
        {
            transform.DOScale(ScaleFrom, Duration).SetDelay(DelayTime).SetLoops(LoopTimes, LoopType.Yoyo)
                .SetEase(Ease).OnStart(
                    () => transform.gameObject.SetActive(true))
                    .OnComplete(() => { callback?.Invoke(); });
        }
        else
        {
            transform.DOScale(ScaleFrom, Duration).SetDelay(DelayTime).SetLoops(LoopTimes)
                .OnStart(() => transform.gameObject.SetActive(true))
                .OnComplete(() => { callback?.Invoke(); });
        }
    }

    public void Play(float delay, Action callback)
    {
        StartCoroutine(PlayWithDelayCallback(delay, callback));
    }

    public void Stop()
    {
        if (DOTween.IsTweening(transform))
            DOTween.Kill(transform);
        transform.localScale = Vector3.one;
    }

    IEnumerator PlayWithDelayCallback(float delay, Action callback)
    {
        Stop();
        transform.localScale = new Vector3(ScaleFrom, ScaleFrom, ScaleFrom);

        if (PingPong)
        {
            transform.DOScale(ScaleTo, Duration).SetDelay(DelayTime).SetLoops(LoopTimes, LoopType.Yoyo)
                .SetEase(Ease).OnStart(() => transform.gameObject.SetActive(true));
        }
        else
        {
            transform.DOScale(ScaleTo, Duration).SetDelay(DelayTime).SetLoops(LoopTimes)
                .OnStart(() => transform.gameObject.SetActive(true));
        }

        delay += Duration;
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }
}
