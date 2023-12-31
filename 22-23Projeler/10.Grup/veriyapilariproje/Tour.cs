using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

// Servis sınıfı

public class Tour
{
    public Tour(int id, DateTime dt, string placeOfArrival, string placeOfDeparture, double cost)
    {
        ID = id;
        this.dt = dt;
        this.placeOfArrival = placeOfArrival;
        this.placeOfDeparture = placeOfDeparture;
        this.cost = cost;
    }

    public Tour()
    {
    }

    public int ID { get; set; } 
    public DateTime dt { get; set; } 
    public string placeOfDeparture { get; set; } 
    public string placeOfArrival { get; set; } 
    public double cost { get; set; } 
    //public Personel personel;
    //public Arac arac;



    public override string ToString()
    {
        return $"Date: {dt}, From: {placeOfDeparture}, To: {placeOfArrival}, Cost: {cost}";
    }

    internal static Tour Parse(string row)
    {
        throw new NotImplementedException();
    }
}
