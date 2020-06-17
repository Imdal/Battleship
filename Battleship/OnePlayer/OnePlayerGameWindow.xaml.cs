using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace Battleship
{
    /// <summary>
    /// Interaction logic for OnePlayerGameWindow.xaml
    /// </summary>
    public partial class OnePlayerGameWindow : Window
    {
        private Point position;
        private Point position1;
        private Point position2;
        private bool place;
        private bool firstShoot;
        private int move;
        private Render render;
        private Random random = new Random();
        private Table table1;
        private Table table2;
        private OnePlayerState onePlayerState;
        List<OnePlayerState> states = new List<OnePlayerState>();

        public OnePlayerGameWindow()
        {
            InitializeComponent();
            render = new Render();
            table1 = new Table();
            table2 = new Table();
            move = 1;
            place = true;
            render.Rendering(table1, Table1);
            render.Rendering(table2, Table2);
            firstShoot = random.Next() % 2 == 0;
            Trace.WriteLine(firstShoot);
            PlayerInstruction.Text = "Place your ships!";
            table1.randomPlaceShips();
            table1.traceShips();
            render.hiddenRendering(table1, Table1);
            if (mainwindow.player != "") 
            { 
                Player.Text = mainwindow.player;
            }
        }

        private void Table1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!place)
            {

                position = e.GetPosition(Table1);
                if (table1.shoot((int)position.X / Render.GameSize, (int)position.Y / Render.GameSize))
                {
                    table1.shunkenShip();
                    render.renderShipNumberShunken(ship1enemy, ship2enemy, ship3enemy, ship4enemy, ship5enemy, table1);
                    render.hiddenRendering(table1, Table1);
                    move++;
                    table2.randomShoot();
                    table2.shunkenShip();
                    render.renderShipNumberShunken(ship1, ship2, ship3, ship4, ship5, table2);
                    Trace.WriteLine("random shoot");
                    render.Rendering(table2, Table2);
                    move++;
                }
                if (table1.allShooted())
                {
                    PlayerInstruction.Text = "Win!";
                    onePlayerState = new OnePlayerState(Player.Text, true, move / 2, table2.numberOfShipsShunken());
                    states = OnePlayerState.Load("OnePlayerState.xml");
                    states.Add(onePlayerState);
                    onePlayerState.Save("OnePlayerState.xml", states);
                    OnePlayerFinalWindow finalWindow = new OnePlayerFinalWindow();
                    finalWindow.Show();
                    this.Close();
                }
                else if (table2.allShooted())
                {
                    PlayerInstruction.Text = "Lose!";
                    onePlayerState = new OnePlayerState(Player.Text, false, move / 2, table1.numberOfShipsShunken());
                    states = OnePlayerState.Load("OnePlayerState.xml");
                    states.Add(onePlayerState);
                    onePlayerState.Save("OnePlayerState.xml", states);
                    OnePlayerFinalWindow finalWindow = new OnePlayerFinalWindow();
                    finalWindow.Show();
                    this.Close();
                }
            }
        }

        
        private void Table2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            position1 = e.GetPosition(Table2);
        }

        private void Table2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            position2 = e.GetPosition(Table2);
            if(place)
            {
                Trace.Write("X: ");
                Trace.Write((int)position1.X / Render.GameSize);
                Trace.Write(" Y: ");
                Trace.Write((int)position1.Y / Render.GameSize);
                Trace.Write("\nX: ");
                Trace.Write((int)position2.X / Render.GameSize);
                Trace.Write(" Y: ");
                Trace.Write((int)position2.Y / Render.GameSize);
                table2.placeShip((int)position1.X/ Render.GameSize, (int)position1.Y/ Render.GameSize, (int)position2.X/ Render.GameSize, (int)position2.Y/ Render.GameSize);
                render.renderShipNumber(ship1, ship2, ship3, ship4, ship5, table2);
                render.Rendering(table2, Table2);
                if(table2.allShipsPlaced()==true)
                {
                    table2.traceShips();
                    if (!firstShoot)
                    {
                        table2.randomShoot();
                        move++;
                        Trace.WriteLine("random shoot");
                        render.Rendering(table2, Table2);
                        firstShoot = true;
                    }
                    place = false;
                    PlayerInstruction.Text = "Shoot the enemy!";
                }
            }
        }

        

        private void Table1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.H)
            {
                render.Rendering(table1, Table1);
            }
        }

        private void Table1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.H)
            {
                render.hiddenRendering(table1, Table1);
            }
        }
    }
}
