using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region RQUIER COMPONENTS
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
#endregion REQUIER COMPONENTS
public class PlayerShipControllerCurve : MonoBehaviour
{
    [SerializeField] private GameInput gameInput;
    private Rigidbody2D rigidBody2D;

    [SerializeField] private AnimationCurve moveSpeedCurveInput;
    [SerializeField] private AnimationCurve moveSpeedCurveNoInput;
    [SerializeField] private float moveMaxSpeed;
    [SerializeField] private float timeMinToMaxSpeed;

    private Vector2 velocity;
    private float timer;


    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        // subscribed input
        gameInput.OnShipControl_Move_Performed += GameInput_OnShipControl_Move_Performed;
        gameInput.OnShipControl_Move_Cancled += GameInput_OnShipControl_Move_Cancled;
        gameInput.OnShipControl_Rotate_Performed += GameInput_OnShipControl_Rotate_Performed;
        gameInput.OnShipControl_Rotate_Cancled += GameInput_OnShipControl_Rotate_Cancled;

    }

    private void OnDisable()
    {
        // unsubscribed input
        gameInput.OnShipControl_Move_Performed -= GameInput_OnShipControl_Move_Performed;
        gameInput.OnShipControl_Move_Cancled -= GameInput_OnShipControl_Move_Cancled;
        gameInput.OnShipControl_Rotate_Performed -= GameInput_OnShipControl_Rotate_Performed;
        gameInput.OnShipControl_Rotate_Cancled -= GameInput_OnShipControl_Rotate_Cancled;

    }


    private void GameInput_OnShipControl_Move_Performed(object sender, GameInput.OnMoveActionEventArgs e)
    {

    }
    private void GameInput_OnShipControl_Move_Cancled(object sender, System.EventArgs e)
    {

    }
    private void GameInput_OnShipControl_Rotate_Performed(object sender, GameInput.OnRotateActionEventArgs e)
    {

    }
    private void GameInput_OnShipControl_Rotate_Cancled(object sender, System.EventArgs e)
    {

    }
}
