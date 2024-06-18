using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipPartType_", menuName = "Scriptable Objects/Custom Tile Map Editor/Tile Object Type/Ship Part")]
public class ShipPartTypeSO : TileObjectTypeSO
{
    [Space(10f)]
    [Header("SHIP PART TYPE")]

    public GameObject prefab;

    public Sprite iconSprite;

    public List<AttributeData> attributeDataList = new List<AttributeData> ();

    #region Editor Code
#if UNITY_EDITOR
    public override TileObjectSO CreateTileObject()
    {
        TileObjectSO tileObject = ScriptableObject.CreateInstance<ShipPartSO>();
        return tileObject;
    }
#endif
    #endregion

    #region Validation
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        HelperUtilities.ValidateCheckNullReference(this, nameof(prefab), prefab);
        HelperUtilities.ValidateCheckNullReference(this, nameof(iconSprite), iconSprite);

        CheckIfAttributeDataListContainsShipPartAttribute();
        CheckDuplicateAttributeData();
        ValidateAttributeDataList();
    }

    private void CheckIfAttributeDataListContainsShipPartAttribute()
    {
        foreach(AttributeTypeSO attributeType in GameResources.Instance.shipAttributeTypeList.list)
        {
            AttributeData attributeData = attributeDataList.Find(x => x.attributeType == attributeType);
            if(attributeData == null)
            {
                attributeDataList.Add(new AttributeData() { attributeType = attributeType });
            }
        }

        foreach(AttributeData attributeData in attributeDataList)
        {
            if (!GameResources.Instance.shipAttributeTypeList.list.Contains(attributeData.attributeType))
            {
                attributeDataList.Remove(attributeData);
            }
        }
    }

    private void CheckDuplicateAttributeData()
    {
        List<AttributeTypeSO> checkList = new List<AttributeTypeSO>();
        foreach (AttributeData attributeData in attributeDataList)
        {
            if (checkList.Contains(attributeData.attributeType))
            {
                Debug.Log(this + "attributeDataList cantains duplicate attributes");
            }

            checkList.Add(attributeData.attributeType);
        }
    }

    private void ValidateAttributeDataList()
    {
        foreach(AttributeData attributeData in attributeDataList)
        {
            attributeData.ValidateLevel_ValueCurve();
        }
    }
#endif
    #endregion
}
