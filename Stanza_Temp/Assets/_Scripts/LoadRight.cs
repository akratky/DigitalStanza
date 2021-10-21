using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRight : MonoBehaviour
{

    public LoadDecachordum loaderScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        loaderScript.GetText("rightPage");
        Debug.Log("did right");
    }
}
