using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookLineRenderer : MonoBehaviour
{
  //  public GameObject book;
    public GameObject bookLine; 
    public Button showLine_btn;

    bool isLine = false; 
    Vector3 origin;
    Vector3 cam_dir;
    Vector3 end; 

    // Start is called before the first frame update
    void Start()
    {
        bookLine.SetActive(false);
    }

    public void RenderBookLine() {
        isLine = !isLine;
        bookLine.SetActive(isLine);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
