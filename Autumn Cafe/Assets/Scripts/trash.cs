using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash : Interactible
{
    public override void interactFunction()
    {
        if(player.GetComponentInChildren<PickUpAndInteract>().heldItem.GetComponent<Pickupable>().canBeTrashed == true)
        {
            Destroy(player.GetComponentInChildren<PickUpAndInteract>().heldItem);
            player.GetComponentInChildren<PickUpAndInteract>().heldItem = null;
        }
    }
}
