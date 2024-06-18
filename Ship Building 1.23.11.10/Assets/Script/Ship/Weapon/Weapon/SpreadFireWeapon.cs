using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(WeaponFiredEvent))]
[RequireComponent(typeof(InitiateFirableEvent))]
public class SpreadFireWeapon : MonoBehaviour
{
    [SerializeField] private GameObject firablePrefab;
    [SerializeField] private Vector2 firePosition;
    [SerializeField] private int fireAmount;
    [SerializeField] private float spreadAngle;

    private Weapon weapon;

    private Coroutine fireWeaponRoutine;

    private void Awake()
    {
        weapon = GetComponent<Weapon>();

        weapon.fireWeaponEvent.OnFireWeapon += FireWeaponEvent_OnFireWeapon;
    }

    private void FireWeaponEvent_OnFireWeapon(FireWeaponEventArgs fireWeaponEventArgs)
    {
        if (fireWeaponRoutine != null)
            StopCoroutine(fireWeaponRoutine);

        fireWeaponRoutine = StartCoroutine(FireWeaponRoutine());
    }

    private IEnumerator FireWeaponRoutine()
    {
        float aimAngle = HelperUtilities.GetAngleFromVector(this.transform.TransformDirection(Vector3.right));

        float startAngle = aimAngle - ((float)fireAmount - 1) / 2 * spreadAngle;

        for (int fireCount = 0; fireCount < fireAmount; fireCount++)
        {
            float fireAngle = startAngle + (fireCount * spreadAngle);

            Vector3 fireDirection = HelperUtilities.GetDirectionVectorFromAngle(fireAngle);

            InitiateFirable(fireDirection, fireAngle);
        }

        weapon.initiateFirableEvent.CallInitiateFirableEvent(this.transform.TransformPoint(firePosition), aimAngle);

        yield return null;

        weapon.weaponFiredEvent.CallWeaponFiredEvent();
    }

    private void InitiateFirable(Vector3 fireDirection, float fireAngle)
    {
        IFirable firable = (IFirable)ObjectPoolManager.Instance.GetComponentFromPool(firablePrefab);

        firable.FireInitialize(this.transform.TransformPoint(firePosition), fireDirection, fireAngle, this.transform);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullReference(this, nameof(firablePrefab), firablePrefab);
    }
#endif
    #endregion
}
