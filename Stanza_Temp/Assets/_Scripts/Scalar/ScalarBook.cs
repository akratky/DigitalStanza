using System;
using System.Collections;
using System.Collections.Generic;
using ANVC.Scalar;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class ScalarBook : MonoBehaviour
{
    //this will be the home page of the manuscript we are streaming in
    public string manuscriptRootURLSlug;

    public Material leftPage;
    public Material rightPage;

    //denotes the index of the left page
    private int _currentPageindex = 0;
    
    //denotes last page of current manuscript
    private int _lastPageindex;

    private ScalarNode _rootNode;
    private ScalarNode _currentLeftPage;
    private ScalarNode _currentRightPage;
    public TextMeshPro textMeshPro;
        

    void Start()
    {
        LoadManuscriptRoot();
        
    }

    #region Root Page

    private void LoadManuscriptRoot()
    {
        StartCoroutine(ScalarAPI.LoadNode(manuscriptRootURLSlug,
            OnLoadRootSuccess,
            OnLoadRootFailure,
            3,
            true,
            "referee"));
    }

    private void OnLoadRootSuccess(JSONNode jsonNode)
    {
        _rootNode = ScalarAPI.GetNode(manuscriptRootURLSlug);
        //subtract by two because we are actually starting on page 0
        _lastPageindex = _rootNode.outgoingRelations.Count - 2 ;
        LoadPages(_currentPageindex);
        

    }
    private void OnLoadRootFailure(string e)
    {
        Debug.LogError("Unable to retrieve scalar book");
        Debug.LogError(e);        
    }


    #endregion
    
    #region General Page Functions

    private void Update()
    {
        //temporary - TODO - replace this with proper input scripts
        if (Input.GetKey(KeyCode.LeftArrow))
            GotoPreviousPage();
        else if (Input.GetKey(KeyCode.RightArrow))
            GotoNextPage();
    }

    public bool GotoNextPage()
    {
        if (_currentPageindex != _lastPageindex)
        {
            _currentPageindex += 2;
            LoadPages(_currentPageindex);
            return true;

        }

        return false;
    }

    public bool GotoPreviousPage()
    {
        if (_currentPageindex != 0)
        {
            _currentPageindex -= 2;
            LoadPages(_currentPageindex);
            return true;
        }
        
        return true;
    }
    
    private void LoadPages(int pageNum)
    {
        _currentLeftPage = _rootNode.outgoingRelations[pageNum].target;
        
        //todo - add error checking for trying to open page that doesn't exist
        
        //iterate through outgoing relations of page to find image
        foreach (var rel in _currentLeftPage.outgoingRelations)
        {
            if (rel.target.slug.Contains("img"))
            {
                //create URL to source image
                string imgURL = ScalarAPI.urlPrefix  + rel.target.thumbnail;
                
                //remove "_thumb" from image url, if needed
                int thumbStart = -1;
                thumbStart = imgURL.IndexOf("_thumb");
                
                if(thumbStart != -1)
                    imgURL = imgURL.Remove(thumbStart, 6);
                    
                //download manuscript image and set ingame material
                StartCoroutine(DownloadImage(imgURL, true));
                
                
                //get annotation for this page
                textMeshPro.text = ScalarUtilities.ExtractRichTextFromHTMLSource(_currentLeftPage.current.content,
                    this);

            }

        }
        
        _currentRightPage = _rootNode.outgoingRelations[pageNum + 1].target;
        foreach (var rel in _currentRightPage.outgoingRelations)
        {
            if (rel.target.slug.Contains("img"))
            {
                //create URL to source image
                string imgURL = ScalarAPI.urlPrefix  + rel.target.thumbnail;
                
                //remove "_thumb" from image url, if needed
                int thumbStart = -1;
                thumbStart = imgURL.IndexOf("_thumb");
                
                if(thumbStart != -1)
                    imgURL = imgURL.Remove(thumbStart, 6);
                    
                StartCoroutine(DownloadImage(imgURL, false));

            }
        }
        
        //after we load these pages we 'load' them again to be able to go deeper on the scalar node tree
        //todo - see above
    }

    

    #endregion

    //downloads image from URL
    private IEnumerator DownloadImage(string mediaURL,bool isLeftPage)
    {
        
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(mediaURL);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError
            || request.result == UnityWebRequest.Result.ProtocolError
            || request.result == UnityWebRequest.Result.DataProcessingError)
        {
            Debug.LogError("Unable to retrieve image from URL");
            Debug.LogError(request.result);
        }
        else
        {
            if(isLeftPage)
                leftPage.mainTexture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            else
                rightPage.mainTexture = ((DownloadHandlerTexture) request.downloadHandler).texture;
        }
    }

    
}
