using System.Collections;
using System.Collections.Generic;
using ANVC.Scalar;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookLineRenderer : MonoBehaviour
{

    
    private LineRenderer _lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = false;
        _lineRenderer.useWorldSpace = true;
        ScalarCamera.CreateLine += OnRenderNewLine;
    }


    private void OnRenderNewLine(Vector3 pos, Vector3 dir)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPosition(0,pos);
        _lineRenderer.SetPosition(1,dir);
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
