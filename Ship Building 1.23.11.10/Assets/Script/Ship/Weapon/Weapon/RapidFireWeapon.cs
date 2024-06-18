using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(WeaponFiredEvent))]
[RequireComponent(typeof(InitiateFirableEvent))]
public class RapidFireWeapon : MonoBehaviour
{
    [SerializeField] private GameObject firablePrefab;

    [SerializeField] private Vector2 firePosition;
    [SerializeField] private int fireAmount;
    [SerializeField] private float fireInterval;

    private Weapon weapon;

    private Coroutine fireWeaponRoutine;

    private void Awake()
    {
        weapon = GetComponent<Weapon>();

        weapon.fireWeaponEvent.OnFireWeapon += FireWeaponEvent_OnFireWeapon;
    }

    private void FireWeaponEvent_OnFireWeapon(FireWeaponEventArgs fireWeaponEventArgs)
    {
        fireWeaponRoutine = StartCoroutine(FireWeaponRoutine());
    }

    private IEnumerator FireWeaponRoutine()
    {
        int amountCount = 0;
        float intervalCount = fireInterval;

        while (amountCount < fireAmount)
        {
            if(intervalCount <= 0)
            {
                InitializeFirable();

                intervalCount = fireInterval;
                amountCount++;

                yield return null;
            }
            else
            {
                intervalCount -= Time.deltaTime;

                yield return null;
            }
        }

        weapon.weaponFiredEvent.CallWeaponFiredEvent();
    }


    private void InitializeFirable()
    {
        float fireAngle = HelperUtilities.GetAngleFromVector(this.transform.TransformDirection(Vector3.right));

        IFirable firable = (IFirable)ObjectPoolManager.Instance.GetComponentFromPool(firablePrefab);

        firable.FireInitialize(this.transform.TransformPoint(firePosition), this.transform.TransformDirection(Vector3.right), fireAngle, this.transform);

        weapon.initiateFirableEvent.CallInitiateFirableEvent(this.transform.TransformPoint(firePosition), fireAngle);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(fireAmount), fireAmount, false);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(fireInterval), fireInterval, false);
    }
#endif
    #endregion
}
