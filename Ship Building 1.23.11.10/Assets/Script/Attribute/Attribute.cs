using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute 
{
    public Attribute(AnimationCurve normalized_ValueCurve)
    {
        LoadLevel_ValueCurve(normalized_ValueCurve);
    }

    private AnimationCurve level_ValueCurve;

    private void LoadLevel_ValueCurve(AnimationCurve normalized_ValueCurve)
    {
        level_ValueCurve = new AnimationCurve();
        for(int i  = 0; i< normalized_ValueCurve.length; i++)
        {
            Keyframe key = normalized_ValueCurve[i];

            key.time = normalized_ValueCurve[i].time * Settings.shipPartMaxLevel;

            level_ValueCurve.AddKey(key);
        }
    }

    public float GetValue(int level)
    {
        return (level_ValueCurve.Evaluate(level));
    }
}
