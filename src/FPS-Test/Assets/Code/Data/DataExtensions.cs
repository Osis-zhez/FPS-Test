using System;
using System.Collections.Generic;
using Code.Infrastructure.Services;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Data
{
  
  public static class DataExtensions
  {
    public static Vector3Data AsVectorData(this Vector3 vector) => 
      new Vector3Data(vector.x, vector.y, vector.z);
    
    public static Vector3 AsUnityVector(this Vector3Data vector3Data) => 
      new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);

    public static Vector3 AddY(this Vector3 vector, float y)
    {
      vector.y = y;
      return vector;
    }

    public static float SqrMagnitudeTo(this Vector3 from, Vector3 to)
    {
      return Vector3.SqrMagnitude(to - from);
    }

    // public static string ToJson(this object obj) => 
    //   JsonUtility.ToJson(obj);
    //
    // public static T ToDeserialized<T>(this string json) =>
    //   JsonUtility.FromJson<T>(json);
    
    public static JsonSerializerSettings Settings { get; set; } = new JsonSerializerSettings
    {
      TypeNameHandling = TypeNameHandling.All,
      ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
      // Converters = new List<JsonConverter>()
      // {
      //   new Vector2IntConverter()
      // },
    };

    public static string ToJson(this object obj) => 
      JsonConvert.SerializeObject(obj, Settings);

    public static T ToDeserialized<T>(this string json) => 
      JsonConvert.DeserializeObject<T>(json, Settings);

    public static T ToDownCast<T>(this List<IService> serviceList) where T : IService
    {
      foreach (IService service in serviceList)
        if (service is T TService)
          return TService;

      throw new Exception();
    }
  }
}