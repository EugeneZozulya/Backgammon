using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    [Serializable]
    public class GameController
    {
        /// <summary>
        /// Dice.
        /// </summary>
        public int[] Dices { get; set; }
        /// <summary>
        /// Game field.
        /// </summary>
        public GameField gameField { get; }
        /// <summary>
        /// The first player.
        /// </summary>
        public Player Player1 { get; }
        /// <summary>
        /// The second player.
        /// </summary>
        public Player Player2 { get; }
        /// <summary>
        /// Artificial intelligence.
        /// </summary>
        public Computer computer { get; }
        /// <summary>
        /// Game mode.
        /// </summary>
        public GameMode Mode { get; }
        /// <summary>
        /// Constructor without parametrs.
        /// </summary>
        public GameController(GameMode mode)
        {
            Mode = mode;
            Dices = new int[3];
            gameField = new GameField();
            Player1 = new Player();
            Player2 = new Player();
            PrimaryMove();
            gameField.Field[0] = 15;
            gameField.Field[12] = -15;
            computer = new Computer();
        }
        /// <summary>
        /// Generate dice.
        /// </summary>
        public void GenerateDice()
        {
            Random rnd = new Random();
            for (int i = 0; i < Dices.Length - 1; i++)
                Dices[i] = rnd.Next(1, 6);
            if (Dices[0] == Dices[1]) Dices[2] = 2;
        }
        /// <summary>
        /// Determines who moves first and the color of each player's checkers.
        /// </summary>
        void PrimaryMove()
        {
            do
            {
                GenerateDice();
            } while (Dices[0] == Dices[1]);
            if (Dices[0] > Dices[1])
            {
                Player2.Checkers.Color = CheckerColor.Black;
                Player1.State = true;
            }
            else
            {
                Player1.Checkers.Color = CheckerColor.Black;
                Player2.State = true;
            }
        }
        /// <summary>
        /// Take a game turn.
        /// </summary>
        /// <param name="oldIndex"> The number of the cell from which the checker moves. </param>
        /// <param name="newIndex"> The number of the cell where which the checker moves. If user wants take away a checker on the side of the game board, value = -1. </param>
        public void TakeGameTurn(int oldIndex, int newIndex)
        {
            if (Player1.State)
            {
                if (CheckedFirstHome())
                {
                    if (newIndex < 0) TakeAway(oldIndex, '1');
                }
                else
                {
                    MoveCheckers(oldIndex, newIndex, '1');
                }
            }
            if (Player2.State)
            {
                if (CheckedSecondHome())
                {
                    if (newIndex < 0) TakeAway(oldIndex, '2');
                }
                else
                {
                    MoveCheckers(oldIndex, newIndex, '2');
                }
            }
        }
        /// <summary>
        /// Determines how many checkers are in the player1 home.
        /// </summary>
        /// <returns> True - all checkers in the home, false - not all checkers in the home. </returns>
        bool CheckedFirstHome()
        {
            bool result = false;
            int sum = 0, count = 0;
            for (int i = 0; i < gameField.Field.Length; i++)
            {
                if (gameField.Field[i] > 0)
                {
                    if (i > 17) sum += gameField.Field[i];
                    count += gameField.Field[i];
                }
            }
            if (sum == count) result = true;
            return result;
        }
        /// <summary>
        /// Determines how many checkers are in the player2 home.
        /// </summary>
        /// <returns> True - all checkers in the home, false - not all checkers in the home. </returns>
        bool CheckedSecondHome()
        {
            bool result = false;
            int sum = 0, count = 0;
            for (int i = 0; i < gameField.Field.Length; i++)
            {
                if (gameField.Field[i] < 0)
                {
                    if (i > 5 && i < 12) sum += gameField.Field[i];
                    count += gameField.Field[i];
                }
            }
            if (sum == count) result = true;
            return result;
        }
        /// <summary>
        /// Take away a checkers on the side of the game board.
        /// </summary>
        /// <param name="oldIndex"> The number of the cell from which the checker moves. </param>
        /// <param name="numPlayer"> Player number who takes a game turn. Key '1' - the first player, '2' - the second player. </param>
        void TakeAway(int oldIndex, char numPlayer)
        {
            int length = 0, flag = 1;
            switch (numPlayer)
            {
                case '1': length = gameField.Field.Length; break;
                case '2': length = gameField.Field.Length / 2; flag = -1; break;
            }
            if (oldIndex + Dices[0] == length)
            {
                gameField.Field[oldIndex] -= flag;
                if (Dices[2] > 0) Dices[2]--;
                else Dices[0] = 0;
            }
            else if (oldIndex + Dices[1] == length)
            {
                gameField.Field[oldIndex] -= flag;
                if (Dices[2] > 0) Dices[2]--;
                else Dices[1] = 0;
            }
            else if ((oldIndex + Dices[0] + Dices[1]) == length)
            {
                gameField.Field[oldIndex] -= flag;
                if (Dices[2] > 1) Dices[2] -= 2;
                else if (Dices[2] > 0) { Dices[2]--; Dices[1] = 0; }
                else Dices[1] = Dices[0] = 0;
            }

        }
        /// <summary>
        /// The first player is moving game checkers.
        /// </summary>
        /// <param name="oldIndex"> The number of the cell from which the checker moves. </param>
        /// <param name="newIndex"> The number of the cell where which the checker moves. </param>
        void FirstPlayerMove(int oldIndex, int newIndex)
        {
            bool firstMove = true;
            if (gameField.Field[oldIndex] <= 0 || gameField.Field[newIndex] < 0) return;
            if (oldIndex > gameField.Field.Length / 2 && newIndex < gameField.Field.Length / 2) return;
            if (gameField.Field[0] == 15)
            {
                if ((Dices[0] == 6 && Dices[1] == 6) || (Dices[0] == 4 && Dices[1] == 4) || (Dices[0] == 3 && Dices[1] == 3)) firstMove = false;
            }
            else firstMove = false;
            if (Dices[2] == 2 && (oldIndex + (Dices[0] + Dices[1]) * 2) == newIndex)
            {
                gameField.Field[oldIndex]--;
                gameField.Field[newIndex]++;
                Dices[0] = Dices[1] = Dices[2] = 0;
                return;
            }
            if ((oldIndex + Dices[0] + Dices[1]) == newIndex)
            {
                gameField.Field[oldIndex]--;
                gameField.Field[newIndex]++;
                if (Dices[2] > 1) Dices[2] -= 2;
                else if (Dices[2] > 0) { Dices[2]--; Dices[1] = 0; }
                else Dices[1] = Dices[0] = 0;
                return;
            }
            if (((oldIndex + Dices[0]) == newIndex) && !firstMove)
            {
                gameField.Field[oldIndex]--;
                gameField.Field[newIndex]++;
                if (Dices[2] > 0) Dices[2]--;
                else Dices[0] = 0;
                return;
            }
            if (((oldIndex + Dices[1]) == newIndex) && !firstMove)
            {
                gameField.Field[oldIndex]--;
                gameField.Field[newIndex]++;
                if (Dices[2] > 0) Dices[2]--;
                else Dices[1] = 0;
                return;
            }
        }
        /// <summary>
        /// The second player is moving game checkers.
        /// </summary>
        /// <param name="oldIndex"> The number of the cell from which the checker moves. </param>
        /// <param name="newIndex"> The number of the cell where which the checker moves. </param>
        void SecondPlayerMove(int oldIndex, int newIndex)
        {
            bool firstMove = true;
            int temp;
            if (gameField.Field[oldIndex] >= 0 || gameField.Field[newIndex] > 0) return;
            if (oldIndex < gameField.Field.Length / 2 && newIndex > gameField.Field.Length / 2) return;
            if (gameField.Field[12] == -15)
            {
                if ((Dices[0] == 6 && Dices[1] == 6) || (Dices[0] == 4 && Dices[1] == 4) || (Dices[0] == 3 && Dices[1] == 3)) firstMove = false;
            }
            else firstMove = false;
            if (Dices[2] == 2)
            {
                temp = oldIndex + (Dices[1] + Dices[0]) * 2;
                if (temp > gameField.Field.Length) temp = temp - gameField.Field.Length;
                if (temp == newIndex)
                {
                    gameField.Field[oldIndex]++;
                    gameField.Field[newIndex]--;
                    Dices[0] = Dices[1] = Dices[2] = 0;
                    return;
                }
            }
            temp = oldIndex + Dices[1] + Dices[0];
            if (temp > gameField.Field.Length) temp = temp - gameField.Field.Length;
            if (temp == newIndex)
            {
                gameField.Field[oldIndex]++;
                gameField.Field[newIndex]--;
                if (Dices[2] > 1) Dices[2] -= 2;
                else if (Dices[2] > 0) { Dices[2]--; Dices[1] = 0; }
                else Dices[1] = Dices[0] = 0;
                return;
            }
            temp = oldIndex + Dices[0];
            if (temp > gameField.Field.Length) temp = temp - gameField.Field.Length;
            if ((temp == newIndex) && !firstMove)
            {
                gameField.Field[oldIndex]++;
                gameField.Field[newIndex]--;
                if (Dices[2] > 0) Dices[2]--;
                else Dices[0] = 0;
                return;
            }
            temp = oldIndex + Dices[1];
            if (temp > gameField.Field.Length) temp = temp - gameField.Field.Length;
            if ((temp == newIndex) && !firstMove)
            {
                gameField.Field[oldIndex]++;
                gameField.Field[newIndex]--;
                if (Dices[2] > 0) Dices[2]--;
                else Dices[1] = 0;
                return;
            }
        }
        /// <summary>
        /// Checks if the player can take a game turn.
        /// </summary>
        /// <param name="numPlayer"> Player number who takes a game turn. Key '1' - the first player, '2' - the second player. </param>
        /// <returns> True - he can, false - he can't. </returns>
        public bool CheckedMove(char numPlayer)
        {
            bool isPossible = false, player1Moves = false, player2Moves = false;
            for (int i = 0; i < gameField.Field.Length; i++)
            {
                if (gameField.Field[i] > 0)
                {
                    if ((i + Dices[0] < gameField.Field.Length && gameField.Field[i + Dices[0]] >= 0)
                        || (i + Dices[1] < gameField.Field.Length && gameField.Field[i + Dices[1]] >= 0)
                        || (i + Dices[0] + Dices[1] < gameField.Field.Length && gameField.Field[i + Dices[0] + Dices[1]] >= 0))
                        player1Moves = true;
                }
                else if (gameField.Field[i] < 0)
                {
                    int step = i + Dices[0];
                    int step1 = i + Dices[1];
                    int step2 = i + Dices[0] + Dices[1];
                    if (step > gameField.Field.Length) step = step - gameField.Field.Length;
                    if (step1 > gameField.Field.Length) step1 = step1 - gameField.Field.Length;
                    if (step2 > gameField.Field.Length) step2 = step2 - gameField.Field.Length;
                    if ((step < gameField.Field.Length / 2 && gameField.Field[step] <= 0)
                        || (step1 < gameField.Field.Length / 2 && gameField.Field[step] <= 0)
                        || (step2 < gameField.Field.Length / 2 && gameField.Field[step2] <= 0))
                        player2Moves = true;
                }
            }
            switch (numPlayer)
            {
                case '1': isPossible = player1Moves; break;
                case '2': isPossible = player2Moves; break;
            }
            return isPossible;
        }
        public void MoveCheckers(int oldIndex, int newIndex, char numPlayer)
        {
            bool firstMove = true;
            int startIndex = 0, countCheckers = 15, step1 = oldIndex + Dices[0], step2 = oldIndex + Dices[1], step3 = oldIndex + Dices[0] + Dices[1],
                step4 = oldIndex + (Dices[0] + Dices[1]) * 2, flag = 1;
            switch (numPlayer)
            {
                case '1':
                    if (gameField.Field[oldIndex] <= 0 || gameField.Field[newIndex] < 0) return;
                    if (oldIndex > gameField.Field.Length / 2 && newIndex < gameField.Field.Length / 2) return;
                    break;
                case '2':
                    if (gameField.Field[oldIndex] >= 0 || gameField.Field[newIndex] > 0) return;
                    if (oldIndex < gameField.Field.Length / 2 && newIndex > gameField.Field.Length / 2) return;
                    startIndex = 12;
                    countCheckers = -15;
                    flag = -1;
                    if (step1 > gameField.Field.Length) step1 = step1 - gameField.Field.Length;
                    if (step2 > gameField.Field.Length) step2 = step2 - gameField.Field.Length;
                    if (step3 > gameField.Field.Length) step3 = step3 - gameField.Field.Length;
                    if (step4 > gameField.Field.Length) step4 = step4 - gameField.Field.Length;
                    break;
            }
            if (gameField.Field[startIndex] == countCheckers)
            {
                if ((Dices[0] == 6 && Dices[1] == 6) || (Dices[0] == 4 && Dices[1] == 4) || (Dices[0] == 3 && Dices[1] == 3)) firstMove = false;
            }
            else firstMove = false;
            if (Dices[2] == 2 && step4 == newIndex)
            {
                gameField.Field[oldIndex] -= flag;
                gameField.Field[newIndex] += flag;
                Dices[0] = Dices[1] = Dices[2] = 0;
                return;
            }
            if (step3 == newIndex)
            {
                gameField.Field[oldIndex] -= flag;
                gameField.Field[newIndex] += flag;
                if (Dices[2] > 1) Dices[2] -= 2;
                else if (Dices[2] > 0) { Dices[2]--; Dices[1] = 0; }
                else Dices[1] = Dices[0] = 0;
                return;
            }
            if (step1 == newIndex && !firstMove)
            {
                gameField.Field[oldIndex] -= flag;
                gameField.Field[newIndex] += flag;
                if (Dices[2] > 0) Dices[2]--;
                else Dices[0] = 0;
                return;
            }
            if (step2 == newIndex && !firstMove)
            {
                gameField.Field[oldIndex] -= flag;
                gameField.Field[newIndex] += flag;
                if (Dices[2] > 0) Dices[2]--;
                else Dices[1] = 0;
                return;
            }

        }
    }
}
