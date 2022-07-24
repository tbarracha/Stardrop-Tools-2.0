
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Base component for heavy GameObject and Transform manipulation
    /// </summary>
    public class BaseObject : BaseComponent
    {
        #region Core Data
        protected BaseObjectData objectData;

        public BaseObjectData ObjectData { get => objectData; }
        public GameObject GameObject { get => objectData.GameObject; }
        public Transform Transform { get => objectData.Transform; }
        public Transform Parent { get => Transform.parent; set => SetParent(value); }

        public bool IsActive { get => ObjectData.IsActive; }

        public void SetActive(bool value)
        {
            ObjectData.SetActive(value);

            if (value)
                OnActivate?.Invoke();
            else
                OnDeactivate?.Invoke();
        }
        #endregion // core


        #region Position
        public Vector3 Position { get => Transform.position; set => Transform.position = value; }
        public Vector3 LocalPosition { get => Transform.localPosition; set => Transform.localPosition = value; }

        public Vector3 InitializedPosition { get; protected set; }
        public Vector3 EnabledPosition { get; protected set; }
        public Vector3 DisabledPosition { get; protected set; }

        public float PosX { get => Position.x; set => objectData.SetPositionX(value); }
        public float PosY { get => Position.y; set => objectData.SetPositionY(value); }
        public float PosZ { get => Position.z; set => objectData.SetPositionZ(value); }

        public float LocalPosX { get => LocalPosition.x; set => objectData.SetLocalPositionX(value); }
        public float LocalPosY { get => LocalPosition.y; set => objectData.SetLocalPositionY(value); }
        public float LocalPosZ { get => LocalPosition.z; set => objectData.SetLocalPositionZ(value); }

        public void SetPosition(Vector3 position) => Position = position;
        public void SetLocalPosition(Vector3 localPosition) => LocalPosition = localPosition;
        #endregion // position


        #region Rotation
        public Quaternion Rotation { get => Transform.rotation; set => Transform.rotation = value; }
        public Quaternion LocalRotation { get => Transform.localRotation; set => Transform.localRotation = value; }

        public Vector3 EulerAngles { get => Transform.eulerAngles; set => Transform.eulerAngles = value; }
        public Vector3 LocalEulerAngles { get => Transform.localEulerAngles; set => Transform.localEulerAngles = value; }

        public float EulerX { get => EulerAngles.x; set => objectData.SetEulerX(value); }
        public float EulerY { get => EulerAngles.y; set => objectData.SetEulerY(value); }
        public float EulerZ { get => EulerAngles.z; set => objectData.SetEulerZ(value); }

        public float LocalEulerX { get => LocalEulerAngles.x; set => objectData.SetEulerX(value); }
        public float LocalEulerY { get => LocalEulerAngles.y; set => objectData.SetEulerY(value); }
        public float LocalEulerZ { get => LocalEulerAngles.z; set => objectData.SetEulerZ(value); }

        public void SetRotation(Quaternion rotation) => Rotation = rotation;
        public void SetLocalRotation(Quaternion localRotation) => LocalRotation = localRotation;

        public void SetEulerAngles(Vector3 euler) => Rotation = Quaternion.Euler(euler);
        public void SetLocalEulerAngles(Vector3 localEuler) => Rotation = Quaternion.Euler(localEuler);
        #endregion // rotation


        #region Scale
        public Vector3 LocalScale { get => Transform.localScale; set => Transform.localScale = value; }

        public float LocalScaleX { get => LocalScale.x; set => objectData.SetScaleX(value); }
        public float LocalScaleY { get => LocalScale.y; set => objectData.SetScaleY(value); }
        public float LocalScaleZ { get => LocalScale.z; set => objectData.SetScaleZ(value); }

        public void SetLocalScale(Vector3 localScale) => LocalScale = localScale;
        #endregion // scale


        #region Events

        public readonly BaseEvent OnActivate = new BaseEvent();
        public readonly BaseEvent OnDeactivate = new BaseEvent();

        public readonly BaseEvent OnParentChange = new BaseEvent();
        public readonly BaseEvent OnChildrenChange = new BaseEvent();
        #endregion // Events


        protected void DataCheck()
        {
            if (objectData.GameObject == null || objectData.Transform == null)
                objectData = new BaseObjectData(gameObject);
        }

        public override void Initialize()
        {
            base.Initialize();

            DataCheck();
            InitializedPosition = Position;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            EnabledPosition = Position;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            DisabledPosition = Position;
        }

        protected virtual void OnValidate()
        {
            DataCheck();
        }


        public void SetParent(Transform parent)
        {
            if (Transform.parent != parent)
            {
                Transform.parent = parent;
                OnParentChange?.Invoke();
            }

            else
                Debug.Log(name + "is already child of " + parent);
        }

        public void SetChild(Transform child)
        {
            if (child.parent != Transform)
            {
                child.parent = Transform;
                OnChildrenChange?.Invoke();
            }

            else
                Debug.Log(name + "is already parent of " + child);
        }

        public void SetSiblingIndex(int siblingIndex) => Transform.SetSiblingIndex(siblingIndex);


        public Vector3 DirectionTo(Vector3 target) => target - Position;
        public Vector3 DirectionTo(Transform target) => target.position - Position;

        public Vector3 DirectionFrom(Vector3 target) => Position - target;
        public Vector3 DirectionFrom(Transform target) => Position - target.position;


        public float DistanceTo(Vector3 target) => DirectionTo(target).magnitude;
        public float DistanceTo(Transform target) => DirectionTo(target).magnitude;


        public Quaternion LookAt(Vector3 direction, bool lockX = true, bool lockY = false, bool lockZ = true)
        {
            if (direction == Vector3.zero)
                return Quaternion.identity;

            Quaternion lookRot = Quaternion.LookRotation(direction);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            SetRotation(lookRot);

            return lookRot;
        }

        public Quaternion LookAt(Transform target, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(target.position);
            Quaternion targetRot = LookAt(lookDir, lockX, lockY, lockZ);

            return targetRot;
        }


        public Quaternion SmoothLookAt(Vector3 direction, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            if (direction == Vector3.zero)
                return Quaternion.identity;

            Quaternion lookRot = Quaternion.LookRotation(direction);
            Quaternion targetRot = Quaternion.Slerp(Rotation, lookRot, Time.deltaTime * lookSpeed);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            SetRotation(targetRot);

            return targetRot;
        }

        public Quaternion SmoothLookAt(Transform target, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(target.position);
            Quaternion targetRot = SmoothLookAt(lookDir, lookSpeed, lockX, lockY, lockZ);

            return targetRot;
        }
    }
}