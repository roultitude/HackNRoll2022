using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WokCounter : Counter
{
    public float cookingTime;
    public ItemSO pendingOutput;
    [SerializeField]
    GameObject cookingParticles;
    [SerializeField]
    Material stoveMat;
    bool cooking;
    public override void InteractAction(PlayerController player)
    {
        base.InteractAction(player);
        if (cooking && !storedItem)
        {
            setCooking(false);
            pendingOutput = null;
            return;
        }
        if (storedItem)
        {
            RecipeSO recipe = RecipeManager.instance.getValidWokRecipe(storedItem.itemName);
            if (recipe)
            {
                pendingOutput = recipe.outputOne;
                cookingTime = recipe.cookTime;
                setCooking(true);
                player.startCookSound.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        if (cooking)
        {
            if (cookingTime > 0)
            {
                cookingTime -= Time.deltaTime;
            }
        }
        if (cookingTime < 0 && pendingOutput)
        {
            finishCooking();
            cookingTime = 10;
        }
    }

    
    public void setCooking(bool cooking)
    {
        PV.RPC("setCookingRPC", RpcTarget.All, cooking);
    }
    [PunRPC]
    public void setCookingRPC(bool cooking)
    {
        this.cooking = cooking;
        if(cooking == true)
        {
            cookingParticles.SetActive(true);
            stoveMat.color = Color.red;
            
        }
        else
        {
            cookingParticles.SetActive(false);
            stoveMat.color = Color.white;
        }
    }
    public void finishCooking()
    {
        setCooking(false);
        storedItem.Setup(pendingOutput);
        pendingOutput = null;
    }
}
