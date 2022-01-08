using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "ScriptableObjects/RecipeSO", order = 2)]

public class RecipeSO : ScriptableObject
{
    public bool needsWok;
    public ItemSO inputOne;
    public ItemSO inputTwo;
    public ItemSO outputOne = null;
    public ItemSO outputTwo = null;
    public float cookTime;
}
