using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToInventory : MonoBehaviour
{
   [SerializeField] private Spells spells;
   public LootList lootList;

    private void Awake()
    {
        lootList = FindObjectOfType<LootList>();
        spells = FindObjectOfType<LootList>().droppedItem;
    }
}
