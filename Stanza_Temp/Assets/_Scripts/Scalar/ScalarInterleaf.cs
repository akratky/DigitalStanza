using System.Collections;
using System.Collections.Generic;
using ANVC.Scalar;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ScalarInterleaf : MonoBehaviour
{
    
    public string interleafURLSlug;
    public TMP_Text currentPageTextObj;
    public TMP_Text totalPageTextObj;
    private TMP_Text textMeshText;

    private string fullParatext;
    
    // Start is called before the first frame update
    void Start()
    {
        textMeshText = GetComponent<TMP_Text>();
        //LoadInterleafText();
        ScalarBook.OnStartBookLoadEvent += LoadInterleafText;

    }

    
    #region interleaf loading functions

    
    private void LoadInterleafText()
    {
        Debug.Log("Load interleaf");
        StartCoroutine(ScalarAPI.LoadNode(
            interleafURLSlug,
            OnLoadInterleafSuccess,
            OnLoadInterleafFail,
            2,
            true,
            "referee"
        ));

    }

    private void OnLoadInterleafSuccess(JSONNode node)
    {
        LoadInterleafPage(interleafURLSlug);
    }

    private void OnLoadInterleafFail(string e)
    {
        Debug.LogError("Unable to load interleaf text");
        Debug.LogError(e);
    }

    private void LoadInterleafPage(string slug)
    {
        Debug.Log("load interleaf page function");
        ScalarNode interleafNode = ScalarAPI.GetNode(slug);
        
        string interleafText = ScalarUtilities.ExtractRichTextFromInterleafBody(interleafNode.current.content);

        fullParatext = interleafText;
        textMeshText.text = interleafText;
        UpdatePageUI();
    }

    private void UpdatePageUI()
    {
        currentPageTextObj.text = textMeshText.pageToDisplay.ToString();
        totalPageTextObj.text = textMeshText.textInfo.pageCount.ToString();
    }

    public void GoBackToParatext()
    {
        textMeshText.text = fullParatext;
    }

    #endregion

    #region page turning functions

    public void GoToNextInterleafPage()
    {
        if (textMeshText.pageToDisplay != textMeshText.textInfo.pageCount)
        {
            textMeshText.pageToDisplay++;
            UpdatePageUI();
        }
    }

    public void GoToPrevInterleafPage()
    {

        if (textMeshText.pageToDisplay > 1)
        {
            textMeshText.pageToDisplay--;
            UpdatePageUI();
        }
    }

    #endregion
    
}
