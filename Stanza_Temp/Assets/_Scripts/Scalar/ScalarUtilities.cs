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

    public static string manuscriptAnnotationTag = "manuscript";
    
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

        
        //remove embedded spatial links -  TODO: remove this when links are standardized
        regex = new Regex("<(a data-(size|align).*?)/a>");
        matches = regex.Matches(s);
        while (matches.Count > 0)
        {
            Regex tagRegex = new Regex("([a-zA-Z]+)(?=</a>)");
            Match tagMatch = tagRegex.Match(matches[0].Value);

            if (tagMatch.Success)
                s = s.Replace(matches[0].Value, tagMatch.Value);
            else
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
        
        //handle all the triple links
        //regex = new Regex("([a-zA-Z]+)/s/(<(a href.*?)/)");
        regex = new Regex("([a-zA-Z]+) [(](<a href.*?)[)]");
        matches = regex.Matches(s);
        while (matches.Count > 0)
        {
            string tag = ScalarTripleLink.ParseAndAddTripleLink(matches[0].Value);
            string replacementString = "<color=\"blue\"><link=\""
                                       + tag + "\">" + tag
                                       + "</link></color>";
            int insertIndex = matches[0].Index;

            s = s.Remove(matches[0].Index, matches[0].Length);
            s = s.Insert(insertIndex, replacementString);
            matches = regex.Matches(s);

        }
            
            
            
        //replace other misc. stuff
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
