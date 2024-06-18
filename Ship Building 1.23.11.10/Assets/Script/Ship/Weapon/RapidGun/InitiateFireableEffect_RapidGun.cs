using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InitiateFirableEvent))]
public class InitiateFireableEffect_RapidGun : MonoBehaviour
{
    [SerializeField] private SpriteAnimationSO spriteAnimationSO;
    //prefab as key for object pool
    [SerializeField] private GameObject VFX_SpriteAnimatorPrefab;
    //cache a instance of sprite animator to reduce the garbage colect
    private PoolObject_SpriteAnimator spriteAnimationVFX;
    private InitiateFirableEvent initiateFirableEvent;

    private void Awake()
    {
        initiateFirableEvent = GetComponent<InitiateFirableEvent>();

        initiateFirableEvent.OnInitiateFireable += InitiateFirableEvent_OnInitiateFireable;
    }

    private void InitiateFirableEvent_OnInitiateFireable(InitiateFirableEventArgs initiateFirableEventArgs)
    {
        PlaySpriteAnimation(initiateFirableEventArgs.worldPosition, initiateFirableEventArgs.angle);
    }

    private void PlaySpriteAnimation(Vector3 worldPosition, float angle)
    {
        spriteAnimationVFX = (PoolObject_SpriteAnimator)ObjectPoolManager.Instance.GetComponentFromPool(VFX_SpriteAnimatorPrefab);

        spriteAnimationVFX.SetPositionAngle(worldPosition, angle);

        spriteAnimationVFX.PlayEffect(spriteAnimationSO, VFX_SpriteAnimatorPrefab);
    }

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullReference(this, nameof(spriteAnimationSO), spriteAnimationSO);
        HelperUtilities.ValidateCheckNullReference(this, nameof(VFX_SpriteAnimatorPrefab), VFX_SpriteAnimatorPrefab);
    }
#endif
    #endregion
}
