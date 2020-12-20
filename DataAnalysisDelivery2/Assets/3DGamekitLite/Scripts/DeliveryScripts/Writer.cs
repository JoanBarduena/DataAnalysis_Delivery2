using System.IO;
using UnityEngine;

public class Writer : MonoBehaviour
{
    public static void Write(string fileName, string data)
    {
        string path = Application.dataPath + "/3DGamekitLite/Scripts/DeliveryScripts/" + fileName + ".json";

        File.WriteAllText(path, data);
    }
}
