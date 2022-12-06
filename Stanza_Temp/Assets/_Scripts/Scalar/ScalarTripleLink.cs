

using System;
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
        newTripleLink.detailLink = "";
        newTripleLink.manuscriptLink = "";
        newTripleLink.spatialLink = "";
        
        
        
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
        }

        regex = new Regex("([a-zA-Z;!@#$%^&*']+)(?= .<a href)");
        Match tagMatch = regex.Match(s);

        tripleLinkList[tagMatch.Value] = newTripleLink;
        
        return tagMatch.Value;

    }
    
    public static TripleLinkStruct GetTripleLink(string tag)
    {
        TripleLinkStruct linkStruct = new TripleLinkStruct();
        
        try
        {
            linkStruct = tripleLinkList[tag];
            return linkStruct;
        }
        catch (KeyNotFoundException e)
        {
            return linkStruct;
        }
        
    }





}
