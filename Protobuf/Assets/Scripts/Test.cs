using PrptpPlayer;
using System.IO;
using UnityEngine;
using Google.Protobuf;
public class Test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var data = new PlayerData
        {
            Level = 10,
            UserId = 12345,
            UserName = "PlayerOne",
            Damage = 250
        };

        byte[] bytes;
        using (MemoryStream stream = new MemoryStream())
        {
            data.WriteTo(stream);
            bytes = stream.ToArray();
        }
        Debug.Log($"Serialized Data: {bytes.Length}");
        var deserializedData = PlayerData.Parser.ParseFrom(bytes);
        Debug.Log($"Deserialized Data: UserId={deserializedData.UserId}, UserName={deserializedData.UserName}, Level={deserializedData.Level}, Damage={deserializedData.Damage}");
    }
}

    


