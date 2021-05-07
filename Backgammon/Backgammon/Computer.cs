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
        /// <returns> The number of the cell from which the checker moves and the number of the cell where which the checker moves. </returns>
        public (int, int) SearchGameTurn(int[] dice, GameField gameField)
        {
            int oldIndex = 12, newIndex = 0;
            if (gameField.Field[oldIndex] < 0)
            {
                int step = oldIndex + dice[0] + dice[1];
                if (step > gameField.Field.Length) step = step - gameField.Field.Length;
                if (gameField.Field[step] == 0) newIndex = step;
                else if (gameField.Field[oldIndex + dice[0]] == 0) newIndex = oldIndex + dice[0];
                else if (gameField.Field[oldIndex + dice[1]] == 0) newIndex = oldIndex + dice[1];
            }
            return (oldIndex, newIndex);
        }
    }
}
