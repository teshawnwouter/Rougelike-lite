using UnityEngine;

public class AddToInventory : MonoBehaviour
{
    public Spells spells;
    public LootList lootList;

    private void Awake()
    {
        lootList = FindObjectOfType<LootList>();
        spells = FindObjectOfType<LootList>().droppedItem;
    }
}
