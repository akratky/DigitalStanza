using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterleaveMover : MonoBehaviour{

    public Animator interleafAnimator;


    public void MoveInterleaf(){
        if(interleafAnimator.GetBool("IsRight")){
            interleafAnimator.SetBool("IsRight", false);
        }
        else{
            interleafAnimator.SetBool("IsRight", true);
        }
    }   
}
