using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IWorldObjectInstance 
{
    public event Action<OnInitializeEventArgs> OnInitialize;
    public class OnInitializeEventArgs : EventArgs
    {
        public TileObjectTypeSO tileObjectType;
    }
}
