using UnityEngine;

namespace EventSystem
{
    public static class EventManager
    {
        //registered under player
        public static EventDispatcher<Vector2> keyboardMoveActionEvent = new EventDispatcher<Vector2>();
        
        //unused
        public static EventDispatcher<Vector2> mouseMoveEvent = new EventDispatcher<Vector2>();
        public static EventDispatcher<float> leftMouseDownEvent = new EventDispatcher<float>();
        public static EventDispatcher<float> leftMouseUpEvent = new EventDispatcher<float>();
        

    }
}