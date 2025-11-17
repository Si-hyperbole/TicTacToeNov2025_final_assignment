/// Max Csikos W# 0526040

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeNov2025
{
    public partial class LablePl2 : Form
    {
        public LablePl2()
        {
            InitializeComponent();
        }

        bool playerTurn = true;// true = 'X',false = 'O'
        int[,] bordArray = new int[3, 3];
        int[,] magicSquare = { { 2, 7, 6 }, { 9, 5, 1 }, { 4, 3, 8 } }; // rows, colum, diagnoals sum to 15. 3x3 square == 15
        private int _tieCounter = 0;
        private int _winingScoreX = 0;
        private int _winningScoreO = 0;
        bool bordEnable = true;
        //bool winningSquar;
        //bool hilightLight = true;

        //private int _moveCounter = 0;
        public void CheckWinner(int x, int y)
        {
            _tieCounter++;
            int colums = 0;
            int rows = 0;
            int diagonal1 = 0;
            int anitDiagonal1 = 0;

            for (int i = 0; i < 3; i++)//counts the colums
            {
                colums += bordArray[i, y] * magicSquare[i, y];
                rows += bordArray[x, i] * magicSquare[x, i];
                //Console.WriteLine("Colums check for winner For Loop");
            }
            for (int i = 0; i < 3; i++)// counts the diagnoals
            {
                diagonal1 += bordArray[i, i] * magicSquare[i, i];
                anitDiagonal1 += bordArray[i, 2 - i] * magicSquare[i, 2 - i];
                //Console.WriteLine(" Diagonal check for winner For Loop");
            }

            if (colums == 15 || rows == 15 || diagonal1 == 15 || anitDiagonal1 == 15)
            {
                bordEnable = false;
                Winner.Text = ($"The Winner Is {Player1Name.Text}");
                _winingScoreX++;
                ScooreCounterPl1.Text = _winingScoreX.ToString();
                BNewGame.Enabled = true;
                HightLightWinner(x, y, rows, colums, diagonal1, anitDiagonal1);
            }
            else if (colums == 30 || rows == 30 || anitDiagonal1 == 30 || diagonal1 == 30)/// "O" wins
            {
                Winner.Text = ($"The Winner Is {Player2Name.Text}");
                _winningScoreO++;
                ScooreCounterPl2.Text = _winningScoreO.ToString();
                BNewGame.Enabled = true;
                HightLightWinner(x, y, rows, colums, diagonal1, anitDiagonal1);

                //ScooreCounterPl2.Text = winningScore.ToString();
            }
            else if (_tieCounter == 9)// tie counter
            {
                Winner.Text = _tieCounter.ToString("Its a Tie");
                BNewGame.Enabled = true;
            }
        }// end of check winner method
        private void HightLightWinner(int x, int y, int rows, int colums, int diagonal1, int anitDiagonal1)
        {
            Color winnningSquar = Color.Green;
            if (rows == 15 || rows == 30)
            {
                switch (x)
                {
                    case 0:
                        AA.BackColor = winnningSquar;
                        BA.BackColor = winnningSquar;
                        CA.BackColor = winnningSquar;
                        break;
                    case 1:
                        AB.BackColor = winnningSquar;
                        BB.BackColor = winnningSquar;
                        CB.BackColor = winnningSquar;
                        break;
                    case 2:
                        AC.BackColor = winnningSquar;
                        BC.BackColor = winnningSquar;
                        CC.BackColor = winnningSquar;
                        break;
                }
            }
            if (colums == 15 || colums == 30)
            {
                switch (y)
                {
                    case 0:
                        AA.BackColor = winnningSquar;
                        AB.BackColor = winnningSquar;
                        AC.BackColor = winnningSquar;
                        break;
                    case 1:
                        BA.BackColor = winnningSquar;
                        BB.BackColor = winnningSquar;
                        BC.BackColor = winnningSquar;
                        break;
                    case 2:
                        CA.BackColor = winnningSquar;
                        CB.BackColor = winnningSquar;
                        CC.BackColor = winnningSquar;
                        break;
                }
            }
            if (diagonal1 == 15 || diagonal1 == 30)
            {
                AA.BackColor = winnningSquar;
                BB.BackColor = winnningSquar;
                CC.BackColor = winnningSquar;

            }
            else if (anitDiagonal1 == 15 || anitDiagonal1 == 30)
            {
                AC.BackColor = winnningSquar;
                BB.BackColor = winnningSquar;
                CA.BackColor = winnningSquar;
            }
        }
        private void BClickPlayerMoves(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int colum = 0;
            int row = 0;
            //bord = true buttons enabled
            if (bordEnable)
            {
                // represents X access in boardarray
                switch (button.Name[0])
                {
                    case 'A':
                        row = 0;
                        //Console.WriteLine("Row Button 'A' has been pressed");
                        break;
                    case 'B':
                        row = 1;
                        //Console.WriteLine("Row buttion 'B' hass been pressed");
                        break;
                    case 'C':
                        row = 2;
                        //Console.WriteLine("Row buttion 'C' hass been pressed");
                        break;
                }// end of switch row

                // represents y access in boardarray
                switch (button.Name[1])
                {
                    case 'A':
                        colum = 0;
                        //Console.WriteLine("colum button 'A' has been pressed");
                        break;
                    case 'B':
                        colum = 1;
                        //Console.WriteLine("colum buttion 'B' hass been pressed");
                        break;
                    case 'C':
                        colum = 2;
                        //Console.WriteLine("colum buttion 'C' hass been pressed");
                        break;
                }// end of switch colum
                // players moves
                if (playerTurn)
                {
                    button.Text = "X";
                    bordArray[colum, row] = 1;
                    playerTurn = false;
                    BNewGame.Enabled = false;
                    //Console.WriteLine("Player X turn");
                }
                else
                {
                    button.Text = "O";
                    bordArray[colum, row] = 2;
                    playerTurn = true;
                    //Console.WriteLine("Player O turn");
                }
                button.Enabled = false;
                CheckWinner(colum, row);
            }//end boardEnable
        }// end of button click method
        
        private void BNewGame_Click(object sender, EventArgs e)
        {
            // reset all buttoonds and make them active
            AA.Text = ""; AA.Enabled = true;
            AB.Text = ""; AB.Enabled = true;
            AC.Text = ""; AC.Enabled = true;
            BA.Text = ""; BA.Enabled = true;
            BB.Text = ""; BB.Enabled = true;
            BC.Text = ""; BC.Enabled = true;
            CA.Text = ""; CA.Enabled = true;
            CB.Text = ""; CB.Enabled = true;
            CC.Text = ""; CC.Enabled = true;
            // Reset background color
            Color winnningSquar = Color.White;
            AA.BackColor = winnningSquar;
            AB.BackColor = winnningSquar;
            AC.BackColor = winnningSquar;
            BA.BackColor = winnningSquar;
            BB.BackColor = winnningSquar;
            BC.BackColor = winnningSquar;
            CA.BackColor = winnningSquar;
            CB.BackColor = winnningSquar;
            CC.BackColor = winnningSquar;
            Winner.Text = "";
            bordArray = new int[3, 3];
            _tieCounter = 0;
            playerTurn = true;
            bordEnable = true;
        }//end of BNewGame

        private void BExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void LablePl2_Load(object sender, EventArgs e)
        {

        }

        private void LablePl2_Load_1(object sender, EventArgs e)
        {

        }
    }
}

