using CleanCode;

var guessMessage = new GuessStatisticsMessage();
var message = guessMessage.Make("cow",5);

Console.WriteLine(message);