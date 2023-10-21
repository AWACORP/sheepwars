using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public enum SlimeType
{
    Flaming,
    Flying,
    Exploding,
    Reviving
}

public abstract class Slime : NetworkBehaviour
{
    public SlimeType slimeType { get; set; }
    public float lifeSpan { get; set; }
}


