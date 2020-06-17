using Battleship.TwoPlayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Battleship
{
    public partial class TwoPlayerGameWindow : Window
    {
        private Point position;
        private Point position1;
        private Point position2;
        private bool place;
        private bool firstPlayerPlace;
        private bool secondplayerPlace;
        private bool firstShoot;
        private int move;
        private int round;
        private Render render = new Render();
        private Random random = new Random();
        private Table table1;
        private Table table2;
        private TwoPlayerState twoPlayerState;
        List<TwoPlayerState> states = new List<TwoPlayerState>();
        public TwoPlayerGameWindow()
        {
            InitializeComponent();
            table1 = new Table();
            table2 = new Table();
            place = true;
            round = 1;
            firstShoot = random.Next() % 2 == 0;
            if (firstShoot)
            {
                Player1Instruction.Text = "Place your ships!";
                firstPlayerPlace = true;
                secondplayerPlace = false;
                move = 0;
            }
            else
            {
                Player2Instruction.Text = "Place your ships!";
                firstPlayerPlace = false;
                secondplayerPlace = true;
                move = 1;
            }
            render.Rendering(table1, Table1);
            render.Rendering(table2, Table2);
            if (mainwindow.firstPlayer != "")
            {
                Player1.Text = mainwindow.firstPlayer;
            }
            if (mainwindow.secondPlayer != "")
            {
                Player2.Text = mainwindow.secondPlayer;
            }
        }
        
        private void Table1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(firstPlayerPlace)
            {
                position1 = e.GetPosition(Table1);
            }
            else if (!place && !secondplayerPlace)
            {
                if (move % 2 == 0)
                {
                    position = e.GetPosition(Table1);
                    if (table1.shoot((int)position.X / Render.GameSize, (int)position.Y / Render.GameSize))
                    {
                        table1.shunkenShip();
                        render.renderShipNumberShunken(ship1first, ship2first, ship3first, ship4first, ship5first, table1);
                        render.hiddenRendering(table1, Table1);
                        move++;
                        round++;
                        Player2Instruction.Text = "";
                        Player1Instruction.Text = "Shoot the enemy!";
                    }
                    if (table1.allShooted())
                    {
                        Player2Instruction.Text = "You Win!";
                        Player1Instruction.Text = "You Lose!";
                        twoPlayerState = new TwoPlayerState(Player2.Text, Player1.Text, round / 2, table2.numberOfShipsShunken());
                        try { states = TwoPlayerState.Load("TwoPlayerState.xml"); }
                        catch (FileNotFoundException) {  }
                        states.Add(twoPlayerState);
                        twoPlayerState.Save("TwoPlayerState.xml", states);
                        TwoPlayerFinalWindow twoPlayerFinalWindow = new TwoPlayerFinalWindow();
                        twoPlayerFinalWindow.Show();
                        this.Close();
                    }
                }
            }
        }

        private void Table1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (firstPlayerPlace)
            {
                position2 = e.GetPosition(Table1);
                table1.placeShip((int)position1.X / Render.GameSize, (int)position1.Y / Render.GameSize, (int)position2.X / Render.GameSize, (int)position2.Y / Render.GameSize);
                render.renderShipNumber(ship1first, ship2first, ship3first, ship4first, ship5first, table1);
                render.Rendering(table1, Table1);
                if (table1.allShipsPlaced() == true)
                {
                    Player1Instruction.Text = "All ships placed.";
                    firstPlayerPlace = false;
                    if(place)
                    {
                        Player2Instruction.Text = "Place your ships!";
                        secondplayerPlace = true;
                        place = false;
                    }
                    else
                    {
                        Player2Instruction.Text = "";
                        Player1Instruction.Text = "Shoot the enemy!";
                    }
                    render.hiddenRendering(table1, Table1);
                }
            }
        }

        private void Table2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (secondplayerPlace)
            {
                position1 = e.GetPosition(Table2);
            }
            else if (!place && !secondplayerPlace)
            {
                if (move % 2 == 1)
                {
                    position = e.GetPosition(Table2);
                    if (table2.shoot((int)position.X / Render.GameSize, (int)position.Y / Render.GameSize))
                    {
                        table2.shunkenShip();
                        render.renderShipNumberShunken(ship1second, ship2second, ship3second, ship4second, ship5second, table2);
                        render.hiddenRendering(table2, Table2);
                        move++;
                        round++;
                        Player1Instruction.Text = "";
                        Player2Instruction.Text = "Shoot the enemy!";
                    }
                    if (table2.allShooted())
                    {
                        Player1Instruction.Text = "You Win!";
                        Player2Instruction.Text = "You Lose!";
                        twoPlayerState = new TwoPlayerState(Player1.Text, Player2.Text, round / 2, table1.numberOfShipsShunken());
                        try { states = TwoPlayerState.Load("TwoPlayerState.xml"); }
                        catch (FileNotFoundException) { }
                        states.Add(twoPlayerState);
                        twoPlayerState.Save("TwoPlayerState.xml", states);
                        TwoPlayerFinalWindow twoPlayerFinalWindow = new TwoPlayerFinalWindow();
                        twoPlayerFinalWindow.Show();
                        this.Close();
                    }
                }
            }
        }

        private void Table2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (secondplayerPlace)
            {
                position2 = e.GetPosition(Table2);
                table2.placeShip((int)position1.X / Render.GameSize, (int)position1.Y / Render.GameSize, (int)position2.X / Render.GameSize, (int)position2.Y / Render.GameSize);
                render.renderShipNumber(ship1second, ship2second, ship3second, ship4second, ship5second, table2);
                render.Rendering(table2, Table2);
                if (table2.allShipsPlaced() == true)
                {
                    Player2Instruction.Text = "All ships placed.";
                    secondplayerPlace = false;
                    if (place)
                    {
                        Player1Instruction.Text = "Place your ships!";
                        firstPlayerPlace = true;
                        place = false;
                    }
                    else
                    {
                        Player1Instruction.Text = "";
                        Player2Instruction.Text = "Shoot the enemy!";
                    }
                    render.hiddenRendering(table2, Table2);
                }
            }
        }
    }
}
