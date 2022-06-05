using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTurnButton : MonoBehaviour
{
    public ScalarBook bookObj;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextPage()
    {
        bookObj.GotoNextPage();
    }

    public void PrevPage()
    {
        bookObj.GotoPreviousPage();
    }
}
