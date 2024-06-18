using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    /// <summary>
    /// Input actions events
    /// </summary>

    // Ship Control Map
    public event EventHandler<OnMoveActionEventArgs> OnShipControl_Move_Performed;
    public class OnMoveActionEventArgs : EventArgs
    {
        public Vector2 value;
    }
    public event EventHandler OnShipControl_Move_Cancled;


    public event EventHandler<OnRotateActionEventArgs> OnShipControl_Rotate_Performed;
    public class OnRotateActionEventArgs : EventArgs
    {
        public float value;
    }
    public event EventHandler OnShipControl_Rotate_Cancled;
    public event EventHandler OnShipControl_RotateTapPositive_Performed;
    public event EventHandler OnShipControl_RotateTapNegative_Performed;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        
        // Ship Control Map
        playerInputActions.ShipControl.Move.performed += ShipControl_Move_Performed;
        playerInputActions.ShipControl.Move.canceled += ShipControl_Move_Cancled;
        playerInputActions.ShipControl.Rotate.performed += ShipControl_Rotate_Performed;
        playerInputActions.ShipControl.Rotate.canceled += ShipControl_Rotate_canceled;
        playerInputActions.ShipControl.RotateTapPositive.performed += ShipControl_RotateTapPositive_performed;
        playerInputActions.ShipControl.RotateTapNegative.performed += ShipControl_RotateTapNegative_performed;
    }


    private void Start()
    {
        SetShipControlMapEnable(true);
    }

    /// <summary>
    /// Calling action events
    /// </summary>
    
    // Ship Control Map
    private void ShipControl_Move_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShipControl_Move_Performed?.Invoke(this, new OnMoveActionEventArgs() { value = obj.ReadValue<Vector2>()});
    }
    private void ShipControl_Move_Cancled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShipControl_Move_Cancled?.Invoke(this,EventArgs.Empty);
    }
    private void ShipControl_Rotate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShipControl_Rotate_Performed?.Invoke(this, new OnRotateActionEventArgs() { value = obj.ReadValue<float>()});
    }
    private void ShipControl_Rotate_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShipControl_Rotate_Cancled?.Invoke(this, EventArgs.Empty);
    }
    private void ShipControl_RotateTapPositive_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShipControl_RotateTapPositive_Performed?.Invoke(this, EventArgs.Empty);
    }
    private void ShipControl_RotateTapNegative_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShipControl_RotateTapNegative_Performed?.Invoke(this, EventArgs.Empty);
    }


    /// <summary>
    /// Get action values
    /// </summary>

    /// Ship Control Map
    public Vector2 GetValueShipControl_Move()
    {
        return playerInputActions.ShipControl.Move.ReadValue<Vector2>();
    }

    public float GetValueShipControl_Rotate()
    {
        return playerInputActions.ShipControl.Rotate.ReadValue<float>();
    }


    /// <summary>
    /// Set actionmap enabled
    /// </summary>
    public void SetShipControlMapEnable(bool isEnable)
    {
        if (isEnable)
            playerInputActions.ShipControl.Enable();
        else
            playerInputActions.ShipControl.Disable();
    }
}
