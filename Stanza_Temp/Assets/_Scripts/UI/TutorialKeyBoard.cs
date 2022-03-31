using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialKeyBoard : MonoBehaviour
{
    [SerializeField] Image Akey;
    private Color mycolorA;
    [SerializeField] Image Wkey;
    private Color mycolorW;
    [SerializeField] Image Skey;
    private Color mycolorS;
    [SerializeField] Image Dkey;
    private Color mycolorD;

    private bool[] ispress = new bool[4];

    public GameObject canvas;
    private Animator anim;
    void Start()
    {
        anim = canvas.GetComponent<Animator>();
        mycolorA = Akey.color;
        mycolorW = Wkey.color;
        mycolorS = Skey.color;
        mycolorD = Dkey.color;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            mycolorA.a = 1f;
            Akey.color = mycolorA;
            ispress[0] = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            mycolorD.a = 1f;
            Dkey.color = mycolorD;
            ispress[1] = true;
        }

        if (Input.GetKey(KeyCode.W))
        {
            mycolorW.a = 1f;
            Wkey.color = mycolorW;
            ispress[2] = true;
        }

        if (Input.GetKey(KeyCode.S))
        {
            mycolorS.a = 1f;
            Skey.color = mycolorS;
            ispress[3] = true;
        }

        NextAnim();
    }

    public void NextAnim()
    {
        if (ispress[0] && ispress[1] && ispress[2] && ispress[3])
            {
            //Debug.Log("ALL PRESSED");
            anim.SetBool("CanTransit", true);

            }
        
    }


}
