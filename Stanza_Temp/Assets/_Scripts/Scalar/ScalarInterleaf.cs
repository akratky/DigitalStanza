using System.Collections;
using System.Collections.Generic;
using ANVC.Scalar;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScalarInterleaf : MonoBehaviour
{

    public string interleafURLSlug;
    private TMP_Text textMeshText;

    public delegate void OnInterleafTextChange(string s);

    public static event OnInterleafTextChange InterleafTextChangeEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        textMeshText = GetComponentInChildren<TMP_Text>();
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
        ScalarNode interleafNode = ScalarAPI.GetNode(interleafURLSlug);


        string interleafText = ScalarUtilities.ExtractRichTextFromInterleafBody(interleafNode.current.content);

    }

    private void OnLoadInterleafFail(string e)
    {
        Debug.LogError("Unable to load interleaf text");
        Debug.LogError(e);
    }

    #endregion
    
}
