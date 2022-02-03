using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterleaveMover : MonoBehaviour
{   
    public float moveTime;
    public GameObject interleaveContainer;
    public GameObject startPositionHolder;
    public GameObject endPositionHolder;
    public Vector3 pos1;
    public Vector3 pos2;
    Vector3 startPos;
    Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        pos1 = startPositionHolder.transform.position;
        pos2 = endPositionHolder.transform.position;
        
    }
    public void MoveInterleave(){
        if(interleaveContainer.transform.position == pos1){
                    Debug.Log("move left");
                    startPos = pos1;
                    endPos = pos2;
                }
                else if(interleaveContainer.transform.position == pos2){
                    Debug.Log("move right");
                    startPos = pos2;
                    endPos = pos1;
                }
                StartCoroutine(SwapSide(startPos, endPos));
    }

    public IEnumerator SwapSide(Vector3 startP, Vector3 endP){    
        float timeElapsed = 0;
        while(timeElapsed < moveTime){
            float t = timeElapsed/moveTime;
            // we gon ease on out
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            interleaveContainer.transform.position = Vector3.Lerp(startP, endP, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        // snap to end points once time is up
        interleaveContainer.transform.position = endP;
    }
}
