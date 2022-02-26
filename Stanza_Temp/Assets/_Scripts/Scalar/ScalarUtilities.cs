using System.Collections.Generic;
using System.Text.RegularExpressions;
using ANVC.Scalar;
using SimpleJSON;

public class ScalarUtilities
{
    
    
    
    //annotations that swing the camera to a new spot in teh room
    public static string roomSpatialAnnotationTag = "space";
    
    //annotations that pop up against a fresco wall
    public static string frescoImageAnnotationTag = "image";
    
    //Dictionary of phrases that will be replaced when the page text is being extracted from a scalar page
    private static Dictionary<string, string> wordReplacementDict = new Dictionary<string, string>()
    {
        {"<a href", "<color=\"blue\"><link"},
        {"<br />","<br>"},
        {"</a>","</link></color>"},
        {"<em>","<b>"},
        {"</em>","</b>"}
    };
    
    //processes the interleaf text into a string that can be used in game
    //i.e. replaces html links with unity hypertext links etc.
    public static string ExtractRichTextFromInterleafBody(string s)
    {
        
        //remove embedded unity scene
        Regex regex = new Regex("<p dir(.*?)>");
        MatchCollection matches;
        matches = regex.Matches(s);
        while (matches.Count > 0)
        {
            s = s.Replace(matches[0].Value, "");
            matches = regex.Matches(s);
        }

        

        regex = new Regex("<(a data-align.*?)/a>");
        matches = regex.Matches(s);
        while (matches.Count > 0)
        {
            s = s.Replace(matches[0].Value, "");
            matches = regex.Matches(s);
        }
        
        //get rid of HTML artifacts
        regex = new Regex("&(.*?);");
        matches = regex.Matches(s);
        while (matches.Count > 0)
        {
            s = s.Replace(matches[0].Value, "");
            matches = regex.Matches(s);
        }
        
        
        foreach (var pair in wordReplacementDict)
        {
            s = s.Replace(pair.Key, pair.Value);
        }
        
        
        return s;
    }

    public static string ExtractImgURLFromScalarNode(ScalarNode node)
    {
        string bodyText = node.current.content;

        Regex regex = new Regex("href=\"(.*?)\"");
        Match match = regex.Match(bodyText);

        bodyText = match.Value.Replace("href=\"", "");
        bodyText = bodyText.Replace("\"", "");

        return bodyText;

    }
    
}
