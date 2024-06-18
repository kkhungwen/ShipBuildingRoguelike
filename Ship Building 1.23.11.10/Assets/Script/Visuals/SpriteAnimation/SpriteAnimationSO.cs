using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteAnimationSO_", menuName = "Scriptable Objects/VFX/Sprite Animation SO")]
public class SpriteAnimationSO : ScriptableObject
{
    public Sprite[] frameArray;
    public Material material;
    public float secPerFrame;
    public bool isLoop;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(frameArray), frameArray);
        HelperUtilities.ValidateCheckPositiveValue(this, nameof(secPerFrame), secPerFrame, false);
        HelperUtilities.ValidateCheckNullReference(this, nameof(material), material);
    }
#endif
    #endregion
}
