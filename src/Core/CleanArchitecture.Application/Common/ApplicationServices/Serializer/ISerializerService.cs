﻿namespace CleanArchitecture.Application.Common.ApplicationServices.Serializer;

public interface ISerializerService
{
    string Serialize<T>(T obj);

    string Serialize<T>(T obj, Type type);

    T Deserialize<T>(string text);
}
