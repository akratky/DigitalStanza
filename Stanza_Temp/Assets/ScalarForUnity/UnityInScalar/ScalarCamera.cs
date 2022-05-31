using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.Events;
using System.Runtime.InteropServices;
using TMPro;

namespace ANVC.Scalar
{
    public class ScalarCamera : MonoBehaviour
    {
        public float transitionDuration = 1.5f;
        public Transform ManuscriptPos;
        public BookLineRenderer lineRenderer;
        [SerializeField]
        private Rigidbody _rb;

        private Camera _camera;
        private Vector3 _targetPosition;
        private string _currentLinkID;


        // Use this for initialization


        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _camera = GetComponent<Camera>();
            TMP_TextEventHandler.OnSpatialLinkSelected += OnSpatialLinkClicked;
        }


        
        #region Hyperlink Handling
        private void OnSpatialLinkClicked(string spatialLinkSlug)
        {
            Debug.Log( "spatial tag: " + spatialLinkSlug);

            String spatialLink = ScalarTripleLink.GetTripleLink(spatialLinkSlug).spatialLink;
    
            Debug.Log("Spatial Link: " + spatialLink);
                
            if (spatialLink.Contains(ScalarUtilities.roomSpatialAnnotationTag))
            {
                _currentLinkID = spatialLink;
                StartCoroutine(ScalarAPI.LoadNode(
                    spatialLink,
                    OnPageLoadSuccess,
                    OnPageLoadFail,
                    2,
                    true,
                    "annotation"
                ));
                

            }
            

        }


        public void JumpToPosition(Vector3 cameraPos, Vector3 targetPos)
        {
            _rb.useGravity = false;
            _targetPosition = targetPos;
            Vector3 cameraPosition = cameraPos;
            LeanTween.cancel(transform.gameObject);
            LeanTween.move(transform.gameObject, cameraPos, transitionDuration).setEaseInOutCubic();
            //Vector3 upwards = new Vector3(Mathf.Sin(node["roll"] * Mathf.Deg2Rad), Mathf.Cos(node["roll"] * Mathf.Deg2Rad), 0);
            Quaternion rotation = Quaternion.LookRotation(targetPos - cameraPos, Vector3.up);
            LeanTween.rotate(transform.gameObject, rotation.eulerAngles, transitionDuration).setEaseInOutCubic();
            //lineRenderer.TrackingLine(PlatoManuscriptAnnotation,TargetPos.gameObject);

        }


        public void TurnToManuscript()
        {
            _targetPosition = ManuscriptPos.position;
            Vector3 cameraPosition = transform.position;
            LeanTween.cancel(transform.gameObject);
            Quaternion rotation = Quaternion.LookRotation(_targetPosition - cameraPosition, Vector3.up);
            LeanTween.rotate(transform.gameObject, rotation.eulerAngles, transitionDuration).setEaseInOutCubic();
        }

        private void OnPageLoadSuccess(JSONNode node)
        {
            ScalarNode spatialNode = ScalarAPI.GetNode(_currentLinkID);

            foreach (var rel in spatialNode.outgoingRelations)
            {
                if (rel.subType == "spatial")
                {
                    //SetTransformNoEvent(rel.body.data);

                    
                    _targetPosition = new Vector3(float.Parse(rel.properties.targetX),
                        float.Parse(rel.properties.targetY), float.Parse(rel.properties.targetZ));
                    Vector3 cameraPosition = new Vector3(float.Parse(rel.properties.cameraX), 
                        float.Parse(rel.properties.cameraY),float.Parse(rel.properties.cameraZ));
                    LeanTween.cancel(transform.gameObject);
                    LeanTween.move(transform.gameObject, cameraPosition, transitionDuration).setEaseInOutCubic();
                    Vector3 upwards = new Vector3(Mathf.Sin(float.Parse(rel.properties.roll) * Mathf.Deg2Rad), 
                        float.Parse(rel.properties.roll) * Mathf.Deg2Rad, 0);
                    
                    
                    Quaternion rotation = Quaternion.LookRotation(_targetPosition - cameraPosition, upwards);
                    LeanTween.rotate(transform.gameObject, rotation.eulerAngles, transitionDuration).setEaseInOutCubic();

                    //StartCoroutine(DelayCreateLine(cameraPosition,-(cameraPosition + _targetPosition)));

                }
            }
            
        }

        private void OnPageLoadFail(string err)
        {
            Debug.LogError(err);
        }

        #endregion



        
    }
    
    
}

