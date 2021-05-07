using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backgammon;

namespace TestBackgammon
{
    [TestClass]
    public class UnitTest1
    {
        GameController game;
        /// <summary>
        /// Test TakeGameTurn method of the class GameController for the player1.
        /// </summary>
        [TestMethod]
        public void FirstPlayerGameTurn()
        {
            game = new GameController(GameMode.playerVsComp);
            game.gameField.Field[0] = 13;
            game.gameField.Field[2] = 1;
            game.gameField.Field[5] = 1;
            game.Dices[0] = 2;
            game.Dices[1] = 2;
            game.Player1.State = true;
            game.Player2.State = false;
            game.TakeGameTurn(0, 4);
            Assert.AreEqual(1, game.gameField.Field[4]);
        }
        /// <summary>
        /// Test TakeGameTurn method of the class GameController for the player2.
        /// </summary>
        [TestMethod]
        public void SecondPlayerGameTurn()
        {
            game = new GameController(GameMode.playerVsComp);
            game.gameField.Field[12] = -13;
            game.gameField.Field[15] = -1;
            game.gameField.Field[17] = -1;
            game.Dices[0] = 2;
            game.Dices[1] = 2;
            game.Player1.State = false;
            game.Player2.State = true;
            game.TakeGameTurn(12, 16);
            Assert.AreEqual(-1, game.gameField.Field[16]);
        }
        [TestMethod]
        public void FirstPlayerTakeAway()
        {
            game = new GameController(GameMode.playerVsComp);
            game.gameField.Field[0] = 0;
            game.gameField.Field[18] = 7;
            game.gameField.Field[19] = 8;
            game.Dices[0] = 2;
            game.Dices[1] = 3;
            game.Player1.State = true;
            game.Player2.State = false;
            game.TakeGameTurn(19, -1);
            Assert.AreEqual(7, game.gameField.Field[19]);
        }
        [TestMethod]
        public void SecondPlayerTakeAway()
        {
            game = new GameController(GameMode.playerVsComp);
            game.gameField.Field[12] = 0;
            game.gameField.Field[6] = -7;
            game.gameField.Field[7] = -8;
            game.Dices[0] = 2;
            game.Dices[1] = 3;
            game.Player1.State = false;
            game.Player2.State = true;
            game.TakeGameTurn(7, -1);
            Assert.AreEqual(-7, game.gameField.Field[7]);
        }
        /// <summary>
        /// Test SearchGameTurn method of the class Computer.
        /// </summary>
        [TestMethod]
        public void SerchGameTurn()
        {
            game = new GameController(GameMode.playerVsComp);
            int[] dices = new int[] { 2, 3 };
            int oIndex = 12, nIndex = 17, oldIndex, newIndex;
            (oldIndex, newIndex) = game.computer.SearchGameTurn(dices, game.gameField);
            Assert.AreEqual(oIndex, oldIndex);
            Assert.AreEqual(nIndex, newIndex);
        }
    }
}
