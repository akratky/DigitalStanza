using UnityEngine;
using System.IO;

public class WriteToFile : MonoBehaviour
{
    public static void WriteString(string test)
    {
        string path = Application.persistentDataPath + "/test.txt";
        Debug.Log("path sdf " + path);
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(test);
        writer.Close();
        StreamReader reader = new StreamReader(path);
        //Print the text from the file
        Debug.Log("end " + reader.ReadToEnd());
        reader.Close();
    }

    public static void ReadString()
    {
        string path = Application.persistentDataPath + "/test.txt";
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log("sjlkfl " + reader.ReadToEnd());
        reader.Close();
    }
}