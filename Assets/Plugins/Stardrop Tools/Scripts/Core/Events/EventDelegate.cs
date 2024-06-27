using System;

namespace StardropTools
{
    public class EventDelegate
    {
        private event Action action = delegate { };

        public void Invoke() => action?.Invoke();

        public void AddListener(Action listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void RemoveListener(Action listener) => action -= listener;

        public void ClearAllListeners()
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

    public class EventDelegate<T>
    {
        private event Action<T> action = delegate { };

        public void Invoke(T arg) => action?.Invoke(arg);

        public void AddListener(Action<T> listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void RemoveListener(Action<T> listener) => action -= listener;

        public void ClearAllListeners()
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

    // EventDelegate classes with additional generic types

    public class EventDelegate<T1, T2>
    {
        private event Action<T1, T2> action = delegate { };

        public void Invoke(T1 arg1, T2 arg2) => action?.Invoke(arg1, arg2);

        public void AddListener(Action<T1, T2> listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void RemoveListener(Action<T1, T2> listener) => action -= listener;

        public void ClearAllListeners()
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

    public class EventDelegate<T1, T2, T3>
    {
        private event Action<T1, T2, T3> action = delegate { };

        public void Invoke(T1 arg1, T2 arg2, T3 arg3) => action?.Invoke(arg1, arg2, arg3);

        public void AddListener(Action<T1, T2, T3> listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void RemoveListener(Action<T1, T2, T3> listener) => action -= listener;

        public void ClearAllListeners()
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

    public class EventDelegate<T1, T2, T3, T4>
    {
        private event Action<T1, T2, T3, T4> action = delegate { };

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => action?.Invoke(arg1, arg2, arg3, arg4);

        public void AddListener(Action<T1, T2, T3, T4> listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void RemoveListener(Action<T1, T2, T3, T4> listener) => action -= listener;

        public void ClearAllListeners()
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

    public class EventDelegate<T1, T2, T3, T4, T5>
    {
        private event Action<T1, T2, T3, T4, T5> action = delegate { };

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) => action?.Invoke(arg1, arg2, arg3, arg4, arg5);

        public void AddListener(Action<T1, T2, T3, T4, T5> listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void RemoveListener(Action<T1, T2, T3, T4, T5> listener) => action -= listener;

        public void ClearAllListeners()
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

    public class EventDelegate<T1, T2, T3, T4, T5, T6>
    {
        private event Action<T1, T2, T3, T4, T5, T6> action = delegate { };

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) => action?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);

        public void AddListener(Action<T1, T2, T3, T4, T5, T6> listener)
        {
            action -= listener; // prevents duplicate subscriptions (does nothing if listener is not subscribed)
            action += listener;
        }

        public void RemoveListener(Action<T1, T2, T3, T4, T5, T6> listener) => action -= listener;

        public void ClearAllListeners()
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
