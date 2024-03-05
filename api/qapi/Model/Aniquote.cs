namespace qapi.Model;

public class Aniquote{
    public Int64 Id{get;set;}
    public string ImageLink{get;set;}
    public string InfoLink{get;set;}
    public string Quote{get;set;}
    public string Author{get;set;}
    public string AuthorLink{get;set;}
    public int DayNumber{get;set;}

    public Aniquote()
    {
        
    }

    public int getDayNumber(DateTime userDate){
        return userDate.DayOfYear;
    }
}