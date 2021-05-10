using System.IO;
using System.Xml.Serialization;

namespace Backgammon
{
    /// <summary>
    /// Save/download game in the xml file.
    /// </summary>
    static class XML
    {
        /// <summary>
        /// Save game.
        /// </summary>
        /// <param name="fileName"> File name. </param>
        /// <param name="game"> Game. </param>
        static public void Save(string fileName, GameController game)
        {
            if (!Directory.Exists("Save")) Directory.CreateDirectory("Save");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameController));
            using (FileStream stream = new FileStream(@"Save\" + fileName, FileMode.Create))
            {
                xmlSerializer.Serialize(stream, game);
            }
        }
        /// <summary>
        /// Download game.
        /// </summary>
        /// <param name="fileName"> File name. </param>
        /// <returns> Game. </returns>
        static public GameController Download(string fileName)
        {
            GameController game = null;
            if (Directory.Exists("Save"))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameController));
                using (FileStream stream = new FileStream(@"Save\" + fileName, FileMode.Create))
                {
                    game = (GameController)xmlSerializer.Deserialize(stream);
                }
            }
            return game;
        } 
        /// <summary>
        /// Search save game on the computer.
        /// </summary>
        /// <returns> Files name and files info. </returns>
        static public (string[], string[]) SearchSave()
        {
            string[] filesName = Directory.GetFiles("Save");
            string[] filesInfo = new string[filesName.Length];
            for(int i = 0; i<filesName.Length; i++)
            {
                filesInfo[i] = File.GetCreationTime(filesName[i]).ToLongDateString();
            }
            return (filesName,filesInfo);
        }
    }
}
