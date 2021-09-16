using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mixingBowl : Plate
{
    public Transform holdingPosition;
    public float mixAmount;

    public override void Update()
    {
        onPlate.RemoveAll(GameObject => GameObject == null);
    }
}
