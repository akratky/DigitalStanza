using System;
using System.Collections;
using System.Collections.Generic;
using ANVC.Scalar;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SecondaryParatext : MonoBehaviour
{

    
    
    [Header("Asset refs")]
    public GameObject interleafObj; //what is toggled off/on
    public TMP_Text secondaryInterleafBody; //object containing TMP body text
    public TMP_Text secondaryInterleafHeader; //obj containing TMP header text
    public PlayerBookPickerUp pickerUpper;
    [Header("UI Refs")]
    public GameObject UIBackButton; //secondary interleaf back button

    public TMP_Text currentPageCounterTMP;
    public TMP_Text totalPageCounterTMP;
    public TMP_Text breadcrumbTMP;

    [Header("Line Renderer")]
    public BookLineRenderer lineRenderer;

    public GameObject lineStart;
    
    private string _currentLinkID;

    private ScalarCamera _scalarCamera;
    // Start is called before the first frame update
    void Start()
    {
        //because secondary interleaf text is on the spatial scalar pages
        TMP_TextEventHandler.OnSpatialLinkSelected += OnAnnotationClicked;

        _scalarCamera = GameObject.FindWithTag("MainCamera").GetComponent<ScalarCamera>();
        
        secondaryInterleafBody.text = string.Empty;
        secondaryInterleafHeader.text = string.Empty;
        UIBackButton.SetActive(false);
        interleafObj.SetActive(false);
    }

    #region scalar page handling

    
    private void OnAnnotationClicked(string tag)
    {
        //have to retrieve scalar page and check for text
        TripleLinkStruct tripleLink = ScalarTripleLink.GetTripleLink(tag);
        try
        {
            if (tripleLink.spatialLink.Length <= 0)
            {
                return;
            }

            string spatialLink = tripleLink.spatialLink;
            string[] splitStrings = spatialLink.Split('#');
            spatialLink = splitStrings[^1];
            Debug.Log("Accessing spatial link: " + spatialLink);
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

        catch (NullReferenceException e)
        {
            Debug.LogError("Tried to access null spatial link: " + e.Message);
        }
    }

    private void OnPageLoadSuccess(JSONNode node)
    {
        ScalarNode paratextNode = ScalarAPI.GetNode(_currentLinkID);
        
        //determine whether there is actually text on this page in an arbitrary way...
        if (paratextNode.current.content.Length > 2)
        {
            string parsedInterleafText = ScalarUtilities.ExtractRichTextFromInterleafBody(paratextNode.current.content);
            secondaryInterleafBody.text = parsedInterleafText;
            secondaryInterleafHeader.text = paratextNode.current.title;
            secondaryInterleafBody.pageToDisplay = 1;
            
            interleafObj.SetActive(true);
            UIBackButton.SetActive(true);

            //update scalar camera to focus on spatial annotation
            foreach (var rel in paratextNode.outgoingRelations)
            {
                if (rel.subType == "spatial")
                {
                    _scalarCamera.SetTransformNoEvent(rel.properties);
                }
            }
            
            StartCoroutine(UpdatePageUIDelay());
            StartCoroutine(WaitToCreateLine());
            UpdateBreadcrumbDisplay();
            

        }
    }

    IEnumerator WaitToCreateLine()
    {
        yield return new WaitForSeconds(1);
        GameObject wallFrame = GameObject.FindWithTag("WallAnnotation");
        lineRenderer.TrackingLine(lineStart,wallFrame);
    }
    private void OnPageLoadFail(string err)
    {
        Debug.LogError(err);
    }
    
    

    #endregion

    #region UI Handling

    private void UpdatePageUI()
    {
        currentPageCounterTMP.text = secondaryInterleafBody.pageToDisplay.ToString();
        totalPageCounterTMP.text = secondaryInterleafBody.textInfo.pageCount.ToString();
    }

    private IEnumerator UpdatePageUIDelay()
    {
        yield return new WaitForSeconds(0.25f);

        currentPageCounterTMP.text = secondaryInterleafBody.pageToDisplay.ToString();
        totalPageCounterTMP.text = secondaryInterleafBody.textInfo.pageCount.ToString();
    }

    private void UpdateBreadcrumbDisplay()
    {
        string primaryText = pickerUpper.currentlyHeldBook.bookName;

        string secondaryText = secondaryInterleafHeader.text;

        breadcrumbTMP.text = primaryText + "/" + secondaryText;
    }
    public void GoToNextPage()
    {
        if (secondaryInterleafBody.pageToDisplay != secondaryInterleafBody.textInfo.pageCount)
        {
            secondaryInterleafBody.pageToDisplay++;
            UpdatePageUI();
        }
    }

    public void GoToPrevPage()
    {
        if (secondaryInterleafBody.pageToDisplay > 1)
        {
            secondaryInterleafBody.pageToDisplay--;
            UpdatePageUI();
        }
    }

    public void BackButton()
    {
        interleafObj.SetActive(!interleafObj.activeSelf);
        BookLineRenderer.DestroyLineEvent?.Invoke();
    }
    

    #endregion
    


}
