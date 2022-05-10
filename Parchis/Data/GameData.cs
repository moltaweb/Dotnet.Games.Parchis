using Parchis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Parchis.Data
{
    public class GameData
    {
        public List<PlayerModel> Players { get; set; }

        public int Turn { get; set; }

        public GameData(bool isNewGame=true)
        {
            if (isNewGame)
            {
                CreateNewGame();
            }
            else
            {
                LoadGame();
            }
        }

        private void LoadGame()
        {
            throw new NotImplementedException();
        }

        private void CreateNewGame()
        {
            // create Players
            Players = new List<PlayerModel>
            {
                new PlayerModel("PlayerRed", Brushes.Red, true),
                new PlayerModel("PlayerBlue", Brushes.Blue, false),
                new PlayerModel("PlayerYellow", Brushes.Yellow, false),
                new PlayerModel("PlayerGreen", Brushes.Green, false)
            };

            // determine starter Player
            Random random = new Random();
            Turn = random.Next(0, 4);


        }


    }
}
