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
    

    private ScalarNode _rootNode;
    
    

    void Start()
    {
        LoadManuscriptRoot();
        TMP_TextEventHandler.OnManuscriptLinkSelected += JumpToLyrePage;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            GotoNextPage();
        else if (Input.GetKeyDown(KeyCode.N))
            GotoPreviousPage();
    }

    #region Root Page

    private void LoadManuscriptRoot()
    {
        StartCoroutine(ScalarAPI.LoadNode(manuscriptRootURLSlug,
            OnLoadRootSuccess,
            OnLoadRootFailure,
            4,
            true,
            "referee"));
        
    }

    private void OnLoadRootSuccess(JSONNode jsonNode)
    {
        _rootNode = ScalarAPI.GetNode(manuscriptRootURLSlug);

        LoadPages(_currentPageindex);

    }

    private void OnLoadRootFailure(string e)
    {
        Debug.LogError("Unable to retrieve scalar book");
        Debug.LogError(e);
    }


    #endregion

    #region General Page Functions


    public void GetNext()
    {
        GotoNextPage();
    }

    public bool GotoNextPage()
    {
        print("next page");
        if (_currentPageindex + 2 <= _rootNode.outgoingRelations.Count - 1)
        {
            _currentPageindex += 2;
            ClearPages();
            LoadPages(_currentPageindex);

            return true;
        }

        return false;
    }

    public bool GotoPreviousPage()
    {
        print("prev page");

        if (_currentPageindex - 2 >= 0)
        {
            _currentPageindex -= 2;
            ClearPages();
            LoadPages(_currentPageindex);

            return true;
        }
        

        return false;
    }

    private void LoadPages(int pageNum)
    {
        //todo - has issues - fix later
        ScalarNode currentPage = _rootNode.outgoingRelations[pageNum].target;

        if (pageNum == 0)
            currentPage = _rootNode;
        else
            currentPage = _rootNode.outgoingRelations[pageNum-1].target;


        bool isRecto;
        isRecto = currentPage.slug.EndsWith("r");

        string imgURL = ScalarUtilities.ExtractImgURLFromScalarNode(currentPage);
        
        StartCoroutine(DownloadImage(imgURL, !isRecto));

        //determine if current page has a corresponding neighbour page
        ScalarNode neighborNode = GetNeighborPage(currentPage, isRecto);
        imgURL = ScalarUtilities.ExtractImgURLFromScalarNode(neighborNode);
        
        if (neighborNode != null)
        {
            StartCoroutine(DownloadImage(imgURL, isRecto));
        }

    }

    private void ClearPages()
    {
        leftPage.mainTexture = null;
        rightPage.mainTexture = null;
    }

    #endregion

    //a neighbor page is the corresponding left-hand/right-hand side of a page
    //i.e. 001r and 001v are neighbor pages
    private ScalarNode GetNeighborPage(ScalarNode currentPage, bool isRecto)
    {
        string neighbourURL = currentPage.slug;
        if (isRecto)
        {
            neighbourURL = neighbourURL.TrimEnd('r');
            neighbourURL += 'v';

        }
        else
        {
            neighbourURL = neighbourURL.TrimEnd('v');
            neighbourURL += 'r';
        }

        ScalarNode neighborNode = null;


        int lowerBound = Mathf.Clamp(_currentPageindex - 3, 0, _rootNode.outgoingRelations.Count);
        int upperBound = Mathf.Clamp(_currentPageindex + 3, 0, _rootNode.outgoingRelations.Count + 1);
        
        for(int i = lowerBound; i < upperBound; i++)
            if (_rootNode.outgoingRelations[i].target.slug.Contains(neighbourURL))
                return _rootNode.outgoingRelations[i].target;

        return null;
    }
    
    //downloads image from URL
    private IEnumerator DownloadImage(string mediaURL, bool isLeftPage)
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
            if (isLeftPage)
                leftPage.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            else
                rightPage.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }

    private void JumpToLyrePage(string tag)
    {
        //if(tag == "lyre")
            //LoadPages();
    }


}
