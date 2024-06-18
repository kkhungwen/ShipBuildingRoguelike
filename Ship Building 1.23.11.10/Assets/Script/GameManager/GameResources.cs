using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources : MonoBehaviour
{
    private static GameResources instance;  
    public static GameResources Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameResources>("GameResources");
            }
            return instance;
        }
    }

    [Space(10f)]
    [Header("SHIP PART")]
    public AttributeTypeListSO shipAttributeTypeList;


    #region Valiadation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullReference(this, nameof(shipAttributeTypeList), shipAttributeTypeList);
    }

#endif
    #endregion
}
