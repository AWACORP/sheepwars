using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talisman
{
    protected string _name { get; }
    protected Character _character { get; set; }

    public Talisman()
    {
        this._character = null;
        this._name = "Talisman null";
    }
    
    public Talisman(string name, Character character)
    {
        this._character = character;
        this._name = name;
    }

    public virtual void Effect()
    {
        
    }
}
