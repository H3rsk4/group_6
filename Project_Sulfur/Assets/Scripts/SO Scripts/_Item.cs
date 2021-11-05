using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class _Item : ScriptableObject
{
    public new string name;
    public Sprite icon;

    public _Tile tile;

    public _CraftingRecipe craftingRecipe;

    public float pickUpDistance;
}
