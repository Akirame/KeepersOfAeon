using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject ObjectiveToGo;

    public void GotoObjective(GameObject go)
    {
        go.transform.position = ObjectiveToGo.transform.position;
    }
}
