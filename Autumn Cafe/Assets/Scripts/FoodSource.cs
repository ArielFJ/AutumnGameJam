using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSource : Interactible
{
    public PickUpAndInteract pickup;
    public GameObject prefab;

    private void Start()
    {
        interactableWithHeldItem = false;
    }

    public override void interactFunction()
    {
        GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.identity, null);
        pickup.heldItem = instance;
        pickup.Grab();
    }
}
