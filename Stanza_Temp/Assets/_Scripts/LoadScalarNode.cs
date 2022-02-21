using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ANVC.Scalar;
using SimpleJSON;

public class LoadScalarNode : MonoBehaviour
{
    public string nodeIndex = "index"; //"digital-stanza"
    public string nodePath = "path"; //  "annotation" <- type of data element
    public string connectionType = "outgoing"; //"incoming"

    public string jsonType = "annotation";
    public string jsonDirection = "incoming";

    public List<float[]> CamCoordList = new List<float[]>();

    // Start is called before the first frame update
    void Start()
    {
        // Request the home page for the book, plus its path relationships
        // int is num of relationships

        StartCoroutine(ScalarAPI.LoadNode(nodeIndex, HandleSuccess, HandleError, 1, false, nodePath));
    }

    public void GetNextTransform() {
        
    }

    //Where JSON data from LoadNode gets passed
    public void HandleSuccess(JSONNode json)
    {
        //Debug.Log("Received Scalar data");

       

        //Get singular element in book
        ScalarNode indexPage = ScalarAPI.GetNode(nodeIndex);
        List<ScalarNode> tempNodes = indexPage.GetRelatedNodes(jsonType, jsonDirection);

        // shell that contains all info associated with node
        
       Debug.Log("node title " + tempNodes[0].outgoingRelations.Count);
        Debug.Log("node count " + tempNodes[1]);

        foreach (ScalarNode temp in tempNodes) {
            float[] cam_coord = new float[3];
            List<ScalarRelation> tempTransform = temp.GetRelations("annotation", "outgoing");
            cam_coord[0] = float.Parse( tempTransform[0].properties.cameraX) ;
            cam_coord[1] = float.Parse(tempTransform[0].properties.cameraY);
            cam_coord[2] = float.Parse(tempTransform[0].properties.cameraZ);
           // Debug.Log($"cam coord: {cam_coord[0]}, {cam_coord[1]} , {cam_coord[2]}");
            CamCoordList.Add(cam_coord);
        }
    }

    public void HandleError(string error)
    {
        Debug.Log(error);
    }


    /*
     Steps
    0. Figure out which keys are annotations and which are coordinates
    1. Find key with this value "scalar-ns#urn"
    2. parse out under this key "urn:scalar:version:"
    3. Generate new key from  urn:scalar:version:xxx , xxx is number
    4. Store number ,and look for number in JSON again
    http://www.openannotation.org/ns/hasBody //anno
    urn:scalar:anno:


    http://www.openannotation.org/ns/hasTarget //position
     
     */


    private static List<ScalarNode> ParseNodes(JSONNode data)
    {
        bool isNode;
        ScalarNode node;
        string versionUrl = "";
        List<ScalarNode> resultNodes = new List<ScalarNode>();
        List<JSONNode> versionData = new List<JSONNode>();

        //find all necessary keys
        foreach (var key in data.Keys)
        {
            if (key.Contains("urn:scalar:anno:"))
            {
                Debug.Log("key has " + key);

                Debug.Log(key.Split(':')[3]);

                //find keys with hasBody and hasTarget
            }

        }

        

        return resultNodes;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
