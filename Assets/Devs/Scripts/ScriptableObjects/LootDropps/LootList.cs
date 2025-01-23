using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootList : MonoBehaviour
{
    [Header("Drops")]
    public GameObject droppedLoot;
    public List<Spells> lootList = new List<Spells>();
    public Spells droppedItem;

    Spells DroppingItems()
    {
        int randomNumber = Random.Range(1, 10);
        List<Spells> possibleItems = new List<Spells>();
        foreach (Spells item in lootList)
        {
            if (randomNumber <= item.dropRate)
            {
                possibleItems.Add(item);
            }
        }

        if (possibleItems.Count > 0)
        {
            Spells droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        return null;

    }

    public void DroppedTheItem(Vector2 SpawnPoint)
    {
        droppedItem = DroppingItems();
        if (droppedItem != null)
        {
            GameObject dropGameObject = Instantiate(droppedLoot, SpawnPoint, Quaternion.identity);
            dropGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.sprite;
        }
        Debug.Log(droppedItem.name);
    }
}
