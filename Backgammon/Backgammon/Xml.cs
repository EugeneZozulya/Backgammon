using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon
{
    static class XML
    {
        static public void Save(string fileName, GameController game)
        {
        }
        static public GameController Download(string fileName)
        {
            GameController game = null;
            return game;
        } 
        static public (List<string>, List<string>) SearchSave()
        {
            List<string> saves = new List<string>();
            List<string> info = new List<string>();
            return (saves,info);
        }
    }
}
