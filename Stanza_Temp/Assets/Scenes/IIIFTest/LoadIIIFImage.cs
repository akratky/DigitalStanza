using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LoadIIIFImage : MonoBehaviour
{

    public string IIIFUrl;
    private MeshRenderer _renderer;

    public bool bUseNoCorsFetchMode;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();

        StartCoroutine(GetText());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetText()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(IIIFUrl);

        if (bUseNoCorsFetchMode)
        {
            request.SetRequestHeader("Set-Fetch-Mode","no-cors");
        }
        
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
            _renderer.material.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
    }
}
