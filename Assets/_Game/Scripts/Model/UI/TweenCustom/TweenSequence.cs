using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


    public class TweenSequence : MonoBehaviour
    {
        public int LoopTimes;
        public float DelayTime;
        public bool PlayOnStart;
        public bool PlayOnEnable;
        public List<TweenData> ListTweenData;

        private Vector3 _originPos;
        private Vector3 _originScale;
        private Vector3 _originRot;
        private Sequence _sequence;
        private void Awake()
        {
            var ts = transform;
            _originPos = ts.localPosition;
            _originScale = ts.localScale;
            _originRot = ts.localEulerAngles;
        }

        private void OnEnable()
        {
            if (PlayOnEnable)
                Play();
        }
        
        private void Start()
        {
            if (PlayOnEnable) return;
            if (PlayOnStart)
                Play();
        }

        public void Play()
        {
            Stop();
            _sequence = DOTween.Sequence();
            foreach (var t in ListTweenData)
            {
                Tween tween = null;
                if (t.TweenType == TweenType.Position)
                    tween = transform.DOMove(t.ValueTween, t.Duration).SetDelay(t.DelayTime);
                else if (t.TweenType == TweenType.LocalPosition)
                    tween = transform.DOLocalMove(t.ValueTween, t.Duration).SetDelay(t.DelayTime);
                else if (t.TweenType == TweenType.ScaleX)
                    tween = transform.DOScaleX(t.ValueTween.x, t.Duration).SetDelay(t.DelayTime);
                else if (t.TweenType == TweenType.ScaleY)
                    tween = transform.DOScaleY(t.ValueTween.y, t.Duration).SetDelay(t.DelayTime);
                else if (t.TweenType == TweenType.ScaleZ)
                    tween = transform.DOScaleZ(t.ValueTween.z, t.Duration).SetDelay(t.DelayTime);

                if (t.AddType == AddType.Append)
                    _sequence.Append(tween);
                else
                    _sequence.Join(tween);
            }

            _sequence.SetDelay(DelayTime);
            _sequence.SetLoops(LoopTimes);
            _sequence.Play();
        }

        private void Stop()
        {
            if (_sequence == null)
                return;
            var ts = transform;
            ts.localPosition = _originPos;
            ts.localScale = _originScale;
            ts.localEulerAngles = _originRot;

            if (DOTween.IsTweening(transform))
                DOTween.Kill(transform);
            if (_sequence.IsPlaying())
                _sequence.Kill();
            _sequence = null;
        }

        private void OnDisable()
        {
            Stop();
        }

        public class TweenData
        {
            public TweenType TweenType;
            public AddType AddType;
            public Vector3 ValueTween;
            public float Duration;
            public float DelayTime;
        }

        public enum TweenType
        {
            Position,
            LocalPosition,
            ScaleX,
            ScaleY,
            ScaleZ,
            Rotation,
        }

        public enum AddType
        {
            Append,
            Join
        }
    }


