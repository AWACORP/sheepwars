using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[System.Serializable]
public class SlimeTypeSprite
{
    public Slime.SlimeType slimeType;
    public Sprite sprite;
    public GameObject modelPrefab;

}

public class Slime : NetworkBehaviour
{
    public enum SlimeType
    {
        Exploding,
        Healing,
        Nearing
    }

    public SlimeType slimeType;
    public int amount;
    public float lifeSpan;

    void OnTriggerEnter(Collider other)
    {

        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            if (character.inventory.CanAddSlime(this))
            {
                character.AddSlimeToInventory(this);
                Destroy(gameObject); // Détruit le Slime de la scène
            }
            else
            {
                Debug.Log("Inventaire plein. Ne peut pas ramasser le slime " + this.slimeType);
            }
        }
    }
}

