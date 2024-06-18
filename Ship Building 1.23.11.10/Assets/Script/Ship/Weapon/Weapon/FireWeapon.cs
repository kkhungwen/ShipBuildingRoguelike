using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(FireWeaponEvent))]
[RequireComponent(typeof(WeaponFiredEvent))]
[RequireComponent(typeof(InitiateFirableEvent))]
public class FireWeapon : MonoBehaviour
{
    [SerializeField] private GameObject firablePrefab;
    [SerializeField] private Vector2 firePosition;
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
        float fireAngle = HelperUtilities.GetAngleFromVector(this.transform.TransformDirection(Vector3.right));

        IFirable firable = (IFirable)ObjectPoolManager.Instance.GetComponentFromPool(firablePrefab);

        firable.FireInitialize(this.transform.TransformPoint(firePosition), this.transform.TransformDirection(Vector3.right), fireAngle, this.transform);

        weapon.initiateFirableEvent.CallInitiateFirableEvent(this.transform.TransformPoint(firePosition), fireAngle);

        yield return null;

        weapon.weaponFiredEvent.CallWeaponFiredEvent();
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
