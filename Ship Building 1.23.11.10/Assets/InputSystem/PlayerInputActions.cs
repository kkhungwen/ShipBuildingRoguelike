//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/InputSystem/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""ShipControl"",
            ""id"": ""d544411f-17d5-4921-8754-ed9cdd368389"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""656af035-4c85-4f6d-af42-fdb00c5c72e3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""66291933-6292-416f-b92c-b39007d47519"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RotateTapPositive"",
                    ""type"": ""Button"",
                    ""id"": ""52e214ee-3949-4d1d-a12a-0b4adcbaffce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RotateTapNegative"",
                    ""type"": ""Button"",
                    ""id"": ""c78a1631-62f7-4136-863b-ca2c6c3972c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""6d4c4193-2694-457f-94b2-ed74ba897088"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dbc941cb-d129-4a83-8b15-37ea501843c2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""43d9f589-d33e-40ae-85c8-09356b150ca6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9744e84b-2579-436d-aba4-3485d87fa8b9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fe63587d-a72f-4941-9f0a-3cbd6333f82d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ff2eca86-22c0-4937-8f48-a44364a8e914"",
                    ""path"": ""<SwitchProControllerHID>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Mouse Button LeftRight "",
                    ""id"": ""6031ca93-4dfc-4198-92bf-c78b3f752bc0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f6ea2f74-881a-479b-a679-0870c93d25f8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5b8f2f09-204b-4c48-b707-967a992d0628"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ProController YA Button"",
                    ""id"": ""98dfb7df-f16c-45d3-ae58-75c84615ba2b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5e62afc2-5555-426e-bf0e-c4610edf8103"",
                    ""path"": ""<SwitchProControllerHID>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""177a7db0-6334-44c5-bad2-1ed32cd8e85a"",
                    ""path"": ""<SwitchProControllerHID>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ProController Right Stick"",
                    ""id"": ""f7be69bb-11e2-4e2b-be0d-35179a15b6de"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6f2fe313-b25c-4a1a-bad7-1864c7b49c69"",
                    ""path"": ""<SwitchProControllerHID>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""cfd298cb-8181-4efe-81a6-28900f5c4cf2"",
                    ""path"": ""<SwitchProControllerHID>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ProController ZLZR"",
                    ""id"": ""c8ea7357-3b47-4040-8a77-a0f08e3fcea9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8da7aa87-d3fa-4436-ae4e-e585b0decb74"",
                    ""path"": ""<SwitchProControllerHID>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6df2f429-e24b-4485-b6e8-84842e6f6b34"",
                    ""path"": ""<SwitchProControllerHID>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6b5f09e7-08c5-4fdd-9b9b-813f27ca7342"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateTapPositive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""625befb3-c79f-40ee-8d4a-e22e9da36219"",
                    ""path"": ""<SwitchProControllerHID>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateTapPositive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53a98782-49dc-4aa0-8874-2e9b81abff73"",
                    ""path"": ""<SwitchProControllerHID>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateTapPositive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0694f10b-9a89-4864-a2d3-556d65d23af0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateTapNegative"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7401b446-71f9-4e93-8d53-edcb98ba4ca0"",
                    ""path"": ""<SwitchProControllerHID>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateTapNegative"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42245f73-8bf9-44f8-8b32-0ba4c9f70597"",
                    ""path"": ""<SwitchProControllerHID>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateTapNegative"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ShipControl
        m_ShipControl = asset.FindActionMap("ShipControl", throwIfNotFound: true);
        m_ShipControl_Move = m_ShipControl.FindAction("Move", throwIfNotFound: true);
        m_ShipControl_Rotate = m_ShipControl.FindAction("Rotate", throwIfNotFound: true);
        m_ShipControl_RotateTapPositive = m_ShipControl.FindAction("RotateTapPositive", throwIfNotFound: true);
        m_ShipControl_RotateTapNegative = m_ShipControl.FindAction("RotateTapNegative", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // ShipControl
    private readonly InputActionMap m_ShipControl;
    private IShipControlActions m_ShipControlActionsCallbackInterface;
    private readonly InputAction m_ShipControl_Move;
    private readonly InputAction m_ShipControl_Rotate;
    private readonly InputAction m_ShipControl_RotateTapPositive;
    private readonly InputAction m_ShipControl_RotateTapNegative;
    public struct ShipControlActions
    {
        private @PlayerInputActions m_Wrapper;
        public ShipControlActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_ShipControl_Move;
        public InputAction @Rotate => m_Wrapper.m_ShipControl_Rotate;
        public InputAction @RotateTapPositive => m_Wrapper.m_ShipControl_RotateTapPositive;
        public InputAction @RotateTapNegative => m_Wrapper.m_ShipControl_RotateTapNegative;
        public InputActionMap Get() { return m_Wrapper.m_ShipControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShipControlActions set) { return set.Get(); }
        public void SetCallbacks(IShipControlActions instance)
        {
            if (m_Wrapper.m_ShipControlActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnMove;
                @Rotate.started -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnRotate;
                @RotateTapPositive.started -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnRotateTapPositive;
                @RotateTapPositive.performed -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnRotateTapPositive;
                @RotateTapPositive.canceled -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnRotateTapPositive;
                @RotateTapNegative.started -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnRotateTapNegative;
                @RotateTapNegative.performed -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnRotateTapNegative;
                @RotateTapNegative.canceled -= m_Wrapper.m_ShipControlActionsCallbackInterface.OnRotateTapNegative;
            }
            m_Wrapper.m_ShipControlActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @RotateTapPositive.started += instance.OnRotateTapPositive;
                @RotateTapPositive.performed += instance.OnRotateTapPositive;
                @RotateTapPositive.canceled += instance.OnRotateTapPositive;
                @RotateTapNegative.started += instance.OnRotateTapNegative;
                @RotateTapNegative.performed += instance.OnRotateTapNegative;
                @RotateTapNegative.canceled += instance.OnRotateTapNegative;
            }
        }
    }
    public ShipControlActions @ShipControl => new ShipControlActions(this);
    public interface IShipControlActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnRotateTapPositive(InputAction.CallbackContext context);
        void OnRotateTapNegative(InputAction.CallbackContext context);
    }
}
