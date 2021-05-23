using System;

namespace Backgammon
{
    [Serializable]
    /// <summary>
    /// Game field.
    /// </summary>
    public class GameField
    {
        /// <summary>
        /// Field
        /// </summary>
        public int[] Field { get; set; }
        /// <summary>
        /// Constructor without parametrs.
        /// </summary>
        public GameField()
        {
            Field = new int[24];
        }
    }
}
