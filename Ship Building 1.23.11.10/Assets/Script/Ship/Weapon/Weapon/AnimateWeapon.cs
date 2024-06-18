using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]

public class AnimateWeapon : MonoBehaviour
{
    private Animator animator;
    private Weapon weapon;

    private void Awake()
    {
        weapon = GetComponentInParent<Weapon>();
        if (weapon == null)
            Debug.Log("Cannot get Weapon component in parent");
        animator = GetComponent<Animator>();

        weapon.activateWeaponEvent.OnAcctivateWeapon += ActivateWeaponEvent_OnAcctivateWeapon;
        weapon.fireWeaponEvent.OnFireWeapon += FireWeaponEvent_OnFireWeapon;
        weapon.chargeWeaponEvent.OnChargeWeapon += ChargeWeaponEvent_OnChargeWeapon;
        weapon.weaponFiredEvent.OnWeaponFired += WeaponFiredEvent_OnWeaponFired;
    }


    private void ActivateWeaponEvent_OnAcctivateWeapon(ActivateWeaponEventArgs activateWeaponEventArgs)
    {
        if (activateWeaponEventArgs.isActivate)
        {
            ResetAnitmatorParameters();
            animator.SetBool("isActivate", true);
        }
        else
        {
            ResetAnitmatorParameters();
            animator.SetBool("isActivate", false);
        }
    }

    private void FireWeaponEvent_OnFireWeapon(FireWeaponEventArgs fireWeaponEventArgs)
    {
        ResetAnitmatorParameters();
        animator.SetBool("isActivate", true);
        animator.SetBool("isFire", true);
    }

    private void ChargeWeaponEvent_OnChargeWeapon(ChargeWeaponEventArgs chargeWeaponEventArgs)
    {
        ResetAnitmatorParameters();
        animator.SetBool("isActivate", true);
        animator.SetBool("isCharge", true);
    }

    private void WeaponFiredEvent_OnWeaponFired(WeaponFiredEventArgs weaponFiredEventArgs)
    {
        ResetAnitmatorParameters();
        animator.SetBool("isActivate", true);
    }

    private void ResetAnitmatorParameters()
    {
        animator.SetBool("isActivate", false);
        animator.SetBool("isFire", false);
        animator.SetBool("isCharge", false);
    }
}
