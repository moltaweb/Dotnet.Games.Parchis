using Parchis.Controls;
using Parchis.Data;
using Parchis.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Parchis
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private GameData _data;
        private PlayerModel _currentPlayer;
        private List<CellControl> _board;
        private int _dice;

        public GameWindow()
        {
            InitializeComponent();

            _data = new GameData();

            LoadBoard(_data);

            this.Loaded += WelcomeMessage_Loaded;
        }

        private void BtnRollDice_Click(object sender, RoutedEventArgs e)
        {
            RollDice();
            CheckIfChipsAtHome();

            if (_currentPlayer.isHuman == false)
            {
                RunTurnComputer();
                NextTurn();
            }

        }

        private void CheckIfChipsAtHome()
        {
            if (_dice == 5)
            {
                foreach (var chip in _currentPlayer.Chips)
                {
                    if (chip.OldPosition.Area == ChipPositionModel.AreaType.Home)
                    {
                        ChipPositionModel newPosition = new ChipPositionModel(ChipPositionModel.AreaType.Board, chip.StartPosition);
                        MoveChip(chip, newPosition);
                    }
                }
            }
        }

        private void WelcomeMessage_Loaded(object sender, RoutedEventArgs e)
        {
            // temp: always player Red starts
            _data.Turn = 0;

            string message = $"This is a New Game\n Player {_data.Turn} starts.";
            MessageBox.Show(message);

            DisplayNextPlayerTurn();
        }
        

        private void RunTurnComputer()
        {
            ChipModel chip;
            bool isTrying = true;

            bool canMove;
            ChipPositionModel? newPosition;

            do
            {                
                chip = SelectRandomChip();
                (canMove, newPosition) = CanMoveChip(chip);
                if (canMove)
                {                    
                    isTrying = false;
                }

            } while (isTrying);
                
            MoveChip(chip, newPosition);
            diceCentral.IsEnabled = true;
            //NextTurn();
        }

        private (bool canMove, ChipPositionModel? newPosition) CanMoveChip(ChipModel chip)
        {                        
            if (chip.OldPosition.Area == ChipPositionModel.AreaType.Board)
            {
                int newPositionNumber = chip.OldPosition.CellNumber + _dice;

                if (newPositionNumber <= chip.EndPosition)
                {
                    // TODO: abstract this from UI
                    int cnt = _board[newPositionNumber].stackpanel.Children.Count;

                    if (cnt < 2)
                    {
                        ChipPositionModel chipPositionModel = new ChipPositionModel(ChipPositionModel.AreaType.Board, newPositionNumber);
                        return (true, chipPositionModel);
                    }
                }                

            }
            else if (chip.OldPosition.Area == ChipPositionModel.AreaType.FinishLane)
            {

            }


            return (false, null);
        }

        private void MoveChip(ChipModel chip, ChipPositionModel newPosition)
        {            
            // Update the Model
            chip.OldPosition = chip.NewPosition;
            chip.NewPosition = newPosition;

            DisplayInfo($"{_currentPlayer.Name} moves chip from {chip.OldPosition.Area}({chip.OldPosition.CellNumber}) " +
                $"to {chip.NewPosition.Area}({chip.NewPosition.CellNumber})");

            UpdateChipUI(chip);
            
        }

        private void DisplayInfo(string message)
        {
            txtDisplayInfo.Items.Add(message);
        }

        

        private void RollDice()
        {
            HideDiceImages();
            txtCurrentPlayer.Text = _currentPlayer.Name;

            // Get a random dice throw
            Random random = new Random();
            _dice = random.Next(1, 7);

            switch (_dice)
            {
                case 1:
                    imgDice1.Visibility = Visibility.Visible;
                    break;
                case 2:
                    imgDice2.Visibility = Visibility.Visible;
                    break;
                case 3:
                    imgDice3.Visibility = Visibility.Visible;
                    break;
                case 4:
                    imgDice4.Visibility = Visibility.Visible;
                    break;
                case 5:
                    imgDice5.Visibility = Visibility.Visible;
                    break;
                case 6:
                    imgDice6.Visibility = Visibility.Visible;
                    break;
            }

            //txtDiceRolled.Text = _dice.ToString();

            DisplayInfo($"{_currentPlayer.Name} rolls dice: {_dice}");

            diceCentral.IsEnabled = false;
        }

        private void HideDiceImages()
        {
            imgDice1.Visibility = Visibility.Collapsed;
            imgDice2.Visibility = Visibility.Collapsed;
            imgDice3.Visibility = Visibility.Collapsed;
            imgDice4.Visibility = Visibility.Collapsed;
            imgDice5.Visibility = Visibility.Collapsed;
            imgDice6.Visibility = Visibility.Collapsed;
        }

        private void BtnChip_Click(object sender, RoutedEventArgs e)
        {
            if (diceCentral.IsEnabled)
            {
                MessageBox.Show("Please roll the dice before moving!");
                return;
            }

            var btn = sender as Button;
            if (btn != null)
            {
                ChipModel chip = (ChipModel)btn.Tag;

                // check if Current Player can move this Chip
                if (chip.Color != _currentPlayer.Color)
                {
                    MessageBox.Show("Sorry, it's not your turn!");
                    return;
                }

                (bool canMove, ChipPositionModel? newPosition) = CanMoveChip(chip);

                if (canMove)
                {
                    MoveChip(chip, newPosition);
                    diceCentral.IsEnabled = true;

                    NextTurn();
                }
                else
                {
                    MessageBox.Show("Cannot move there");
                }

            }

        }        

        private void DisplayNextPlayerTurn()
        {
            _currentPlayer = _data.Players[_data.Turn];

            txtNextPlayer.Text = _currentPlayer.Name;
        }

        private void NextTurn()
        {           
            if (_data.Turn < 3)
            {
                _data.Turn++;
            } 
            else
            {
                _data.Turn = 0;
            }

            

            DisplayInfo($"___________________");

            DisplayNextPlayerTurn();
            //HideDiceImages();
        }

        private ChipModel SelectRandomChip()
        {
            // Get a random dice throw
            Random random = new Random();
            int number = random.Next(0, 4);

            return _currentPlayer.Chips[number];
        }

        private void LoadBoard(GameData data)
        {
            CreateBoard();            

            // add Chips to the UI
            foreach (var player in data.Players)
            {
                foreach (var chip in player.Chips)
                {
                    SolidColorBrush blackStroke = new SolidColorBrush();
                    blackStroke.Color = Colors.Black;

                    if (player.isHuman)
                    {
                        Button playerChip = new Button();
                        playerChip.Tag = chip;

                        playerChip.Background = chip.Color;                        
                        playerChip.Height = 30;
                        playerChip.Width = playerChip.Height;
                        playerChip.IsEnabled = player.isHuman;

                        Style? style = this.FindResource("chipTemplate") as Style;
                        playerChip.Style = style;                        

                        playerChip.Click += BtnChip_Click;


                        PlaceChip(chip, playerChip);
                        //_board[chip.Position].stackpanel.Children.Add(playerChip);
                    }
                    else
                    {
                        Ellipse computerChip = new Ellipse();
                        computerChip.Tag = chip;

                        computerChip.Fill = chip.Color;      
                        computerChip.Stroke = blackStroke;
                        computerChip.StrokeThickness = 2;
                        computerChip.Height = 30;
                        computerChip.Width = computerChip.Height;                                              

                        PlaceChip(chip, computerChip);
                        //_board[chip.Position].stackpanel.Children.Add(computerChip);
                    }
                    
                }
            }

        }

        private void PlaceChip(ChipModel chip, UIElement chipUIType)
        {
            if (chip.NewPosition.Area == ChipPositionModel.AreaType.Home)
            {
                if (chip.Color == Brushes.Blue)
                {
                    homeBlue.Children.Add(chipUIType);
                }
                else if (chip.Color == Brushes.Red)
                {
                    homeRed.Children.Add(chipUIType);
                }
                else if (chip.Color == Brushes.Yellow)
                {
                    homeYellow.Children.Add(chipUIType);
                }
                else if (chip.Color == Brushes.Green)
                {
                    homeGreen.Children.Add(chipUIType);
                }
            }
        }

        private void UpdateChipUI(ChipModel chip)
        {
            var cellContent = _board[chip.OldPosition.CellNumber].stackpanel.Children;

            if (chip.IsHumanChip)
            {
                Button chipUI = (Button)cellContent[0];
                if (chipUI.Background != chip.Color)
                {
                    chipUI = (Button)cellContent[1];
                }
                _board[chip.OldPosition.CellNumber].stackpanel.Children.Remove(chipUI);
                _board[chip.OldPosition.CellNumber].stackpanel.Children.Add(chipUI);
            }
            else
            {
                Ellipse chipUI = (Ellipse)cellContent[0];
                if (chipUI.Fill != chip.Color)
                {
                    chipUI = (Ellipse)cellContent[1];
                }
                _board[chip.OldPosition.CellNumber].stackpanel.Children.Remove(chipUI);
                _board[chip.OldPosition.CellNumber].stackpanel.Children.Add(chipUI);
            }
        }

        private void CreateBoard()
        {
            _board = new List<CellControl>();

            _board.Add(boardCell1);
            _board.Add(boardCell2);
            _board.Add(boardCell3);
            _board.Add(boardCell4);
            _board.Add(boardCell5);
            _board.Add(boardCell6);
            _board.Add(boardCell7);
            _board.Add(boardCell8);
            _board.Add(boardCell9);
            _board.Add(boardCell10);
            _board.Add(boardCell11);
            _board.Add(boardCell12);
            _board.Add(boardCell13);
            _board.Add(boardCell14);
            _board.Add(boardCell15);
            _board.Add(boardCell16);
            _board.Add(boardCell17);
            _board.Add(boardCell18);
            _board.Add(boardCell19);
            _board.Add(boardCell20);
            _board.Add(boardCell21);
            _board.Add(boardCell22);
            _board.Add(boardCell23);
            _board.Add(boardCell24);
            _board.Add(boardCell25);
            _board.Add(boardCell26);
            _board.Add(boardCell27);
            _board.Add(boardCell28);
            _board.Add(boardCell29);
            _board.Add(boardCell30);
            _board.Add(boardCell31);
            _board.Add(boardCell32);
            _board.Add(boardCell33);
            _board.Add(boardCell34);
            _board.Add(boardCell35);
            _board.Add(boardCell36);
            _board.Add(boardCell37);
            _board.Add(boardCell38);
            _board.Add(boardCell39);
            _board.Add(boardCell40);
            _board.Add(boardCell41);
            _board.Add(boardCell42);
            _board.Add(boardCell43);
            _board.Add(boardCell44);
            _board.Add(boardCell45);
            _board.Add(boardCell46);
            _board.Add(boardCell47);
            _board.Add(boardCell48);
            _board.Add(boardCell49);
            _board.Add(boardCell50);
            _board.Add(boardCell51);
            _board.Add(boardCell52);
            _board.Add(boardCell53);
            _board.Add(boardCell54);
            _board.Add(boardCell55);
            _board.Add(boardCell56);
            _board.Add(boardCell57);
            _board.Add(boardCell58);
            _board.Add(boardCell59);
            _board.Add(boardCell60);
            _board.Add(boardCell61);
            _board.Add(boardCell62);
            _board.Add(boardCell63);
            _board.Add(boardCell64);
            _board.Add(boardCell65);
            _board.Add(boardCell66);
            _board.Add(boardCell67);
            _board.Add(boardCell68);

            for (int i = 0; i < _board.Count; i++)
            {
                _board[i].number.Text = (i + 1).ToString();                
            }

        }

    }
}
