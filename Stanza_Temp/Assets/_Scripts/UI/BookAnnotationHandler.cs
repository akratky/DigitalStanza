using System;
using System.Collections;
using System.Collections.Generic;
using ANVC.Scalar;
using SimpleJSON;
using TMPro;
using UnityEngine;

public class BookAnnotationHandler : MonoBehaviour
{

    public GameObject manuscriptAnnotationPrefab;
    //used for parenting annotaiton transfomr
    public GameObject annotationParentObj;
    public bool isRecto;
    [Header("Tuning Params")] 
    public Vector3 spatialAnnotationOffset;

    private string _targetPageSlug;
    private string _targetAnnotationTag;
    
    private BoxCollider _collider;
    private GameObject _currentAnnotationInstance;

    public delegate void OnDestroyManuAnnotation();
    public static event OnDestroyManuAnnotation DestroyManuAnnotationEvent;

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        DestroyManuAnnotationEvent += DestroyAnnotationInstance;
        ScalarBook.BookAnnotationEvent += OnManuLinkClicked;
    }

    private void OnManuLinkClicked(string linkSlug,bool rectPage)
    {
        if (rectPage != isRecto)
            return;
        
        
        if (linkSlug.Contains(ScalarUtilities.manuscriptAnnotationTag))
        {
            _targetPageSlug = linkSlug;
            
            if(_currentAnnotationInstance)
                DestroyAnnotationInstance();

            StartCoroutine(ScalarAPI.LoadNode(
                linkSlug,
                OnPageLoadSuccess,
                OnPageLoadFail,
                2,
                true,
                "referee"
            ));
        }
        
        else if(_currentAnnotationInstance)
            DestroyAnnotationInstance();
        
    }

    private void OnPageLoadSuccess(JSONNode node)
    {
        ScalarNode manuNode = ScalarAPI.GetNode(_targetPageSlug);
        Bounds colliderBounds = _collider.bounds;
        

        foreach (var rel in manuNode.outgoingRelations)
        {
            if (rel.subType == "spatial" && rel.id.Contains(_targetPageSlug))
            {
                
                string xCoord = rel.startString[2].ToString();
                //account for single digit values
                if (rel.startString[3] != '%')
                    xCoord += rel.startString[3];

                string yCoord = "";
                int yCoordIndex = rel.startString.LastIndexOf(':');
                yCoord += rel.startString[yCoordIndex + 1];
                if (rel.startString[yCoordIndex + 2] != '%')
                    yCoord += rel.startString[yCoordIndex + 2];

                float xCoordf = Int32.Parse(xCoord);
                float yCoordf = Int32.Parse(yCoord);

                Vector3 worldCoord = new Vector3();
                worldCoord.x = colliderBounds.max.x - (xCoordf / 100) * colliderBounds.size.x;
                worldCoord.y = colliderBounds.max.y - (yCoordf / 100) * colliderBounds.size.y;
                worldCoord.z = colliderBounds.center.z;

                /*
                worldCoord.x += spatialAnnotationOffset.x;
                worldCoord.y += spatialAnnotationOffset.y;
                worldCoord.z += spatialAnnotationOffset.z;
                */
                
                _currentAnnotationInstance = Instantiate(manuscriptAnnotationPrefab, worldCoord, 
                    Quaternion.identity);
                _currentAnnotationInstance.transform.SetParent(annotationParentObj.transform);
                
                _currentAnnotationInstance.transform.Translate(spatialAnnotationOffset,Space.World);
            }
        }

    }

    private void OnPageLoadFail(string err)
    {
        Debug.LogError(err);
    }
    

    private void DestroyAnnotationInstance()
    {
        Destroy(_currentAnnotationInstance);
        _currentAnnotationInstance = null;   
    }
}




