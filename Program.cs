using System.Text.RegularExpressions;
using static System.Console;


class ShipBattle
{
  static void Main()
  {
    int maxWidth = LargestWindowWidth;
    int maxHeight = LargestWindowHeight;
    
    SetWindowSize(maxWidth, maxHeight);
    SetBufferSize(maxWidth, maxHeight);
    
    BackgroundColor = ConsoleColor.DarkBlue;
    ForegroundColor = ConsoleColor.White;
    Clear();
    
    string[,] matrix_empty = new[,]
    {
      { "   ", "|", "А", "Б", "В", "Г", "Д", "Е", "Ж", "З", "И", "К"},
      { "———", "|", "—", "—", "—", "—", "—", "—", "—", "—", "—", "—"},
      { " 1 ", "|", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
      { " 2 ", "|", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
      { " 3 ", "|", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
      { " 4 ", "|", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
      { " 5 ", "|", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
      { " 6 ", "|", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
      { " 7 ", "|", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
      { " 8 ", "|", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
      { " 9 ", "|", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "},
      { "10 ", "|", " ", " ", " ", " ", " ", " ", " ", " ", " ", " "}
    };
    int rowsCount = matrix_empty.GetLength(0);
    int colsCount = matrix_empty.GetLength(1);
    
    string[,] matrix_User_1_Move = (string[,])matrix_empty.Clone(); // matrix for shotship
    string[,] matrix_User_1_UserShip = (string[,])matrix_empty.Clone(); // matrix for coord my ship
    
    string[,] matrix_User_2_Move = (string[,])matrix_empty.Clone();
    string[,] matrix_User_2_UserShip = (string[,])matrix_empty.Clone(); // matrix for coord enemy ship

    
    WriteLine("Добро пожаловать в игру 'Морской бой'!\n");
    Write("Желаете начать? (да/нет): ");
    bool answer;
    answer = CheckUserAnswer();
    if (answer)
    {
        Write("\n");
        RegelnTextPrint();
    }
    else
    {
        Write("\n");
        WriteLine("Ну и не надо..");
        Thread.Sleep(3000);
        Clear();
        return;
    }
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
  
  static void ShipStandField(ref string[,] matrix, int sizeShip)
  {
      int NumCellsShip = 20;
      int totalQuanity_Speedboat = 5; // на 1 больше чтобы циклы обрабатывали последний кораблик (в данном случае 4-й)
      int totalQuanity_Destroyer = 4;
      int totalQuanity_Cruiser = 4;
      int totalQuanity_Battleship = 2;
  
      if (sizeShip == 1)
      {
          for (int s = 1; s < totalQuanity_Speedboat; s++)
          {
              WriteLine($"Введите координату для 1-клеточного коробля №{s}/4");
              string coord = ReadLine().ToUpper().Replace(" ", "")
                                                 .Replace("'", ""); ;
              if (string.IsNullOrEmpty(coord))
              {
                  Console.WriteLine("Вы не ввели координату. Попробуйте снова.");
                  continue;
              }
  
              if (!Regex.IsMatch(coord, @"^[А-Я][0-9]+$"))
              {
                  WriteLine("Некорректный формат. Введите данные в виде БукваЦифра (например, А1, и никак иначе).");
                  continue;
              }
            
              char firstCoord = coord[0];
              string numPart = coord.Substring(1);
  
              if (!int.TryParse(numPart, out int number))
              {
                  Console.WriteLine("Некорректное число в координате. Попробуйте снова.");
                  continue;
              }
            
              if (number < 1 || number > 10)
              {
                  Console.WriteLine("Число вне диапазона 1-10.");
                  continue;
              }
  
              int row = number + 1;
              int col = GetColumnIndex(firstCoord);
  
              if (col == -1)
              {
                  Console.WriteLine("Некорректная буква координаты.");
                  continue;
              }
  
              matrix[row, col] = "⬛";
          }
          NumCellsShip--;
      }
  
      if (sizeShip == 2)
      {
          for (int d = 1; d < totalQuanity_Destroyer; d++)
              {
                  WriteLine($"Введите координату для 2-клеточного коробля №{d}/3");
                  string coord = ReadLine().ToUpper().Replace(" ", "")
                                                     .Replace("'", ""); ;
                  if (string.IsNullOrEmpty(coord))
                  {
                      Console.WriteLine("Вы не ввели координату. Попробуйте снова.");
                      continue;
                  }
  
                  if (!Regex.IsMatch(coord, @"^[А-Я][0-9]+[А-Я][0-9]+$"))
                  {
                      WriteLine("Некорректный формат. Введите данные в виде БукваЦифраБукваЦифра (например, А1А2, и никак иначе).");
                      continue;
                  }
  
                  char firstCoord = coord[0];
                  string numPart = Convert.ToString(coord[1]);
  
                  char secondCoord = coord[2];
                  string numPart2 = Convert.ToString(coord[3]);
  
                  if (!int.TryParse(numPart, out int number))
                  {
                      Console.WriteLine("Некорректное число в координате. Попробуйте снова.");
                      continue;
                  }
                  if (!int.TryParse(numPart2, out int number2))
                  {
                      Console.WriteLine("Некорректное число в координате. Попробуйте снова.");
                      continue;
                  }
            
                  if (number < 1 || number > 10)
                  {
                      Console.WriteLine("Число в 1-й координате вне диапазона 1-10.");
                      continue;
                  }
                  if (number2 < 1 || number2 > 10)
                  {
                      Console.WriteLine("Число во 2-й координате вне диапазона 1-10.");
                      continue;
                  }
  
                  int row = number + 1;
                  int row2 = number2 + 1;
  
                  int col = GetColumnIndex(firstCoord);
                  int col2 = GetColumnIndex(secondCoord);
  
                  if (col == -1)
                  {
                      Console.WriteLine("Некорректная буква координаты.");
                      continue;
                  }
                  if (col2 == -1)
                  {
                      Console.WriteLine("Некорректная буква координаты.");
                      continue;
                  }
  
                  matrix[row, col] = "⬛";
                  matrix[row2, col2] = "⬛";
          }
          NumCellsShip -= 6;
      }
  
      if (sizeShip == 3)
      {
          for (int c = 1; c < totalQuanity_Cruiser; c++)
          {
              WriteLine($"Введите координату для 3-клеточного коробля №{c}/2");
              string coord = ReadLine().ToUpper().Replace(" ", "")
                                                 .Replace("'", ""); ;
              if (string.IsNullOrEmpty(coord))
              {
                  Console.WriteLine("Вы не ввели координату. Попробуйте снова.");
                  continue;
              }
  
              if (!Regex.IsMatch(coord, @"^[А-Я][0-9]+[А-Я][0-9]+[А-Я][0-9]+$"))
              {
                  WriteLine("Некорректный формат. Введите данные в виде БукваЦифраБукваЦифраБукваЦифра (например, А1А2А3, и никак иначе).");
                  continue;
              }
  
              char firstCoord = coord[0];
              string numPart = Convert.ToString(coord[1]);
  
              char secondCoord = coord[2];
              string numPart2 = Convert.ToString(coord[3]);
  
              char dreiCoord = coord[4];
              string numPart3 = Convert.ToString(coord[5]);
  
              if (!int.TryParse(numPart, out int number))
              {
                  Console.WriteLine("Некорректное число в координате. Попробуйте снова.");
                  continue;
              }
              if (!int.TryParse(numPart2, out int number2))
              {
                  Console.WriteLine("Некорректное число в координате. Попробуйте снова.");
                  continue;
              }
              if (!int.TryParse(numPart3, out int number3))
              {
                  Console.WriteLine("Некорректное число в координате. Попробуйте снова.");
                  continue;
              }
            
              if (number < 1 || number > 10)
              {
                  Console.WriteLine("Число в 1-й координате вне диапазона 1-10.");
                  continue;
              }
              if (number2 < 1 || number2 > 10)
              {
                  Console.WriteLine("Число во 2-й координате вне диапазона 1-10.");
                  continue;
              }
              if (number3 < 1 || number3 > 10)
              {
                  Console.WriteLine("Число в 3-й координате вне диапазона 1-10.");
                  continue;
              }
  
              int row = number + 1;
              int row2 = number2 + 1;
              int row3 = number3 + 1;
  
              int col = GetColumnIndex(firstCoord);
              int col2 = GetColumnIndex(secondCoord);
              int col3 = GetColumnIndex(dreiCoord);
  
              if (col == -1)
              {
                  Console.WriteLine("Некорректная буква координаты.");
                  continue;
              }
              if (col2 == -1)
              {
                  Console.WriteLine("Некорректная буква координаты.");
                  continue;
              }
              if (col3 == -1)
              {
                  Console.WriteLine("Некорректная буква координаты.");
                  continue;
              }
  
              matrix[row, col] = "⬛";
              matrix[row2, col2] = "⬛";
              matrix[row3, col3] = "⬛";
          }
          NumCellsShip -= 6;
      }
  
      if (sizeShip == 4)
      {
          for (int b = 1; b < totalQuanity_Battleship; b++)
          {
              WriteLine($"Введите координату для 4-клеточного коробля {b}/1");
              string coord = ReadLine().ToUpper().Replace(" ", "")
                                                 .Replace("'", "");
              bool isValidInput = true;
              while (!isValidInput)
              {
                  if (string.IsNullOrEmpty(coord))
                  {
                      Console.WriteLine("Вы не ввели координату. Попробуйте снова.");
                      continue;
                  }
  
                  if (!Regex.IsMatch(coord, @"^[А-Я][0-9]+[А-Я][0-9]+[А-Я][0-9]+[А-Я][0-9]+$"))
                  {
                      WriteLine("Некорректный формат. Введите данные в виде БукваЦифраБукваЦифраБукваЦифра (например, А1А2А3, и никак иначе).");
                      continue;
                  }
              
                  char firstCoord = coord[0];
                  string numPart = Convert.ToString(coord[1]);
  
                  char secondCoord = coord[2];
                  string numPart2 = Convert.ToString(coord[3]);
  
                  char dreiCoord = coord[4];
                  string numPart3 = Convert.ToString(coord[5]);
  
                  char vierCoord = coord[6];
                  string numPart4 = Convert.ToString(coord[7]);
  
                  if (!int.TryParse(numPart, out int number))
                  {
                      Console.WriteLine("Не число в 1-й координате. Попробуйте снова.");
                      continue;
                  }
                  if (!int.TryParse(numPart2, out int number2))
                  {
                      Console.WriteLine("Не число в 2-й координате. Попробуйте снова.");
                      continue;
                  }
                  if (!int.TryParse(numPart3, out int number3))
                  {
                      Console.WriteLine("Не число в 3-й координате. Попробуйте снова.");
                      continue;
                  }
                  if (!int.TryParse(numPart4, out int number4))
                  {
                      Console.WriteLine("Не число в 4-й координате. Попробуйте снова.");
                      continue;
                  }
  
                  if (number < 1 || number > 10)
                  {
                      Console.WriteLine("Число в 1-й координате вне диапазона 1-10.");
                      continue;
                  }
                  if (number2 < 1 || number2 > 10)
                  {
                      Console.WriteLine("Число во 2-й координате вне диапазона 1-10.");
                      continue;
                  }
                  if (number3 < 1 || number3 > 10)
                  {
                      Console.WriteLine("Число в 3-й координате вне диапазона 1-10.");
                      continue;
                  }
                  if (number4 < 1 || number4 > 10)
                  {
                      Console.WriteLine("Число в 4-й координате вне диапазона 1-10.");
                      continue;
                  }
  
                  int row = number + 1;
                  int row2 = number2 + 1;
                  int row3 = number3 + 1;
                  int row4 = number4 + 1;
  
                  int col = GetColumnIndex(firstCoord);
                  int col2 = GetColumnIndex(secondCoord);
                  int col3 = GetColumnIndex(dreiCoord);
                  int col4 = GetColumnIndex(vierCoord);
  
                  if (col == -1)
                  {
                      Console.WriteLine("Некорректная буква 1-й координаты.");
                      continue;
                  }
                  if (col2 == -1)
                  {
                      Console.WriteLine("Некорректная буква 2-й координаты.");
                      continue;
                  }
                  if (col3 == -1)
                  {
                      Console.WriteLine("Некорректная буква 3-й координаты.");
                      continue;
                  }
                  if (col4 == -1)
                  {
                      Console.WriteLine("Некорректная буква 4-й координаты.");
                      continue;
                  }
  
                  matrix[row, col] = "⬛";
                  matrix[row2, col2] = "⬛";
                  matrix[row3, col3] = "⬛";
                  matrix[row4, col4] = "⬛";
              }
          }
          NumCellsShip -= 4;
      }
  }

  static int GetColumnIndex(char letter)
  {
      switch (letter)
      {
          case 'А': return 2;
          case 'Б': return 3;
          case 'В': return 4;
          case 'Г': return 5;
          case 'Д': return 6;
          case 'Е': return 7;
          case 'Ж': return 8;
          case 'З': return 9;
          case 'И': return 10;
          case 'К': return 11;
          default: return -1;
      }
  }

  static void Any_Matrix_Print(ref string[,] matrix)
  {
      for (int i = 0; i < matrix.GetLength(0); ++i)
      {
          for (int j = 0; j < matrix.GetLength(1); ++j)
          {
              Write($"{matrix[i, j]}");
          }
          WriteLine();
      }
  }

  static void Game_Moves_Players(int colsCount, int rowsCount, int NumCellsShip,
                              string name_user_1, string[,] matrix_User_1_UserShip, string[,] matrix_User_1_Move,
                              string name_user_2, string[,] matrix_User_2_UserShip, string[,] matrix_User_2_Move)
  {
      bool Game_Over = false;
      while (!Game_Over)
      {
          WriteLine($"Ваша очередь ходить, {name_user_1}");
          ShipShot(matrix_User_2_UserShip, matrix_User_1_Move, colsCount, rowsCount);
          if (NumCellsShip == 0)
          {
              break;
          }
  
          WriteLine($"Ваша очередь ходить, {name_user_2}");
          ShipShot(matrix_User_1_UserShip, matrix_User_2_Move, colsCount, rowsCount);
          if (NumCellsShip == 0)
          {
              break;
          }
      }
  } 

  static void ShipShot(string[,] matrix, string[,] matrix_empty, int colsCount, int rowsCount)
  {
      int NumCellsShip = 20;
  
      Console.Write("Введите координаты для вашего выстрела: ");
      string ship_shot = Console.ReadLine().ToUpper()
                                  .Replace(" ", "")
                                  .Replace("'", "");
  
      string pattern = @"^[А-Я][0-9]+$";
  
      if (Regex.IsMatch(ship_shot, pattern))
      {
          string letterCoord = ship_shot.Substring(0, 1);
          string numCoord = ship_shot.Substring(1);
  
          int col_shot_Index = letterCoord[0] - 'А';
          int row_shot_Index = int.Parse(numCoord) - 1;
  
          if (row_shot_Index >= 0 && row_shot_Index < rowsCount &&
              col_shot_Index >= 0 && col_shot_Index < colsCount)
          {
              matrix_empty[row_shot_Index, col_shot_Index] = "\u25A1";
  
              if (matrix[row_shot_Index, col_shot_Index] == "⬛")
              {
                  matrix[row_shot_Index, col_shot_Index] = "❌";
              }
              else
              {
                  matrix[row_shot_Index, col_shot_Index] = "⚪";
              }
              NumCellsShip--;
          }
          else
          {
              WriteLine("Координата выходит за рамки игрового поля");
              return;
          }
      }
      else
      {
          WriteLine("Некорректный формат координат");
          return;
      }
  }  
}
