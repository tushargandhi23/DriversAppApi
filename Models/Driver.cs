using MongoDB.Bson.Serialization.Attributes;
namespace DriversAppApi.Models;

public class Driver
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string? Id {get; set;}

    [BsonElement("Name")]    
    public string? DriverName {get; set;}

    public int Number {get;set;}
    public string Team {get;set;} = null;



}

