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
        public AnnotationSelectedExternallyEvent annotationSelectedExternallyEvent;
        public UnityEvent annotationsUpdatedExternallyEvent;
        public MessageReceivedEvent messageReceivedEvent;
        public Transform CameraPos;
        public Transform TargetPos;
        public delegate void RenderLine(Vector3 pos, Vector3 dir);
        public static event RenderLine CreateLine;
        private Camera _camera;
        private Vector3 _targetPosition;
        private string _currentLinkID;
        [DllImport("__Internal")]
        private static extern void ReturnPosition3D(string position3D);

        // Use this for initialization
        void Start()
        {
            _camera = GetComponent<Camera>();
            TMP_TextEventHandler.OnSpatialLinkSelected += OnSpatialLinkClicked;
        }

        public void HandleAnnotationsUpdated()
        {
            annotationsUpdatedExternallyEvent.Invoke();
        }


        
        public void GetTransform()
        {
            JSONObject data = new JSONObject();
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                _targetPosition = transform.position + (transform.forward * hit.distance);
            }
            else
            {
                _targetPosition = transform.position + (transform.forward * 5);
            }
            data["targetX"] = _targetPosition.x;
            data["targetY"] = _targetPosition.y;
            data["targetZ"] = _targetPosition.z;
            data["cameraX"] = transform.position.x;
            data["cameraY"] = transform.position.y;
            data["cameraZ"] = transform.position.z;
            data["roll"] = 360 - transform.rotation.eulerAngles.z;
            data["fieldOfView"] = _camera.fieldOfView;
            ReturnPosition3D(data.ToString());
        }

        public void SetTransform(string data)
        {
            JSONNode json = JSON.Parse(data);
            SetTransformNoEvent(json);
            annotationSelectedExternallyEvent.Invoke(json);
        }

        public void SetTransformNoEvent(JSONNode node)
        {
            _targetPosition = new Vector3(node["targetX"], node["targetY"], node["targetZ"]);
            Vector3 cameraPosition = new Vector3(node["cameraX"], node["cameraY"], node["cameraZ"]);
            LeanTween.cancel(transform.gameObject);
            LeanTween.move(transform.gameObject, cameraPosition, transitionDuration).setEaseInOutCubic();
            Vector3 upwards = new Vector3(Mathf.Sin(node["roll"] * Mathf.Deg2Rad), Mathf.Cos(node["roll"] * Mathf.Deg2Rad), 0);
            Quaternion rotation = Quaternion.LookRotation(_targetPosition - cameraPosition, upwards);
            LeanTween.rotate(transform.gameObject, rotation.eulerAngles, transitionDuration).setEaseInOutCubic();
            //LeanTween.value(transform.gameObject, updateFieldOfView, _camera.fieldOfView, node["fieldOfView"], transitionDuration);
            
            /*
            void updateFieldOfView(float val, float ratio)
            {
                _camera.fieldOfView = val;
            }
            */
        }

        public void HandleMessage(string data)
        {
            JSONNode json = JSON.Parse(data);
            messageReceivedEvent.Invoke(json);
        }
        
        #region Hyperlink Handling
        private void OnSpatialLinkClicked(string spatialLinkSlug)
        {
            Debug.Log(spatialLinkSlug);

            if (spatialLinkSlug == "lyre") 
                JumpToLyre();
                /*
            if (spatialLinkSlug.Contains(ScalarUtilities.roomSpatialAnnotationTag))
            {
                _currentLinkID = spatialLinkSlug;
                StartCoroutine(ScalarAPI.LoadNode(
                    spatialLinkSlug,
                    OnPageLoadSuccess,
                    OnPageLoadFail,
                    2,
                    true,
                    "annotation"
                ));
                

            }
            */

        }

        private void JumpToLyre()
        {           
            _targetPosition = TargetPos.position;
            Vector3 cameraPosition = CameraPos.position;
            LeanTween.cancel(transform.gameObject);
            LeanTween.move(transform.gameObject, cameraPosition, transitionDuration).setEaseInOutCubic();
            //Vector3 upwards = new Vector3(Mathf.Sin(node["roll"] * Mathf.Deg2Rad), Mathf.Cos(node["roll"] * Mathf.Deg2Rad), 0);
            Quaternion rotation = Quaternion.LookRotation(_targetPosition - cameraPosition, Vector3.up);
            LeanTween.rotate(transform.gameObject, rotation.eulerAngles, transitionDuration).setEaseInOutCubic();
            CreateLine?.Invoke(CameraPos.position,TargetPos.position);
            
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

        private IEnumerator DelayCreateLine(Vector3 camPos, Vector3 targetPos)
        {
            yield return new WaitForSeconds(3);

            CreateLine?.Invoke(camPos,targetPos);

        }

        
    }
    
    
}





[System.Serializable]
public class AnnotationSelectedExternallyEvent : UnityEvent<JSONNode> { }

[System.Serializable]
public class MessageReceivedEvent : UnityEvent<JSONNode> { }