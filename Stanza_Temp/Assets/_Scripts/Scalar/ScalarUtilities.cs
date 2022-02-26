using System.Text.RegularExpressions;
using ANVC.Scalar;
using SimpleJSON;

public class ScalarUtilities
{
    
    
    
    //annotations that swing the camera to a new spot in teh room
    public static string roomSpatialAnnotationTag = "annotation";
    
    //annotations that pop up against a fresco wall
    public static string frescoImageAnnotationTag = "image";
    
    //processes the interleaf text into a string that can be used in game
    //i.e. replaces html links with unity hypertext links etc.
    public static string ExtractRichTextFromInterleafBody(string s)
    {
        
        //remove embedded unity scene
        Regex regex = new Regex("<(p dir.*)>");
        Match match = regex.Match(s);
        s = s.Remove(match.Index, match.Index + match.Length);
        
        //find instances of '<a href...' which indicates inline link
        s = s.Replace("<a href", "<color=\"blue\"><link");
        s = s.Replace("</a>", "</link></color>");
        
/*
        //removes embedded image from page source
        Regex regex = new Regex("<(a data.*?)/a>");
        Match match = regex.Match(s);
        s = s.Remove(match.Index, match.Index + match.Length);
        


        */
        
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
