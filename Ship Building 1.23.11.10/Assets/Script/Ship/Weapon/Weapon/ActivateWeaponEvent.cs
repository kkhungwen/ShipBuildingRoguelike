using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class ActivateWeaponEvent : MonoBehaviour
{
    public event Action<ActivateWeaponEventArgs> OnAcctivateWeapon;

    public void CallActivateWeapon(bool isActivate)
    {
        OnAcctivateWeapon?.Invoke(new ActivateWeaponEventArgs { isActivate = isActivate });
    }
}

public class ActivateWeaponEventArgs : EventArgs
{
    public bool isActivate;
}
