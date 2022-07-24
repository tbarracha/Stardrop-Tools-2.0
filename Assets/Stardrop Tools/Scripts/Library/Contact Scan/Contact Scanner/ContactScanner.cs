﻿
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Checks and invokes events based on contact with filtered colliders
    /// </summary>
    public abstract class ContactScanner : BaseObject
    {
        public LayerMask contactLayers;
        [SerializeField] protected bool hasContact;

        public bool HasContact { get => hasContact; }

        public readonly BaseEvent OnContactStart = new BaseEvent();
        public readonly BaseEvent OnContact = new BaseEvent();
        public readonly BaseEvent OnContactEnd = new BaseEvent();

        public abstract bool ContactScan();

        /// <summary>
        /// Checks & broadcasts events based on contact
        /// </summary>
        protected bool ContactCheck(bool physicsContactBoolean)
        {
            if (hasContact != physicsContactBoolean)
            {
                // contact start
                if (physicsContactBoolean && hasContact == false)
                    OnContactStart?.Invoke();

                // contact end
                if (physicsContactBoolean == false && hasContact)
                    OnContactEnd?.Invoke();

                hasContact = physicsContactBoolean;
            }

            // is in contact
            else
                OnContact?.Invoke();

            return physicsContactBoolean;
        }
    }
}