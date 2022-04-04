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
        

    private LineRenderer _lineRenderer;
    
    [SerializeField]
    private GameObject _trackingOriginObj;
    [SerializeField]
    private GameObject _trackingDirObj;
    
    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _lineRenderer.useWorldSpace = true;
        DestroyLineEvent += DestroyLine;
    }



    public void TrackingLine(GameObject trackingOrigin, GameObject trackingDir)
    {
        _lineRenderer.enabled = true;
        _trackingDirObj = trackingDir;
        _trackingOriginObj = trackingOrigin;
    }

    public void DestroyLine()
    {
        _trackingDirObj = null;
        _trackingDirObj = null;
        _lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_trackingDirObj && _trackingDirObj)
        {
            _lineRenderer.SetPosition(0,_trackingOriginObj.transform.position);
            _lineRenderer.SetPosition(1,_trackingDirObj.transform.position);
        }
        
        
    }
}
