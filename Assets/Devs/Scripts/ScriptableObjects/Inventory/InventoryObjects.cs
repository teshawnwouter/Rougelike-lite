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


    //private Spells[] m_SpellsList = new Spells[6];

    //public bool Add(Spells spell)
    //{
    //    for (int i = 0; i < m_SpellsList.Length; ++i)
    //    {
    //        if (m_SpellsList[i] == null)
    //        {
    //            m_SpellsList[i] = spell;
    //            return true;
    //        }
    //    }

    //    // er was geen space meer.
    //    return false;
    //}

}

//[System.Serializable]
//public class SpellContainer
//{
//    public Spells spells;
//    public SpellContainer(Spells spells)
//    {
//        this.spells = spells;
//    }

//}