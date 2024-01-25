
using UnityEngine;

namespace StardropTools
{
    public abstract class SingletonBaseTransformManager<T> : BaseTransformManager where T : BaseTransformManager
    {
		public override void Initialize()
		{
			base.Initialize();
			EventFlow();
		}

		/// <summary>
		/// Method reserved for Game Event/Action subscriptions, that change the current objects behaviour
		/// </summary>
		protected virtual void EventFlow() { }

        #region Singleton
        /// <summary>
        /// The instance.
        /// </summary>
        private static T instance;


		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = FindObjectOfType<T>();
					if (instance == null)
					{
						GameObject obj = new GameObject();
						obj.name = typeof(T).Name;
						instance = obj.AddComponent<T>();
					}
				}
				return instance;
			}
		}

		void SingletonInitialization()
		{
			if (instance == null)
			{
				instance = this as T;
				DontDestroyOnLoad(gameObject);
            }

            //else
            //	Destroy(gameObject);
        }


        protected override void Awake()
		{
			SingletonInitialization();
			base.Awake();
		}

        #endregion // singleton
    }
}