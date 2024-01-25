
namespace StardropTools
{
    using UnityEngine;

    /// <summary>
    /// The BaseTransform abstract class, serves as an extension of the BaseComponent class that provides a foundation for objects with a presence in the game world.
    /// It introduces methods and properties related to object positioning, rotation, scaling, parenting, and transformation, among other functionalities.
    /// </summary>
    public abstract class BaseTransform : BaseComponent
    {
        [NaughtyAttributes.Foldout("Object Info")]
        [NaughtyAttributes.ReadOnly]
        [SerializeField] protected Transform thisTransform;

        public Transform Transform      => thisTransform;
        public Transform Parent         => thisTransform.parent;
        public Transform[] Children     => thisTransform.GetChildrenArray();

        public Vector3      Position    { get => thisTransform.position;        set => thisTransform.position       = value; }
        public Quaternion   Rotation    { get => thisTransform.rotation;        set => thisTransform.rotation       = value; }
        public Vector3      EulerAngles { get => thisTransform.eulerAngles;     set => thisTransform.eulerAngles    = value; }
        public Vector3      LocalScale  { get => transform.localScale;          set => transform.localScale         = value; }



        #region Position
        public Vector3 LocalPosition { get => thisTransform.localPosition; set => thisTransform.localPosition = value; }

        /// <summary>
        /// Transform.forward
        /// </summary>
        public Vector3 Forward { get => thisTransform.forward; }

        /// <summary>
        /// X (horizontal) value of the World Position vector
        /// </summary>
        public float PosX { get => Position.x; set => SetPositionX(value); }

        /// <summary>
        /// Y (height) value of the World Position vector
        /// </summary>
        public float PosY { get => Position.y; set => SetPositionY(value); }

        /// <summary>
        /// Z (depth) value of the World Position vector
        /// </summary>
        public float PosZ { get => Position.z; set => SetPositionZ(value); }

        /// <summary>
        /// X (horizontal) value of the Local Position vector
        /// </summary>
        public float LocalPosX { get => LocalPosition.x; set => SetLocalPositionX(value); }

        /// <summary>
        /// Y (height) value of the Local Position vector
        /// </summary>
        public float LocalPosY { get => LocalPosition.y; set => SetLocalPositionY(value); }

        /// <summary>
        /// Z (depth) value of the Local Position vector
        /// </summary>
        public float LocalPosZ { get => LocalPosition.z; set => SetLocalPositionZ(value); }

        /// <summary>
        /// Set the current world position of this object
        /// </summary>
        public void SetPosition(Vector3 position) => Position = position;

        /// <summary>
        /// Set the current local position of this object
        /// </summary>
        public void SetLocalPosition(Vector3 localPosition) => LocalPosition = localPosition;

        public void SetPositionX(float x) => Position = VecUtils.SetVectorX(Position, x);
        public void SetPositionY(float y) => Position = VecUtils.SetVectorY(Position, y);
        public void SetPositionZ(float z) => Position = VecUtils.SetVectorZ(Position, z);

        public void SetLocalPositionX(float x) => LocalPosition = VecUtils.SetVectorX(LocalPosition, x);
        public void SetLocalPositionY(float y) => LocalPosition = VecUtils.SetVectorY(LocalPosition, y);
        public void SetLocalPositionZ(float z) => LocalPosition = VecUtils.SetVectorZ(LocalPosition, z);

        public void ResetPosition() => Position = Vector3.zero;
        public void ResetLocalPosition() => LocalPosition = Vector3.zero;
        #endregion // position


        #region Rotation
        public Quaternion LocalRotation { get => thisTransform.localRotation; set => thisTransform.localRotation = value; }
        public Vector3 LocalEulerAngles { get => thisTransform.localEulerAngles; set => thisTransform.localEulerAngles = value; }

        public void SetRotation(Quaternion rotation) => Rotation = rotation;
        public void SetRotation(Vector3 eulerAngles) => SetEulerAngles(eulerAngles);
        public void SetLocalRotation(Quaternion localRotation) => LocalRotation = localRotation;
        public void SetLocalRotation(Vector3 localEulerAngles) => SetLocalEulerAngles(localEulerAngles);

        public void SetEulerAngles(Vector3 eulerAngles) => EulerAngles = eulerAngles;
        public void SetLocalEulerAngles(Vector3 localEulerAngles) => LocalEulerAngles = localEulerAngles;

        public void SetEulerX(float x) => EulerAngles = VecUtils.SetVectorX(EulerAngles, x);
        public void SetEulerY(float y) => EulerAngles = VecUtils.SetVectorY(EulerAngles, y);
        public void SetEulerZ(float z) => EulerAngles = VecUtils.SetVectorZ(EulerAngles, z);

        public void SetLocalEulerX(float x) => LocalEulerAngles = VecUtils.SetVectorX(LocalEulerAngles, x);
        public void SetLocalEulerY(float y) => LocalEulerAngles = VecUtils.SetVectorY(LocalEulerAngles, y);
        public void SetLocalEulerZ(float z) => LocalEulerAngles = VecUtils.SetVectorZ(LocalEulerAngles, z);

        public void ResetRotation() => Rotation = Quaternion.identity;
        public void ResetLocalRotation() => LocalRotation = Quaternion.identity;
        #endregion // Rotation


        #region Scale
        public void SetScale(Vector3 scale) => LocalScale = scale;

        public void SetScaleX(float x) => LocalScale = VecUtils.SetVectorX(LocalScale, x);
        public void SetScaleY(float y) => LocalScale = VecUtils.SetVectorY(LocalScale, y);
        public void SetScaleZ(float z) => LocalScale = VecUtils.SetVectorZ(LocalScale, z);

        public void ResetScale() => LocalScale = Vector3.one;
        #endregion // Scale


        #region Events

        /// <summary>
        /// Event fired when we change parent via the SetParent() method
        /// </summary>
        public readonly CustomEvent OnParentChanged = new CustomEvent();

        /// <summary>
        /// Event fired when children change via the SetChild() method
        /// </summary>
        public readonly CustomEvent OnChildrenChanged = new CustomEvent();
        
        #endregion // Events


        protected override void OnValidate()
        {
            base.OnValidate();

            if (thisTransform == null)
                thisTransform = transform;
        }


        /// <summary>
        /// Sets parent as Null
        /// </summary>
        public void ClearParent() => SetParent(null);


        /// <summary>
        /// Set the new Parent of this object, if it isn't its child
        /// </summary>
        public void SetParent(Transform parent)
        {
            thisTransform.SetParent(parent);
        }

        /// <summary>
        /// Set a new child of this object, it it isn't already a child
        /// </summary>
        public void SetChild(Transform child)
        {
            if (child.parent != thisTransform)
            {
                child.SetParent(thisTransform);
                OnChildrenChanged?.Invoke();
            }
        }

        /// <summary>
        /// Set this objects children index in relation to its parent
        /// </summary>
        public void SetSiblingIndex(int siblingIndex) => thisTransform.SetSiblingIndex(siblingIndex);

        /// <summary>
        /// Move the transform to the end of the local transform list.
        /// </summary>
        public void SetAsLastSibling() => thisTransform.SetAsLastSibling();

        public void IsChildOf(Transform parent)
        {
            thisTransform.IsChildOf(parent);
        }



        /// <summary>
        /// Returns the Direction from current position TO target Vector Position
        /// </summary>
        public Vector3 DirectionTo(Vector3 targetPosition) => targetPosition - Position;

        /// <summary>
        /// Returns the Direction from current position TO target Transform
        /// </summary>
        public Vector3 DirectionTo(Transform targetTransform) => targetTransform.position - Position;

        /// <summary>
        /// Returns the Direction FROM target Transform to this objects position
        /// </summary>
        public Vector3 DirectionFrom(Vector3 targetPosition) => Position - targetPosition;

        /// <summary>
        /// Returns the Direction FROM target Transform to this objects position
        /// </summary>
        public Vector3 DirectionFrom(Transform targetTransform) => Position - targetTransform.position;


        /// <summary>
        /// Returns the Distance TO and FROM this objects position relative to target Vector
        /// </summary>
        public float DistanceTo(Vector3 targetPosition) => DirectionTo(targetPosition).magnitude;

        /// <summary>
        /// Returns the Distance TO and FROM this objects position relative to target Transform
        /// </summary>
        public float DistanceTo(Transform targetTransform) => DirectionTo(targetTransform).magnitude;


        /// <summary>
        /// Rotates this object towards target Vector imediately.
        /// <para>Optionally, can lock certain axis</para>
        /// </summary>
        public Quaternion RotateTo(Vector3 direction, bool lockX = true, bool lockY = false, bool lockZ = true)
        {
            if (direction == Vector3.zero)
                return Quaternion.identity;

            Quaternion lookRot = Quaternion.LookRotation(direction);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            Rotation = lookRot;

            return lookRot;
        }


        /// <summary>
        /// Rotates object towards target Transform imediately.
        /// <para>Optionally, can lock certain axis</para>
        /// </summary>
        public Quaternion RotateTo(Transform targetTransform, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(targetTransform.position);
            Quaternion targetRot = RotateTo(lookDir, lockX, lockY, lockZ);

            return targetRot;
        }


        /// <summary>
        /// Rotates object smoothly based on lookSpeed toward target direction. Must be updated!
        /// <para>Optionally, can lock certain axis</para>
        /// </summary>
        public Quaternion SmoothRotateTo(Vector3 direction, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            if (direction == Vector3.zero)
                return Quaternion.identity;

            Quaternion lookRot = Quaternion.LookRotation(direction);
            Quaternion targetRot = Quaternion.Slerp(Rotation, lookRot, Time.deltaTime * lookSpeed);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            Rotation = targetRot;

            return targetRot;
        }

        /// <summary>
        /// Rotates object smoothly based on lookSpeed toward target transform. Must be updated!
        /// <para>Optionally, can lock certain axis</para>
        /// </summary>
        public Quaternion SmoothRotateTo(Transform target, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(target.position);
            Quaternion targetRot = SmoothRotateTo(lookDir, lookSpeed, lockX, lockY, lockZ);

            return targetRot;
        }

        /// <summary>
        /// Clamps this objects position. Set a Clamp Type for more controll over positive or negative clamps
        /// </summary>
        public void ClampPosition(Vector3 clampedVector, PositionClampType clampType = PositionClampType.Both)
        {
            ClampPositionX(clampedVector.x, clampType);
            ClampPositionY(clampedVector.y, clampType);
            ClampPositionZ(clampedVector.z, clampType);
        }

        /// <summary>
        /// Clamps this objects X axis position
        /// </summary>
        public void ClampPositionX(float maxClampX, PositionClampType clampType = PositionClampType.Both)
        {
            if (PosX < maxClampX)
                return;

            if (clampType == PositionClampType.Both)
                PosX = Mathf.Clamp(PosX, -maxClampX, maxClampX);

            else if (clampType == PositionClampType.Positive)
                PosX = Mathf.Clamp(PosX, 0, maxClampX);

            else if (clampType == PositionClampType.Negative)
                PosX = Mathf.Clamp(PosX, -maxClampX, 0);
        }

        /// <summary>
        /// Clamps this objects Y axis position
        /// </summary>
        public void ClampPositionY(float maxClampY, PositionClampType clampType = PositionClampType.Both)
        {
            if (PosY < maxClampY)
                return;

            if (clampType == PositionClampType.Both)
                PosY = Mathf.Clamp(PosX, -maxClampY, maxClampY);

            else if (clampType == PositionClampType.Positive)
                PosY = Mathf.Clamp(PosX, 0, maxClampY);

            else if (clampType == PositionClampType.Negative)
                PosY = Mathf.Clamp(PosX, -maxClampY, 0);
        }

        /// <summary>
        /// Clamps this objects Z axis position
        /// </summary>
        public void ClampPositionZ(float maxClampY, PositionClampType clampType = PositionClampType.Both)
        {
            if (PosZ < maxClampY)
                return;

            if (clampType == PositionClampType.Both)
                PosZ = Mathf.Clamp(PosX, -maxClampY, maxClampY);

            else if (clampType == PositionClampType.Positive)
                PosZ = Mathf.Clamp(PosX, 0, maxClampY);

            else if (clampType == PositionClampType.Negative)
                PosZ = Mathf.Clamp(PosX, -maxClampY, 0);
        }

        /// <summary>
        /// Translates object in the normalized direction multiplied by speed
        /// </summary>
        public Vector3 Translate(Vector3 direction, float speed, Space space = Space.World)
        {
            thisTransform.Translate(direction.normalized * speed * Time.deltaTime, space);
            return Position;
        }

        public Vector3 MoveTowards(Vector3 target, float speed)
            => Position = Vector3.MoveTowards(Position, target, speed * Time.deltaTime);

        public Vector3 MoveTowards(Transform target, float speed)
            => Position = Vector3.MoveTowards(Position, target.position, speed * Time.deltaTime);
    }
}