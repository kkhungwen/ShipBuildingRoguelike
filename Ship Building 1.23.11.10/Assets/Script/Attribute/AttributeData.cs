using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AttributeData 
{
    public AttributeTypeSO attributeType;

    public float baseLevelValue;

    public float maxLevelValue;

    public AnimationCurve normalized_ValueCurve = new AnimationCurve();

    public void ValidateLevel_ValueCurve()
    {
        if(normalized_ValueCurve.length < 2)
        {
            normalized_ValueCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 0), new Keyframe(1, 1) });
        }

        Keyframe startKey = normalized_ValueCurve.keys[0];
        startKey.time = 0;
        startKey.value = baseLevelValue;
        normalized_ValueCurve.MoveKey(0, startKey);

        Keyframe endKey = normalized_ValueCurve.keys[normalized_ValueCurve.keys.Length - 1];
        endKey.time = 1;
        endKey.value = maxLevelValue;
        normalized_ValueCurve.MoveKey(normalized_ValueCurve.keys.Length - 1, endKey);
    }
}
