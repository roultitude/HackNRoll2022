using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
}
