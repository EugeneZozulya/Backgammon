using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using System;

namespace Backgammon
{
    /// <summary>
    /// Save/download game in the xml file.
    /// </summary>
    class XML
    {
        /// <summary>
        /// Save game.
        /// </summary>
        /// <param name="fileName"> File name. </param>
        /// <param name="game"> Game. </param>
        public void Save(string fileName, GameController game)
        {
            if (!Directory.Exists("Saves")) Directory.CreateDirectory("Saves");
            if (File.Exists(Path.GetFullPath("Saves") + "\\" + fileName + ".xml")) File.Delete(Path.GetFullPath("Saves") + "\\" + fileName + ".xml");
            XmlTextWriter xmlTextWriter = new XmlTextWriter(Path.GetFullPath("Saves") + "\\" + fileName + ".xml", Encoding.ASCII);
            xmlTextWriter.WriteStartDocument();
            xmlTextWriter.WriteStartElement("GameController");
            xmlTextWriter.WriteEndDocument();
            xmlTextWriter.Close();
            XmlDocument document = new XmlDocument();
            document.Load(Path.GetFullPath("Saves") + "\\" + fileName + ".xml");
            //Save Dices value
            XmlElement root = document.DocumentElement;
            XmlElement dices = document.CreateElement("Dices");
            for(int i = 0; i < game.Dices.Length; i++)
            {
                XmlElement element = document.CreateElement("int");
                XmlText nameDice = document.CreateTextNode(game.Dices[i].ToString());
                element.AppendChild(nameDice);
                dices.AppendChild(element);
            }
            root.AppendChild(dices);
            //Save Mode value.
            XmlElement gameMode = document.CreateElement("Mode");
            XmlElement mode = document.CreateElement("GameMode");
            XmlText nameMode = document.CreateTextNode(game.Mode.ToString());
            mode.AppendChild(nameMode);
            gameMode.AppendChild(mode);
            root.AppendChild(gameMode);
            //Save Player1
            WritePlayers("Player1", root, document, game.Player1);
            //Save Player2
            WritePlayers("Player2", root, document, game.Player2);
            //Save GameField
            XmlElement gameField = document.CreateElement("gameField");
            XmlElement field = document.CreateElement("Field");
            for(int i = 0; i<game.gameField.Field.Length; i++)
            {
                XmlElement typeData = document.CreateElement("int");
                XmlText value = document.CreateTextNode(game.gameField.Field[i].ToString());
                typeData.AppendChild(value);
                field.AppendChild(typeData);
            }
            gameField.AppendChild(field);
            root.AppendChild(gameField);
            document.Save(Path.GetFullPath("Saves") + "\\" + fileName + ".xml");
        }
        /// <summary>
        /// Write Players value of the GameController class in the file.
        /// </summary>
        /// <param name="namePlayer"> Name player. </param>
        /// <param name="root"> Root of the xml document. </param>
        /// <param name="document"> Xml document. </param>
        /// <param name="gamer"> Player. </param>
        void WritePlayers(string namePlayer, XmlElement root, XmlDocument document, Player gamer)
        {
            XmlElement player = document.CreateElement(namePlayer);
            XmlElement state = document.CreateElement("State");
            XmlElement boolean = document.CreateElement("bool");
            XmlText numState = document.CreateTextNode(gamer.State.ToString());
            boolean.AppendChild(numState);
            state.AppendChild(boolean);
            XmlElement checker = document.CreateElement("Checker");
            XmlElement color = document.CreateElement("Color");
            XmlElement type = document.CreateElement("CheckerColor");
            XmlText nameColor = document.CreateTextNode(gamer.Checkers.Color.ToString());
            type.AppendChild(nameColor);
            color.AppendChild(type);
            checker.AppendChild(color);
            player.AppendChild(state);
            player.AppendChild(checker);
            root.AppendChild(player);
        }
        /// <summary>
        /// Download game.
        /// </summary>
        /// <param name="fileName"> File name. </param>
        /// <returns> Game. </returns>
        public GameController Download(string fileName)
        {
            GameController game = null;
            if (!Directory.Exists("Saves")) Directory.CreateDirectory("Saves");
            if (File.Exists(Path.GetFullPath("Saves") + "\\" + fileName + ".xml"))
            {
                game = new GameController();
                XmlDocument document = new XmlDocument();
                document.Load(Path.GetFullPath("Saves") + "\\" + fileName + ".xml");
                XmlElement root = document.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;
                //load Dices
                List<int> dices = new List<int>();
                XmlElement dice = (XmlElement)nodeList[0];
                foreach (XmlNode node in dice)
                    dices.Add(int.Parse(node.InnerText));
                game.Dices = dices.ToArray();
                //load Mode
                XmlNode mode = nodeList[1];
                game.Mode = (GameMode)Enum.Parse(typeof(GameMode), mode.InnerText);
                //load Player1
                game.Player1 = new Player();
                ReadPlayers(nodeList[2], game.Player1);
                //load Player2
                if (game.Mode == GameMode.playerVsComp) game.Player2 = new Computer();
                else game.Player2 = new Player();
                ReadPlayers(nodeList[3], game.Player2);
                //load gameField
                List<int> field = new List<int>();
                XmlElement fld = (XmlElement)nodeList[4].FirstChild;
                foreach (XmlNode node in fld)
                    field.Add(int.Parse(node.InnerText));
                game.gameField = new GameField();
                game.gameField.Field = field.ToArray();
            }
            return game;
        } 
        /// <summary>
        /// Read players from xml file.
        /// </summary>
        /// <param name="node"> Node of the xml document. </param>
        /// <param name="player"> Player. </param>
        void ReadPlayers(XmlNode node, Player player)
        {
            XmlNodeList nodeList = node.ChildNodes;
            XmlNode state = nodeList[0];
            player.State = Convert.ToBoolean(state.InnerText);
            XmlNode checker = nodeList[1].FirstChild;
            player.Checkers.Color = (CheckerColor)Enum.Parse(typeof(CheckerColor), checker.InnerText);

        }
        /// <summary>
        /// Search save game on the computer.
        /// </summary>
        /// <returns> Files name and files info. </returns>
        public (string[], string[]) SearchSave()
        {
            string[] filesName = Directory.GetFiles("Saves");
            string[] filesInfo = new string[filesName.Length];
            for(int i = 0; i<filesName.Length; i++)
            {
                filesInfo[i] = File.GetCreationTime(filesName[i]).ToLongDateString();
            }
            return (filesName,filesInfo);
        }
    }
}
