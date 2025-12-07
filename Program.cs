using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Text.RegularExpressions;
using static System.Console;
using static System.Formats.Asn1.AsnWriter;


class ShipBattle
{
  static void Main()
  {
    OutputEncoding = System.Text.Encoding.UTF8;
    InputEncoding = System.Text.Encoding.UTF8;

    int maxWidth = LargestWindowWidth;
    int maxHeight = LargestWindowHeight;
    
    SetWindowSize(maxWidth, maxHeight);
    SetBufferSize(maxWidth, maxHeight);
    
    BackgroundColor = ConsoleColor.DarkBlue;
    ForegroundColor = ConsoleColor.White;
    Clear();


    string[,] matrix_empty = new[,]
    {
    { "   ", "|", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J"},
    { "——" +
    "—", "|", "—", "—", "—", "—", "—", "—", "—", "—", "—", "—"},
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
    
    string[,] matrix_User_1_Move = (string[,])matrix_empty.Clone();
    string[,] matrix_User_1_UserShip = (string[,])matrix_empty.Clone();
    
    string[,] matrix_User_2_Move = (string[,])matrix_empty.Clone();
    string[,] matrix_User_2_UserShip = (string[,])matrix_empty.Clone();


    #region Хотите поиграть?
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
    #endregion

    int num_cells_ship_1 = 20;
    int num_cells_ship_2 = 20;    

    #region ИГРОК №1
    Write("Введите имя игрока №1: ");
    string name_user_1 = ReadLine().TrimEnd();
    if (name_user_1 == null)
    {
        Write("\n");
        WriteLine("Вы не ввели имя игрока №1. Еще одна попытка..");
        return;
    }
    
    WriteLine($"ИГРОК {name_user_1} | Заполненните игровое поле:\n");
    Any_Matrix_Print(matrix_User_1_UserShip);
    Write("\n");

    ShipStandField(matrix_User_1_UserShip, 1, ref num_cells_ship_1);
    Write("\n");
    Any_Matrix_Print(matrix_User_1_UserShip);
    ClearConsole();
    
    ShipStandField(matrix_User_1_UserShip, 2, ref num_cells_ship_1);
    Write("\n");
    Any_Matrix_Print(matrix_User_1_UserShip);
    ClearConsole();
    
    ShipStandField(matrix_User_1_UserShip, 3, ref num_cells_ship_1);
    Write("\n");
    Any_Matrix_Print(matrix_User_1_UserShip);
    ClearConsole();

    ShipStandField(matrix_User_1_UserShip, 4, ref num_cells_ship_1);
    Write("\n");
    Any_Matrix_Print(matrix_User_1_UserShip);
    ClearConsole();
    
    WriteLine($"Поле игрока {name_user_1}: \n");
    Any_Matrix_Print(matrix_User_1_UserShip);
    ClearConsole();
    #endregion

    #region ИГРОК №2
    Write("Введите имя игрока №2: ");
    string name_user_2 = ReadLine().TrimEnd();
    if (name_user_2 == null)
    {
        Write("\n");
        WriteLine("Вы не ввели имя игрока №2. Еще одна попытка..");
        return;
    }

    WriteLine($"ИГРОК {name_user_2} | Заполненните игровое поле:\n");
    Any_Matrix_Print(matrix_User_2_UserShip);
    Write("\n");
    
    ShipStandField(matrix_User_2_UserShip, 1, ref num_cells_ship_2);
    Write("\n");
    Any_Matrix_Print(matrix_User_2_UserShip);
    ClearConsole();
    
    ShipStandField(matrix_User_2_UserShip, 2, ref num_cells_ship_2);
    Write("\n");
    Any_Matrix_Print(matrix_User_2_UserShip);
    ClearConsole();

    ShipStandField(matrix_User_2_UserShip, 3, ref num_cells_ship_2);
    Write("\n");
    Any_Matrix_Print(matrix_User_2_UserShip);
    ClearConsole();
    
    ShipStandField(matrix_User_2_UserShip, 4, ref num_cells_ship_2);
    Write("\n");
    Any_Matrix_Print(matrix_User_2_UserShip);
    ClearConsole();
    
    WriteLine($"Поле игрока {name_user_2}: \n");
    Any_Matrix_Print(matrix_User_2_UserShip);
    ClearConsole();
    #endregion


    #region Game Over
    Game_Moves_Players(colsCount, rowsCount, num_cells_ship_1, num_cells_ship_2,
                               name_user_1, matrix_User_1_UserShip, matrix_User_1_Move,
                               name_user_2, matrix_User_2_UserShip, matrix_User_2_Move);
      
    if (num_cells_ship_1 > 0)
    {
        WriteLine($"Победил игрок 🎉🎉🎉'{name_user_1}'🎉🎉🎉!");
    }
    else
    {
        WriteLine($"Победил игрок 🎉🎉🎉'{name_user_2}'🎉🎉🎉!");
    }
    
    ClearConsole();
    #endregion
  }

  #region Выдача правил игры
  static void RegelnTextPrint()
  {
      WriteLine("Правила игры:\n");
      WriteLine("«Морской бой» — игра для двух участников, в которой игроки по очереди называют или сообщают иным способом координаты на карте соперника." +
                "Если у противника имеется корабль с этими координатами, то корабль или его палуба (дека) поражается, попавший делает ещё один ход.\n\n" +
                "Цель игрока: первым уничтожить все корабли противника. (дописать в конце)\n\n" +
                "Игровое поле: Каждый игрок рисует два поля 10x10: одно для своих кораблей, второе — для обстрела кораблей противника. Поля подписывают буквами (A-J) по горизонтали и цифрами (1-10) по вертикали.\n\n" +
                "Флот: У каждого игрока стандартный флот из 10 кораблей: 1 четырехпалубный, 2 трехпалубных, 3 двухпалубных, 4 однопалубных.\n\n" +
                "Расстановка: Корабли ставятся строго вертикально или горизонтально (не по диагонали), не касаясь друг друга и не соприкасаясь углами. Вокруг каждого корабля (кроме однопалубных) должен быть зазор в одну клетку.\n\n" +
                "Обозначения: '■' - кораблик, '×' - попали в кораблик, '○' - не попали в кораблик. " +
                "Начало хода обозначает номер игрока: 1 игрок ходит первым, 2-й - вторым.\n\n" +
                "Игра заканчивается как только у одного из игроков будут убиты все кораблики.\n\n");
      WriteLine("Нажмите, чтобы продолжить...");
      ReadKey();
      Clear();
  }
  #endregion

  #region запрос на начало игры у пользователя
  static bool CheckUserAnswer()
  {
      string answerUser = ReadLine().Trim();
  
      string[] positiveAnswers = { "да", "д", "yes", "y" };
      string[] negativeAnswers = { "нет", "н", "no", "n" };
  
      if (positiveAnswers.Contains(answerUser, StringComparer.OrdinalIgnoreCase))
      {
          return true;
      }
      else if (negativeAnswers.Contains(answerUser, StringComparer.OrdinalIgnoreCase))
      {
          return false;
      }
      else
      {
          WriteLine("Некорректный ответ. Попроуйте снова:");
          return CheckUserAnswer();
      }
  }
  #endregion

  #region печать матрицы (любой)
  static void Any_Matrix_Print(string[,] matrix)
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
  #endregion

  #region Clear Console
  static void ClearConsole()
  {
      Write("\n");
      Write("\nНажмите, чтобы продолжить...");
      ReadKey();
      Clear();
  }
  #endregion

  #region 4 обработки букв-cols
  static int GetColumnIndex(char letter)
  {
      switch (letter)
      {
          case 'A': return 2;
          case 'B': return 3;
          case 'C': return 4;
          case 'D': return 5;
          case 'E': return 6;
          case 'F': return 7;
          case 'G': return 8;
          case 'H': return 9;
          case 'I': return 10;
          case 'J': return 11;
          default: return -1;
      }
  }
  #endregion

  #region заполняем поле короблями и возвращаем новую матрицу
  static void ShipStandField(string[,] matrix, int sizeShip, ref int num_cells_ship)
  {
      int totalQuanitySpeedboat = 4;
      int totalQuanityDestroyer = 3;
      int totalQuanityCruiser = 2;
      int totalQuanityBattleship = 1;
  
      if (sizeShip == 1)
      {
          int s = 1;
          while (s <= totalQuanitySpeedboat)
          {
              Write($"Введите координату для 1-клеточного коробля №{s}/4: ");
              string coord = ReadLine().ToUpper().TrimEnd();
  
              string part_letter = "";
              string part_number = "";
  
              if (string.IsNullOrEmpty(coord))
              {
                  WriteLine("Вы не ввели координату для корабля. Попробуйте снова.");
                  continue;
              }
  
              if (!Regex.IsMatch(coord, @"([A-J]{1})(\d{1,2})"))
              {
                  WriteLine("Неверный формат для координат! Введите координату (например: A1). Буквы английские, регистр не важен.");
                  continue;
              }
              else
              {
                  for (int i = 0; i < coord.Length; i++)
                  {
                      string coord_char_like_string = Convert.ToString(coord[i]);
                      if (Regex.IsMatch(coord_char_like_string, @"([A-J]{1})"))
                      {
                          part_letter += coord_char_like_string;
                          continue;
                      }
  
                      if (Regex.IsMatch(coord_char_like_string, @"(\d{1,2})"))
                      {
                          part_number += coord_char_like_string;
                          continue;
                      }
                  }
              }
  
              if(!int.TryParse(part_number, out int part_number_result))
              {
                  WriteLine("Ваша 'цифра' - не цифра!");
                  continue;
              }
  
              if (part_number_result < 1 || part_number_result > 10)
              {
                  WriteLine("Числовая часть координаты вне диапазона [1, 10].");
                  continue;
              }
  
              int row = part_number_result + 1;
              int col = GetColumnIndex(Convert.ToChar(part_letter));
  
              if (col == -1)
              {
                  WriteLine("Некорректная буква для координаты.");
                  continue;
              }
  
              matrix[row, col] = "■";
              s++;
          }
      }
  
      if (sizeShip == 2)
      {
          int d = 1;
          while (d <= totalQuanityDestroyer)
          {
  
              Write($"Введите координату для 2-клеточного корабля №{d}/{totalQuanityDestroyer}: ");
              string coord = ReadLine().ToUpper().TrimEnd();
  
              if (string.IsNullOrEmpty(coord))
              {
                  WriteLine("Вы не ввели координату для корабля. Попробуйте снова.");
                  continue;
              }
  
              if (!Regex.IsMatch(coord, @"^([A-J]\d{1,2})\s+([A-J]\d{1,2})$"))
              {
                  WriteLine("Неверный формат для координат, введите две координаты через пробел (например: A1 B2). Буквы английские, регистр не важен, координаты через пробел.");
                  continue;
              }
  
              string[] parts = coord.Split(' ', StringSplitOptions.RemoveEmptyEntries);
              if (parts.Length != 2)
              {
                  WriteLine("Для 2-палубного коробля должно быть 2 координаты.");
                  continue;
              }
               
  
              string part_1 = parts[0];
              string part_2 = parts[1];
  
              char letter_1 = part_1[0];
              if (!int.TryParse(part_1.Substring(1), out int number_1) || number_1 < 1 || number_1 > 10)
              {
                  WriteLine("Ошибка в первой координате: цифра должна быть в промежутке [1, 10].");
                  continue;
              }
  
              char letter_2 = part_2[0];
              if (!int.TryParse(part_2.Substring(1), out int number_2) || number_2 < 1 || number_2 > 10)
              {
                  WriteLine("Ошибка во второй координате: цифра должна быть в промежутке [1, 10].");
                  continue;
              }
  
  
              bool decks_2 = false;
  
              if (letter_1 == letter_2 && Math.Abs(number_1 - number_2) == 1)
              {
                  decks_2 = true;
              }
              else if (number_1 == number_2 && Math.Abs(letter_1 - letter_2) == 1)
              {
                  decks_2 = true;
              }
              if (!decks_2)
              {
                  WriteLine("Координаты должны быть соседними (по горизонтали или вертикали).");
                  continue;
              }
  
  
              int col_1 = GetColumnIndex(letter_1);
              int row_1 = number_1 + 1;
  
              int col_2 = GetColumnIndex(letter_2);
              int row_2 = number_2 + 1;
  
              if (row_1 < 2 || row_1 > 11 || col_1 < 2 || col_1 > 11 ||
                  row_2 < 2 || row_2 > 11 || col_2 < 2 || col_2 > 11)
              {
                  WriteLine("Координаты вне игрового поля.");
                  continue;
              }
  
              matrix[row_1, col_1] = "■";
              matrix[row_2, col_2] = "■";
  
              d++;
          }
      }
  
      if (sizeShip == 3)
      {
          int c = 1;
          while (c <= totalQuanityCruiser)
          {
  
              Write($"Введите координату для 3-клеточного корабля №{c}/{totalQuanityCruiser}: ");
              string coord = ReadLine().ToUpper().TrimEnd();
  
              if (string.IsNullOrEmpty(coord))
              {
                  WriteLine("Вы не ввели координату для корабля. Попробуйте снова.");
                  continue;
              }
  
              if (!Regex.IsMatch(coord, @"^([A-J]\d{1,2})\s+([A-J]\d{1,2})\s+([A-J]\d{1,2})$"))
              {
                  WriteLine("Неверный формат для координат! Введите три координаты через пробел (например: A1 Б1 B1). Буквы английские, регистр не важен.");
                  continue;
              }
  
              string[] parts = coord.Split(' ', StringSplitOptions.RemoveEmptyEntries);
              if (parts.Length != 3)
              {
                  WriteLine("Для 3-палубного коробля должно быть 3 координаты.");
                  continue;
              }
  
              string part_1 = parts[0];
              string part_2 = parts[1];
              string part_3 = parts[2];
  
  
              char letter_1 = part_1[0];
              if (!int.TryParse(part_1.Substring(1), out int number_1) || number_1 < 1 || number_1 > 10)
              {
                  WriteLine("Ошибка в первой координате: цифра должна быть в промежутке [1, 10].");
                  continue;
              }
  
              char letter_2 = part_2[0];
              if (!int.TryParse(part_2.Substring(1), out int number_2) || number_2 < 1 || number_2 > 10)
              {
                  WriteLine("Ошибка во второй координате: цифра должна быть в промежутке [1, 10].");
                  continue;
              }
  
              char letter_3 = part_3[0];
              if (!int.TryParse(part_3.Substring(1), out int number_3) || number_3 < 1 || number_3 > 10)
              {
                  WriteLine("Ошибка во второй координате: цифра должна быть в промежутке [1, 10].");
                  continue;
              }
  
  
              bool decks_3 = false;
  
              if (letter_1 == letter_2 && letter_2 == letter_3)
              {
                  int[] numbers = {number_1, number_2, number_3};
                  Array.Sort(numbers);
                  if (numbers[1] == numbers[0] + 1 && numbers[2] == numbers[1] + 1)
                      decks_3 = true;
              }
  
              else if (number_1 == number_2 && number_2 == number_3)
              {
                  char[] letters = {letter_1, letter_2, letter_3};
                  Array.Sort(letters);
                  if (letters[1] == letters[0] + 1 && letters[2] == letters[1] + 1)
                      decks_3 = true;
              }
  
              if (!decks_3)
              {
                  WriteLine("Координаты должны быть соседями (по горизонтали или по вертикали).");
                  continue;
              }
  
  
              int col_1 = GetColumnIndex(letter_1);
              int row_1 = number_1 + 1;
  
              int col_2 = GetColumnIndex(letter_2);
              int row_2 = number_2 + 1;
  
              int col_3 = GetColumnIndex(letter_3);
              int row_3 = number_3 + 1;
  
              if (row_1 < 2 || row_1 > 11 || col_1 < 2 || col_1 > 11 ||
                  row_2 < 2 || row_2 > 11 || col_2 < 2 || col_2 > 11 ||
                  row_3 < 2 || row_3 > 11 || col_3 < 2 || col_3 > 11)
              {
                  WriteLine("Координаты выходят за пределы игрового поля.");
                  continue;
              }
  
              matrix[row_1, col_1] = "■";
              matrix[row_2, col_2] = "■";
              matrix[row_3, col_3] = "■";
  
              c++;
          }
      }
  
      if (sizeShip == 4)
      {
          int b = 1;
          while (b <= totalQuanityBattleship)
          {
  
              Write($"Введите координату для 4-клеточного корабля №{b}/{totalQuanityBattleship}: ");
              string coord = ReadLine().ToUpper().TrimEnd();
  
              if (string.IsNullOrEmpty(coord))
              {
                  WriteLine("Вы не ввели координаты для корабля. Попробуйте снова.");
                  continue;
              }
  
              if (!Regex.IsMatch(coord, @"^([A-J]\d{1,2})\s+([A-J]\d{1,2})\s+([A-J]\d{1,2})\s+([A-J]\d{1,2})$"))
              {
                  WriteLine("Неверный формат для координат! Введите четыре координаты через пробел (например: A1 Б1 B1 Г1). Буквы английские, регистр не важен.");
                  continue;
              }
  
              string[] parts = coord.Split(' ', StringSplitOptions.RemoveEmptyEntries);
              if (parts.Length != 4)
              {
                  WriteLine("Для 4-палубного коробля должно быть 4 координаты.");
                  continue;
              }
  
              string part_1 = parts[0];
              string part_2 = parts[1];
              string part_3 = parts[2];
              string part_4 = parts[3];
  
              char letter_1 = part_1[0];
              if (!int.TryParse(part_1.Substring(1), out int number_1) || number_1 < 1 || number_1 > 10)
              {
                  WriteLine("Ошибка в первой координате: цифра должна быть в промежутке [1, 10].");
                  continue;
              }
  
              char letter_2 = part_2[0];
              if (!int.TryParse(part_2.Substring(1), out int number_2) || number_2 < 1 || number_2 > 10)
              {
                  WriteLine("Ошибка во второй координате: цифра должна быть в промежутке [1, 10].");
                  continue;
              }
  
              char letter_3 = part_3[0];
              if (!int.TryParse(part_3.Substring(1), out int number_3) || number_3 < 1 || number_3 > 10)
              {
                  WriteLine("Ошибка во второй координате: цифра должна быть в промежутке [1, 10].");
                  continue;
              }
  
              char letter_4 = part_4[0];
              if (!int.TryParse(part_4.Substring(1), out int number_4) || number_4 < 1 || number_4 > 10)
              {
                  WriteLine("Ошибка во второй координате: цифра должна быть в промежутке [1, 10].");
                  continue;
              }
  
  
              bool decks_4 = false;
  
              if (letter_1 == letter_2 && letter_2 == letter_3 && letter_3 == letter_4)
              {
                  int[] numbers = {number_1, number_2, number_3, number_4};
                  Array.Sort(numbers);
                  if (numbers[1] == numbers[0] + 1 && numbers[2] == numbers[1] + 1 && numbers[3] == numbers[2] + 1)
                      decks_4 = true;
              }
  
              else if (number_1 == number_2 && number_2 == number_3 && number_3 == number_4)
              {
                  char[] letters = {letter_1, letter_2, letter_3, letter_4};
                  Array.Sort(letters);
                  if (letters[1] == letters[0] + 1 && letters[2] == letters[1] + 1 && letters[3] == letters[2] + 1)
                      decks_4 = true;
              }
  
              if (!decks_4)
              {
                  WriteLine("Координаты должны быть соседними (по горизонтали или вертикали).");
                  continue;
              }
  
  
              int col_1 = GetColumnIndex(letter_1);
              int row_1 = number_1 + 1;
  
              int col_2 = GetColumnIndex(letter_2);
              int row_2 = number_2 + 1;
  
              int col_3 = GetColumnIndex(letter_3);
              int row_3 = number_3 + 1;
  
              int col_4 = GetColumnIndex(letter_4);
              int row_4 = number_4 + 1;
  
              if (row_1 < 2 || row_1 > 11 || col_1 < 2 || col_1 > 11 ||
                  row_2 < 2 || row_2 > 11 || col_2 < 2 || col_2 > 11 ||
                  row_3 < 2 || row_3 > 11 || col_3 < 2 || col_3 > 11 ||
                  row_4 < 2 || row_4 > 11 || col_4 < 2 || col_4 > 11)
              {
                  WriteLine("Координаты находятся за пределами игрового поля.");
                  continue;
              }
  
              matrix[row_1, col_1] = "■";
              matrix[row_2, col_2] = "■";
              matrix[row_3, col_3] = "■";
              matrix[row_4, col_4] = "■";
  
              b++;
          }
      }
  }
  #endregion
     
  #region Игровой процесс
    
  #region Moves Player
  static void Game_Moves_Players(int colsCount, int rowsCount,
                                 int num_cells_ship_1, int num_cells_ship_2,
                                 string name_user_1, string[,] matrix_User_1_UserShip, string[,] matrix_User_1_Move,
                                 string name_user_2, string[,] matrix_User_2_UserShip, string[,] matrix_User_2_Move)
  {
      bool Game_Over = false;
      bool Turn_User_1 = true;
      while (!Game_Over)
      {
          bool hit = false;

          if (Turn_User_1)
          {
              WriteLine($"Ваша очередь ходить, {name_user_1}");
              ShipShot(matrix_User_2_UserShip, matrix_User_1_Move, colsCount, rowsCount, ref num_cells_ship_2, ref hit);
  
              if (num_cells_ship_2 == 0)
              {
                  WriteLine($"Игра окончена | Победитель: {name_user_1}");
                  Game_Over = true;
                  break;
              }
              
              if (!hit)
              {
                  Turn_User_1 = false;
              }
          }
          else
          {
              WriteLine($"Ваша очередь ходить, {name_user_2}");
              ShipShot(matrix_User_1_UserShip, matrix_User_2_Move, colsCount, rowsCount, ref num_cells_ship_1, ref hit);
              
              if (num_cells_ship_1 == 0)
              {
                  WriteLine($"Игра окончена со счетом | Победитель: {name_user_2}");
                  Game_Over = true;
                  break;
              }
  
              if (!hit)
              {
                  Turn_User_1 = true;
              }
          }
      }
  }
  #endregion

  #region Удары по ships
  static void ShipShot(string[,] matrix, string[,] matrix_empty, int colsCount, int rowsCount, ref int num_cells_ship, ref bool hit)
  {
      Write($"Введите координату для удара по короблю: ");
      string ship_shot = ReadLine().ToUpper().TrimEnd();

      int w = 0;
      while (w == 0)
      {
          string shot_part_letter = "";
          string shot_part_number = "";

          if (string.IsNullOrEmpty(ship_shot))
          {
              WriteLine("Вы не ввели координату. Попробуйте снова.");
              continue;
          }

          if (!Regex.IsMatch(ship_shot, @"([A-J]{1})(\d{1,2})"))
          {
              WriteLine("Вы ввели некоректную координату. Она должна быть виде БукваЧисло (вне зависимости от регистра). Попробуйте снова.");
              continue;
          }
          else
          {
              for (int i = 0; i < ship_shot.Length; i++)
              {
                  string coord_char_like_string = Convert.ToString(ship_shot[i]);
                  if (Regex.IsMatch(coord_char_like_string, @"([A-J]{1})"))
                  {
                      shot_part_letter += coord_char_like_string;
                      continue;
                  }

                  if (Regex.IsMatch(coord_char_like_string, @"(\d{1,2})"))
                  {
                      shot_part_number += coord_char_like_string;
                      continue;
                  }
              }
          }

          if (!int.TryParse(shot_part_number, out int shot_part_number_result))
          {
              WriteLine("Ваша 'цифра' - не цифра!");
              continue;
          }

          if (shot_part_number_result < 1 || shot_part_number_result > 10)
          {
              WriteLine("Числовая часть координаты вне диапазона [1, 10].");
              continue;
          }

          int row = shot_part_number_result + 1;
          int col = GetColumnIndex(Convert.ToChar(shot_part_letter));

          if (col == -1)
          {
              WriteLine("Некорректная буква для координаты.");
              continue;
          }
            
          string shot_pattern = @"^[A-J][0-9]+$";

          if (Regex.IsMatch(ship_shot, shot_pattern))
          {
              if (row >= 2 && row <= 11 && col >= 2 && col <= 11)
              {
                  matrix_empty[row, col] = "■";

                  if (matrix[row, col] == "■")
                  {
                      matrix[row, col] = "×";
                      WriteLine("Вы попали!");
                      num_cells_ship--;
                      hit = true;
                      w++;
                  }
                  else
                  {
                      matrix[row, col] = "○";
                      WriteLine("Вы промазали!");
                      w++;
                  }
              }
              else
              {
                  WriteLine("Координата за пределами поля, попробуйте еще раз.");
                  return;
              }
          }
          else
          {
              WriteLine("Некорректный формат координат.");
              return;
          }
      }
  }
  #endregion  
}
