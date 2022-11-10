using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CodeStringers.Utils
{
    public class TweenRotation : MonoBehaviour
    {
        public enum ScaleValue
        {
            None,
            X,
            Y,
            Z
        }

        public ScaleValue ScaleValueEnum;
        public float ScaleFrom;
        public float ScaleTo;
        public float Delay;
        public float Duration;

        [Tooltip("Set as -1 to loop forever")]
        public int LoopTimes;

        public Ease ease = Ease.Unset;
        public Vector3 From;
        public Vector3 To;
        public List<Vector3> Sequences;
        public bool PingPong;
        public bool PlayOnAwake;
        public bool IsLocalTransfrom;

        [HideInInspector]
        public float Multiple = 1;
        public UnityEngine.Events.UnityEvent OnFinished;
        public UnityEngine.Events.UnityEvent OnStart;
        private Vector3 _originalPos;

        
        void Start()
        {
            Sequences = new List<Vector3>();
            if (IsLocalTransfrom)
                _originalPos = transform.localPosition;
            else
                _originalPos = transform.position;
            if (PlayOnAwake)
            {
                // if(Delay <= 0)
                Play();
                // else 
                //     Play(true);
            }
        }

        /*public void Play(bool isDelayed) {
            var sq = DOTween.Sequence();
            //var loopType = PingPong ? LoopType.Yoyo : LoopType.Restart;
            sq.SetDelay(Delay);
            if (IsLocalTransfrom) {
                transform.localPosition = From;
                sq.Append(transform.DOLocalMove(To, Duration));
            }
            else {
                transform.position = From;
                sq.Append(transform.DOMove(To, Duration));
            }

            if (ScaleValueEnum == ScaleValue.X) {
                transform.localScale = new Vector3(ScaleFrom, 1, 1);
                sq.Join(transform.DOScaleX(ScaleTo, Duration));
            }

            if (ScaleValueEnum == ScaleValue.Y) {
                transform.localScale = new Vector3(1, ScaleFrom, 1);
                sq.Join(transform.DOScaleY(ScaleTo, Duration));
            }

            if (ScaleValueEnum == ScaleValue.Z) {
                transform.localScale = new Vector3(1, 1, ScaleFrom);
                sq.Join(transform.DOScaleZ(ScaleTo, Duration));
            }

            if (PingPong) {
                if (IsLocalTransfrom)
                    sq.Append(transform.DOLocalMove(From, Duration));
                else 
                    sq.Append(transform.DOMove(From, Duration));
                if (ScaleValueEnum == ScaleValue.X)
                    sq.Join(transform.DOScaleX(ScaleFrom, Duration));
                if (ScaleValueEnum == ScaleValue.Y)
                    sq.Join(transform.DOScaleY(ScaleFrom, Duration));
                if (ScaleValueEnum == ScaleValue.Z)
                    sq.Join(transform.DOScaleZ(ScaleFrom, Duration));
            }
            sq.SetLoops(LoopTimes);

        }*/

        public void Play()
        {
            var loopType = PingPong ? LoopType.Yoyo : LoopType.Restart;
            if (Sequences == null)
            {
                Sequences = new List<Vector3>();
            }

            if (Sequences.Count > 0)
            {
                var sq = DOTween.Sequence();
                if (IsLocalTransfrom)
                {
                    foreach (var pos in Sequences)
                        sq.Append(transform.AsRectTransform().DOAnchorPos(pos * Multiple, Duration));

                    sq.OnStart(() =>
                    {
                        OnStart?.Invoke();
                    });
                    sq.Play();
                    sq.OnComplete(() =>
                    {
                        OnFinished?.Invoke();
                        transform.localPosition = _originalPos;
                    });
                }
                else
                {
                    foreach (var pos in Sequences)
                        sq.Append(transform.DOMove(pos * Multiple, Duration));

                    sq.OnStart(() =>
                    {
                        OnStart?.Invoke();
                    });
                    sq.Play();
                    sq.OnComplete(() =>
                    {
                        OnFinished?.Invoke();
                        transform.position = _originalPos;
                    });
                }
            }
            else
            {
                if (IsLocalTransfrom)
                {
                    //transform.localRotation = From;
                    transform.DORotate(To, Duration).SetDelay(Delay).SetLoops(LoopTimes, loopType).SetEase(ease)
                        .OnComplete(
                            () =>
                            {
                                OnFinished?.Invoke();
                                transform.localPosition = To;
                            })
                        .OnStart(() =>
                        {
                            OnStart?.Invoke();
                        });
                }
                else
                {
                    //transform.rotation = From;
                    transform.DORotate(To * Multiple, Duration).SetDelay(Delay).SetLoops(LoopTimes, loopType).OnComplete(
                        () =>
                        {
                            OnFinished?.Invoke();
                            transform.position = To;
                        })
                        .OnStart(() =>
                        {
                            OnStart?.Invoke();
                        });
                }
            }

            if (ScaleValueEnum == ScaleValue.X)
            {
                transform.localScale = new Vector3(ScaleFrom, 1, 1);
                transform.DOScaleX(ScaleTo, Duration).SetLoops(LoopTimes, loopType).SetDelay(Delay);
            }

            if (ScaleValueEnum == ScaleValue.Y)
            {
                transform.localScale = new Vector3(1, ScaleFrom, 1);
                transform.DOScaleY(ScaleTo, Duration).SetLoops(LoopTimes, loopType).SetDelay(Delay);
            }

            if (ScaleValueEnum == ScaleValue.Z)
            {
                transform.localScale = new Vector3(1, 1, ScaleFrom);
                transform.DOScaleZ(ScaleTo, Duration).SetLoops(LoopTimes, loopType).SetDelay(Delay);
            }
        }

        public void Rewind()
        {
            if (transform.localPosition.Equals(From)) return;
            var loopType = PingPong ? LoopType.Yoyo : LoopType.Restart;
            if (IsLocalTransfrom)
            {
                transform.localPosition = To;
                transform.DORotate(From, Duration).SetLoops(LoopTimes, loopType).OnComplete(() =>
                {
                    OnFinished?.Invoke();
                    transform.localPosition = From;
                })
                    .OnStart(() =>
                    {
                        OnStart?.Invoke();
                    });
            }
            else
            {
                transform.position = To;
                transform.DORotate(From, Duration).SetLoops(LoopTimes, loopType).OnComplete(() =>
                {
                    OnFinished?.Invoke();
                    transform.position = From;
                })
                    .OnStart(() =>
                    {
                        OnStart?.Invoke();
                    });
            }
        }

        public void Stop()
        {
            transform.DOKill();
            if (IsLocalTransfrom)
                _originalPos = transform.localPosition;
            else
                _originalPos = transform.position;
        }
    }
}

