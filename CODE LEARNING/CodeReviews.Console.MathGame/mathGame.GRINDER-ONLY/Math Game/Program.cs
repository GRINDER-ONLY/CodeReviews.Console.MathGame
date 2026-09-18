// Console application for a math game.
// This program allows the user to choose various options from a menu,
// like to play the game, see game history.
// Choosing the play option, lets the user choose the game difficulty
// and what math operation to solve, then
// a random number generator determines the number of questions
// you have to answer and generates random equations for you to solve.
// Choosing the game history option, lets the user see
// the history of his previous games.

using System.Text;
using System.Diagnostics;

string? userInput;
string gameDifficulty = "easy";

int operand1 = 0;
int operand2 = 0;
int equationResult = 0;
int questionCount = 0;
int inputResult;

bool parseResult;
bool newProblem = true;
bool randomOperator = false;

char equationOperator = '+';

Random rng = new Random();

Stopwatch stopWatch = new Stopwatch();

int points = 0;
int wonGames = 0;
int lostGames = 0;
List<string> gameResultLogs = new List<string>();
List<string> gameEquationLogs = new List<string>();

AllGames allGames = new AllGames();

// Using the "line" variable to add lines of "-" in my code.
StringBuilder builder = new StringBuilder();
string line = builder.Append('-', 50).ToString();

Console.WriteLine();
Console.WriteLine("Welcome to the Math Game!");
do
{
    // Reset the variables used for creating a Game object.
    points = 0;
    wonGames = 0;
    lostGames = 0;
    gameResultLogs.Clear();
    gameEquationLogs.Clear();

    randomOperator = false;

    Console.WriteLine();
    Console.WriteLine("Choose your menu option");
    Console.WriteLine("1. Play the Math Game");
    Console.WriteLine("2. History of previous games");
    Console.WriteLine("Type \"exit\" to quit the game");
    Console.WriteLine(line);

    userInput = Console.ReadLine().Trim().ToLower();

    if (userInput == "1")
    {
        GameLogic();
    }
    else if (userInput == "2")
    {
        GameHistory();
    }
    else if (userInput == "exit")
    {
        break;
    }
} while (true);

void GameLogic()
{
    do
    {
        Console.WriteLine();
        Console.WriteLine("Choose the difficulty");
        Console.WriteLine("1. Easy");
        Console.WriteLine("2. Medium");
        Console.WriteLine("3. Hard");
        Console.WriteLine("Type \"exit\" to go to the main menu");
        Console.WriteLine(line);

        userInput = Console.ReadLine();

        // Set the game difficulty, continue asking if the user input is wrong.
        switch (userInput.Trim().ToLower())
        {
            case "1":
                gameDifficulty = "easy";
                break;

            case "2":
                gameDifficulty = "medium";
                break;

            case "3":
                gameDifficulty = "hard";
                break;

            case "exit":
                return;

            default:
                Console.WriteLine("Enter a number of the menu option");
                continue;
        }
        break;

    } while (true);

    do
    {
        Console.WriteLine();
        Console.WriteLine("Choose the operation");
        Console.WriteLine("1. Addition");
        Console.WriteLine("2. Subtraction");
        Console.WriteLine("3. Multiplication");
        Console.WriteLine("4. Division");
        Console.WriteLine("5. Random Game (random operations)");
        Console.WriteLine("Type \"exit\" to go to the main menu");
        Console.WriteLine(line);

        userInput = Console.ReadLine();

        // Set the equation operation or make it random,
        // continue asking if the user input is wrong.
        switch (userInput.Trim().ToLower())
        {
            case "1":
                equationOperator = '+';
                break;

            case "2":
                equationOperator = '-';
                break;

            case "3":
                equationOperator = '*';
                break;

            case "4":
                equationOperator = '/';
                break;

            case "5":
                randomOperator = true;
                break;

            case "exit":
                return;

            default:
                Console.WriteLine("Enter a number of the menu option");
                continue;
        }

        // Set the question count to a random number and start the timer.
        questionCount = rng.Next(5, 11);
        stopWatch.Start();

        Console.WriteLine();
        Console.WriteLine($"You will have to answer {questionCount} questions, good luck!");
        Console.WriteLine();

        break;

    } while (true);

    while (questionCount != 0)
    {
        // If the user chose the "Random game" option.
        if (randomOperator)
        {
            char[] equationOperators = ['+', '-', '*', '/'];
            equationOperator = equationOperators[rng.Next(4)];
        }

        // Assigns the operands, based on the difficulty chosen.
        if (newProblem == true)
        {
            if (gameDifficulty == "easy")
            {
                NumberGenerator(11);
            }
            else if (gameDifficulty == "medium")
            {
                NumberGenerator(22);
            }
            else if (gameDifficulty == "hard")
            {
                NumberGenerator(33);
            }
        }

        newProblem = false;
        string gameEquation = $"{operand1} {equationOperator} {operand2} = ?";

        Console.WriteLine("Solve the following math equation!:");
        Console.WriteLine(gameEquation);

        // Set the equation operator.
        switch (equationOperator)
        {
            case '+':
                equationResult = operand1 + operand2;
                break;

            case '-':
                equationResult = operand1 - operand2;
                break;

            case '*':
                equationResult = operand1 * operand2;
                break;

            case '/':
                equationResult = operand1 / operand2;
                break;
        }

        userInput = Console.ReadLine();
        parseResult = int.TryParse(userInput, out inputResult);

        // If user input is not a number.
        if (parseResult == false)
        {
            Console.WriteLine();
            Console.WriteLine("Enter a valid integer!");
            Console.WriteLine(line);
            continue;
        }
        // If user got the equation wrong.
        if (inputResult != equationResult)
        {
            newProblem = true;
            questionCount--;
            lostGames++;
            string gameResult = $"{inputResult} is the wrong answer, the right answer was {equationResult}";

            gameResultLogs.Add(gameResult);
            gameEquationLogs.Add(gameEquation);

            Console.WriteLine();
            Console.WriteLine(gameResult);

        }
        // If user got the equation right.
        else
        {
            newProblem = true;
            questionCount--;
            wonGames++;
            string gameResult;

            if (gameDifficulty == "easy")
            {
                points += 1;
                gameResult = $"{inputResult} is the correct answer, You get 1 point!";
            }
            else if (gameDifficulty == "medium")
            {
                points += 2;
                gameResult = $"{inputResult} is the correct answer, You get 2 points!";
            }
            else
            {
                points += 3;
                gameResult = $"{inputResult} is the correct answer, You get 3 points!";
            }

            gameResultLogs.Add(gameResult);
            gameEquationLogs.Add(gameEquation);

            Console.WriteLine();
            Console.WriteLine(gameResult);
        }

        // The game ends when all the questions have been asked.
        // Save the results in a list for the game history later.
        if (questionCount == 0)
        {
            stopWatch.Stop();
            decimal finishTime = (decimal)stopWatch.ElapsedMilliseconds / 1000;

            Console.WriteLine(line);
            Console.WriteLine($"Got right this many equations: {wonGames}");
            Console.WriteLine($"Got wrong this many equations: {lostGames}");
            Console.WriteLine($"Got this many points: {points}");
            Console.WriteLine($"Finished the game in {finishTime:N2} seconds");

            allGames.games.Add(new Game(points, wonGames, lostGames, gameResultLogs, gameEquationLogs, finishTime));
            stopWatch.Reset();
        }
        Console.WriteLine(line);
    }
}

// Assigns random values to the operand variables
void NumberGenerator(int randomValue)
{
    do
    {
        operand1 = rng.Next(randomValue);
        operand2 = rng.Next(randomValue);

        // If the operation is division,
        // make sure there is no division from 0
        // and dividing gives whole numbers.
        if (equationOperator == '/')
        {
            operand1 = rng.Next(101 + randomValue * 2);
            operand2 = rng.Next(101 + randomValue * 2);

            if (operand2 == 0)
                continue;
            if (operand1 % operand2 != 0)
                continue;
            else
                break;
        }
        break;

    } while (true);
}

// Give a detailed history for each of the games played
void GameHistory()
{
    if (allGames.games.Count == 0)
    {
        Console.WriteLine();
        Console.WriteLine("No games have been played before, nothing to show!");
        return;
    }

    int gameCount = 1;
    foreach (Game game in allGames.games)
    {
        Console.WriteLine(line);
        Console.WriteLine($"Game {gameCount} Result:");
        Console.WriteLine();
        for (int i = 0; i < game.EquationLogs.Count; i++)
        {
            Console.WriteLine(game.EquationLogs[i]);
            Console.WriteLine(game.ResultLogs[i]);
            Console.WriteLine();
        }
        Console.WriteLine($"Got right this many equations: {game.WonGames}");
        Console.WriteLine($"Got wrong this many equations: {game.LostGames}");
        Console.WriteLine($"Got this many points: {game.Points}");
        Console.WriteLine($"Finished the game in {game.TimeToFinish:N2} seconds");
        Console.WriteLine(line);
        gameCount++;
    }
}

class Game
{
    public int Points { get; set; }
    public int WonGames { get; set; }
    public int LostGames { get; set; }
    public List<string> ResultLogs { get; set; }
    public List<string> EquationLogs { get; set; }
    public decimal TimeToFinish { get; set; }

    public Game(int points, int wonGames, int lostGames, List<string> resultLogs, List<string> equationLogs, decimal timeToFinish)
    {
        Points = points;
        WonGames = wonGames;
        LostGames = lostGames;
        ResultLogs = new List<string>(resultLogs);
        EquationLogs = new List<string>(equationLogs);
        TimeToFinish = timeToFinish;
    }
}

class AllGames
{
    public List<Game> games;
    public AllGames()
    {
        games = new List<Game>();
    }
}
