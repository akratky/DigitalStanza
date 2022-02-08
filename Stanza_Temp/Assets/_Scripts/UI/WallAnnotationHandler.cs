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
    public string frescoImageFileName;
    public GameObject spatialAnnotationPrefab;
    [Header("Tuning Parameters")] 
    public Vector2 spatialAnnotationOffset;
    private string _targetPageSlug;
    private BoxCollider _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
        
        TMP_TextEventHandler.OnLinkSelectedEvent += OnHyperlinkClicked;
    }

    private void OnHyperlinkClicked(string linkID, string linkText, int linkIndex)
    {
        if (linkID.Contains(ScalarUtilities.frescoImageAnnotationTag))
        {
            _targetPageSlug = linkID;
            
            StartCoroutine(ScalarAPI.LoadNode(
                linkID,
                OnPageLoadSuccess,
                OnPageLoadFail,
                2,
                true,
                "referee"
            ));
        }
        
    }

    private void OnPageLoadSuccess(JSONNode node)
    {
        ScalarNode imageNode = ScalarAPI.GetNode(_targetPageSlug);
        Bounds colliderBounds = _collider.bounds;
        

        foreach (var rel in imageNode.outgoingRelations)
        {
            if (rel.subType == "spatial" && rel.id.Contains(ScalarUtilities.frescoImageAnnotationTag))
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

                worldCoord.x += spatialAnnotationOffset.x;
                worldCoord.y += spatialAnnotationOffset.y;

                Instantiate<GameObject>(spatialAnnotationPrefab, worldCoord, Quaternion.identity);
            }
        }
        
        
        Debug.Log("");
    }

    private void OnPageLoadFail(string err)
    {
        Debug.LogError(err);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
