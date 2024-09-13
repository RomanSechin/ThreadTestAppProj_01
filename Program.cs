
//Objectives:
//•  Make a RecentNumbers class that holds at least the two most recent numbers. 
//•  Make a method that loops forever, generating random numbers from 0 to 9 once a second. Hint: 
//Thread.Sleep can help you wait. 
//•  Write the numbers to the console window, put the generated numbers in a RecentNumbers object,
//and update it as new numbers are generated.
//•  Make a thread that runs the above method. 
//•  Wait for the user to push a key in a second loop (on the main thread or another new thread).  When
//the user presses a key Enter, check if the last two numbers are the same. If they are, tell the user that they 
//correctly identified the repeat. If they are not, indicate that they got it wrong. 
//•  Use lock statements to ensure that only one thread accesses the shared data at a time.

using System.Threading;

internal class Program
{
  private static void Main(string[] args)
  {
    Console.WriteLine("ThreadTestProj_01");
    RecentNumbers recentNumbers = new RecentNumbers();
    Thread thread1 = new Thread(recentNumbers.LoopForever);
    thread1.Start();
    Thread thread2 = new Thread(recentNumbers.IsSame);
    thread2.Start();
  }
}

public class RecentNumbers {
  
  int RecentOne { set; get; } = 0;
  int RecentTwo { set; get; } = 0;

  public void LoopForever() {
    Random random = new Random();
    int number = 0;
    while (true)
    {      
      number = random.Next(0, 10);
      lock (this) 
      {
        RecentTwo = RecentOne;
        RecentOne = number;
      }
      Console.WriteLine($"{RecentOne} {RecentTwo}");
      Thread.Sleep(1000);
    }
  }
  public void IsSame()
  {
    while (Console.ReadKey().Key == ConsoleKey.Enter)
    {
      if (RecentOne == RecentTwo)
      {
        Console.WriteLine("Same");
      }
      else
      {
        Console.WriteLine("Different");
      }
    }
  }
}