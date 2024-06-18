using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartSO : TileObjectSO
{
    public override object CreateTileObjectData()
    {
        ShipPartTypeSO shipPartType = tileObjectType as ShipPartTypeSO;

        ShipPartData shipPartData = new ShipPartData(tileObjectType, gridPositionUnityCordinate, orientation, shipPartType.prefab, shipPartType.iconSprite, shipPartType.attributeDataList);

        return shipPartData;
    }
}
