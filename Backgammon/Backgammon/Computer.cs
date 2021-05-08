using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    /// <summary>
    /// Artificial intelligence.
    /// </summary>
    public class Computer
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
            int oldIndex = 12, newIndex = -2;
            if (allHome)
            {
                for(int i = 6; i < gameField.Field.Length / 2;i++)
                {
                    if ((i + dice[0] == gameField.Field.Length / 2) || (i + dice[1] == gameField.Field.Length / 2) || (i + dice[1]+dice[0] == gameField.Field.Length / 2) || (i + (dice[1]+dice[0]*2) == gameField.Field.Length / 2))
                    {
                        oldIndex = i;
                        newIndex = -1;
                        break;
                    }
                    
                }
            }
            else if (gameField.Field[oldIndex] < 0)
            {
                int step = oldIndex + dice[0] + dice[1];
                int step1 = 0;
                if (dice[2] > 1)
                {
                    step1 = (dice[0] + dice[1]) * 2 + oldIndex;
                    if (step1 > gameField.Field.Length) step1 = step1 - gameField.Field.Length;
                    if (gameField.Field[step1] == 0) newIndex = step1;
                }
                if (step > gameField.Field.Length) step = step - gameField.Field.Length;
                else if (gameField.Field[step] == 0) newIndex = step;
                else if (gameField.Field[oldIndex + dice[0]] == 0) newIndex = oldIndex + dice[0];
                else if (gameField.Field[oldIndex + dice[1]] == 0) newIndex = oldIndex + dice[1];
            }
            if (newIndex == -2)
            {
                for (int i = 0; i < gameField.Field.Length; i++)
                {
                    oldIndex = i; newIndex = -1;
                    if (gameField.Field[i] < 0)
                    {
                        if (i < gameField.Field.Length / 2-1)
                        {
                            if ((i + dice[0]) < gameField.Field.Length / 2 && gameField.Field[i + dice[0]] <= 0) newIndex = i + dice[0];
                            if ((i + dice[1]) < gameField.Field.Length / 2 && gameField.Field[i + dice[1]] <= 0) newIndex = i + dice[1];
                            if ((i + dice[0] + dice[1]) < gameField.Field.Length / 2 && gameField.Field[i + dice[0] + dice[1]] <= 0) newIndex = i + dice[0]+dice[1];
                            if ((i + (dice[0] + dice[1])*2) < gameField.Field.Length / 2 && gameField.Field[i + (dice[0] + dice[1])*2] <= 0) newIndex = i + (dice[0] + dice[1])*2;
                        }
                    }
                    if (newIndex > -2) break;
                    if (i == gameField.Field.Length - 2) i = 0;
                    if (i == gameField.Field.Length / 2 - 1) break;
                }
            }
            return (oldIndex, newIndex);
        }
    }
}
