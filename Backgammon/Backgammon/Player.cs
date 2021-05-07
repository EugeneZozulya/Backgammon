using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    [Serializable]
    /// <summary>
    /// Player.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Checkers of the player.
        /// </summary>
        public Checker Checkers { get; set; }
        /// <summary>
        /// Game turn state, true - the player is moving, false = the player isn't moving.
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// Constructor without parametrs.
        /// </summary>
        public Player()
        {
            Checkers = new Checker();
            State = false;
        }

    }
}
