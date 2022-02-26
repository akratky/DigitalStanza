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

    public GameObject line;

    //denotes the index of the left page
    private int _currentPageindex = 0;
    

    private ScalarNode _rootNode;

    

    void Start()
    {
        LoadManuscriptRoot();
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
            0,
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

        ScalarNode currentPage;

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
            neighbourURL = neighbourURL.TrimEnd('r');
        else
            neighbourURL = neighbourURL.TrimEnd('v');

        ScalarNode neighborNode = null;
        
        if (_currentPageindex - 1 > 0)
        {
            neighborNode = _rootNode.outgoingRelations[_currentPageindex - 1].target;
            if(neighborNode != null)
                if (neighborNode.slug.Contains(neighbourURL))
                    return neighborNode;
        }

        if (_currentPageindex + 1 < _rootNode.outgoingRelations.Count)
        {
            
            neighborNode = _rootNode.outgoingRelations[_currentPageindex + 1].target;
            if(neighborNode != null)
                if (neighborNode.slug.Contains(neighbourURL))
                    return neighborNode;
        }

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

    /* Line Renderer Controller*/

    public void showLine()
    {
        StartCoroutine(LineCoroutine());

    }

    IEnumerator LineCoroutine()
    {
        yield return new WaitForSeconds(2);
        
        line.SetActive(true);
        LineRenderer renderer = line.GetComponent<LineRenderer>();
        renderer.SetPosition(0, this.transform.position);
        renderer.SetPosition(1, new Vector3(0, 0.5f, 1));
        
    }
}
