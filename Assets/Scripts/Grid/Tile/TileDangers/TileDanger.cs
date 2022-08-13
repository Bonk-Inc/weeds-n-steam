using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileDanger : MonoBehaviour
{
    [SerializeField]
    protected bool safetyOn = true;

    public abstract void ToggleDanger(bool safe);
}
