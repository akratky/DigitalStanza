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
    [Header("Book Params")]
    //this will be the home page of the manuscript we are streaming in
    public string manuscriptRootURLSlug;
    //how many digits are in the page number for this manuscript
    public int numDigitsInPageNumber;
    [Header("References")]
    public Material leftPageMaterial;
    public Material rightPageMaterial;
    public BookAnnotationHandler leftPageAnnotationHandler;
    public BookAnnotationHandler rightPageAnnotationHandler;

    //denotes the index of the left page
    private int _currentPageindex = 0;
    

    private ScalarNode _rootNode;
    //do this for quick look up for manuscript annotations
    private Dictionary<string, int> _pageSlugToIndexDict;
    private Dictionary<ScalarNode, ScalarNode> _neighborPageDict = new Dictionary<ScalarNode, ScalarNode>();
    
    public delegate void OnStartBookLoad();
    public static event OnStartBookLoad OnStartBookLoadEvent;

    public delegate void BookAnnotationDelegate(string pageSlug,bool isRecto);
    public static event BookAnnotationDelegate BookAnnotationEvent;
    
    //annotation/link handling
    private string _targetSlug;


    void Start()
    {
        TMP_TextEventHandler.OnManuscriptLinkSelected += HandleTripleLink;
        
        //LoadManuscriptRoot();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            GotoNextPage();
        else if (Input.GetKeyDown(KeyCode.N))
            GotoPreviousPage();
    }

    #region Root Page

    public void LoadManuscriptRoot()
    {
        OnStartBookLoadEvent?.Invoke();
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
        
        
        //build library of manuscript pages to make manu annotations easier later
        StartCoroutine(BuildPageLibrary(_rootNode));
    }

    private void OnLoadRootFailure(string e)
    {
        Debug.LogError("Unable to retrieve scalar book");
        Debug.LogError(e);
    }


    #endregion

   // #region General Page Functions


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

        //edge cases
        if (pageNum == 0)
            currentPage = _rootNode;
        else
            currentPage = _rootNode.outgoingRelations[pageNum-1].target;


        bool isRecto;
        isRecto = currentPage.slug.EndsWith("r");

        string imgURL = ScalarUtilities.ExtractImgURLFromScalarNode(currentPage);
        
        StartCoroutine(DownloadImage(imgURL, !isRecto));

        //determine if current page has a corresponding neighbour page
        ScalarNode neighborNode = null;
        if (_neighborPageDict.ContainsKey(currentPage))
            neighborNode = _neighborPageDict[currentPage];
        else
            neighborNode = GetNeighborPage(currentPage, isRecto);
        
        
        imgURL = ScalarUtilities.ExtractImgURLFromScalarNode(neighborNode);
        
        if (neighborNode != null)
        {
            StartCoroutine(DownloadImage(imgURL, isRecto));
        }

    }

    private void ClearPages()
    {
        leftPageMaterial.mainTexture = null;
        rightPageMaterial.mainTexture = null;
    }

    //#endregion
    
    //all this does is turn to the appropriate page and trigger the book annotation handler
    #region Link Handling

    private void HandleTripleLink(string linkSlug)
    {
        if (linkSlug.Contains(ScalarUtilities.manuscriptAnnotationTag))
        {
            _targetSlug = linkSlug;

            StartCoroutine(ScalarAPI.LoadNode(
                linkSlug,
                OnManuscriptAnnoLoadSuccess,
                OnManuscriptAnnoLoadFail,
                2,
                true,
                "referee"
            ));

        }
    }

    private void OnManuscriptAnnoLoadSuccess(JSONNode node)
    {
        ScalarNode manuNode = ScalarAPI.GetNode(_targetSlug);
        string pageSlug = null; 
            
        //flip to page containing clicked on annotation
        foreach (var rel in manuNode.outgoingRelations)
        {
            if (_pageSlugToIndexDict.ContainsKey(rel.target.slug))
            {
                pageSlug = rel.target.slug;
                int pageNum = _pageSlugToIndexDict[rel.target.slug];
                LoadPages(pageNum);
                break;
            }
        }

        bool isRecto = pageSlug.EndsWith("r");
        BookAnnotationEvent?.Invoke(_targetSlug,isRecto);
        
    }

    private void OnManuscriptAnnoLoadFail(string err)
    {
        Debug.LogError("Unable to load manuscript annotation");
        Debug.LogError(err);
    }
    
    
    #endregion
    
    #region Utility

    //a neighbor page is the corresponding left-hand/right-hand side of a page
    //i.e. 001r and 001v are neighbor pages
    //Constraint: Assumes there are less than 1000 pages in manuscript
    private ScalarNode GetNeighborPage(ScalarNode currentPage, bool isRecto)
    {
        string neighbourURL = currentPage.slug;
        if (isRecto)
        {
            neighbourURL = neighbourURL.TrimEnd('r');
            
            //get page number from URL and convert to int then incremement to get url of neighbor page
            string[] neighbourURLSub = neighbourURL.Split('-');
            
            int neighbourpageNum = int.Parse(neighbourURLSub[neighbourURLSub.Length-1]);
            neighbourpageNum++;


            int numLeadingZeroesNeeded = 0;
            //figure out how many leading zeroes are needed for URL - defs an easier way 
            for (int i = 1; i < numDigitsInPageNumber - 1; i++)
            {
                if (neighbourpageNum < (10 ^ i))
                {
                    numLeadingZeroesNeeded = numDigitsInPageNumber - i;
                    break;
                }
            }

            string neighbourPageString = neighbourpageNum.ToString();
            //add leading zeroes needed for URL
            for (int i = 0; i < numLeadingZeroesNeeded; i++)
                neighbourPageString = neighbourPageString.Insert(0, "0");
            

            neighbourURL = "";
            //reconstruct URL for neighbor page
            for (int i = 0; i < neighbourURLSub.Length - 1; i++)
                neighbourURL += neighbourURLSub[i] + "-";

            neighbourURL += neighbourPageString;
 
            
            neighbourURL += 'v';

        }
        else
        {
            neighbourURL = neighbourURL.TrimEnd('v');
            
            //get page number from URL and convert to int then incremement to get url of neighbor page
            string[] neighbourURLSub = neighbourURL.Split('-');
            
            int neighbourpageNum = int.Parse(neighbourURLSub[neighbourURLSub.Length-1]);
            neighbourpageNum++;


            int numLeadingZeroesNeeded = 0;
            //figure out how many leading zeroes are needed for URL - defs an easier way 
            for (int i = 1; i < numDigitsInPageNumber - 1; i++)
            {
                if (neighbourpageNum < (10 ^ i))
                {
                    numLeadingZeroesNeeded = numDigitsInPageNumber - i;
                    break;
                }
            }

            string neighbourPageString = neighbourpageNum.ToString();
            //add leading zeroes needed for URL
            for (int i = 0; i < numLeadingZeroesNeeded; i++)
                neighbourPageString = neighbourPageString.Insert(0, "0");
            

            neighbourURL = "";
            //reconstruct URL for neighbor page
            for (int i = 0; i < neighbourURLSub.Length - 1; i++)
                neighbourURL += neighbourURLSub[i] + "-";

            neighbourURL += neighbourPageString;
            neighbourURL += 'r';
        }
        
        //check local neighbourhood for neighbour page
        int lowerBound = Mathf.Clamp(_currentPageindex - 3, 0, _rootNode.outgoingRelations.Count);
        int upperBound = Mathf.Clamp(_currentPageindex + 3, 0, _rootNode.outgoingRelations.Count + 1);
        
        for(int i = lowerBound; i < upperBound; i++)
            if (_rootNode.outgoingRelations[i].target.slug.Contains(neighbourURL))
            {
                //store neighbour in dictionary for quick lookup later
                _neighborPageDict[currentPage] = _rootNode.outgoingRelations[i].target;
                
                return _rootNode.outgoingRelations[i].target;
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
                leftPageMaterial.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            else
                rightPageMaterial.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }

    private IEnumerator BuildPageLibrary(ScalarNode rootNode)
    {
        _pageSlugToIndexDict = new Dictionary<string, int>(rootNode.outgoingRelations.Count);
        _pageSlugToIndexDict[rootNode.slug] = 0;
        
        int i = 1;
        foreach (var rel in rootNode.outgoingRelations)
        {
            if (rel.type.id == "path")
            {
                _pageSlugToIndexDict[rel.target.slug] = i;        
                i++;    
            }
            
        }
        
        
        yield return null;
    }

    #endregion

    
  


}
