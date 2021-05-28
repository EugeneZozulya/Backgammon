using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Backgammon
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameController game;
        GameMode mode;
        //Selected checker
        Image selectedImage;
        //Representation checkers of each player
        List<Image> player1Checkers;
        List<Image> player2Checkers;
        //Checker focus
        bool isFocus = false;
        //Chekcer outline
        Border border = new Border();
        XML xmlManage = new XML();
        //Offset for checker take away 
        int offsetChecker1, offsetChecker2;
        public MainWindow()
        {
            InitializeComponent();
            ColumnDefinitionCollection columns = gameField.ColumnDefinitions;
            Checker.Width = columns[2].Width.Value;
            Checker.Height = columns[2].Width.Value;
            offsetChecker1 = offsetChecker2 = 0;
        }
        /// <summary>
        /// MouseDown event of the label "exit".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// MouseDown event of the label "playerVsPlayer".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void playerVsPlayer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mode = GameMode.playerVsPlayer;
            if (game != null) newGame.Visibility = Visibility.Visible;
            else
            {
                game = new GameController(mode);
                control.Visibility = Visibility.Visible;
                gameField.Visibility = Visibility.Visible;
                DrawCheckers();
            }
            mainMenu.Visibility = Visibility.Hidden;
            lPlayer2.Content = "Player2";
            homePlayer2.Content = "Дом Player2";
        }
        /// <summary>
        /// MouseDown event of the label "controlMenu".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void controlMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (game != null) backToGame.Visibility = Visibility.Visible;
            gameField.Visibility = Visibility.Hidden;
            control.Visibility = Visibility.Hidden;
            mainMenu.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// MouseDown event of the label "playerVsComp".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void playerVsComp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mode = GameMode.playerVsComp;
            if (game != null) newGame.Visibility = Visibility.Visible;
            else
            {
                game = new GameController(mode);
                gameField.Visibility = Visibility.Visible;
                control.Visibility = Visibility.Visible;
                DrawCheckers();
            }
            lPlayer2.Content = "Computer";
            homePlayer2.Content = "Дом Computer";
            mainMenu.Visibility = Visibility.Hidden;
            if (game.Player2.State && game.Player2 is Computer) ComputerMove();
        }
        /// <summary>
        /// MouseDown event of the label "controlSurrender".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void controlSurrender_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ImageSource winPlayer1 = new BitmapImage(new Uri("pack://application:,,,/Backgammon;component/Image/WinPlayer1.png", UriKind.RelativeOrAbsolute));
            ImageSource winPlayer2 = new BitmapImage(new Uri("pack://application:,,,/Backgammon;component/Image/WinPlayer2.png", UriKind.RelativeOrAbsolute));
            ImageSource winComp = new BitmapImage(new Uri("pack://application:,,,/Backgammon;component/Image/WinComp.png", UriKind.RelativeOrAbsolute));
            if (game.Player1.State && game.Mode == GameMode.playerVsPlayer) resultGame.Background = new ImageBrush(winPlayer2);
            else if (game.Player2.State) resultGame.Background = new ImageBrush(winPlayer1);
            else resultGame.Background = new ImageBrush(winComp);
            resultGame.Visibility = Visibility.Visible;
            gameField.Visibility = Visibility.Hidden;
            control.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// MouseDown event of the grid "resultGame".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void resultGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            resultGame.Visibility = Visibility.Hidden;
            backToGame.Visibility = Visibility.Hidden;
            mainMenu.Visibility = Visibility.Visible;
            player1Dice.Visibility = Visibility.Hidden;
            player2Dice.Visibility = Visibility.Hidden;

        }
        /// <summary>
        /// MouseEnter event of the labels "controlSurrender" and "controlMenu".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseEventArgs class. </param>
        private void controlText_MouseEnter(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            label.Foreground = Brushes.White;
        }
        /// <summary>
        /// MouseLeave event of the labels "controlSurrender" and "controlMenu".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseEventArgs class. </param>
        private void controlText_MouseLeave(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            label.Foreground = new SolidColorBrush(Color.FromRgb(255, 254, 204));
        }
        /// <summary>
        /// KeyDown event of the Window "MainWindow". 
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of KeyEventArgs class. </param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                gameField.Visibility = Visibility.Hidden;
                control.Visibility = Visibility.Hidden;
                saveOrDownload.Visibility = Visibility.Hidden;
                dialog.Visibility = Visibility.Hidden;
                resultGame.Visibility = Visibility.Hidden;
                if (game != null) backToGame.Visibility = Visibility.Visible;
                mainMenu.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// MouseDown event of the label "save".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void save_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowFileInfo();
            mainMenu.Visibility = Visibility.Hidden;
            saveOrDownload.Visibility = Visibility.Visible;
            dialog.Visibility = Visibility.Visible;
            loadingOrSaving.Content = "Сохранить игру";
            fileName.IsEnabled = true;
            fileName.Text = "";
            if (game == null)
            {
                fileName.IsEnabled = false;
                fileName.Text = "Сохранить игру невозмно. Начините/загрузите игру.";
            }
            fileName.Focus();
        }
        /// <summary>
        /// MouseDown event of the label "load".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void load_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowFileInfo();
            mainMenu.Visibility = Visibility.Hidden;
            saveOrDownload.Visibility = Visibility.Visible;
            dialog.Visibility = Visibility.Visible;
            loadingOrSaving.Content = "Загрузить игру";
            fileName.IsEnabled = false;
            fileName.Text = "";
        }
        /// <summary>
        /// MouseDown event of the Image "controlDice".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void controlDice_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(game.Mode == GameMode.playerVsComp && game.Player2.State)
            {
                game.Player1.State = !game.Player1.State;
                game.Player2.State = !game.Player2.State;
                game.Dices[0] = game.Dices[1] = 0;
            }
            if (game.Dices[0] != 0 || game.Dices[1] != 0) return;
            game.GenerateDice();
            ShowDice();
            if(!game.CheckedMove() && !game.CheckedTakingAway())
            {
                game.Dices[0] = game.Dices[1] = 0;
                game.Player1.State = !game.Player1.State;
                game.Player2.State = !game.Player2.State;
            }
            if (game.Mode == GameMode.playerVsComp && game.Player2.State) ComputerMove();
        }
        /// <summary>
        /// MouseDown event of the label "backToGame".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void backToGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainMenu.Visibility = Visibility.Hidden;
            gameField.Visibility = Visibility.Visible;
            control.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// MouseDown event of the label "yes".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void yes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newGame.Visibility = Visibility.Hidden;
            game = new GameController(mode);
            offsetChecker1 = offsetChecker2 = 0;
            gameField.Visibility = Visibility.Visible;
            control.Visibility = Visibility.Visible;
            player1Dice.Visibility = Visibility.Hidden;
            player2Dice.Visibility = Visibility.Hidden;
            DrawCheckers();
            if (mode == GameMode.playerVsComp && game.Player2.State) ComputerMove();
        }
        /// <summary>
        /// MouseDown event of the label "no".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void no_MouseDown(object sender, MouseButtonEventArgs e)
        {
            newGame.Visibility = Visibility.Hidden;
            mainMenu.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// MouseDown event of the label "back".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void back_MouseDown(object sender, MouseButtonEventArgs e)
        {
            saveOrDownload.Visibility = Visibility.Hidden;
            dialog.Visibility = Visibility.Hidden;
            mainMenu.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// MouseDown event of the label "loadOrSave".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void loadOrSave_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (loadingOrSaving.Content.ToString() == "Сохранить игру")
            {
                if (game != null)
                {
                    xmlManage.Save(fileName.Text, game);
                    back_MouseDown(null, null);
                }
            }
            else if(!String.IsNullOrEmpty(fileName.Text))
            {
                game = xmlManage.Download(fileName.Text);
                offsetChecker1 = offsetChecker2 = 0;
                int player1Checker=0, player2Checker=0;
                DrawCheckers();
                UIElementCollection checkers = gameField.Children;
                //placing checkers on the board
                for (int i = 1; i < game.gameField.Field.Length; i++)
                {
                    if (i == 12) continue;
                    int numChecker = game.gameField.Field[i];
                    int oldRow = 2, oldColumn = 2, newRow, newColumn;
                    (newRow, newColumn) = CalculateRowAndColumn(i);
                    if (numChecker < 0)
                    {
                        numChecker = -numChecker;
                        oldRow = 1;
                        oldColumn = 24;
                        player2Checker += numChecker;
                    }
                    else player1Checker+= numChecker;
                    int offset = numChecker + 1;
                    for (int j = checkers.Count-1; j >=0; j--)
                    {
                        if (Grid.GetColumn(checkers[j]) == oldColumn && Grid.GetRow(checkers[j]) == oldRow)
                        {
                            selectedImage = (Image)checkers[j];
                            Grid.SetColumn(selectedImage, newColumn);
                            Grid.SetRow(selectedImage, newRow);
                            Panel.SetZIndex(selectedImage, offset - numChecker);
                            SetMargin(i, newRow, offset - numChecker - 1, offset - numChecker - 1);
                            numChecker--;
                        }
                        if (numChecker == 0) break;
                    }
                }
                player1Checker = 15 - player1Checker;
                player2Checker = 15 - player2Checker;
                if (player1Checker != 0 || player2Checker != 0)
                {
                    PutOnTheBoard(player1Checker, player2Checker);
                }
                selectedImage = null;
                ShowDice();
                back_MouseDown(null, null);
                backToGame_MouseDown(null, null);
            }
        }
        /// <summary>
        /// Draw the checkers on the game field.
        /// </summary>
        private void DrawCheckers()
        {
            gameField.Children.Clear();
            player1Checkers = new List<Image>();
            player2Checkers = new List<Image>();
            CheckerColor player1Color = game.Player1.Checkers.Color, player2Color = game.Player2.Checkers.Color;
            BitmapImage image1 = new BitmapImage(new Uri("Image/" + player1Color.ToString().ToLower() + ".PNG", UriKind.RelativeOrAbsolute));
            BitmapImage image2 = new BitmapImage(new Uri("Image/" + player2Color.ToString().ToLower() + ".PNG", UriKind.RelativeOrAbsolute));
            for (int i = 0; i < 30; i++)
            {
                Image image = new Image();
                image.Width = Checker.Width;
                image.Height = Checker.Height;
                gameField.Children.Add(image);
                image.MouseLeftButtonDown += selectChecker_MouseLeftButtonDown;
                image.HorizontalAlignment = HorizontalAlignment.Center;
                if (i < 15)
                {
                    image.Name = "lChecker" + i.ToString();
                    image.Source = image1;
                    Panel.SetZIndex(image, i);
                    Grid.SetColumn(image, 2);
                    Grid.SetRow(image, 2);
                    image.VerticalAlignment = VerticalAlignment.Bottom;
                    image.Margin = new Thickness(0, 0, 0, 19 * i);
                    player1Checkers.Add(image);
                }
                else
                {
                    image.Name = "rChecker" + (i-15).ToString();
                    image.Source = image2;
                    Panel.SetZIndex(image, i-15);
                    Grid.SetColumn(image, 24);
                    Grid.SetRow(image, 1);
                    image.VerticalAlignment = VerticalAlignment.Top;
                    image.Margin = new Thickness(0, 19 * (i-15), 0, 0);
                    player2Checkers.Add(image);
                }
            }
        }
        /// <summary>
        /// Animation for the labels.
        /// </summary>
        /// <param name="label"> Label. </param>
        /// <param name="size"> Font size. </param>
        /// <param name="color"> Font color. </param>
        private void Animation(Label label, int size, Color color)
        {
            label.FontSize = size;
            label.Foreground = new SolidColorBrush(color);
        }
        /// <summary>
        /// MouseEnter event of the labels "save", "load","exit","playerVsPlayer","playerVsComp","backToGame","back","loadOrSave".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseEventArgs class. </param>
        private void bigText_MouseEnter(object sender, MouseEventArgs e) => Animation((Label)sender, 52, Color.FromRgb(255, 255, 255));
        /// <summary>
        /// MouseLeave event of the labels "save", "load","exit","playerVsPlayer","playerVsComp","backToGame","back","loadOrSave".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseEventArgs class. </param>
        private void smallText_MouseLeave(object sender, MouseEventArgs e) => Animation((Label)sender, 50, Color.FromRgb(255, 254, 204));
        /// <summary>
        /// MouseEnter event of the labels "yes", "no".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseEventArgs class. </param>
        private void noOrYesText_MouseEnter(object sender, MouseEventArgs e) => Animation((Label)sender, 92, Color.FromRgb(255, 255, 255));
        /// <summary>
        /// MouseLeave event of the labels "yes", "no".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseEventArgs class. </param>
        private void noOrYesText_MouseLeave(object sender, MouseEventArgs e) => Animation((Label)sender, 90, Color.FromRgb(255, 254, 204));
        /// <summary>
        /// MouseLeftButtonDown event of the image "checker".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void selectChecker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image checker = (Image)sender;
            if (game.Dices[0] == 0 && game.Dices[1] == 0) return;
            //draw rectangle around checker
            if ((game.Player1.State && player1Checkers.Contains(checker)) || (game.Player2.State && player2Checkers.Contains(checker)))
            {
                gameField.Background = Brushes.Transparent;
                selectedImage = checker;
                border.Width = selectedImage.Width;
                border.Height = selectedImage.Height;
                UIElementCollection collection = gameField.Children;
                if (!collection.Contains(border))
                {
                    gameField.Children.Add(border);
                    border.BorderBrush = Brushes.White;
                }
                Grid.SetColumn(border, Grid.GetColumn(selectedImage));
                Grid.SetRow(border, Grid.GetRow(selectedImage));
                border.Margin = selectedImage.Margin;
                border.BorderThickness = new Thickness(4);
                border.VerticalAlignment = selectedImage.VerticalAlignment;
                Panel.SetZIndex(border, Panel.GetZIndex(selectedImage));
            }
        }
        /// <summary>
        /// MouseLeftButtonDown event of the grid "gameFiled".
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of MouseButtonEventArgs class. </param>
        private void gameField_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (selectedImage != null && isFocus)
            {
                //Calculate number of the row and number of the column of the Grid gameField for moving checker 
                Point position = e.GetPosition(gameField);
                ColumnDefinitionCollection columns = gameField.ColumnDefinitions;
                int oldindex, newIndex, numRow = 1, numColumn;
                if (position.Y > gameField.Height / 2)
                    numRow = 2;
                numColumn = (int)Math.Round(position.X / (columns[2].ActualWidth + columns[3].ActualWidth));
                if (position.X > gameField.Width / 2) numColumn--;
                newIndex = numColumn-1;
                oldindex = (Grid.GetColumn(selectedImage) - 1) / 2;
                if (Grid.GetRow(selectedImage)==1)
                    oldindex = 23 - oldindex;
                if(numRow == 1 && newIndex!=-1) newIndex = 23 - newIndex;
                if (numColumn > 12)
                {
                    numColumn = 27;
                    newIndex = -1;
                }
                else numColumn *= 2;
                border.BorderThickness = new Thickness(0);
                //Take a game turn
                if ((game.CheckedMove() || newIndex == -1) && GameTurn(oldindex, newIndex))
                {
                    if(newIndex!=-1)
                    {
                        Grid.SetRow(selectedImage, numRow);
                        Grid.SetColumn(selectedImage, numColumn);
                        if (game.gameField.Field[newIndex] < 0) Panel.SetZIndex(selectedImage, 0 - game.gameField.Field[newIndex]);
                        else Panel.SetZIndex(selectedImage, 0 + game.gameField.Field[newIndex]);
                    }
                    if (newIndex == -1) TakeAwayCheckers(oldindex);
                    else SetMargin(newIndex, numRow, -(game.gameField.Field[newIndex] + 1), (game.gameField.Field[newIndex] - 1));
                }
                if (!game.CheckedCheckers()) return;
                //if player can't move and take away checker
                if ((!game.CheckedMove() && !game.CheckedTakingAway()) ||(game.Dices[0] == 0 && game.Dices[1] == 0 && game.Dices[2] == 0))
                {
                    game.Dices[0] = game.Dices[1] = 0;
                    game.Player1.State = !game.Player1.State;
                    game.Player2.State = !game.Player2.State;
                }
                isFocus = false;
                gameField.Background = null;
                //if Player 2 is a computer then computer take a game turn
                if (game.Player2.State && game.Mode == GameMode.playerVsComp) ComputerMove();
                if (!game.CheckedCheckers()) game = null;
            }
            else if(selectedImage!=null) isFocus = true;
        }
        /// <summary>
        /// Take a game turn.
        /// </summary>
        /// <param name="oldIndex"> The number of the cell from which the checker moves. </param>
        /// <param name="newIndex"> The number of the cell where which the checker moves. </param>
        /// <returns> True - executed, false - didn't executed </returns>
        private bool GameTurn(int oldIndex, int newIndex)
        {
            int countCheckers = 0;
            if(newIndex>=0 ) countCheckers = game.gameField.Field[newIndex];
            bool isMove = false;
            game.TakeGameTurn(oldIndex, newIndex);
            //if player took a game turn
            if (newIndex== -1 || (game.Player1.State && countCheckers < game.gameField.Field[newIndex]) || (game.Player2.State && countCheckers > game.gameField.Field[newIndex]))
            {
                ShowDice();
                isMove = true;
                //if player don't have checker on the game board then end game
                if (!game.CheckedCheckers())
                {
                    game.Player1.State = !game.Player1.State;
                    game.Player2.State = !game.Player2.State;
                    controlSurrender_MouseDown(null, null);
                    return false;
                }
            }
            return isMove;
        }
        /// <summary>
        /// Show dice value on the grid "control".
        /// </summary>
        private void ShowDice()
        {
            if (game.Player1.State)
            {
                player1Dice.Content = game.Dices[0].ToString() + " : " + game.Dices[1].ToString();
                player1Dice.Visibility = Visibility.Visible;
                player2Dice.Visibility = Visibility.Hidden;
            }
            else if (game.Player2.State)
            {
                player2Dice.Content = game.Dices[0].ToString() + " : " + game.Dices[1].ToString();
                player2Dice.Visibility = Visibility.Visible;
                player1Dice.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// Take away checkers on the side of the board.
        /// </summary>
        /// <param name="oldindex">The number of the cell from which the checker moves. </param>
        private void TakeAwayCheckers(int oldindex)
        {
            //if the first player is a checker
            if (oldindex > 17 && oldindex <24 && game.CheckedFirstHome())
            {
                Grid.SetRow(selectedImage, 2);
                Grid.SetColumn(selectedImage, 27);
                selectedImage.Margin = new Thickness(0, 0, 0, 10 * offsetChecker1);
                selectedImage.VerticalAlignment = VerticalAlignment.Bottom;
                Panel.SetZIndex(selectedImage, 15 - offsetChecker1);
                offsetChecker1++;
            }
            //if the second player is a checker
            else if (oldindex > 5 && oldindex < 12 && game.CheckedSecondHome())
            {
                Grid.SetRow(selectedImage, 1);
                Grid.SetColumn(selectedImage, 27);
                selectedImage.Margin = new Thickness(0, 10 * offsetChecker2, 0, 0);
                selectedImage.VerticalAlignment = VerticalAlignment.Top;
                Panel.SetZIndex(selectedImage, 15 + offsetChecker2);
                offsetChecker2++;
            }
        }
        /// <summary>
        /// Set offset on the grid columns.
        /// </summary>
        /// <param name="newIndex">The number of the cell where which the checker moves.</param>
        /// <param name="numRow"> The number of the row. </param>
        /// <param name="offsetBottom"> Offset for the row 2. </param>
        /// <param name="offsetTop"> Offset for the row 1. </param>
        private void SetMargin(int newIndex, int numRow, int offsetBottom, int offsetTop)
        {
            if (game.gameField.Field[newIndex] < 0)
            {
                if (numRow == 2)
                {
                    selectedImage.Margin = new Thickness(0, 0, 0, 19 * offsetBottom);
                    selectedImage.VerticalAlignment = VerticalAlignment.Bottom;
                }
                else selectedImage.Margin = new Thickness(0, 19 * offsetBottom, 0, 0);
            }
            else if (game.gameField.Field[newIndex] > 0)
            {
                if (numRow == 2) selectedImage.Margin = new Thickness(0, 0, 0, 19 * offsetTop);
                else
                {
                    selectedImage.Margin = new Thickness(0, 19 * offsetTop, 0, 0);
                    selectedImage.VerticalAlignment = VerticalAlignment.Top;
                }
            }
        }
        /// <summary>
        /// Computer take a game turn.
        /// </summary>
        private void ComputerMove()
        {
            int newIndex, oldIndex;
            game.GenerateDice();
            Computer computer = (Computer)game.Player2;
            //take a game turn
            if ((!game.CheckedMove() && game.CheckedSecondHome()) || game.CheckedMove())
            {
                while (game.Dices[0] != 0 || game.Dices[1] != 0)
                {
                    //Calculate number of the row and number of the column of the Grid gameField for moving checker
                    (oldIndex, newIndex) = computer.SearchGameTurn(game.Dices, game.gameField, game.CheckedSecondHome());
                    if (newIndex == -2) break;
                    GameTurn(oldIndex, newIndex);
                    int oldRow, oldColumn, newRow, newColumn;
                    (oldRow, oldColumn) = CalculateRowAndColumn(oldIndex);
                    (newRow, newColumn) = CalculateRowAndColumn(newIndex);
                    UIElementCollection checkers = gameField.Children;
                    //Moving checker
                    for (int i = checkers.Count - 1; i >= 0; i--)
                    {
                        if (Grid.GetColumn(checkers[i]) == oldColumn && Grid.GetRow(checkers[i]) == oldRow)
                        {
                            selectedImage = (Image)checkers[i];
                            //if computer take away a checker
                            if (newIndex == -1) TakeAwayCheckers(oldIndex);
                            else
                            {
                                Grid.SetColumn(selectedImage, newColumn);
                                Grid.SetRow(selectedImage, newRow);
                                Panel.SetZIndex(selectedImage, -game.gameField.Field[newIndex]);
                                SetMargin(newIndex, newRow, -(game.gameField.Field[newIndex] + 1), (game.gameField.Field[newIndex] - 1));
                            }
                            break;
                        }
                    }
                    //if computer can't take away a checker
                    if (game.Player2.State && game.CheckedSecondHome() && !game.CheckedTakingAway()) game.Dices[0] = game.Dices[1] = 0;
                }
            }
            game.Dices[0] = game.Dices[3];
            game.Dices[1] = game.Dices[4];
            ShowDice();
        }
        /// <summary>
        /// Show save files info.
        /// </summary>
        private void ShowFileInfo()
        {
            ItemCollection collection = listDialog.Items;
            while(collection.Count!=0)
                listDialog.Items.Remove(collection[0]);
            string[] fileNames, fileInfo;
            (fileNames, fileInfo) = xmlManage.SearchSave();
            if (fileNames != null && fileInfo != null)
            {
                for (int i = 0; i < fileNames.Length; i++)
                {
                    int index = fileNames[i].LastIndexOf('\\') + 1;
                    string name = fileNames[i].Substring(index, fileNames[i].Length - index);
                    listDialog.Items.Add(name + "    Дата создания: " + fileInfo[i]);
                }
            }
        }
        /// <summary>
        /// Calculate the number of the rown and of the column for cell of the game field.
        /// </summary>
        /// <param name="index"> The number of the cell where which the checker moves. </param>
        /// <returns></returns>
        private (int,int) CalculateRowAndColumn(int index)
        {
            int row = 2, column;
            if (index > 11) row = 1;
            if (index < 12) index++;
            else if (index > 12) index = 24 - index;
            column = index * 2;
            return (row, column);
        }
        /// <summary>
        /// SelectionChanged event of the ListBox "listDialog". 
        /// </summary>
        /// <param name="sender"> Object. </param>
        /// <param name="e"> Object of SelectionChangedEventArgs class. </param>>
        private void listDialog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (game != null || (game == null && loadingOrSaving.Content.ToString() != "Сохранить игру"))
            {
                System.Collections.IList collection = e.AddedItems;
                if (collection.Count != 0)
                {
                    string name = (string)collection[0], flName;
                    int index = name.IndexOf(".xml");
                    flName = name.Substring(0, index);
                    fileName.Text = flName;
                }
            }
        }
        /// <summary>
        /// Put checker on the side of the board.
        /// </summary>
        /// <param name="numPlayer1Checkers"> Number of player 1 checker on the side of the board. </param>
        /// <param name="numPlayer2Checkers"> Number of player 2 checker on the side of the board. </param>
        private void PutOnTheBoard(int numPlayer1Checkers, int numPlayer2Checkers)
        {
            UIElementCollection checkers = gameField.Children;
            int oldColumn1 = 2, oldColumn2 = 24, oldRow1 = 2, oldRow2 = 1;
            for (int j = checkers.Count - 1; j >= 0; j--)
            {
                int oldindex;
                selectedImage = (Image)checkers[j];
                //if the first player is a checker
                if (Grid.GetColumn(checkers[j]) == oldColumn1 && Grid.GetRow(checkers[j]) == oldRow1 && numPlayer1Checkers!=0)
                {
                    oldindex = 19;
                    TakeAwayCheckers(oldindex);
                    numPlayer1Checkers--;
                    if (numPlayer1Checkers == 0) break;
                }
                //if the second player is a checker
                else if (Grid.GetColumn(checkers[j]) == oldColumn2 && Grid.GetRow(checkers[j]) == oldRow2 && numPlayer2Checkers != 0)
                {
                    oldindex = 9;
                    TakeAwayCheckers(oldindex);
                    numPlayer2Checkers--;
                    if (numPlayer2Checkers == 0) break;
                }
            }
        }
    }
}

