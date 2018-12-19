using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    public int dmg = 35;

    public int GetDamage() { return dmg; }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
