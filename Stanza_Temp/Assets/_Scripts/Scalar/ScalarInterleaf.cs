using System.Collections;
using System.Collections.Generic;
using ANVC.Scalar;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ScalarInterleaf : MonoBehaviour
{

    public string interleafURLSlug;
    public TMP_Text currentPageTextObj;
    public TMP_Text totalPageTextObj;
    private TMP_Text textMeshText;

    
    // Start is called before the first frame update
    void Start()
    {
        textMeshText = GetComponent<TMP_Text>();
        LoadInterleafText();
        UpdatePageUI();

    }

    
    #region interleaf loading functions

    
    private void LoadInterleafText()
    {
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
        ScalarNode interleafNode = ScalarAPI.GetNode(slug);
        
        string interleafText = ScalarUtilities.ExtractRichTextFromInterleafBody(interleafNode.current.content);

        textMeshText.text = interleafText;
    }

    private void UpdatePageUI()
    {
        currentPageTextObj.text = textMeshText.pageToDisplay.ToString();
        totalPageTextObj.text = textMeshText.textInfo.pageCount.ToString();
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
