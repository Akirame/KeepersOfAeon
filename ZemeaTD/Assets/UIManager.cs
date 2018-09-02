using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text waveText;
    private string waveTextAux;

    private void Update()
    {
        if (waveTextAux != EnemyManager.GetInstance().wave.name)
        {
            waveTextAux = EnemyManager.GetInstance().wave.name;
            waveText.text = waveTextAux;
        }
    }
}
