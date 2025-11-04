using System;

namespace EventSystem
{
    public class EventDispatcher<T>
    {
        private event Action<T> action;

        public void callEvent(T data)
        {
            action?.Invoke(data);
        }

        public void Subscribe(Action<T> handler)
        {
            action += handler;
        }

        public void Unsubscribe(Action<T> handler)
        {
            action -= handler;
        }
    }
}