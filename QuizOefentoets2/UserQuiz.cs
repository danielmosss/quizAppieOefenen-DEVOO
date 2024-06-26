namespace QuizOefentoets2;

public class antwoord
{
    private string _answerString;
    private bool _answerBool;
    private int _answerInt;

    public int IntValue
    {
        get => _answerInt;
        set => _answerInt = value;
    }

    public bool BoolValue
    {
        get => _answerBool;
        set => _answerBool = value;
    }

    public string StringValue
    {
        get => _answerString;
        set => _answerString = value;
    }
}

public class VraagMetAntwoord
{
    public vraag vraag { get; }
    public antwoord antwoord { get; }
    
    public VraagMetAntwoord(vraag vraag, antwoord antwoord)
    {
        this.vraag = vraag;
        this.antwoord = antwoord;
    }
}

public class UserQuiz
{
    private string _userName { get; set; }
    private DateTime _date { get; set; }
    public Quiz _quiz { get; set; }
    public int _quizAantalVragen { get; set; }
    private List<VraagMetAntwoord> _quizMetAntwoorden { get; set; }

    public UserQuiz(Quiz quiz, string userName)
    {
        _userName = userName;
        _date = DateTime.Now;
        _quiz = quiz;
        _quizAantalVragen = quiz.totaleVragen;
        _quizMetAntwoorden = new List<VraagMetAntwoord>();
    }
    
    public void AddQuizMetAntwoorden(vraag vraag, antwoord antwoord)
    {
        _quizMetAntwoorden.Add(new VraagMetAntwoord(vraag, antwoord));
    }
    
    public List<VraagMetAntwoord> GetQuizMetAntwoorden()
    {
        return _quizMetAntwoorden;
    }
}