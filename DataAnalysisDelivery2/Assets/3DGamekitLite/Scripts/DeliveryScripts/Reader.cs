using System.IO;
using UnityEngine;

public class Reader : MonoBehaviour
{
    public static string[] Read(string fileName)
    {
        string path = Application.dataPath + "/3DGamekitLite/Scripts/DeliveryScripts/" + fileName + ".json";

        string data = File.ReadAllText(path);

        string[] objects = data.Split('\n');

        return objects;
    }
}
