using System.Text.RegularExpressions;
using static System.Console;


class ShipBattle
{
  static void Main()
  {
    //основной поток будет добавлен в конце после реалезации функций
  }
  
  static void RegelnTextPrint()
  {
      WriteLine("Правила игры:\n");
      WriteLine("«Морской бой» — игра для двух участников, в которой игроки по очереди называют или сообщают иным способом координаты на карте соперника." +
                "Если у противника имеется корабль с этими координатами, то корабль или его палуба (дека) поражается, попавший делает ещё один ход." +
                "Цель игрока: первым уничтожить все корабли противника. (дописать в конце)\n");
  }

  static bool CheckUserAnswer()
  {
      string answerUser = ReadLine().Trim().ToLower();
  
      string[] positiveAnswers = { "да", "д", "yes", "y" };
      string[] negativeAnswers = { "нет", "н", "no", "n" };
  
      if (positiveAnswers.Contains(answerUser))
      {
          return true;
      }
      else if (negativeAnswers.Contains(answerUser))
      {
          return false;
      }
      else
      {
          WriteLine("Некорректный ответ. Попроуйте снова:");
          return CheckUserAnswer();
      }
  }
  
}
