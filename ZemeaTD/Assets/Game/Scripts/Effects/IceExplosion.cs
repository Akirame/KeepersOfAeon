using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceExplosion : MonoBehaviour {


    public void FreezeEnemies()
    {
        WaveControl.GetInstance().RalenticeEnemies();
        Destroy(gameObject);
    }

}
