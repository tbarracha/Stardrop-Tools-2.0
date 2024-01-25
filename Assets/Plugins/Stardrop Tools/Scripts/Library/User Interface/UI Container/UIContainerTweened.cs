
using StardropTools.Tween;
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.UI
{
    public class UIContainerTweened : UIContainer
    {
        [Header("Open & Close Tweens")]
        [SerializeField] protected bool closeOnInitialize;
        [SerializeField] protected bool deleteContentSizeFitterOnInitilize;
        [SerializeField] protected TweenComponentManager tweenManager_Open;
        [SerializeField] protected TweenComponentManager tweenManager_Close;
        
        public CustomEvent OnOpen
        {
            get
            {
                if (tweenManager_Open != null)
                {
                    return tweenManager_Open.OnTweenComplete;
                }
                else
                    return null;
            }
        }

        public CustomEvent OnClose
        {
            get
            {
                if (tweenManager_Open != null)
                {
                    return tweenManager_Close.OnTweenComplete;
                }
                else
                    return null;
            }
        }

        public override void Initialize()
        {
            base.Initialize();

            if (deleteContentSizeFitterOnInitilize)
            {
                var size = SizeDelta;
                var sizeFitter = GetComponent<ContentSizeFitter>();
                Destroy(sizeFitter);

                do
                {
                    SizeDelta = size;
                } while (SizeDelta != size);
            }

            OnOpen?.AddListener(OnOpened);
            OnClose?.AddListener(OnClosed);

            if (closeOnInitialize)
                Close();
        }

        [NaughtyAttributes.Button("Open")]
        public override void Open()
        {
            base.Open();

            if (toggleOpenClose != null && toggleOpenClose.Value == false)
                toggleOpenClose.SetToggle(true);

            if (tweenManager_Close != null)
                tweenManager_Close.Stop();
            if (tweenManager_Open != null)
                tweenManager_Open.Play();
        }

        [NaughtyAttributes.Button("Close")]
        public override void Close()
        {
            if (toggleOpenClose != null && toggleOpenClose.Value == true)
                toggleOpenClose.SetToggle(false);

            isOpen = false;

            tweenManager_Open?.Stop();
            tweenManager_Close?.Play();
        }

        protected virtual void OnOpened()
        {

        }

        protected virtual void OnClosed()
        {
            if (disableOnClose)
                SetActive(false);
        }
    }
}