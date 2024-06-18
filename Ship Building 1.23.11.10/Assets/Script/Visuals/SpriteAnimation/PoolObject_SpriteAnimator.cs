using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteAnimator))]
public class PoolObject_SpriteAnimator : MonoBehaviour
{
    private GameObject poolKey;

    private SpriteAnimator spriteAnimator;

    private void Awake()
    {
        spriteAnimator = GetComponent<SpriteAnimator>();

        spriteAnimator.OnAnimationFinished += SpriteAnimator_OnAnimationFinished;
    }

    public void SetPositionAngle(Vector3 position, float angle)
    {
        transform.position = position;

        Vector3 eulerAngle = new Vector3(0, 0, angle);

        transform.eulerAngles = eulerAngle;
    }

    public void PlayEffect(SpriteAnimationSO spriteAnimationSO, GameObject poolKey)
    {
        this.poolKey = poolKey;

        gameObject.SetActive(true);

        spriteAnimator.PlayAnimation(spriteAnimationSO);
    }

    private void SpriteAnimator_OnAnimationFinished()
    {
        gameObject.SetActive(false);
        //Release object to pool
        ObjectPoolManager.Instance.ReleaseComponentToPool(poolKey, this);
    }
}
