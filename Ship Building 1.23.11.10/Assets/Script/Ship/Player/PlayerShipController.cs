using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region RQUIER COMPONENTS
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody2D))]
#endregion REQUIER COMPONENTS
public class PlayerShipController : MonoBehaviour
{
    [Header("REFERANCE")]
    [SerializeField] private GameInput gameInput;
    private Rigidbody2D rigidBody2D;

    [Space(10f)]
    [Header("MOVE")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveVectorLerpInput;
    [SerializeField] private float moveVectorLerp;
    [SerializeField] private float moveLerpDistanceMin;
    private bool isMoveInput = false;

    [Space(10f)]
    [Header("ROTATE")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float rotateVectorLerpInput;
    [SerializeField] private float rotateVectorLerp;
    [SerializeField] private float rotateLerpDistanceMin;
    private bool isRotateInput = false;

    [Space(10f)]
    [Header("ROTATE TAP")]
    [SerializeField] private float rotateTapStopAngularVelocityMin;

    [Space(10f)]
    [Header("DEBUG")]
    [SerializeField] private bool debug = false;
    [SerializeField] private Vector2 moveVector;
    [SerializeField] private float rotateDirection;



    private void Awake()
    {
        // load compoments
        rigidBody2D = GetComponent<Rigidbody2D>();

        //initialize parameters
        Initialize();
    }

    private void OnEnable()
    {
        // subscribed input
        gameInput.OnShipControl_Move_Performed += GameInput_OnShipControl_Move_Performed;
        gameInput.OnShipControl_Move_Cancled += GameInput_OnShipControl_Move_Cancled;
        gameInput.OnShipControl_Rotate_Performed += GameInput_OnShipControl_Rotate_Performed;
        gameInput.OnShipControl_Rotate_Cancled += GameInput_OnShipControl_Rotate_Cancled;
        gameInput.OnShipControl_RotateTapPositive_Performed += GameInput_OnShipControl_RotateTapPositive_Performed;
        gameInput.OnShipControl_RotateTapNegative_Performed += GameInput_OnShipControl_RotateTapNegative_Performed;
    }

    private void OnDisable()
    {
        // unsubscribed input
        gameInput.OnShipControl_Move_Performed -= GameInput_OnShipControl_Move_Performed;
        gameInput.OnShipControl_Move_Cancled -= GameInput_OnShipControl_Move_Cancled;
        gameInput.OnShipControl_Rotate_Performed -= GameInput_OnShipControl_Rotate_Performed;
        gameInput.OnShipControl_Rotate_Cancled -= GameInput_OnShipControl_Rotate_Cancled;
        gameInput.OnShipControl_RotateTapPositive_Performed -= GameInput_OnShipControl_RotateTapPositive_Performed;
        gameInput.OnShipControl_RotateTapNegative_Performed -= GameInput_OnShipControl_RotateTapNegative_Performed;
    }


    private void GameInput_OnShipControl_Move_Performed(object sender, GameInput.OnMoveActionEventArgs e)
    {
        isMoveInput = true;
    }
    private void GameInput_OnShipControl_Move_Cancled(object sender, System.EventArgs e)
    {
        isMoveInput = false;
    }
    private void GameInput_OnShipControl_Rotate_Performed(object sender, GameInput.OnRotateActionEventArgs e)
    {
        isRotateInput = true;
    }
    private void GameInput_OnShipControl_Rotate_Cancled(object sender, System.EventArgs e)
    {
        isRotateInput = false;
    }
    private void GameInput_OnShipControl_RotateTapPositive_Performed(object sender, System.EventArgs e)
    {
        HandleRotateTap(true);
    }
    private void GameInput_OnShipControl_RotateTapNegative_Performed(object sender, System.EventArgs e)
    {
        HandleRotateTap(false);
    }


    private void FixedUpdate()
    {
        HandleMove();
        HandleRotate();
    }


    private void HandleMove()
    {
        // DEBUG
        if (debug)
        {
            if (moveVector != Vector2.zero)
            {
                Vector2 inputVelocity = moveVector * moveSpeed;

                if (Vector2.Distance(inputVelocity, rigidBody2D.velocity) > moveLerpDistanceMin)
                {
                    rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, inputVelocity, moveVectorLerpInput);
                }
                else
                {
                    rigidBody2D.velocity = inputVelocity;
                }
            }
            else
            {
                if (Vector2.Distance(Vector2.zero, rigidBody2D.velocity) > moveLerpDistanceMin)
                {
                    rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, Vector2.zero, moveVectorLerp);
                }
                else
                {
                    rigidBody2D.velocity = Vector2.zero;
                }
            }
            return;
        }
        // DEBUG

        if (isMoveInput)
        {
            Vector2 inputVelocity = gameInput.GetValueShipControl_Move() * moveSpeed;

            if (Vector2.Distance(inputVelocity, rigidBody2D.velocity) > moveLerpDistanceMin)
            {
                rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, inputVelocity, moveVectorLerpInput);
            }
            else
            {
                rigidBody2D.velocity = inputVelocity;
            }
        }
        else
        {
            if (Vector2.Distance(Vector2.zero, rigidBody2D.velocity) > moveLerpDistanceMin)
            {
                rigidBody2D.velocity = Vector2.Lerp(rigidBody2D.velocity, Vector2.zero, moveVectorLerp);
            }
            else
            {
                rigidBody2D.velocity = Vector2.zero;
            }
        }
    }


    private void HandleRotate()
    {
        // DEBUG
        if (debug)
        {
            if (rotateDirection != 0)
            {
                float inputVelocity = -rotateDirection * rotateSpeed;

                if (Mathf.Abs(inputVelocity - rigidBody2D.angularVelocity) > rotateLerpDistanceMin)
                {
                    rigidBody2D.angularVelocity = Mathf.Lerp(rigidBody2D.angularVelocity, inputVelocity, rotateVectorLerpInput);
                }
                else
                {
                    rigidBody2D.angularVelocity = inputVelocity;
                }
            }
            else
            {
                if (Mathf.Abs(0 - rigidBody2D.angularVelocity) > rotateLerpDistanceMin)
                {
                    rigidBody2D.angularVelocity = Mathf.Lerp(rigidBody2D.angularVelocity, 0, rotateVectorLerp);
                }
                else
                {
                    rigidBody2D.angularVelocity = 0;
                }
            }

            return;
        }
        // DEBUG

        if (isRotateInput)
        {
            float inputVelocity = -gameInput.GetValueShipControl_Rotate() * rotateSpeed;

            if (Mathf.Abs(inputVelocity - rigidBody2D.angularVelocity) > rotateLerpDistanceMin)
            {
                rigidBody2D.angularVelocity = Mathf.Lerp(rigidBody2D.angularVelocity, inputVelocity, rotateVectorLerpInput);
            }
            else
            {
                rigidBody2D.angularVelocity = inputVelocity;
            }
        }
        else
        {
            if (Mathf.Abs(0 - rigidBody2D.angularVelocity) > rotateLerpDistanceMin)
            {
                rigidBody2D.angularVelocity = Mathf.Lerp(rigidBody2D.angularVelocity, 0, rotateVectorLerp);
            }
            else
            {
                rigidBody2D.angularVelocity = 0;
            }
        }
    }

    private void HandleRotateTap(bool isPositive)
    {
        if (isPositive)
        {
            if(rigidBody2D.angularVelocity > rotateTapStopAngularVelocityMin)
            {
                rigidBody2D.angularVelocity = 0;
            }
        }
        else
        {
            if (rigidBody2D.angularVelocity < -rotateTapStopAngularVelocityMin)
            {
                rigidBody2D.angularVelocity = 0;
            }
        }
    }


    private void Initialize()
    {
        rigidBody2D.velocity = Vector2.zero;
        rigidBody2D.angularVelocity = 0;

        isMoveInput = false;
        isRotateInput = false;
    }
}
