

using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

public struct TripleLinkStruct
{
    
    public string detailLink;
    public string manuscriptLink;
    public string spatialLink;
    
}

public static class ScalarTripleLink
{

    private static Dictionary<string,TripleLinkStruct> tripleLinkList = new Dictionary<string, TripleLinkStruct>();

    //takes in a string containing the components of a triple link
    //parses that string and adds it to the list that can be accessed elsewhere
    public static string ParseAndAddTripleLink(string s)
    {

        TripleLinkStruct newTripleLink = new TripleLinkStruct();
        
        Regex regex = new Regex("(?<=a href=\")(.*?)(?=\")");
        MatchCollection matches = regex.Matches(s);

        foreach (Match match in matches)
        {
            if (match.Value.Contains(ScalarUtilities.manuscriptAnnotationTag))
                newTripleLink.manuscriptLink = match.Value;
            else if (match.Value.Contains(ScalarUtilities.roomSpatialAnnotationTag))
                newTripleLink.spatialLink = match.Value;
            else if (match.Value.Contains(ScalarUtilities.frescoImageAnnotationTag))
                newTripleLink.detailLink = match.Value;
            else
                Debug.LogError("Unknown triple link tag spotted: " + match.Value);
        }

        regex = new Regex("([a-zA-z]+)(?= .<a href)");
        Match tagMatch = regex.Match(s);

        tripleLinkList[tagMatch.Value] = newTripleLink;
        
        return tagMatch.Value;

    }


}
