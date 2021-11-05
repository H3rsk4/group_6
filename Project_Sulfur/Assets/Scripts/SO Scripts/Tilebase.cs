using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tilebase : ScriptableObject
{
    public abstract void Do(Vector3Int tilePosition,tile_manager tileManager, int entityIndex, object sender);
}
