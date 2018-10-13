using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour 
{
    #region singleton

    private static DebugScreen instance;

    public static DebugScreen GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    public Canvas screenDebug;
    public GameObject buttonPrefab;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
            screenDebug.gameObject.SetActive(!screenDebug.gameObject.activeInHierarchy);
    }

    public void AddButton(string buttonName, UnityAction callback)
    {
        GameObject go = Instantiate(buttonPrefab, screenDebug.transform.position, Quaternion.identity,
            screenDebug.transform);
        go.GetComponent<Button>().GetComponentInChildren<Text>().text = buttonName;
        go.GetComponent<Button>().onClick.AddListener(()=>callback());
    }
}
