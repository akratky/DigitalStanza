using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeManuscript : MonoBehaviour
{
    public Texture2D[] manuscriptPages;
    private int _pageIndex = 0;
    
    
    public Material leftPage;
    public Material rightPage;
    
    
    // Start is called before the first frame update
    void Start()
    {
        LoadPages(_pageIndex);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
            NextPage();
        else if(Input.GetKeyDown(KeyCode.N))
            PrevPage();
    }

    public void NextPage()
    {
        if (_pageIndex + 2 < manuscriptPages.Length)
        {
            _pageIndex += 2;
            LoadPages(_pageIndex);
        }
        
            
    }

    public void PrevPage()
    {
        if (_pageIndex != 0)
        {
            _pageIndex -= 2;
            LoadPages(_pageIndex);
        }
    }

    public void LoadPages(int i)
    {
        leftPage.mainTexture = manuscriptPages[i];
        rightPage.mainTexture = manuscriptPages[i + 1];
    }

}
