using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(WeaponFiredEvent))]
[RequireComponent(typeof(ActivateWeaponEvent))]
public class CoolDownTimer : MonoBehaviour
{
    [SerializeField] AttributeTypeSO coolDownAttributeType;
    [SerializeField] bool needCharge;

    private Weapon weapon;

    private float timeCount;
    private bool isFired;
    private bool isActivate;
    

    private void Awake()
    {
        weapon = GetComponent<Weapon>();

        weapon.activateWeaponEvent.OnAcctivateWeapon += ActivateWeaponEvent_OnAcctivateWeapon;
        weapon.weaponFiredEvent.OnWeaponFired+= WeaponFiredEvent_OnWeaponFired;
    }

    private void ActivateWeaponEvent_OnAcctivateWeapon(ActivateWeaponEventArgs activateWeaponEventArgs)
    {
        isActivate = activateWeaponEventArgs.isActivate;
        ResetTimer();
    }

    private void WeaponFiredEvent_OnWeaponFired(WeaponFiredEventArgs obj)
    {
        ResetTimer();
    }

    private void Update()
    {
        if(isActivate)
            CountTimer();
    }

    private void CountTimer()
    {
        timeCount -= Time.deltaTime;

        if (timeCount <= 0 && IsAbleToFire())
        {
            isFired = true;

            if(needCharge)
                weapon.chargeWeaponEvent.CallChargeWeaponEvent();

            else
                weapon.fireWeaponEvent.CallFireWeaponEvent();
        }
    }

    private bool IsAbleToFire()
    {
        if (isFired)
            return false;

        return true;
    }

    public void ResetTimer()
    {
        timeCount = weapon.GetAttributeValue(coolDownAttributeType);
        isFired = false;
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullReference(this, nameof(coolDownAttributeType), coolDownAttributeType);
    }
#endif
    #endregion
}
