using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookZoomer : MonoBehaviour
{
    public GameObject heldBook;
    public GameObject startPositionHolder;
    public GameObject endPositionHolder;
    public float moveTime;

    public Vector3 pos1;
    public Vector3 pos2;
    public Quaternion rot1;
    public Quaternion rot2;
    Vector3 startPos;
    Vector3 endPos;
    Quaternion startRot;
    Quaternion endRot;

    void Update()
    {
        // grab pos and rot of game objects
        pos1 = startPositionHolder.transform.position;
        pos2 = endPositionHolder.transform.position;
        rot1 = startPositionHolder.transform.rotation;
        rot2 = endPositionHolder.transform.rotation;
    }

    public void ChangeBookView(){
        // if we're at the first position, go to the second, and vice versa
        if(heldBook.transform.position == pos1){
            //Debug.Log("Zooming in");
            startPos = pos1;
            endPos = pos2;
            startRot = rot1;
            endRot = rot2;
        }
        else if(heldBook.transform.position == pos2){
            //Debug.Log("Zooming out");
            startPos = pos2;
            endPos = pos1;
            startRot = rot2;
            endRot = rot1;
        }
        StartCoroutine(SetZoom(startPos, endPos, startRot, endRot));
    }

    public IEnumerator SetZoom(Vector3 startP, Vector3 endP, Quaternion startR, Quaternion endR){    
        float timeElapsed = 0;
        while(timeElapsed < moveTime){
            float t = timeElapsed/moveTime;
            // we gon ease on out
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
            heldBook.transform.position = Vector3.Lerp(startP, endP, t);
            heldBook.transform.rotation = Quaternion.Lerp(startR, endR, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        // snap to end points once time is up
        heldBook.transform.position = endP;
        heldBook.transform.rotation = endR;
    }
}
