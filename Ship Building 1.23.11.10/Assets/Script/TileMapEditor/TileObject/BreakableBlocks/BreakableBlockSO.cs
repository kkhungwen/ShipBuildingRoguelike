using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BreakableBlockSO : TileObjectSO
{
    public override object CreateTileObjectData()
    {
        BreakableBlockTypeSO breakableBlockType = tileObjectType as BreakableBlockTypeSO;

        BreakableBlockData breakableBlockData = new BreakableBlockData(tileObjectType, gridPositionUnityCordinate, orientation, breakableBlockType.prefab, breakableBlockType.defaultSprite, breakableBlockType.isSpriteRuleTile, breakableBlockType.spriteRuleTile);

        return breakableBlockData;
    }
}
