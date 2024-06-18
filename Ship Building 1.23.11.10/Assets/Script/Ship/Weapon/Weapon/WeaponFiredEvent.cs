using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class WeaponFiredEvent : MonoBehaviour
{
    public event Action<WeaponFiredEventArgs> OnWeaponFired;

    public void CallWeaponFiredEvent()
    {
        OnWeaponFired?.Invoke(new WeaponFiredEventArgs());
    }
}

public class WeaponFiredEventArgs : EventArgs
{

}
