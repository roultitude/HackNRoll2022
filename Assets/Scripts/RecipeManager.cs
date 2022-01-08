using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;
    public RecipeSO[] recipes;
    public ItemSO[] items;

    private void Awake()
    {
        if (instance && this!=instance)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }
    }
    public RecipeSO getValidHandRecipe(string itemOne, string itemTwo)
    {
        foreach (RecipeSO recipe in recipes)
        {
            if (!recipe.needsWok && ((recipe.inputOne.itemName == itemOne && recipe.inputTwo.itemName == itemTwo) || (recipe.inputOne.itemName == itemTwo && recipe.inputTwo.itemName == itemOne)))
            {
                return recipe;
            }
        }
        return null;
    }

    public RecipeSO getValidWokRecipe(string itemOne)
    {
        foreach(RecipeSO recipe in recipes)
        {
            if(recipe.needsWok && recipe.inputOne.itemName == itemOne)
            {
                return recipe;
            }
        }
        return null;
    }
    public ItemSO getItemSOfromName(string itemSOName)
    {
        foreach(ItemSO item in items)
        {
            if(item.itemName == itemSOName)
            {
                return item;
            }
        }
        return null;
    }
}
