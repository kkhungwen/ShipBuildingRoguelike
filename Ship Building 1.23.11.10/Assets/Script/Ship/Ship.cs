using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#region RQUIER COMPONENTS
[DisallowMultipleComponent]
[RequireComponent(typeof(ShipBuilder))]
#endregion REQUIER COMPONENTS
public class Ship : MonoBehaviour
{
    [SerializeField] ShipTileMapSO shipTileMap;

    private ShipBuilder shipBuilder;

    private CustomGrid<ShipGridObject> shipGrid;

    public event Action OnLoad;

    private void Awake()
    {
        shipBuilder = GetComponent<ShipBuilder>();
    }

    private void Start()
    {
        shipGrid = shipBuilder.BuildShip(shipTileMap);

        InvokeOnLoad();
    }

    public void InvokeOnLoad()
    {
        OnLoad?.Invoke();
    }
}
