using System.Collections;
using System.Collections.Generic;
using ANVC.Scalar;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookLineRenderer : MonoBehaviour
{

    public delegate void DestroyLineDelegate();

    public static DestroyLineDelegate DestroyLineEvent;

    public delegate void ToggleLineDelegate();

    public static ToggleLineDelegate ToggleLineEvent;

    public GameObject trackingOriginObj;
    private LineRenderer _lineRenderer;
    
    private GameObject _trackingOriginObj;
    private GameObject _trackingDirObj;
    
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _lineRenderer.useWorldSpace = true;
        DestroyLineEvent += DestroyLine;
        ToggleLineEvent += ToggleLine;
    }



    public void TrackingLine(GameObject trackingOrigin, GameObject trackingDir)
    {
        _lineRenderer.enabled = true;
        _trackingDirObj = trackingDir;
        _trackingOriginObj = trackingOrigin;
    }

    public IEnumerator DelayedTrackingLine(float delay)
    {
        //not ideal but YOLO
        
        yield return new WaitForSeconds(delay);
        
        GameObject wallFrame = GameObject.FindWithTag("WallAnnotation");

        _trackingDirObj = wallFrame;
        _trackingOriginObj = trackingOriginObj;
        _lineRenderer.enabled = true;

    }

    private void ToggleLine()
    {
        _lineRenderer.enabled = !_lineRenderer.enabled;
    }

    public void DestroyLine()
    {
        _trackingDirObj = null;
        _trackingOriginObj = null;
        _lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_trackingOriginObj && _trackingDirObj)
        {
            _lineRenderer.SetPosition(0,_trackingOriginObj.transform.position);
            _lineRenderer.SetPosition(1,_trackingDirObj.transform.position);
        }
        
        
    }
}
