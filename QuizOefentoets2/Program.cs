namespace QuizOefentoets2;

class Program
{
    static void Main(string[] args)
    {
        var quiz = new Quiz("Quiz1", "Dit is een test");
        var vraag1 = new MultipleChoice()
        {
            text = "Wat is de hoofdstad van Nederland?",
            Options = new List<Option>()
            {
                new Option() { Text = "Amsterdam", IsRight = true },
                new Option() { Text = "Rotterdam", IsRight = false },
                new Option() { Text = "Den Haag", IsRight = false },
                new Option() { Text = "Utrecht", IsRight = false }
            },
            AntwoordText = "Amsterdam"
        };
        var vraag2 = new TrueFalse()
        {
            text = "Is de zon geel?",
            IsTrue = true
        };
        var vraag3 = new Open()
        {
            text = "Wat is de hoofdstad van Frankrijk?",
            Answer = "Parijs"
        };
        quiz.VoegVraagToe(vraag1);
        quiz.VoegVraagToe(vraag2);
        quiz.VoegVraagToe(vraag3);

        var program = new Program();
        program.LetUserTakeQuiz(quiz);
    }

    public void LetUserTakeQuiz(Quiz quiz)
    {
        var currentQuiz = new QuizOefentoets2.UserQuiz(quiz, "user");
        // foreach (var vraag in currentQuiz._quiz.GetVragen())
        for (int o = 0; o < currentQuiz._quiz.GetVragen().Count(); o++)
        {
            var vraag = currentQuiz._quiz.GetVragen().ToList()[o];
            
            Console.WriteLine("Vraag: " + vraag.text);
            if (vraag is MultipleChoice)
            {
                for (int i = 0; i < (vraag as MultipleChoice).Options.Count(); i++)
                {
                    Console.WriteLine(i + " = " + (vraag as MultipleChoice).Options.ToList()[i].Text);
                }
            }
            else if (vraag is TrueFalse)
            {
                Console.WriteLine("Is dit waar of niet waar? Type 'true' of 'false'");
            }
            else if (vraag is Open)
            {
                Console.WriteLine("Antwoord:");
            }

            var userInput = Console.ReadLine();
            Console.WriteLine("User inputted:" + userInput);

            var antwoord = new antwoord();
            
            switch (vraag)
            {
                case MultipleChoice:
                    antwoord.IntValue = int.Parse(userInput);
                    if (antwoord.IntValue < 0 || antwoord.IntValue >= (vraag as MultipleChoice).Options.Count())
                    {
                        Console.WriteLine("Foutieve input, probeer opnieuw");
                        o--;
                        continue;
                    }
                    break;
                case TrueFalse:
                    try
                    {
                        antwoord.BoolValue = bool.Parse(userInput);
                    }catch(Exception e)
                    {
                        Console.WriteLine("Foutieve input, probeer opnieuw");
                        o--;
                        continue;
                    }

                    break;
                case Open:
                    antwoord.StringValue = userInput;
                    break;
            }
            
            currentQuiz.AddQuizMetAntwoorden(vraag, antwoord);
        }
        
        PrintResults(currentQuiz);
    }
    
    public void PrintResults(UserQuiz userQuiz)
    {
        Console.Clear();
        Console.WriteLine("Resultaten van " + userQuiz._quiz.GetTitle());
        var vraagCount = 1;
        var correctAnswers = 0;
        foreach (var vraagMetAntwoord in userQuiz.GetQuizMetAntwoorden())
        {
            Console.WriteLine($"\nVraag {vraagCount}: " + vraagMetAntwoord.vraag.text);
            var correctAntwoord = "";
            var jouwAntwoord = "";
            switch (vraagMetAntwoord.vraag)
            {
                case MultipleChoice:
                    correctAntwoord = (vraagMetAntwoord.vraag as MultipleChoice).AntwoordText;
                    jouwAntwoord = (vraagMetAntwoord.vraag as MultipleChoice).Options.ToList()[vraagMetAntwoord.antwoord.IntValue].Text;
                    break;
                case TrueFalse:
                    correctAntwoord = vraagMetAntwoord.vraag.GetAntwoord().ToString() == "True" ? "waar" : "niet waar";
                    jouwAntwoord = (vraagMetAntwoord.antwoord.BoolValue ? "waar" : "niet waar");
                    break;
                case Open:
                    correctAntwoord = vraagMetAntwoord.antwoord.StringValue;
                    jouwAntwoord = vraagMetAntwoord.vraag.GetAntwoord().ToString();
                    break;
            }
            if (jouwAntwoord == correctAntwoord)
            {
                correctAnswers++;
            }
            
            Console.WriteLine("Jouw antwoord: " + jouwAntwoord);
            Console.WriteLine("Correct antwoord: " + correctAntwoord);
            Console.WriteLine("Jouw antwoord is " + (jouwAntwoord == correctAntwoord ? "correct" : "incorrect"));
            vraagCount++;
        }
        Console.WriteLine($"\nJe hebt {correctAnswers} van de {userQuiz._quiz.GetVragen().Count()} vragen goed beantwoord.");
    }
}