using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{

    private void Start()
    {
        GameManager.Get().onGameScene = true;

    }
}
