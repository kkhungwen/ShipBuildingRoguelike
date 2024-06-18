using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGridObject 
{
    private IShipObject shipObject;

    public bool IsShipObjectPlacable()
    {
        return shipObject == null;
    }

    public bool TryAddShipObject(IShipObject shipObject)
    {
        if (this.shipObject != null)
        {
            this.shipObject = shipObject;
            return true;
        }

        return false;
    }

    public bool TryGetShipObject(out IShipObject shipObject)
    {
        if(this.shipObject != null)
        {
            shipObject = this.shipObject;
            return true;
        }

        shipObject = null;
        return false;
    }
}
