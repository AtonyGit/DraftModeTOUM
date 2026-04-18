using System;
using System.Collections.Generic;
using Hazel;
using Reactor.Networking.Attributes;
using Reactor.Networking.Serialization;

namespace DraftModeTOUM.Networking;

[MessageConverter]
public class IntStringDictionaryMessageConverter : MessageConverter<Dictionary<int, string>?>
{
    public override Dictionary<int, string>? Read(MessageReader reader, Type objectType)
    {
        var count = reader.ReadInt32();
        var data = new Dictionary<int, string>(count);
        for (var i = 0; i < count; i++)
        {
            var key = reader.ReadInt32();
            var value = reader.ReadString();
            data[key] = value;
        }

        return data;
    }

    public override void Write(MessageWriter writer, Dictionary<int, string>? value)
    {
        if (value == null)
        {
            writer.Write(0);
            return;
        }

        writer.Write(value.Count);
        foreach (var kvp in value)
        {
            writer.Write(kvp.Key);
            writer.Write(kvp.Value);
        }
    }
}