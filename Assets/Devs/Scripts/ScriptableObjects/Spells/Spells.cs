using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "spells to use", menuName ="PickUps/Spells")]
public class Spells : PickUpItem
{
    private void Awake()
    {
        pickUpType = PickUpType.spell;
    }
}
