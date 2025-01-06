using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickupS", menuName ="PickUps/Health")]
public class HealthPickUp : PickUpItem
{
    public int healthRecovered;
    private void Awake()
    {
        pickUpType = PickUpType.health;
    }
}
