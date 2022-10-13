using System.Text;

class GameAccount
{
    private List<Game> _allGames = new List<Game>();
    public string UserName { get; set;}
    public int GamesCount { get; set; }
    
    public class Game
    {
        public int Amount { get; }
        public string Opponent { get; }

        public int GameIndex { get; } 
        public string ResultOfGame { get; }

        public Game(String opponent, string resultOfGame, int amount, int gameIndex)
        {
            Opponent = opponent;
            Amount = amount;
            ResultOfGame = resultOfGame;
            GameIndex = gameIndex;
        }
    }
    public int CurrentRating {
        get
        {
            int currentRating = 0;
            foreach (var item in _allGames)
            {
                currentRating += item.Amount;
            }

            return currentRating;
        }
    }

    public GameAccount(string name)
    {
      this.UserName = name;
    }

    public void WinGame(String opponentName, int rating)
    {
        if (rating <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rating), "The rating price of game must be positive number");
        }
        var ratingNumber = new Game(opponentName,"Win", rating,GamesCount);
        _allGames.Add(ratingNumber);
    }
    
    public void LoseGame(String opponentName, int rating)
    {
        if (rating <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rating), "The rating price of game must be positive number");
        }

        if (CurrentRating - rating < 0)
        {
            throw new InvalidOperationException("This account doesn't have so much rating");
        }

        var ratingNumber = new Game(opponentName,"Lose", -rating,GamesCount);
        _allGames.Add(ratingNumber);
    }

    public string GetStats()
    {
        var report = new System.Text.StringBuilder();
        int rating = 0;
        int gameIndex = 0;
        report.AppendLine("Opponent       Amount       ResultOfGame    GameIndex ");
        report.AppendLine("---------------------------------------------------------");
        
        foreach (var item in _allGames)
        {
            rating += item.Amount;
            gameIndex++;
            report.AppendLine($"{item.Opponent}\t|\t{rating}\t|\t{item.ResultOfGame}\t|\t{gameIndex}");
        }
        
        return report.ToString();
    }
    
    static void Main(string[] args)
    {
        var tom = new GameAccount("Tom");
        tom.WinGame("Jorg",400);
        tom.LoseGame("Tom",400);
        tom.WinGame("Liz",50);
        tom.WinGame("Kate",450);
        tom.WinGame("Jon",100);
        
       Console.WriteLine(tom.GetStats());
    } 

}
