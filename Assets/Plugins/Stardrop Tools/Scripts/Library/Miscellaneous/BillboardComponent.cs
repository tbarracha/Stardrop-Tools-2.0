using UnityEngine;

namespace StardropTools
{
    public class BillboardComponent : BaseTransform
    {
        public Transform camTransform;
        public bool updateOnInitialization = true;

        protected Quaternion originalRotation;

        public override void Initialize()
        {
            base.Initialize();

            if (updateOnInitialization)
                StartUpdate();
        }

        public override void StartUpdate()
        {
            base.StartUpdate();
            IsCamNull();
        }

        public override void HandleUpdate()
        {
            IsCamNull();
            Rotation = camTransform.rotation * originalRotation;
        }

        void IsCamNull()
        {
            if (camTransform == null)
            {
                camTransform = Camera.main.transform;
                originalRotation = Rotation;
            }
        }
    }
}
