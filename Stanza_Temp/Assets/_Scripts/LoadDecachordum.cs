using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDecachordum : MonoBehaviour
{

    public GameObject book;

    public Material page1;
    public Material page2;
    public int book_index = 1;
    public int max_pages = 10;


    // Start is called before the first frame update
    void Start()
    {
        // left page
        string pageName = "Decachordum_pages/Decachordum_0";
        Texture2D texture = Resources.Load<Texture2D>(pageName);
        page1.SetTexture("_BaseMap", texture);


        // right page
        pageName = "Decachordum_pages/Decachordum_1";
        texture = Resources.Load<Texture2D>(pageName);
        page2.SetTexture("_BaseMap", texture);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GetText(string page)
    {


        if (page == "rightPage")
        {
            if (book_index < max_pages - 2)
            {

                book_index += 2;

                // left page
                string pageName = "Decachordum_pages/Decachordum_" + (book_index - 1).ToString();
                Texture2D texture = Resources.Load<Texture2D>(pageName);
                page1.SetTexture("_BaseMap", texture);
                

                // right page
                pageName = "Decachordum_pages/Decachordum_" + (book_index).ToString();
                texture = Resources.Load<Texture2D>(pageName);
                page2.SetTexture("_BaseMap", texture);
                

                
            }
        }
        else
        {
            if (book_index > 2)
            {

                book_index -= 2;
                // left page
                string pageName = "Decachordum_pages/Decachordum_" + (book_index - 1).ToString();
                Texture2D texture = Resources.Load<Texture2D>(pageName);
                page1.SetTexture("_BaseMap", texture);

                // right page
                pageName = "Decachordum_pages/Decachordum_" + (book_index).ToString();
                texture = Resources.Load<Texture2D>(pageName);
                page2.SetTexture("_BaseMap", texture);

                
            }
        }
    }
}

