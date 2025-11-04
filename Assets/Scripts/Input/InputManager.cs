using UnityEngine;
using UnityEngine.InputSystem;

namespace EventSystem {
    
    public class InputManager: MonoBehaviour
    {

        [SerializeField]
        public InputActionAsset controls;
        

        private InputAction keyboardMoveAction;
        private InputAction mouseMoveAction;
        private InputAction mouseButtonAction;
        

        private Vector2 lastKeyboardVector;
        private Vector2 lastMousePosition;
        private float lastLeftMouseState;
        
        private void Awake()
        {
            keyboardMoveAction = controls.FindActionMap("Player").FindAction("Move");
            mouseMoveAction = controls.FindActionMap("Player").FindAction("Look");
            mouseButtonAction = controls.FindActionMap("Player").FindAction("Fire");
            
            DontDestroyOnLoad(gameObject);
            lastKeyboardVector = Vector2.zero;
            lastMousePosition = Vector2.zero;
            lastLeftMouseState = 0;
        }


        //this is required to have the input work
        private void OnEnable()
        {
            controls.Enable();
        }

        //this is required to have the input work
        private void OnDisable()
        {
            controls.Disable();
        }
        
        
        void Update()
        {
            
            //call events here
            Vector2 keyboardVector = keyboardMoveAction.ReadValue<Vector2>();
            
            if (lastKeyboardVector != keyboardVector)
            {
              //  EventManager.keyboardMoveActionEvent.callEvent(keyboardVector);
                lastKeyboardVector = keyboardVector;
            }

            Vector2 mouseVector = mouseMoveAction.ReadValue<Vector2>();
           // EventManager.mouseMoveEvent.callEvent(mouseVector);
            lastMousePosition = mouseVector;
        
            
            
            float mouseState = mouseButtonAction.ReadValue<float>();
            if (mouseState >= 1 && lastLeftMouseState < 1)
                return;
            //    EventManager.mouseButtonDownEvent.callEvent(mouseState);
            else if (mouseState < 1 && lastLeftMouseState >= 1)
                return;
             //   EventManager.mouseButtonUpEvent.callEvent(mouseState);
            

            if (mouseState >= 1)
            {
              //  EventManager.mouseHoldDownEvent.callEvent(mouseState);
            }

            lastLeftMouseState = mouseState;
            
        }
    }
}
