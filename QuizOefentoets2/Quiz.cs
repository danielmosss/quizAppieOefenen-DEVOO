namespace QuizOefentoets2;

public interface IVraag
{
    string text { get; set; }
    object GetAntwoord();
}

public abstract class vraag : IVraag
{
    public string text { get; set; }
    public abstract object GetAntwoord();
}

public class Option
{
    public string Text { get; set; }
    public bool IsRight { get; set; }
}

public class MultipleChoice : vraag
{
    public IEnumerable<Option> Options { get; set; }
    public string AntwoordText { get; set; }
    public override object GetAntwoord()
    {
        return AntwoordText;
    }
}

public class TrueFalse : vraag
{
    public bool IsTrue { get; set; }
    public override object GetAntwoord()
    {
        return IsTrue;
    }
}

public class Open : vraag
{
    public string Answer { get; set; }
    public override object GetAntwoord()
    {
        return Answer;
    }
}

public class Quiz
{
    private string _title { get; set; }
    private string _beschrijving { get; set; }
    public int totaleVragen { get; set; }
    private List<vraag> _vragen { get; set; }
    
    public Quiz(string title, string beschrijving)
    {
        _title = title;
        _beschrijving = beschrijving;
        _vragen = new List<vraag>();
    }
    
    public string GetTitle()
    {
        return _title;
    }
    
    public string GetBeschrijving()
    {
        return _beschrijving;
    }
    
    public List<vraag> GetVragen()
    {
        return _vragen;
    }

    public void VoegVraagToe(vraag vraag)
    {
        _vragen.Add(vraag);
    }
    
}