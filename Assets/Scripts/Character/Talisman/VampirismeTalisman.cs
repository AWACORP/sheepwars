using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampirismeTalisman : Talisman
{
    public VampirismeTalisman(Character character) : base("Vampirisme", character)
    {
        
    }

    public override void Effect()
    {
        _character.HealCharacter(_character.GetDamageAttack() * 0.05f);
    }
}
