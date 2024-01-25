using UnityEngine;

namespace StardropTools
{
    public class BillboardComponent : BaseComponent
    {
        public Transform camTransform;
        public bool updateOnInitialization = true;
        protected Transform thisTransform;

        protected Quaternion originalRotation;

        public override void Initialize()
        {
            base.Initialize();

            GetThisTransform();
            if (updateOnInitialization)
                StartUpdate();
        }

        public override void StartUpdate()
        {
            base.StartUpdate();
            GetThisTransform();
            IsCamNull();
        }

        public override void HandleUpdate()
        {
            IsCamNull();
            transform.rotation = camTransform.rotation * originalRotation;
        }

        void IsCamNull()
        {
            if (camTransform == null)
            {
                camTransform = Camera.main.transform;
                originalRotation = transform.rotation;
            }
        }

        protected void GetThisTransform()
        {
            if (thisTransform == null)
                thisTransform = transform;
        }
    }
}
