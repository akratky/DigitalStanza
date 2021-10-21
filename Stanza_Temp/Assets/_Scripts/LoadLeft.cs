using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLeft : MonoBehaviour
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
        loaderScript.GetText("leftPage");
        Debug.Log("did left");
    }
}
