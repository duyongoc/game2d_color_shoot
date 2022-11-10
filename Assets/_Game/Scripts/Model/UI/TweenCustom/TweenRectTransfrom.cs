using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;



public class TweenRectTransfrom : TweenObject
{

    private Vector3 _originPosition;
    private Vector3 _originScale;
    private Quaternion _originRotation;
    private CanvasGroup _canvasGroup;


    public TweenRectTransfrom(Transform transform)
    {
        _canvasGroup = transform.GetComponent<CanvasGroup>();
        _originPosition = transform.AsRectTransform().localPosition;
        _originRotation = transform.AsRectTransform().localRotation;
        _originScale = transform.localScale;
    }


    public override void SetUpScale(CommonTween.TweenScale _scale)
    {
        activeScale = _scale.activeScale;
        scaleFrom = _scale.scaleFrom;
        scaleTo = _scale.scaleTo;
    }

    public override void SetUpRotation(CommonTween.TweenRotation _rotation)
    {
        activeRotation = _rotation.activeRotation;
        rotationFrom = _rotation.rotationFrom;
        rotationTo = _rotation.rotationTo;
    }

    public override void SetUpPosition(CommonTween.TweenPosition _postion)
    {
        activePosition = _postion.activePosition;
        positionFrom = _postion.positionFrom;
        positionTo = _postion.positionTo;
    }



    public override void Play(Transform transform, float duration, int loop, Action callback = null)
    {
        if (activePosition)
        {
            transform.localPosition = positionFrom;
            transform.DOLocalMove(positionTo, duration).SetLoops(loop).Play().OnComplete(() => { callback?.Invoke(); });
        }

        if (activeRotation)
        {
            transform.localRotation = Quaternion.Euler(rotationFrom.x, rotationFrom.y, rotationFrom.z);
            transform.DOLocalRotate(rotationTo, duration).SetLoops(loop).Play().OnComplete(() => { callback?.Invoke(); });
        }

        if (activeScale)
        {
            transform.localScale = scaleFrom;
            transform.DOScale(scaleTo, duration).SetLoops(loop).Play().OnComplete(() => { callback?.Invoke(); });
        }
    }


    public override void PlayScale(Transform transform, float duration, int loop, Action callback = null)
    {
        transform.localScale = scaleFrom;
        transform.DOScale(scaleTo, duration).SetLoops(loop).Play().OnComplete(() => { callback?.Invoke(); });
    }

    public override void PlayRotation(Transform transform, float duration, int loop, Action callback = null)
    {
        transform.localRotation = Quaternion.Euler(rotationFrom.x, rotationFrom.y, rotationFrom.z);
        transform.DOLocalRotate(rotationTo, duration).SetLoops(loop).Play().OnComplete(() => { callback?.Invoke(); });
    }

    public override void PlayPosition(Transform transform, float duration, int loop, Action callback = null)
    {
        transform.AsRectTransform().localPosition = positionFrom;
        transform.AsRectTransform().DOAnchorPos(positionTo, duration).SetLoops(loop).Play().OnComplete(() => { callback?.Invoke(); });
    }

    public override void PlayFading(float duration, int loop, Ease easeType, Action callback = null)
    {
        _canvasGroup.DOFade(1, 0);
        _canvasGroup.DOFade(0, duration).SetLoops(loop).SetEase(easeType).Play().OnComplete(() => { callback?.Invoke(); });
    }
    
    public override void PlayFading(float value, float duration, int loop, Ease easeType, Action callback = null)
    {
        _canvasGroup.DOFade(value, duration).SetLoops(loop).SetEase(easeType).Play().OnComplete(() => { callback?.Invoke(); });
    }

    public override void PlayMoveTo(Transform transform, Vector3 _positionTo, float duration, int loop, Action callback = null)
    {
        transform.AsRectTransform().DOAnchorPos(_positionTo, duration).SetLoops(loop).Play().OnComplete(() => { callback?.Invoke(); });
    }

    public override void PlayRotateTo(Transform transform, Vector3 _rotateTo, float duration, int loop, Action callback = null)
    {
        transform.DOLocalRotate(_rotateTo, duration).SetLoops(loop).Play().OnComplete(() => { callback?.Invoke(); });
    }

    public override void PlayScaleTo(Transform transform, Vector3 _scaleTo, float duration, int loop, Action callback = null)
    {
        transform.DOScale(_scaleTo, duration).SetLoops(loop).Play().OnComplete(() => { callback?.Invoke(); });
    }


    public override void ResetTween(Transform transform)
    {
        base.ResetTween(transform);

        transform.AsRectTransform().localPosition = _originPosition;
        transform.AsRectTransform().localRotation = _originRotation;
        transform.localScale = _originScale;

        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, 0);
        
        // positionFrom = _originPosition;
        // rotationFrom = _originRotation.eulerAngles;
        // scaleFrom = _originScale;
    }

}
