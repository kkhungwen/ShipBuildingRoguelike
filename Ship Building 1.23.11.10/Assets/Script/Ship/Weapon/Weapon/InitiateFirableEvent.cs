using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class InitiateFirableEvent : MonoBehaviour
{
    public event Action<InitiateFirableEventArgs> OnInitiateFireable;

    public void CallInitiateFirableEvent(Vector3 worldPosition, float angle)
    {
        OnInitiateFireable?.Invoke(new InitiateFirableEventArgs() { worldPosition = worldPosition, angle = angle});
    }
}

public class InitiateFirableEventArgs : EventArgs
{
    public Vector3 worldPosition;
    public float angle;
}
