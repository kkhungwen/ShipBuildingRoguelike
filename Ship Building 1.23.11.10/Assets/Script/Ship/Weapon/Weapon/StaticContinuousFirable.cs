using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider2D))]
public class StaticContinuousFirable : MonoBehaviour
{
    private Collider2D damageCollider;
    private Weapon weapon;

    private WaitForSeconds waitForContinuousDamageTick = new WaitForSeconds(Settings.continuousDamageTick);
    private Coroutine continuousDamageRoutine;
    private ContactFilter2D contactFilter = new ContactFilter2D();
    private List<Collider2D> collideList = new List<Collider2D>();

    private void Awake()
    {
        damageCollider = GetComponent<Collider2D>();
        weapon = GetComponentInParent<Weapon>();
        if (weapon == null)
            Debug.Log("Cannot get Weapon component in parent");

        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(damageCollider.gameObject.layer));
        contactFilter.useLayerMask = true;

        weapon.activateWeaponEvent.OnAcctivateWeapon += ActivateWeaponEvent_OnAcctivateWeapon;
    }

    private void ActivateWeaponEvent_OnAcctivateWeapon(ActivateWeaponEventArgs activateWeaponEventArgs)
    {
        if (activateWeaponEventArgs.isActivate)
        {
            if(continuousDamageRoutine!=null)
                StopCoroutine(continuousDamageRoutine);

            continuousDamageRoutine = StartCoroutine(DealContinuousDamageRoutine());
        }
        else
        {
            StopCoroutine(continuousDamageRoutine);
        }
    }

    private IEnumerator DealContinuousDamageRoutine()
    {
        while (true)
        {
            DealDamage();
            yield return waitForContinuousDamageTick;
        }
    }

    private void DealDamage()
    {
        Physics2D.OverlapCollider(damageCollider, contactFilter, collideList);
    }
}
