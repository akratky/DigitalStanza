using System;
using System.Collections;
using System.Collections.Generic;
using ANVC.Scalar;
using SimpleJSON;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WallAnnotationHandler : MonoBehaviour
{
    //the filename of the image this plane imposes on
    public string frescoImageSlug;
    //object prefab of the physical annotation object
    public GameObject spatialAnnotationPrefab;
    [Header("Tuning Parameters")] 
    public Vector3 spatialAnnotationOffset;
    
    private string _targetPageSlug;
    private BoxCollider _collider;
    private GameObject _currentAnnotationInstance;

    public delegate void OnDestroyDetailAnnotation();

    public static event OnDestroyDetailAnnotation DestroyDetailAnnotationEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        
        DestroyDetailAnnotationEvent += DestroyAnnotationInstance;
        TMP_TextEventHandler.OnDetailLinkSelected += OnDetailLinkClicked;
    }

    private void OnDetailLinkClicked(string linkSlug)
    {
        if (linkSlug.Contains(ScalarUtilities.frescoImageAnnotationTag))
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
        ScalarNode imageNode = ScalarAPI.GetNode(_targetPageSlug);
        Bounds colliderBounds = _collider.bounds;
        

        foreach (var rel in imageNode.outgoingRelations)
        {
            if (rel.subType == "spatial" && rel.id.Contains(frescoImageSlug))
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

                //wall is facing "other" direction
                if (gameObject.transform.rotation.eulerAngles.y > 150)
                {
                    worldCoord.x = colliderBounds.min.x + (xCoordf / 100) * colliderBounds.size.x;
                }
                else
                {
                    worldCoord.x = colliderBounds.max.x - (xCoordf / 100) * colliderBounds.size.x;
                }
                
                worldCoord.y = colliderBounds.max.y - (yCoordf / 100) * colliderBounds.size.y;
                worldCoord.z = colliderBounds.center.z;     
                
                worldCoord.x += spatialAnnotationOffset.x;
                worldCoord.y += spatialAnnotationOffset.y;
                worldCoord.z += spatialAnnotationOffset.z;

                _currentAnnotationInstance = Instantiate(spatialAnnotationPrefab, 
                    worldCoord, gameObject.transform.rotation);
                //_currentAnnotationInstance.transform.SetParent(gameObject.transform);
                
            }
        }
        
        
    }

    private void DestroyAnnotationInstance()
    {
        Destroy(_currentAnnotationInstance);
        _currentAnnotationInstance = null;
    }

    private void OnPageLoadFail(string err)
    {
        Debug.LogError(err);
    }

}
