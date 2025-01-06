using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "spells you have", menuName = "inventory system/inventory")]
public class InventoryObjects : ScriptableObject
{
   public List<SpellContainer> container = new List<SpellContainer>();
    public void addSpell(Spells spell, int amountofspells)
    {
        bool hasItem = false;
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].spells == spell) 
            {
                container[i].AddSpell(amountofspells);
                hasItem = true;
                break;
            }
        }if (!hasItem) 
        { 
            container.Add(new SpellContainer(spell, amountofspells));
        }
    }
}

[System.Serializable]
public class SpellContainer
{
    public Spells spells;
    public int amount;
    public SpellContainer(Spells spells, int amount)
    {
        this.spells = spells;
        this.amount = amount;
    }
    public void AddSpell(int value)
    {
        amount += value;
    }
}