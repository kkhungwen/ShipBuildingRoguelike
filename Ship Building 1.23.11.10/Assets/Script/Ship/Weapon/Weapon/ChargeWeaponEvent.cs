using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class ChargeWeaponEvent : MonoBehaviour
{
    public event Action<ChargeWeaponEventArgs> OnChargeWeapon;

    public void CallChargeWeaponEvent()
    {
        OnChargeWeapon?.Invoke(new ChargeWeaponEventArgs { });
    }
}

public class ChargeWeaponEventArgs : EventArgs
{

}
