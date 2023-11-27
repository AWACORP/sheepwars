using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class Character : NetworkBehaviour
{
    /*
     * Vie
     * Shield ?
     * Passif
     * Pouvoir
     * Amulette
     *
     * Stat attack
     *
     *
     * Slot slime
     * 
     *
     * Attack
     * Lancer un slime
     * Move
     * Jump
     * Ramasser slime
     * Utliser pouvoir
     */

    public int MAX_NB_SLIMES = 3;
    public int MAX_NB_TALISMAN = 2;

    [SerializeField] private float health;
    [SerializeField] private float shield;
    [SerializeField] private float attack;
    [SerializeField] private CharacterPassif passif;
    [SerializeField] private CharacterPower power;

    [SerializeField] private Talisman[] talisman;

    public Inventory inventory;
    private UI_Inventory uiInventory;
    private GameObject currentSlimeInHand;

    // Start is called before the first frame update
    void Start()
    {
        //slimes = new Slime[MAX_NB_SLIMES];
        talisman = new Talisman[MAX_NB_TALISMAN];
        talisman[0] = new VampirismeTalisman(this);

        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        if (Input.GetKey(KeyCode.A))
        {
            talisman[0].Effect();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            inventory.SelectNextSlime();
            uiInventory.UpdateSlimeSelectionUI();
            UpdateSlimeInHand();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            inventory.SelectPreviousSlime();
            uiInventory.UpdateSlimeSelectionUI();
            UpdateSlimeInHand();
        }
    }

    public void BaseAttack()
    {
        //Debug.Log("Attack mon gars");
    }

    public void ThrowSlime()
    {
        //Debug.Log("Lance mon gars");
    }

    public void HealCharacter(float amountOfHeal)
    {
        health += amountOfHeal;
    }

    public void ShielCharacter(float amountOfShield)
    {
        shield += amountOfShield;
    }

    public float GetDamageAttack()
    {
        return attack;
    }

    public void SetUIInventory(UI_Inventory inventory)
    {
        this.uiInventory = inventory;
    }

    public void AddSlimeToInventory(Slime slime)
    {
        inventory.AddSlime(slime);
        uiInventory.RefreshInventorySlimes();
    }

    private void UpdateSlimeInHand()
    {
        if (currentSlimeInHand != null)
        {
            Destroy(currentSlimeInHand);
        }

        Slime selectedSlime = inventory.GetSelectedSlime();
        if (selectedSlime.slimeType != null)
        {
            GameObject modelPrefab = GetModelPrefabForSlimeType(selectedSlime.slimeType);
            if (modelPrefab != null)
            {
                currentSlimeInHand = Instantiate(modelPrefab, transform.position, Quaternion.identity);
                currentSlimeInHand.transform.SetParent(transform); // Faites en sorte que le slime suive le joueur
                //currentSlimeInHand.transform.localPosition = new Vector3(0, 0, 1); // Ajustez cette position comme nécessaire
            }
        }
    }

    private GameObject GetModelPrefabForSlimeType(Slime.SlimeType slimeType)
    {
        foreach (var slimeTypeModel in uiInventory.slimeSprites)
        {
            if (slimeTypeModel.slimeType == slimeType)
            {
                return slimeTypeModel.modelPrefab;
            }
        }
        return null; // Retourne null si aucun modèle n'est trouvé pour ce type.
    }




}
