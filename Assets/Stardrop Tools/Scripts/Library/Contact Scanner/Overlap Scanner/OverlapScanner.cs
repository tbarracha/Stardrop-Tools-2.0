
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Lists colliders and invokes events based on contact with filtered colliders
    /// </summary>
    public abstract class OverlapScanner : BaseObject
    {
        [Header("Overlap Scanner")]
        [SerializeField] protected LayerMask contactLayers;
        [SerializeField] protected Vector3 positionOffset;
        [SerializeField] protected bool hasContact;
        [SerializeField] protected bool debug;
        [Space]
        [SerializeField] protected System.Collections.Generic.List<Collider> listColliders;
        protected Collider[] colliders;

        public int ColliderCount { get => colliders.Exists() ? colliders.Length : 0; }
        public Collider[] Colliders { get => colliders; }
        public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }
        public bool HasContact { get => hasContact; }

        #region Events
        public readonly GameEvent OnDetected = new GameEvent();

        public readonly GameEvent OnEnter = new GameEvent();
        public readonly GameEvent OnStay = new GameEvent();
        public readonly GameEvent OnExit = new GameEvent();

        public readonly GameEvent<Collider> OnColliderEnter = new GameEvent<Collider>();
        public readonly GameEvent<Collider> OnColliderStay = new GameEvent<Collider>();
        public readonly GameEvent<Collider> OnColliderExit = new GameEvent<Collider>();

        public readonly GameEvent<string> OnTagEnter = new GameEvent<string>();
        public readonly GameEvent<string> OnTagStay = new GameEvent<string>();
        public readonly GameEvent<string> OnTagExit = new GameEvent<string>();

        public readonly GameEvent<int> OnCountEnter = new GameEvent<int>();
        public readonly GameEvent<int> OnCountStay = new GameEvent<int>();
        public readonly GameEvent<int> OnCountExit = new GameEvent<int>();
        #endregion // events

        public override void Initialize()
        {
            base.Initialize();
            colliders = new Collider[0];
        }

        public void SetLayerMask(LayerMask layerMask) => contactLayers = layerMask;

        public virtual void OverlapScan()
        {
            ColliderCheck();
        }

        public virtual Collider[] Scan(LayerMask mask)
        {
            return colliders;
        }


        // Add & Remove detected colliders in order;
        protected void ColliderCheck()
        {
            if (colliders == null)
                return;

            if (listColliders == null)
                listColliders = new System.Collections.Generic.List<Collider>();

            // check if list and array is of equal length
            // if not, either add or remove from list & invoke events
            if (colliders.Length != listColliders.Count)
            {
                if (colliders.Length > 0)
                {
                    // check colliders to add to queue
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        Collider col = colliders[i];

                        // add to queue
                        if (listColliders.Contains(col) == false)
                        {
                            listColliders.Add(col);

                            OnEnter?.Invoke();
                            OnColliderEnter?.Invoke(col);
                            OnTagEnter?.Invoke(col.tag);
                            OnCountEnter?.Invoke(ColliderCount);

                            if (debug)
                                Debug.Log("Collider Entered");
                        }
                    }
                }

                else
                {
                    // check list to remove Out Of Bounds colliders
                    for (int i = 0; i < listColliders.Count; i++)
                    {
                        // check if collider in list exists in collider array
                        Collider col = listColliders[i];

                        listColliders.Remove(col);

                        OnExit?.Invoke();
                        OnColliderExit?.Invoke(col);
                        OnTagExit?.Invoke(col.tag);
                        OnCountExit?.Invoke(ColliderCount);

                        if (debug)
                            Debug.Log("Collider Exited");
                    }

                    listColliders.Clear();
                }

                OnDetected?.Invoke();
            }

            hasContact = colliders.Length > 0;
        }

        protected void FixedCollisionCheck()
        {
            if (colliders.Exists())
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    OnColliderStay?.Invoke(colliders[i]);
                    OnTagStay?.Invoke(colliders[i].tag);
                    OnCountStay?.Invoke(ColliderCount);
                }

                OnStay.Invoke();
            }
        }

        // To do
        public void SortByDistance(Vector3 referencePosition)
        {

        }

        public virtual T GetDetectedComponent<T>(T component)
        {
            if (colliders != null && colliders.Length > 0)
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    T obj = colliders[i].GetComponent<T>();
                    if (obj != null)
                        return obj;
                }

                Debug.Log("Object not found");
                return default;
            }

            else
            {
                Debug.Log("No colliders detected");
                return default;
            }
        }
    }
}


