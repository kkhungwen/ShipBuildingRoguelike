using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteAnimator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Firable_RapidGun : MonoBehaviour, IFirable
{
    private Vector3 eularAngles = Vector3.zero;

    [Space(10f)]
    [Header("BEHAVIOUR PROPERTIES")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    private Vector2 directionVector;

    [Space(10f)]
    [Header("ANIMATION")]
    [SerializeField] private SpriteAnimationSO firableAnimationSO;
    [SerializeField] private SpriteAnimationSO vFX_fireableDestroyAnimation;
    [SerializeField] private GameObject vFX_SpriteAnimatorPrefab;
    private SpriteAnimator spriteAnimator;

    private void Awake()
    {
        spriteAnimator = GetComponent<SpriteAnimator>();
    }

    private void Update()
    {
        Vector3 distanceVector = directionVector * speed * Time.deltaTime;

        transform.position += distanceVector;

        range -= distanceVector.magnitude;

        if (range < 0f)
        {
            DisableAmmo();
        }
    }

    public void FireInitialize(Vector3 startPosition, Vector3 directionVector, float angle, Transform parentTranform)
    {
        transform.position = startPosition;
        this.directionVector = directionVector.normalized;
        eularAngles.z = angle;

        transform.eulerAngles = eularAngles;

        gameObject.SetActive(true);

        spriteAnimator.PlayAnimation(firableAnimationSO);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DisableAmmo();
    }

    private void DisableAmmo()
    {
        PoolObject_SpriteAnimator vFX_SpriteAnimator =  (PoolObject_SpriteAnimator)ObjectPoolManager.Instance.GetComponentFromPool(vFX_SpriteAnimatorPrefab);
        vFX_SpriteAnimator.SetPositionAngle(transform.position, 0);
        vFX_SpriteAnimator.PlayEffect(vFX_fireableDestroyAnimation,vFX_SpriteAnimatorPrefab);

        if (!gameObject.activeSelf)
            return;
        gameObject.SetActive(false);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullReference(this, nameof(firableAnimationSO), firableAnimationSO);
    }
#endif
    #endregion
}
