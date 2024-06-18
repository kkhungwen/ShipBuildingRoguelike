using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttributeTypeList_", menuName = "Scriptable Objects/Attribute/Attribute Type List")]
public class AttributeTypeListSO : ScriptableObject
{
    public List<AttributeTypeSO> list;

    #region Validation
#if UNITY_EDITOR
    private void OnValidate()
    {
        HelperUtilities.ValidateCheckEnumerableValues(this, nameof(list), list);
        CheckDuplicateListValues();
    }

    private void CheckDuplicateListValues()
    {
        List<AttributeTypeSO> checkList = new List<AttributeTypeSO>();
        foreach (AttributeTypeSO value in list)
        {
            if (checkList.Contains(value))
            {
                Debug.Log(this + "list cantains duplicate values");
            }

            checkList.Add(value);
        }
    }
#endif
    #endregion
}
