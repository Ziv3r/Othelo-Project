using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace View
{
    
    public partial class BoardForm : Form
    {
        private const int k_ButtonsHeightAndWidth = 55;
        private int m_MatrixSize;
        private Button[,] m_Buttons;
        public event Action<Point> onClick;
        public BoardForm()
        {
            InitializeComponent();
        }

        private void BoardForm_Load(object sender, EventArgs e)
        {

        }

        public void AddButtons(int i_MatrixSize)
        {
            m_Buttons = new Button[i_MatrixSize, i_MatrixSize];
            this.Size = new Size(i_MatrixSize * 55+200, i_MatrixSize*55 +200);
            this.ClientSize = new Size(i_MatrixSize * 55 + 15, i_MatrixSize * 55 + 15);

            m_MatrixSize = i_MatrixSize;

            for (int i = 0; i < i_MatrixSize; i++)
            {
                for (int j = 0; j < i_MatrixSize; j++)
                {
                    Point indexes = new Point(i, j);
                    Button newButton = createButton(indexes);
                    newButton.Click += new EventHandler(button_Click);
                    m_Buttons[i, j] = newButton;
                    m_Buttons[i, j].Top = 3 + i * 55;
                    m_Buttons[i, j].Left = 3 + j * 55;
                    this.Controls.Add(newButton);
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            Point coords = getPointFromButton(sender as Button);
            onClick(coords);
            this.Close();
        }
        private Point getPointFromButton(Button currentButton)
        {
            string[] coordinates = currentButton.Name.Split(',');

            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);

            return new Point(x,y);
        }

        private Button createButton(Point i_coords)
        {
            Button button = new Button();

            button.Enabled = false;
            button.Name = i_coords.X.ToString() + "," + i_coords.Y.ToString();
            button.Size = new System.Drawing.Size(55, 55);
            button.TabIndex = 1;
            button.UseVisualStyleBackColor = true;
            return button;
        }

        public void UpdateBoard(List<Point> i_Optionals, char[,] i_LogicMatrix, string i_CurrentPlayer)
        {
            //1.chagne the text to X/O due to to logic matrix .
            updateMatrix(i_LogicMatrix);

            //2. change enable-- true /fasle 
            updateOptional(i_Optionals);

            //3chagne title of form 
            this.Text = i_CurrentPlayer;
        }

        private void updateOptional(List<Point> i_Optionals)
        {
            foreach (Point currentPoint in i_Optionals)
            {
                Button currentButton = m_Buttons[currentPoint.X, currentPoint.Y];
                currentButton.BackColor = Color.Green;
                currentButton.Enabled = true;
            }
        }
        private void updateMatrix(char[,] i_LogicMatrix)
        {
            for (int i = 0; i < m_MatrixSize; i++)
            {
                for (int j = 0; j < m_MatrixSize; j++)
                {
                    Button currentButton = m_Buttons[i, j];
                    if (i_LogicMatrix[i, j] == ' ')
                    {
                        currentButton.Text = string.Empty;
                    } 
                    else if(i_LogicMatrix[i, j] =='X')
                    {
                        m_Buttons[i, j].BackgroundImage = Properties.Resources.refreshBlue;
                    }
                    else
                    {
                        m_Buttons[i, j].BackgroundImage = Properties.Resources.refreshGreen;

                    }
                    currentButton.BackColor = default(Color);
                    currentButton.Enabled = false;
                }
            }
        }
    }
}
