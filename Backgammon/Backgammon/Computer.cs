namespace Backgammon
{
    /// <summary>
    /// Artificial intelligence.
    /// </summary>
    public class Computer : Player
    {
        /// <summary>
        /// Search the best game turn.
        /// </summary>
        /// <param name="dice"> Game dices. </param>
        /// <param name="gameField"> Game field. </param>
        /// <param name="allHome"> True - all the checkers in the home, false - not all. </param>
        /// <returns> The number of the cell from which the checker moves and the number of the cell where which the checker moves. </returns>
        public (int, int) SearchGameTurn(int[] dice, GameField gameField, bool allHome)
        {
            int oldIndex = 12, newIndex = -2, dice3 = 0, dice4 = 0;
            if (allHome)// Take sway checkers
            {
                TakeAway(ref oldIndex, ref newIndex, dice, gameField);
            }
            else if (gameField.Field[oldIndex] < 0) //Move checker from start cell
            {
                int step = oldIndex + dice[0] + dice[1], step1, step2;
                if (step > gameField.Field.Length) step = step - gameField.Field.Length;
                if (dice[2] == 2) dice3 = dice4 = dice[0];
                else if (dice[2] == 1) dice3 = dice[0];
                if (dice[2] > 0)//if took a double
                {
                    if (dice[2] == 2)
                    {
                        step1 = dice[0] + dice[1] + dice3 + dice4 + oldIndex;
                        if (step1 >= gameField.Field.Length) step1 = step1 - gameField.Field.Length;
                        if (gameField.Field[step1] == 0) newIndex = step1; // move on the sum values of the dice
                    }
                    else
                    {
                        step2 = dice[0] + dice[1] + dice3  + oldIndex;
                        if (step2 >= gameField.Field.Length) step2 = step2 - gameField.Field.Length;
                        if (gameField.Field[step2] == 0) newIndex = step2; // move on the sum values of the dice
                    }
                }
                else if (gameField.Field[step] == 0) newIndex = step; // move on the sum values of the dices
                else if (gameField.Field[oldIndex + dice[0]] == 0) newIndex = oldIndex + dice[0]; // move on the first dice value
                else if (gameField.Field[oldIndex + dice[1]] == 0) newIndex = oldIndex + dice[1]; // move on the second dice value
            }
            if (newIndex == -2)
            {
                for (int i = 0; i < gameField.Field.Length; i++) //Find the game move for the checkers.
                {
                    int step1 = 0, step2 = 0, step3 = 0, step4 = 0, step5 = 0;
                    oldIndex = i;
                    if (dice[2] == 2) dice3 = dice4 = dice[0];
                    else if (dice[2] == 1) dice3 = dice[0];
                    if (dice[0] != 0) step1 = i + dice[0];
                    if (dice[1] != 0) step2 = i + dice[1];
                    if (dice[0] != 0 && dice[1] != 0) {
                        step3 = i + dice[0] + dice[1];
                        step4 = i + dice[0] + dice[1] + dice3 + dice4;
                        step5 = i + dice[0] + dice[1] + dice3;
                    }
                    if (gameField.Field[i] < 0)
                    {
                        if (i < gameField.Field.Length / 2-1) //First, find the game move for the checkers, which are in cells 0 through 11.
                        {
                            if (step4 < gameField.Field.Length / 2 && gameField.Field[step4] <= 0) newIndex = step4; // move on the sum values of the dice, if took a double
                            if (step5 < gameField.Field.Length / 2 && gameField.Field[step5] <= 0) newIndex = step5; // move on the sum values of the three dice
                            else if (step3 < gameField.Field.Length / 2 && gameField.Field[step3] <= 0) newIndex = step3; // move on the sum values of the dices
                            else if (step1 < gameField.Field.Length / 2 && gameField.Field[step1] <= 0) newIndex = step1; // move on the first dice value
                            else if (step2 < gameField.Field.Length / 2 && gameField.Field[step2] <= 0) newIndex = step2; // move on the second dice value
                        }
                        else if(i >= gameField.Field.Length / 2)//Second, find the game move for the checkers, which are in cells 12 through 23.
                        {
                            if (step1 >= gameField.Field.Length) step1 = step1 - gameField.Field.Length;
                            if (step2 >= gameField.Field.Length) step2 = step2 - gameField.Field.Length;
                            if (step3 >= gameField.Field.Length) step3 = step3 - gameField.Field.Length;
                            if (step4 >= gameField.Field.Length) step4 = step4 - gameField.Field.Length;
                            if (step5 >= gameField.Field.Length) step5 = step5 - gameField.Field.Length;
                            if (gameField.Field[step4] <= 0 && !(step4 > 12 && step4 < i)) newIndex = step4; // move on the sum values of the dice, if took a double
                            else if (gameField.Field[step5] <= 0 && !(step4 > 12 && step4 < i)) newIndex = step5; // move on the values of the three dices
                            else if (gameField.Field[step3] <= 0 && !(step4 > 12 && step4 < i)) newIndex = step3; // move on the sum values of the dices
                            else if (gameField.Field[step1] <= 0 && !(step4 > 12 && step4 < i)) newIndex = step1; // move on the first dice value
                            else if (gameField.Field[step2] <= 0 && !(step4 > 12 && step4 < i)) newIndex = step2; // move on the second dice value
                        }
                    }
                    if (newIndex > -2) break;
                    if (i == gameField.Field.Length / 2 - 2) i = gameField.Field.Length/2;
                }
            }
            return (oldIndex, newIndex);
        }
        /// <summary>
        /// Search checkers which can take away on the side of the game board.
        /// </summary>
        /// <param name="oldIndex"> The number of the cell from which the checker moves. </param>
        /// <param name="newIndex"> The number of the cell where which the checker moves. </param>
        /// <param name="dice"> Game dices. </param>
        /// <param name="gameField"> Game field. </param>
        private void TakeAway(ref int oldIndex, ref int newIndex, int[] dice, GameField gameField)
        {
            for (int i = 6; i < gameField.Field.Length / 2; i++)
            {
                //if sum values of the dice or the first dice value or the second dice value equal 
                //gameField length or gameField length/2 then take away checker on the side of the board
                if ((i + dice[0] == gameField.Field.Length / 2) || (i + dice[1] == gameField.Field.Length / 2) || (i + dice[1] + dice[0] == gameField.Field.Length / 2) || (i + (dice[1] + dice[0] * 2) == gameField.Field.Length / 2)) 
                {
                    oldIndex = i;
                    newIndex = -1;
                    break;
                }

            }
        }
    }
}
