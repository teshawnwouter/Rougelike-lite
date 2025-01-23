using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "spells you have", menuName = "inventory system/inventory")]
public class InventoryObjects : ScriptableObject, IPickUpable
{
    public List<Spells> container = new List<Spells>();

    /// <summary>
    /// Add spell only if there is space within the resource list.
    /// </summary>
    /// <param name="spells">Resource to add</param>
    /// <returns>Returns true if the resource was added</returns>
    public bool AddSpells(Spells spells)
    {
        container.Add(spells);
        if (container.Count >= 6)
        {
            container.Remove(container[5]);
            return false;
        }
        return true;
    }
}