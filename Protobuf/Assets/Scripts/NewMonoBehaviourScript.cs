using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 1. 직렬화 대상 클래스임을 명시
[ProtoContract]
public class PlayerData
{
    // 2. 각 필드/프로퍼티에 태그 번호(Tag Number) 부여 (1번부터 시작)
    [ProtoMember(1)]
    public int Id { get; set; }

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public int Level { get; set; }

    [ProtoMember(4)]
    public List<string> Items { get; set; } = new List<string>();
}
public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // -------------------------------------------------------------
        // 1. 객체 생성
        // -------------------------------------------------------------
        PlayerData originalPlayer = new PlayerData
        {
            Id = 1001,
            Name = "Hero",
            Level = 25
        };
        originalPlayer.Items.Add("Excalibur");
        originalPlayer.Items.Add("Health Potion");

        // -------------------------------------------------------------
        // 2. 직렬화 (Serialization): 객체 -> byte[]
        // -------------------------------------------------------------
        byte[] bytes;
        using (MemoryStream stream = new MemoryStream())
        {
            // Serializer.Serialize를 이용해 스트림에 기록
            Serializer.Serialize(stream, originalPlayer);
            bytes = stream.ToArray();
        }

        Debug.Log($"직렬화된 데이터 크기: {bytes.Length} bytes");

        // -------------------------------------------------------------
        // 3. 역직렬화 (Deserialization): byte[] -> 객체
        // -------------------------------------------------------------
        PlayerData deserializedPlayer;
        using (MemoryStream stream = new MemoryStream(bytes))
        {
            // Serializer.Deserialize<T>를 이용해 복원
            deserializedPlayer = Serializer.Deserialize<PlayerData>(stream);
        }

        // -------------------------------------------------------------
        // 4. 결과 출력
        // -------------------------------------------------------------
        Debug.Log($"[복원 데이터] ID: {deserializedPlayer.Id}, 이름: {deserializedPlayer.Name}, 레벨: {deserializedPlayer.Level}");
        foreach (var item in deserializedPlayer.Items)
        {
            Debug.Log($"소지 아이템: {item}");
        }
    }
}

 