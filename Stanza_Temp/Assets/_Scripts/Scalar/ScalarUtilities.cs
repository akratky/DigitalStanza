using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using ANVC.Scalar;
using SimpleJSON;
using UnityEngine;

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
        {"</p>","<br>"},
        {"</a>","</link></color>"},
        {"<strong>","<b>"},
        {"</strong>","</b>"},
        {"<em>","<b>"},
        {"</em>","</b>"},
        {"<p dir=\"ltr\">",""},
    };

    //processes the interleaf text into a string that can be used in game
    //i.e. replaces html links with unity hypertext links etc.
    public static string ExtractRichTextFromInterleafBody(string s)
    {
        s = HttpUtility.HtmlDecode(s);
        //remove embedded unity scene
        Regex regex = new Regex("<p dir(.*?)></a>");
        MatchCollection matches;
        matches = regex.Matches(s);
        while (matches.Count > 0)
        {
            s = s.Replace(matches[0].Value, "");
            matches = regex.Matches(s);
        }

        
        //format spatial links
        regex = new Regex("<(a data-(size|align).*?)/a>");
        matches = regex.Matches(s);
        while (matches.Count > 0)
        {
            //first get target URL
            Regex targetRegex = new Regex("(href=\")(.*?)\"");
            Match targetMatch = targetRegex.Match(matches[0].Value);
            string targetURL = "";
            
            if (targetMatch.Success)
            {
                targetURL = targetMatch.Value;
                targetURL = "<a " + targetURL + ">";
            }
                            
            Regex tagRegex = new Regex("([a-zA-Z]+)(?=</a>)");
            Match tagMatch = tagRegex.Match(matches[0].Value);

            if (tagMatch.Success)
            {
                string spatialTag = "";
                if (targetMatch.Success)
                {
                    spatialTag = targetURL + tagMatch.Value + "</a>";
                }
                s = s.Replace(matches[0].Value, spatialTag);
                                
            }
            else
                s = s.Replace(matches[0].Value, "");
            
            matches = regex.Matches(s);
        }
        
        
        //handle all the triple links
        //regex = new Regex("([a-zA-Z]+)/s/(<(a href.*?)/)");
        regex = new Regex("([a-zA-Z;!@#$%^&*']+) [(](<a href.*?)[)]");
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
