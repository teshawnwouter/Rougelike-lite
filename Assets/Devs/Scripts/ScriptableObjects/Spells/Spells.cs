using UnityEngine;

[CreateAssetMenu(fileName = "spells to use", menuName ="PickUps/Spells")]
public class Spells : PickUpItem
{
    public Spells PickUp;
    private void Awake()
    {
        pickUpType = PickUpType.spell;
    }
}
