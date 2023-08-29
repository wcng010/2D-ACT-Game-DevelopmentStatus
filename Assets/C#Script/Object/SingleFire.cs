using System;
using System.Collections;
using System.Collections.Generic;
using C_Script.Object;
using UnityEngine;

public class SingleFire : MonoBehaviour
{
    private EarthFire _earthFiree;

    private void Awake()
    {
        _earthFiree = GetComponentInParent<EarthFire>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (String.Compare(col.tag, "Player", StringComparison.Ordinal) == 0)
        {
            _earthFiree.SetPlayerInFire(col.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (String.Compare(col.tag, "Player", StringComparison.Ordinal) == 0)
        {
            _earthFiree.OutOfTheFire();
        }
    }
}
