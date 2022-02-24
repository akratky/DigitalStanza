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
    public string manuscriptRootURLSlug = "test-scalar-book";

    public Material leftPage;
    public Material rightPage;

    //denotes the index of the left page
    private int _currentPageindex = 0;
    
    //denotes last page of current manuscript
    private int _lastPageindex;

    private ScalarNode _rootNode;
    private ScalarNode _currentLeftPage;
    private ScalarNode _currentRightPage;
    private List<ScalarNode> _allNodes;
    public TMP_Text textMeshPro;

    private string _currentPageText;
    private string _currentAnnotationText;

    void Start()
    {
        LoadManuscriptRoot();
        
    }

    #region Root Page

    private void LoadManuscriptRoot()
    {
        StartCoroutine(ScalarAPI.LoadNode(
          "index",
          OnLoadRootSuccess,
          OnLoadRootFailure,
          1,
          false,
          "path"
      ));
    }

    private void OnLoadRootSuccess(JSONNode jsonNode)
    {
        _rootNode = ScalarAPI.GetNode("index");
        Debug.Log("last " + _rootNode.GetRelatedNodes("annotation", "incoming"));
        _allNodes  = _rootNode.GetRelatedNodes("annotation", "incoming");

        _lastPageindex = _allNodes.Count - 1 ;


        Debug.Log(" _rootNode.current.content " + _rootNode);
        
        textMeshPro = new TextMeshPro();
        textMeshPro.text = "sdfsdf";
        textMeshPro.text = "something";

        if (textMeshPro.text != null)
        {
            textMeshPro.text = ScalarUtilities.ExtractRichTextFromHTMLSource(
               _rootNode.current.content, this
           );
        }
        else {
            Debug.Log("Unable sdklfjskldj");
        }

        Debug.Log("waht is tmp "+ textMeshPro.text);

    }
    private void OnLoadRootFailure(string e)
    {
        Debug.LogError("Unable to retrieve scalar book");
        Debug.LogError(e);        
    }


    #endregion

    #region General Page Functions

    public void GotoNextPage()
    {
        if (_currentPageindex != _lastPageindex)
        {
            _currentPageindex += 2;
            LoadPages(_currentPageindex);
            

        }

        
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

       if (_allNodes[pageNum] != null) { 
            _currentLeftPage = _allNodes[pageNum]; 
        } 
       else {
            Debug.Log("_currentLeftPage is null");
        }

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
