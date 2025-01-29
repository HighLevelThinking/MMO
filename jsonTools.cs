using Newtonsoft.Json;
using System;
<<<<<<< HEAD
=======
using System.Collections.Generic;
>>>>>>> 4b21481c191f38187ca38338a7e801696edb1665

public class Deserializer
{
    public static Message DeserializeInput(string jsonString)
    {
        var temp = JObject.Parse(jsonString);
        var type = temp["type"].ToString();

        Message input = new Message
        {
            Type = type,
            Data = type switch
            {
                "input" => temp["data"].ToObject<InputData>(),
                "player" => temp["data"].ToObject<PlayerData>(),
                _ => null
            }
        };

        return input;
    }
}

public interface MessageData
{
    // You can define common methods or properties if needed
}


public class InputData : MessageData
{
    public List<string>? Keypresses { get; set; }
    public List<int>? MouseClicks { get; set; }
<<<<<<< HEAD

    public static implicit operator InputData(InputData v)
    {
        throw new NotImplementedException();
    }
=======
    public int[]? MousePos { get; set; }
>>>>>>> 4b21481c191f38187ca38338a7e801696edb1665
}

public class PlayerData : MessageData
{
    public string? Name { get; set; }
    public int[]? Pos { get; set; }
    public int? Id { get; set; }
}

public class Message
{
    public string? Type { get; set; }
    public MessageData? Data { get; set; }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public class Deserializer
{
    public static Message DeserializeInput(string jsonString)
    {
        var temp = JObject.Parse(jsonString);
        var type = temp["type"].ToString();

        Message input = new Message
        {
            Type = type,
            Data = type switch
            {
                "input" => temp["data"].ToObject<InputData>(),
                "player" => temp["data"].ToObject<PlayerData>(),
                _ => null
            }
        };

        return input;
    }
}

public interface MessageData
{
    // You can define common methods or properties if needed
}


public class InputData : MessageData
{
    public List<string>? Keypresses { get; set; }
    public List<int>? MouseClicks { get; set; }
    public int[]? MousePos { get; set; }
}

public class PlayerData : MessageData
{
    public string? Name { get; set; }
    public int[]? Pos { get; set; }
    public int? Id { get; set; }
}

public class Message
{
    public string? Type { get; set; }
    public MessageData? Data { get; set; }
}

