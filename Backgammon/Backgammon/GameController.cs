using System;

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
        public GameField gameField { get; set; }
        /// <summary>
        /// The first player.
        /// </summary>
        public Player Player1 { get; set; }
        /// <summary>
        /// The second player.
        /// </summary>
        public Player Player2 { get; set; }
        /// <summary>
        /// Game mode.
        /// </summary>
        public GameMode Mode { get; set; }
        /// <summary>
        /// Constructor with parametrs.
        /// </summary>
        /// <param name="mode"> Game mode. </param>
        public GameController(GameMode mode)
        {
            Mode = mode;
            Dices = new int[5];
            gameField = new GameField();
            Player1 = new Player();
            if (mode == GameMode.playerVsComp)
                Player2 = new Computer();
            else Player2 = new Player();
            PrimaryMove();
            gameField.Field[0] = 15;
            gameField.Field[12] = -15;
        }
        /// <summary>
        /// Constructor without parametrs.
        /// </summary>
        public GameController()
        {

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
            else Dices[2] = 0;
            Dices[3] = Dices[0];
            Dices[4] = Dices[1];
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
            for (int i = 0; i < Dices.Length; i++)
                Dices[i] = 0;
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
                if (CheckedFirstHome() && newIndex < 0)
                {
                    TakeAway(oldIndex);
                }
                else
                {
                    MoveCheckers(oldIndex, newIndex);
                }
            }
            if (Player2.State)
            {
                if (CheckedSecondHome() && newIndex < 0)
                {
                    TakeAway(oldIndex);
                }
                else
                {
                    MoveCheckers(oldIndex, newIndex);
                }
            }
        }
        /// <summary>
        /// Determines how many checkers are in the player1 home.
        /// </summary>
        /// <returns> True - all checkers in the home, false - not all checkers in the home. </returns>
        public bool CheckedFirstHome()
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
        public bool CheckedSecondHome()
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
        void TakeAway(int oldIndex)
        {
            int length = gameField.Field.Length, flag = 1;
            int dice3 = 0, dice4 = 0;
            if (Dices[2] == 2) dice3 = dice4 = Dices[0];
            else if (Dices[2] == 1) dice3 = Dices[0];
            if (Player2.State) 
            { 
                length = gameField.Field.Length / 2; 
                flag = -1; 
            }
            if (Dices[2] > 0) //if took a double
            {
                if ((oldIndex + Dices[0] + Dices[1] + dice3 + dice4) == length) //take away on the sum values of the dices
                {
                    gameField.Field[oldIndex] -= flag;
                    Dices[0] = Dices[1] = Dices[2] = 0;
                }
                else if ((oldIndex + Dices[0] + Dices[1] + dice3) == length) //take away on the sum values of the three dices
                {
                    gameField.Field[oldIndex] -= flag;
                    Dices[1] = Dices[2] = 0;
                }
            }
            else
            {
                if ((oldIndex + Dices[0] + Dices[1]) == length) // take away on the sum values of the dices
                {
                    gameField.Field[oldIndex] -= flag;
                    if (Dices[2] > 1) Dices[2] -= 2;
                    else if (Dices[2] > 0) { Dices[2]--; Dices[1] = 0; }
                    else Dices[1] = Dices[0] = 0;
                }
                else if (oldIndex + Dices[0] == length) // take away on the first value of the dice
                {
                    gameField.Field[oldIndex] -= flag;
                    if (Dices[2] > 0) Dices[2]--;
                    else Dices[0] = 0;
                }
                else if (oldIndex + Dices[1] == length) // take away on the second value of the dice
                {
                    gameField.Field[oldIndex] -= flag;
                    if (Dices[2] > 0) Dices[2]--;
                    else Dices[1] = 0;
                }
            }
        }
        /// <summary>
        /// Checks if the player can take a game turn.
        /// </summary>
        /// <returns> True - he can, false - he can't. </returns>
        public bool CheckedMove()
        {
            bool isPossible, player1Moves = false, player2Moves = false;
            int dice3 = 0, dice4 = 0;
            if (Dices[2] == 2) dice3 = dice4 = Dices[0];
            else if (Dices[2] == 1) dice3 = Dices[0];
            for (int i = 0; i < gameField.Field.Length; i++)
            {
                int step = i + Dices[0];
                int step1 = i + Dices[1];
                int step2 = i + Dices[0] + Dices[1];
                int step3 = i + Dices[0] + Dices[1] + dice3;
                int step4 = i + Dices[0] + Dices[1] + dice3 + dice4;
                if (gameField.Field[i] > 0)//if player 1 move
                {
                    //if cell with index each combination is >=0 the player 1 can move
                    if ((step!= i && step < gameField.Field.Length && gameField.Field[step] >= 0)
                        || (step1 != i && step1 < gameField.Field.Length && gameField.Field[step1] >= 0)
                        || (step2 != i && step2 < gameField.Field.Length && gameField.Field[step2] >= 0)
                        || (step3 != i && step3 < gameField.Field.Length && gameField.Field[step3] >= 0)
                        || (step4 != i && step4 < gameField.Field.Length && gameField.Field[step4] >= 0))
                        player1Moves = true;
                }
                else if (gameField.Field[i] < 0) //if player2 move
                {
                    //if cell with index each combination is <=0 the player 2 can move
                    if (step > gameField.Field.Length-1) step = step - gameField.Field.Length;
                    if (step1 > gameField.Field.Length-1) step1 = step1 - gameField.Field.Length;
                    if (step2 > gameField.Field.Length-1) step2 = step2 - gameField.Field.Length;
                    if (step3 > gameField.Field.Length-1) step3 = step3 - gameField.Field.Length;
                    if (step4 > gameField.Field.Length-1) step4 = step4 - gameField.Field.Length;
                    if ((step < gameField.Field.Length / 2 && gameField.Field[step] <= 0) || (step1 < gameField.Field.Length / 2 && gameField.Field[step1] <= 0) || (step2 < gameField.Field.Length / 2 && gameField.Field[step2] <= 0) || (step3 < gameField.Field.Length / 2 && gameField.Field[step3] <= 0) || (step4 < gameField.Field.Length / 2 && gameField.Field[step4] <= 0)
                        || (gameField.Field[step] <= 0) || (gameField.Field[step1] <= 0) || (gameField.Field[step2] <= 0) || (gameField.Field[step3] <= 0) || (gameField.Field[step4] <= 0))
                        player2Moves = true;
                }
            }
            if(Player1.State) isPossible = player1Moves;
            else isPossible = player2Moves;
            return isPossible;
        }
        /// <summary>
        /// Moves game pieces.
        /// </summary>
        /// <param name="oldIndex"> The number of the cell from which the checker moves. </param>
        /// <param name="newIndex"> The number of the cell where which the checker moves. </param>
        void MoveCheckers(int oldIndex, int newIndex)
        {
            if (newIndex == -1) return;
            bool firstMove = true;
            int dice3 = 0, dice4 = 0;
            if (Dices[2] == 2) dice3 = dice4 = Dices[0];
            else if (Dices[2] == 1) dice3 = Dices[0];
            int startIndex = 0, countCheckers = 15, step1 = oldIndex + Dices[0], step2 = oldIndex + Dices[1], step3 = oldIndex + Dices[0] + Dices[1],
                step4 = oldIndex + Dices[0] + Dices[1] + dice3,  step5 = oldIndex + Dices[0] + Dices[1] + dice3 + dice4, flag = 1;
            if (Player1.State)//if move player 1
            {
                if (gameField.Field[oldIndex] <= 0 || gameField.Field[newIndex] < 0) return;
                if (oldIndex > gameField.Field.Length / 2 && newIndex < gameField.Field.Length / 2) return;
            }
            else //if player 2 move
            {
                if (gameField.Field[oldIndex] >= 0 || gameField.Field[newIndex] > 0) return;
                if (oldIndex < gameField.Field.Length / 2 && newIndex > gameField.Field.Length / 2) return;
                startIndex = 12;
                countCheckers = -15;
                flag = -1;
                //player 2 move in the opposite direction,transforms combination
                if (step1 >= gameField.Field.Length) step1 = step1 - gameField.Field.Length;
                if (step2 >= gameField.Field.Length) step2 = step2 - gameField.Field.Length;
                if (step3 >= gameField.Field.Length) step3 = step3 - gameField.Field.Length;
                if (step4 >= gameField.Field.Length) step4 = step4 - gameField.Field.Length;
                if (step5 >= gameField.Field.Length) step5 = step5 - gameField.Field.Length;
            }
            if (gameField.Field[startIndex] == countCheckers) //if took a double and values are 4:4 6:6 3:3 at the beginning of the game, the player can move 2 checkers
            {
                if ((Dices[0] == 6 && Dices[1] == 6) || (Dices[0] == 4 && Dices[1] == 4) || (Dices[0] == 3 && Dices[1] == 3)) firstMove = false;
            }
            else firstMove = false;
            if (Dices[2] > 0) //if took a double
            {
                if (step5 == newIndex)//move on the sum values of the dices
                {
                    gameField.Field[oldIndex] -= flag;
                    gameField.Field[newIndex] += flag;
                    Dices[0] = Dices[1] = Dices[2] = 0;
                    return;
                }
                else if (step4 == newIndex)//move on the sum values of the three dices
                {
                    gameField.Field[oldIndex] -= flag;
                    gameField.Field[newIndex] += flag;
                    Dices[1] = Dices[2] = 0;
                    return;
                }
            }
            if (step3 == newIndex) // move on the sum values of the dices
            {
                gameField.Field[oldIndex] -= flag;
                gameField.Field[newIndex] += flag;
                if (Dices[2] > 1) Dices[2] -= 2;
                else if (Dices[2] > 0) { Dices[2]--; Dices[1] = 0; }
                else Dices[1] = Dices[0] = 0;
                return;
            }
            if (step1 == newIndex && !firstMove) //move on the first value of the dice
            {
                gameField.Field[oldIndex] -= flag;
                gameField.Field[newIndex] += flag;
                if (Dices[2] > 0) Dices[2]--;
                else Dices[0] = 0;
                return;
            }
            if (step2 == newIndex && !firstMove) //move on the second value of the dice
            {
                gameField.Field[oldIndex] -= flag;
                gameField.Field[newIndex] += flag;
                if (Dices[2] > 0) Dices[2]--;
                else Dices[1] = 0;
                return;
            }

        }
        /// <summary>
        /// Checks if the checkers are on the board.
        /// </summary>
        /// <param name="numPlayer"> Player number who takes a game turn. Key '1' - the first player, '2' - the second player. </param>
        /// <returns> False - win, true - don't win. </returns>
        public bool CheckedCheckers()
        {
            bool isFirst = false, isSecond = false, result = false;
            for(int i = 0; i<gameField.Field.Length; i++)
            {
                if (gameField.Field[i] < 0) isSecond = true;
                else if (gameField.Field[i] > 0) isFirst = true;
            }
            if(Player1.State) result = isFirst;
            else result = isSecond; 
            return result;
        }
        /// <summary>
        /// Checking if the player can remove a checker from the board by the corresponding dice values
        /// </summary>
        /// <returns> True - he can, false - he can't. </returns>
        public bool CheckedTakingAway()
        {
            bool result = false;
            int step = 0;
            if (Dices[2] == 2) step = Dices[0] + Dices[1];
            if (Player2.State)
            {
                for (int i = 18; i < gameField.Field.Length; i++)
                    if (gameField.Field[i]>0 &&((i + Dices[0] == gameField.Field.Length) || (i + Dices[1] == gameField.Field.Length) || (i + Dices[1] + Dices[0] == gameField.Field.Length) || (i + Dices[1] + Dices[0] + step == gameField.Field.Length))) result = true;
            }
            else
            {
                for (int i = 6; i < gameField.Field.Length/2; i++)
                    if (gameField.Field[i] < 0 && ((i + Dices[0] == gameField.Field.Length/2) || (i + Dices[1] == gameField.Field.Length/2) || (i + Dices[1] + Dices[0] == gameField.Field.Length/2) || (i + Dices[1] + Dices[0] + step == gameField.Field.Length/2))) result = true;
            }
            return result;
        }
    }
}
