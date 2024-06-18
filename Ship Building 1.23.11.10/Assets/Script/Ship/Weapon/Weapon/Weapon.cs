using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[DisallowMultipleComponent]
[RequireComponent(typeof(ActivateWeaponEvent))]
[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(WeaponFiredEvent))]
[RequireComponent(typeof(ChargeWeaponEvent))]
[RequireComponent(typeof(InitiateFirableEvent))]
public class Weapon : MonoBehaviour 
{
    public event Action OnInitialize;

    [HideInInspector] public ActivateWeaponEvent activateWeaponEvent;
    [HideInInspector] public FireWeaponEvent fireWeaponEvent;
    [HideInInspector] public ChargeWeaponEvent chargeWeaponEvent;
    [HideInInspector] public WeaponFiredEvent weaponFiredEvent;
    [HideInInspector] public InitiateFirableEvent initiateFirableEvent;

    private ShipPart shipPart;



    private void Awake()
    {
        shipPart = GetComponentInParent<ShipPart>();
        activateWeaponEvent = GetComponent<ActivateWeaponEvent>();
        fireWeaponEvent = GetComponent<FireWeaponEvent>();
        chargeWeaponEvent = GetComponent<ChargeWeaponEvent>();
        weaponFiredEvent = GetComponent<WeaponFiredEvent>();
        initiateFirableEvent = GetComponent<InitiateFirableEvent>();

        if (shipPart == null)
            Debug.Log(this + " weapon does not contain ship part component in parent");
        shipPart.OnInitialize += ShipPart_OnInitialize;
    }

    private void ShipPart_OnInitialize()
    {
        OnInitialize?.Invoke();

        activateWeaponEvent.CallActivateWeapon(true);
    }


    public float GetAttributeValue(AttributeTypeSO attributeType)
    {
        return shipPart.GetAttributeValue(attributeType);
    }
}
