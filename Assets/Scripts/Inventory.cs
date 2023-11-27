using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    private List<Slime> slimeList;
    public int selectedSlimeIndex = 0;

    public Inventory()
    {
        slimeList = new List<Slime>();
    }

    public bool CanAddSlime(Slime slimeToAdd)
    {
        // Vérifiez s'il y a un slot avec le même type de slime qui n'est pas plein.
        foreach (var existingSlime in slimeList)
        {
            if (existingSlime.slimeType == slimeToAdd.slimeType && existingSlime.amount < 3)
            {
                // Il y a de la place dans un slot existant pour ce type de slime.
                return true;
            }
        }

        // S'il n'y a pas de slot avec le même type de slime, vérifiez s'il y a de la place pour un nouveau slot.
        if (slimeList.Count < 3)
        {
            // Il y a de la place pour un nouveau slot de slime.
            return true;
        }

        // L'inventaire est plein ou il n'y a pas de place dans les slots existants pour ce type de slime.
        return false;
    }

    public void AddSlime(Slime slime)
    {


        if (!CanAddSlime(slime))
        {
            Debug.Log("Impossible d'ajouter le slime : inventaire plein ou limite atteinte pour le type.");
            return; // Ne pas ajouter le slime si on ne peut pas.
        }

        foreach (var existingSlime in slimeList)
        {
            if (existingSlime.slimeType == slime.slimeType && existingSlime.amount < 3)
            {
                // Si le slime existe déjà et qu'il n'a pas atteint la quantité maximale, incrémentez la quantité.
                existingSlime.amount = Mathf.Min(existingSlime.amount + 1, 3);
                return; // Fin de la méthode après avoir ajouté au slot existant.
            }
        }

        if (slimeList.Count < 3)
        {
            slimeList.Add(slime);
        }
        else
        {
            Debug.Log("Impossible d'ajouter le slime : inventaire plein.");
        }

    }

    public void SelectNextSlime()
    {
        selectedSlimeIndex = (selectedSlimeIndex + 1) % slimeList.Count;
    }

    public void SelectPreviousSlime()
    {
        selectedSlimeIndex = (selectedSlimeIndex - 1 + slimeList.Count) % slimeList.Count;
    }

    public Slime GetSelectedSlime()
    {
        if (slimeList.Count == 0)
        {
            return null;
        }
        else
        {
            return slimeList[selectedSlimeIndex];
        }
    }

    public List<Slime> GetSlimeList()
    {
        return slimeList;
    }
}
