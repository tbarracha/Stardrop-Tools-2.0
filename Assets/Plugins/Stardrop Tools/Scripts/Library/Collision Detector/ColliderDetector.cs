
using StardropTools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// The CollisionDetector class, serves as a foundational base for collision detection through Physics.Overlap Box/Sphere colliders.
    /// This abstract class provides methods for detecting and managing colliders within specified bounds, filtering colliders based on criteria, and triggering events upon collision start and end events.
    /// </summary>
    public abstract class ColliderDetector : BaseTransform
    {
        [SerializeField] protected LayerMask contactLayer;
        [SerializeField] protected bool hasContact;
        [SerializeField] protected bool ignoreSelf;
        [Space]
        [SerializeField] List<Collider> ignoreTheseColliders;

        protected Collider[] colliders;
        protected List<Collider> contactColliders, newContactColliders;

        public bool HasContact => hasContact;

        public readonly EventCallback OnContactStart  = new EventCallback();
        public readonly EventCallback OnContactEnd    = new EventCallback();

        public readonly EventCallback<Collider> OnColliderEnter   = new EventCallback<Collider>();
        public readonly EventCallback<Collider> OnColliderExit    = new EventCallback<Collider>();

        protected override void Start()
        {
            base.Start();

            if (ignoreSelf)
                IgnoreSelf();
        }

        public void SetLayerMask(LayerMask layerMask)
        {
            contactLayer = layerMask;
        }

        public override void HandleUpdate()
        {
            FilteredColliderDetection();
        }

        /// <summary>
        /// Returns colliders from a Physics.Overlap Box/Sphere
        /// </summary>
        public abstract Collider[] ColliderDetection();

        /// <summary>
        /// Call this on a collider detector to check if there are any colliders within bounds.
        /// <para>Call every frame to check for colliders every frame!</para>
        /// </summary>
        public virtual List<Collider> FilteredColliderDetection()
        {
            if (contactColliders == null)
                contactColliders = new List<Collider>();

            CheckForContact();
            FilterColliders();
            return contactColliders;
        }

        public bool CheckForContact()
        {
            // colliders detected
            if (colliders.Exists() && hasContact == false)
            {
                OnContactStart?.Invoke();
                hasContact = true;
            }

            // no colliders detected
            else if (hasContact == true)
            {
                OnContactEnd?.Invoke();
                hasContact = false;
            }

            return hasContact;
        }

        protected void FilterColliders()
        {
            FilterCollidersToIgnore();

            if (newContactColliders == null)
                newContactColliders = new List<Collider>();
            else
                newContactColliders.Clear();

            // Check for colliders that are in colliders but not in contactColliders - ENTER events
            foreach (Collider collider in colliders)
            {
                if (contactColliders.Contains(collider) == false)
                {
                    newContactColliders.Add(collider);
                    OnColliderEnter?.Invoke(collider);
                }
            }

            // Update contactColliders
            contactColliders.AddRange(newContactColliders);

            List<Collider> collidersToRemove = contactColliders.Except(colliders).ToList();

            foreach (Collider collider in collidersToRemove)
            {
                OnColliderExit?.Invoke(collider);
                contactColliders.Remove(collider);
            }
        }


        protected void FilterCollidersToIgnore()
        {
            if (ignoreTheseColliders.Exists() == false)
                return;

            List<Collider> filteredColliders = new List<Collider>();
            foreach (Collider collider in colliders)
            {
                if (ignoreTheseColliders.Contains(collider) == false)
                    filteredColliders.Add(collider);
            }

            colliders = filteredColliders.ToArray();
        }

        void IgnoreSelf()
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            List<Collider> ignores = colliders.ToList();

            Collider collider = GetComponent<Collider>();
            if (collider != null)
                ignores.Add(collider);

            foreach (var col in ignores)
            {
                if (ignoreTheseColliders.Contains(col) == false)
                    ignoreTheseColliders.Add(col);
            }
        }

        public bool AddColliderToIgnore(Collider collider)
        {
            if (ignoreTheseColliders.Contains(collider) == false)
            {
                ignoreTheseColliders.Add(collider);
                return true;
            }

            return false;
        }

        public bool RemoveColliderFromIgnore(Collider collider)
        {
            if (ignoreTheseColliders.Contains(collider))
            {
                ignoreTheseColliders.Remove(collider);
                return true;
            }

            return false;
        }
    }
}