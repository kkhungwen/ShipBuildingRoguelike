using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Weapon))]
public class ChargeWeaponTimer : MonoBehaviour
{
    [SerializeField] private float chargeTime;

    private Weapon weapon;

    private WaitForSeconds waitForChargeTime;

    private Coroutine chargeWeaponRoutine;

    private void Awake()
    {
        weapon = GetComponent<Weapon>();

        weapon.chargeWeaponEvent.OnChargeWeapon += ChargeWeaponEvent_OnChargeWeapon;

        waitForChargeTime = new WaitForSeconds(chargeTime);
    }

    private void ChargeWeaponEvent_OnChargeWeapon(ChargeWeaponEventArgs chargeWeaponEventArgs)
    {
        if(chargeWeaponRoutine != null)
            StopCoroutine(chargeWeaponRoutine);

        chargeWeaponRoutine = StartCoroutine(ChargeWeaponRoutine());
    }

    private IEnumerator ChargeWeaponRoutine()
    {
        yield return waitForChargeTime;

        weapon.fireWeaponEvent.CallFireWeaponEvent();
    }
}
