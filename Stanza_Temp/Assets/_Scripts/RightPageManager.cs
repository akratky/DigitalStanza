using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPageManager : MonoBehaviour
{
    public ParseIIIF parseIIIF;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        parseIIIF.GetRight();
        Debug.Log("did right");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
