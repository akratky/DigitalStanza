using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel_UI : MonoBehaviour
{
    public GameObject panelObj;

    public void ToggleInfoPanel()
    {
        panelObj.SetActive(!panelObj.activeSelf);
    }
}
