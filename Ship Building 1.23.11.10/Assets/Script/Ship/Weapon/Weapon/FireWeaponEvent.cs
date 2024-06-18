using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
public class FireWeaponEvent : MonoBehaviour
{
    public event Action<FireWeaponEventArgs> OnFireWeapon;

    public void CallFireWeaponEvent()
    {
        OnFireWeapon?.Invoke(new FireWeaponEventArgs());
    }
}

public class FireWeaponEventArgs : EventArgs
{

}
