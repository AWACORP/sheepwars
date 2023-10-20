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


    //[SerializeField] private Slime[] slimes;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //slimes = new Slime[MAX_NB_SLIMES];
        talisman = new Talisman[MAX_NB_TALISMAN];
        talisman[0] = new VampirismeTalisman(this);
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        if (Input.GetKey(KeyCode.A))
        {
            talisman[0].Effect();
        }
    }

    public void BaseAttack()
    {
        Debug.Log("Attack mon gars");
    }

    public void ThrowSlime()
    {
        Debug.Log("Lance mon gars");
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
    
    
    
    
}
