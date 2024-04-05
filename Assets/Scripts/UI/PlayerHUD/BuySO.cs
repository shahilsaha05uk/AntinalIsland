using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Asset1", menuName ="BuyAsset", order = 0)]
public class BuySO : ScriptableObject
{
    public List<TowerAsset> Towers;
}
