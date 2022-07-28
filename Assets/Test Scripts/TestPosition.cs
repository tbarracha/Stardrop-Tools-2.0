using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StardropTools;
using StardropTools.Tween;

namespace StardropTools.Test
{
    public class TestPosition : BaseObject
    {
        public float positionX;
        public float eulerX;

        [Header("Test Tween")]
        [SerializeField] EaseType easeType;
        [SerializeField] AnimationCurve curve;
        [SerializeField] float radius = 3;
        [SerializeField] float delay = .2f;
        [SerializeField] float duration = 1;
        [SerializeField] float shakeIntensity = .2f;
        [SerializeField] Transform target;
        [SerializeField] Tween.Tween tween;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 end = Position + Random.insideUnitSphere * radius;
                //tween = new TweenPosition(target, end).SetDurationAndDelay(duration, delay).SetEaseType(easeType).SetAnimationCurve(curve).Initialize();
                tween = new TweenShakePosition(target, end).SetIntensity(shakeIntensity).SetDurationAndDelay(duration, delay).SetEaseType(easeType).SetAnimationCurve(curve).Initialize();
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            PosX = positionX;
            EulerX = eulerX;
        }
    }
}