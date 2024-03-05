namespace qapi.Model;

public class Aniquote{
    string Imagelink{get;set;}
    string InfoLink{get;set;}
    string Quote{get;set;}
    string Author{get;set;}
    string AuthorLink{get;set;}

    public Aniquote()
    {
        
    }

    public int getDayNumber(DateTime userDate){
        return userDate.DayOfYear;
    }
}