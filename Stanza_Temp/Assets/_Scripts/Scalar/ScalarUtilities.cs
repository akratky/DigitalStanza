using System.Text.RegularExpressions;

public class ScalarUtilities
{
    
    
    /*In: Raw HTML from Scalar Page
     *Out: rich text string containing page content and hyperlinks
     */
    
    //annotations that swing the camera to a new spot in teh room
    public static string roomSpatialAnnotationTag = "annotation";
    
    //annotations that pop up against a fresco wall
    public static string frescoImageAnnotationTag = "image";
    
    public static string ExtractRichTextFromHTMLSource(string s, ScalarBook book)
    {

        //removes embedded image from page source
        Regex regex = new Regex("<(a data.*?)/a>");
        Match match = regex.Match(s);
        s = s.Remove(match.Index, match.Index + match.Length);
        

        //find instances of '<a href...' which indicates inline link
        s = s.Replace("<a href", "<color=\"blue\"><link");
        s = s.Replace("</a>", "</link></color>");
        
        
        return s;
    }
    
}
