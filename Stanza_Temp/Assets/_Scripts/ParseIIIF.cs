using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UIElements;
using UnityEngine.Networking;

public class ParseIIIF : MonoBehaviour
{
    public string path; 
    

    public GameObject book;

    public Material page1;
    public Material page2;
    public int book_index = 3;

    Texture2D curr_tex;
    Manifest json;
  //  WWW file;
    public int max_pages = 0;

    //tester
    //https://digi.vatlib.it/iiif/MSS_Vat.lat.1125/manifest.json

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadRemoteURL(path));
    }

    /* Parse JSON */
    void ParseJson(string data) {
        json = JsonConvert.DeserializeObject<Manifest>(data);
        max_pages = json.sequences[0].canvases.Count;

        //json.sequences[0].canvases[0].images[0].resource.service["@id"];
        //get the image uri
        //eg. https://digi.vatlib.it/iiifimage/MSS_Vat.lat.1125/Vat.lat.1125_0004_cy_0001v.jp2/0,0,2128,3072/266,/0/native.jpg

        GetNewImage(book_index);
    }

    /* Change Images in Book */

    public void OnMouseDown()
    {
        if (book_index < max_pages)
        {
            GetNewImage(++book_index);
        }
        else {
            book_index = -1;
            Debug.Log("Warning: End of book reached; Resetting scene");
        }
    }

    void GetNewImage(int index) {
        string temp = json.sequences[0].canvases[index].images[0].resource.service["@id"] + "/200,200,3000,2550/512,/0/native.jpg";
        DownloadImage(temp);
    }


    /* Download Remote Assets */

    public void DownloadImage(string url) {
         
        StartCoroutine(ImageRequest(url, (UnityWebRequest req) => {
            if (req.isHttpError || req.isNetworkError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            }
            else {
                Texture2D texture = DownloadHandlerTexture.GetContent(req);
                
                if (book_index == 0)
                {
                    page2.SetTexture("_MainTex", texture);
                    curr_tex = texture;
                }
                else {
                    page2.SetTexture("_MainTex", texture);
                    page1.SetTexture("_MainTex", curr_tex);
                    curr_tex = texture;
                }
               // book.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }));
    }


    IEnumerator ImageRequest(string path, System.Action<UnityWebRequest> callback) {
        using (UnityWebRequest req =  UnityWebRequestTexture.GetTexture(path)) {
            yield return req.SendWebRequest();
            callback(req);
        }
    
    }

    IEnumerator LoadRemoteURL(string path)
    {
        var file = new UnityWebRequest(path);
        file.downloadHandler = new DownloadHandlerBuffer();
        yield return file.SendWebRequest();

        
        if (file.error == null)
        {
            ParseJson((file.downloadHandler.text));
        }
        else
        {
            Debug.Log("ERROR: invalid path" + path);
        }
    }
}

/* JSON Structs */

[System.Serializable]
public class Manifest
{
    public string context;
    public string id; 
    public string type;
    public string label;
    public List<Metadata> metadata;
    public List<Description> description;
    public string license;
    public string attribution;
    public string logo;
    public List<Service> service;
    public string related;
    public string within;
    public List<Sequence> sequences;
    public List<string> structure; 
}

[System.Serializable]
public class Service {
    public string context;
    public string id;
    public string profile;
    public string label;
}

[System.Serializable]
public class Description {
    public string value;
    public string language; 
}

[System.Serializable]
public class Metadata {
    public string label;
}

[System.Serializable]
public class Sequence
{
    public string @id;
    public string @type;
    public List<string> label; //probably not going to use
    public List<Canvas> canvases;
}

[System.Serializable]
public class Canvas
{
    public string @id;
    public string @type;
    public string label;
    public int width;
    public int height; 
    public List<Image> images;
}

[System.Serializable]
public class Image {
    public string id;
    public string type; 
    public string on;
    public string motivation;
    public ImageResources resource { get; set; }
}

[System.Serializable]
public class ImageResources
{
    public string @id;
    public string @type;
    public string width;
    public int height;
    public string format;
    public Dictionary<string, string> service { get; set; }
}