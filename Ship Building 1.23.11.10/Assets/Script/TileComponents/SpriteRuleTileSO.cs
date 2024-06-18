using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SpriteRuleTile_", menuName ="Scriptable Objects/Tile Components/Rule Tile/Sprite Rule Tile")]
public class SpriteRuleTileSO : ScriptableObject
{
    private Vector2Int[] checkPosition = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(1, -1),
        new Vector2Int(0, -1),
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 0),
        new Vector2Int(-1, 1),
        new Vector2Int(0, 1),
        new Vector2Int(1, 1),
    };

    public enum SurroundingTileState
    {
        connectable,
        unconnectable,
        irrelevant,
    }

    [System.Serializable]
    public class RuleSprite
    {
        public RuleSprite(SurroundingTileState[] surroundingTileStateArray)
        {
            rule = surroundingTileStateArray;
        }

        public SurroundingTileState[] rule;
        public Sprite[] spriteArray;
    }

    public RuleSprite[] ruleSpriteArray = new RuleSprite[]
    {
        // 1
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),
        // 2
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),        
        // 3
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),
        // 4
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),
        //5
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),
        //6
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),
        //7
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),
        //8
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),
        //9
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),
        //10
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
        //11
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //12
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //13
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,
        }),
        //14
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.irrelevant,
        }),
        //15
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //16
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //17
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //18
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,
        }),
        //19
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //20
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //21
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //22
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //23
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,
        }),
        //24
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.irrelevant,
        }),
        //25
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
        //26
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
        //27
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //28
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,
        }),
        //29
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
        //30
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
        //31
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
        //32
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),
        //33
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),
        //34
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),
        //35
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,
        }),
        //36
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
        //37
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
        //38
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //39
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,
        }),
        //40
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
        //41
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //42
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
        //43
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
        //44
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
        //45
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.connectable,
        }),
        //46
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.unconnectable,SurroundingTileState.irrelevant,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.irrelevant,
        }),
        //47
        new RuleSprite(new SurroundingTileState[]
        {
            SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,SurroundingTileState.connectable,SurroundingTileState.unconnectable,
        }),
    };

    public TileObjectTypeSO[] connectableTileObjectType = new TileObjectTypeSO[0];

    public Sprite defultSprite;

    public Sprite GetSprite(Dictionary<Vector2Int,TileObjectTypeSO> positionTileObjectTypeDictionary)
    {
        // Transfer TileObjecType dictionary to isConnectable boolean array, corrisponding to check position array
        bool[] isConectableArray = new bool[checkPosition.Length];

        for(int i = 0; i< checkPosition.Length; i++)
        {
            isConectableArray[i] = false;

            if (positionTileObjectTypeDictionary.ContainsKey(checkPosition[i]))
            {
                foreach (TileObjectTypeSO type in connectableTileObjectType)
                {
                    if(positionTileObjectTypeDictionary[checkPosition[i]] == type)
                    {
                        isConectableArray[i] = true;
                        break;
                    }
                }
            }
        }

        // Compair boollean array with rules, remove unmatched index
        List<int> ruleIndexList = new List<int>();

        for (int i = 0; i < ruleSpriteArray.Length; i++)
        {
            ruleIndexList.Add(i);
        }

        for (int surroundingIndex = 0; surroundingIndex < isConectableArray.Length; surroundingIndex++)
        {
            for (int ruleIndex = 0; ruleIndex < ruleSpriteArray.Length; ruleIndex++)
            {
                if (ruleSpriteArray[ruleIndex].rule[surroundingIndex] != SurroundingTileState.irrelevant)
                {
                    if ((ruleSpriteArray[ruleIndex].rule[surroundingIndex] == SurroundingTileState.connectable) != isConectableArray[surroundingIndex])
                    {
                        ruleIndexList.Remove(ruleIndex);
                    }
                }
            }
        }

        // Get sprite in remaining rule index
        if (ruleIndexList.Count > 0)
        {
            Sprite[] spriteArray = ruleSpriteArray[ruleIndexList[0]].spriteArray;
            Sprite sprite = spriteArray[Random.Range(0, spriteArray.Length)];
            return sprite;
        }
        else
        {
            return defultSprite;
        }
    }
}
