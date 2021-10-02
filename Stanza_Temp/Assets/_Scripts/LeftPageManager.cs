using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPageManager : MonoBehaviour
{
    public ParseIIIF parseIIIF;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnMouseDown()
    {
        parseIIIF.GetLeft();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
