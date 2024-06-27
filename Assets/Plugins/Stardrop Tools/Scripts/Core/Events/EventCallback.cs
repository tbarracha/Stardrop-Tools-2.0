
using System;
using System.Linq;

namespace StardropTools
{
    /// <summary>
    /// Represents a generic event callback that allows subscribing, unsubscribing, and invoking actions.
    /// This class prevents duplicate subscriptions and provides methods for safe subscription management.
    /// </summary>
    public class EventCallback
    {
        private event Action action = delegate { };

        public void Invoke() => action?.Invoke();

        public void Subscribe(Action listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void Unsubscribe(Action listener) => action -= listener;

        public void SafeSubscribe(Action listener)
        {
            if (!IsSubscribed(listener))
            {
                Subscribe(listener);
            }
        }

        public void SafeUnsubscribe(Action listener)
        {
            if (IsSubscribed(listener))
            {
                Unsubscribe(listener);
            }
        }

        public bool IsSubscribed(Action listener)
        {
            return action != null && action.GetInvocationList().Contains(listener);
        }

        public void ClearAllSubscriptions()
        {
            Delegate[] eventListeners = action?.GetInvocationList();
            if (eventListeners != null)
            {
                foreach (Delegate del in eventListeners)
                {
                    if (del != null)
                        action -= del as Action;
                }
            }
        }
    }


    /// <summary>
    /// Represents a generic event callback that allows subscribing, unsubscribing, and invoking actions.
    /// This class prevents duplicate subscriptions and provides methods for safe subscription management.
    /// </summary>
    public class EventCallback<T>
    {
        private event Action<T> action = delegate { };

        public void Invoke(T arg) => action?.Invoke(arg);

        public void Subscribe(Action<T> listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void Unsubscribe(Action<T> listener) => action -= listener;

        public void SafeSubscribe(Action<T> listener)
        {
            if (!IsSubscribed(listener))
            {
                Subscribe(listener);
            }
        }

        public void SafeUnsubscribe(Action<T> listener)
        {
            if (IsSubscribed(listener))
            {
                Unsubscribe(listener);
            }
        }

        public bool IsSubscribed(Action<T> listener)
        {
            return action != null && action.GetInvocationList().Contains(listener);
        }

        public void ClearAllSubscriptions()
        {
            Delegate[] eventListeners = action?.GetInvocationList();
            if (eventListeners != null)
            {
                foreach (Delegate del in eventListeners)
                {
                    if (del != null)
                        action -= del as Action<T>;
                }
            }
        }
    }


    /// <summary>
    /// Represents a generic event callback that allows subscribing, unsubscribing, and invoking actions.
    /// This class prevents duplicate subscriptions and provides methods for safe subscription management.
    /// </summary>
    public class EventCallback<T1, T2>
    {
        private event Action<T1, T2> action = delegate { };

        public void Invoke(T1 arg1, T2 arg2) => action?.Invoke(arg1, arg2);

        public void Subscribe(Action<T1, T2> listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void Unsubscribe(Action<T1, T2> listener) => action -= listener;

        public void SafeSubscribe(Action<T1, T2> listener)
        {
            if (!IsSubscribed(listener))
            {
                Subscribe(listener);
            }
        }

        public void SafeUnsubscribe(Action<T1, T2> listener)
        {
            if (IsSubscribed(listener))
            {
                Unsubscribe(listener);
            }
        }

        public bool IsSubscribed(Action<T1, T2> listener)
        {
            return action != null && action.GetInvocationList().Contains(listener);
        }

        public void ClearAllSubscriptions()
        {
            Delegate[] eventListeners = action?.GetInvocationList();
            if (eventListeners != null)
            {
                foreach (Delegate del in eventListeners)
                {
                    if (del != null)
                        action -= del as Action<T1, T2>;
                }
            }
        }
    }


    /// <summary>
    /// Represents a generic event callback that allows subscribing, unsubscribing, and invoking actions.
    /// This class prevents duplicate subscriptions and provides methods for safe subscription management.
    /// </summary>
    public class EventCallback<T1, T2, T3>
    {
        private event Action<T1, T2, T3> action = delegate { };

        public void Invoke(T1 arg1, T2 arg2, T3 arg3) => action?.Invoke(arg1, arg2, arg3);

        public void Subscribe(Action<T1, T2, T3> listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void Unsubscribe(Action<T1, T2, T3> listener) => action -= listener;

        public void SafeSubscribe(Action<T1, T2, T3> listener)
        {
            if (!IsSubscribed(listener))
            {
                Subscribe(listener);
            }
        }

        public void SafeUnsubscribe(Action<T1, T2, T3> listener)
        {
            if (IsSubscribed(listener))
            {
                Unsubscribe(listener);
            }
        }

        public bool IsSubscribed(Action<T1, T2, T3> listener)
        {
            return action != null && action.GetInvocationList().Contains(listener);
        }

        public void ClearAllSubscriptions()
        {
            Delegate[] eventListeners = action?.GetInvocationList();
            if (eventListeners != null)
            {
                foreach (Delegate del in eventListeners)
                {
                    if (del != null)
                        action -= del as Action<T1, T2, T3>;
                }
            }
        }
    }


    /// <summary>
    /// Represents a generic event callback that allows subscribing, unsubscribing, and invoking actions.
    /// This class prevents duplicate subscriptions and provides methods for safe subscription management.
    /// </summary>
    public class EventCallback<T1, T2, T3, T4>
    {
        private event Action<T1, T2, T3, T4> action = delegate { };

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => action?.Invoke(arg1, arg2, arg3, arg4);

        public void Subscribe(Action<T1, T2, T3, T4> listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void Unsubscribe(Action<T1, T2, T3, T4> listener) => action -= listener;

        public void SafeSubscribe(Action<T1, T2, T3, T4> listener)
        {
            if (!IsSubscribed(listener))
            {
                Subscribe(listener);
            }
        }

        public void SafeUnsubscribe(Action<T1, T2, T3, T4> listener)
        {
            if (IsSubscribed(listener))
            {
                Unsubscribe(listener);
            }
        }

        public bool IsSubscribed(Action<T1, T2, T3, T4> listener)
        {
            return action != null && action.GetInvocationList().Contains(listener);
        }

        public void ClearAllSubscriptions()
        {
            Delegate[] eventListeners = action?.GetInvocationList();
            if (eventListeners != null)
            {
                foreach (Delegate del in eventListeners)
                {
                    if (del != null)
                        action -= del as Action<T1, T2, T3, T4>;
                }
            }
        }
    }


    /// <summary>
    /// Represents a generic event callback that allows subscribing, unsubscribing, and invoking actions.
    /// This class prevents duplicate subscriptions and provides methods for safe subscription management.
    /// </summary>
    public class EventCallback<T1, T2, T3, T4, T5>
    {
        private event Action<T1, T2, T3, T4, T5> action = delegate { };

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => action?.Invoke(arg1, arg2, arg3, arg4, arg5);

        public void Subscribe(Action<T1, T2, T3, T4, T5> listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void Unsubscribe(Action<T1, T2, T3, T4, T5> listener) => action -= listener;

        public void SafeSubscribe(Action<T1, T2, T3, T4, T5> listener)
        {
            if (!IsSubscribed(listener))
            {
                Subscribe(listener);
            }
        }

        public void SafeUnsubscribe(Action<T1, T2, T3, T4, T5> listener)
        {
            if (IsSubscribed(listener))
            {
                Unsubscribe(listener);
            }
        }

        public bool IsSubscribed(Action<T1, T2, T3, T4, T5> listener)
        {
            return action != null && action.GetInvocationList().Contains(listener);
        }

        public void ClearAllSubscriptions()
        {
            Delegate[] eventListeners = action?.GetInvocationList();
            if (eventListeners != null)
            {
                foreach (Delegate del in eventListeners)
                {
                    if (del != null)
                        action -= del as Action<T1, T2, T3, T4, T5>;
                }
            }
        }
    }


    /// <summary>
    /// Represents a generic event callback that allows subscribing, unsubscribing, and invoking actions.
    /// This class prevents duplicate subscriptions and provides methods for safe subscription management.
    /// </summary>
    public class EventCallback<T1, T2, T3, T4, T5, T6>
    {
        private event Action<T1, T2, T3, T4, T5, T6> action = delegate { };

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);

        public void Subscribe(Action<T1, T2, T3, T4, T5, T6> listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void Unsubscribe(Action<T1, T2, T3, T4, T5, T6> listener) => action -= listener;

        public void SafeSubscribe(Action<T1, T2, T3, T4, T5, T6> listener)
        {
            if (!IsSubscribed(listener))
            {
                Subscribe(listener);
            }
        }

        public void SafeUnsubscribe(Action<T1, T2, T3, T4, T5, T6> listener)
        {
            if (IsSubscribed(listener))
            {
                Unsubscribe(listener);
            }
        }

        public bool IsSubscribed(Action<T1, T2, T3, T4, T5, T6> listener)
        {
            return action != null && action.GetInvocationList().Contains(listener);
        }

        public void ClearAllSubscriptions()
        {
            Delegate[] eventListeners = action?.GetInvocationList();
            if (eventListeners != null)
            {
                foreach (Delegate del in eventListeners)
                {
                    if (del != null)
                        action -= del as Action<T1, T2, T3, T4, T5, T6>;
                }
            }
        }
    }
}
