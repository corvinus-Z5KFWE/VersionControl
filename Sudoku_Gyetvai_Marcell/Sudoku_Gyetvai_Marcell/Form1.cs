using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku_Gyetvai_Marcell
{
    public partial class Form1 : Form
    {
        private Random _rng = new Random();
        
        public Form1()
        {
            InitializeComponent();

            CreatePlayField();
            LoadSudokus();
            GetRandomQuiz();
            NewGame();
            mainPanel.Width = 280;
            mainPanel.Height = 280;

        }
        private List<Sudoku> _sudokus = new List<Sudoku>();
        private Sudoku _currentQuiz = null;

        private void CreatePlayField()
        {
            string szoveg;
            int lineWidth = 5;
            for (int row = 0; row < 9; row++)
            {
                for (int collumn = 0; collumn < 9; collumn++)
                {
                    SudokuField sf = new SudokuField();
                    sf.Left = collumn * sf.Width + (int)(Math.Floor((double)(collumn / 3))) * lineWidth;
                    MouseDown += Form1_MouseDown;

                    sf.Top = row * sf.Width + (int)(Math.Floor((double)(row / 3))) * lineWidth;
                    mainPanel.Controls.Add(sf);
                }
            }
            foreach (var item in mainPanel.Controls.OfType<SudokuField>())
            {

            }
        }

       

        private void LoadSudokus()
        {
            _sudokus.Clear();
            using (StreamReader sr = new StreamReader("sudoku.csv", Encoding.Default)) 
            {
                sr.ReadLine();
                while(! sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(',');

                    Sudoku s = new Sudoku();
                    s.Quiz = line[0];
                    s.Solution = line[1];

                    _sudokus.Add(s);
                }
            }
        }

        private Sudoku GetRandomQuiz()
        {
            int randomNumber = _rng.Next(_sudokus.Count);
            return _sudokus[randomNumber];
        }

        private void NewGame()
        {
            _currentQuiz = GetRandomQuiz();
            int counter = 0;
            foreach  (var sf in mainPanel.Controls.OfType<SudokuField>())
            {
                sf.Value = int.Parse(_currentQuiz.Quiz[counter].ToString());
                sf.Active = sf.Value == 0;
                counter++;
            }

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void SudoField_MouseDown(object sender, MouseEventArgs e)
        {
            string click = "";
            foreach (var sf  in mainPanel.Controls.OfType<SudokuField>())
            {
                click = click + sf.Value.ToString();
                if (click.Equals(_currentQuiz.Solution))
                {
                    foreach (var sudokutext in mainPanel.Controls.OfType<SudokuField>())
                    {
                        sudokutext.Active = false;
                    }
                    MessageBox.Show("You are the champion.");
                }
            }
          

        }


    }
}