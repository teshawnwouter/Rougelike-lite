using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "spells you have", menuName = "inventory system/inventory")]
public class InventoryObjects : ScriptableObject
{
    public List<SpellContainer> container = new List<SpellContainer>();
    public void addSpell(Spells spell)
    {
        container.Add(new SpellContainer(spell));
        if (container.Count >= 6)
        {
            container.Remove(container[5]);
        }
    }
}

[System.Serializable]
public class SpellContainer
{
    public Spells spells;
    public SpellContainer(Spells spells)
    {
        this.spells = spells;
    }

}