using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialMouse : MonoBehaviour
{
    [SerializeField] Image RightMouse;
    private Color mycolor;
    public GameObject canvas;
    private Animator anim;
    void Start()
    {
        mycolor = RightMouse.color;
        anim = canvas.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mycolor.a = 1f;
            RightMouse.color = mycolor;

            anim.SetBool("ToEnd", true);
            anim.SetBool("CanTransit", false);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            mycolor.a = 0.6f;
            RightMouse.color = mycolor;
        }
    }

  
}
