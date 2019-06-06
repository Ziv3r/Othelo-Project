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

        //private Dictionary<Point, Button> m_buttons;
        private Button[,] m_Buttons;

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
            m_MatrixSize = i_MatrixSize;

            for (int i = 0; i < i_MatrixSize; i++)
            {
                for (int j = 0; j < i_MatrixSize; j++)
                {
                    Point indexes = new Point(i, j);
                    Button newButton = createButton(indexes);
                    newButton.Click += new EventHandler(button_Click);
                    m_Buttons[i, j] = newButton;
                    flowLayoutPanel.Controls.Add(newButton);
                }
            }

            int flowLayOutwidthHeight = calculateFlowLayoutWidth(i_MatrixSize);

            flowLayoutPanel.Width = flowLayOutwidthHeight;
            flowLayoutPanel.Height = flowLayOutwidthHeight;

        }

        private void button_Click(object sender, EventArgs e)
        {
            Point Coords = getPointFromButton(sender as Button);

        }

        private Button createButton(Point i_coords)
        {
            Button button = new Button();

            button.Enabled = false;
            button.Name = "button" + i_coords.X.ToString() + i_coords.Y.ToString();
            button.Size = new System.Drawing.Size(55, 55);
            button.TabIndex = 1;
            button.UseVisualStyleBackColor = true;
            return button;
        }

        public void UpdateBoard(List<Point> i_Optionals, char[,] i_LogicMatrix, string i_CurrentPlayer)
        {
            //1. change enable-- true /fasle 
            updateOptional(i_Optionals);

            //2.chagne the text to X/O due to to logic matrix .
            updateMatrix(i_LogicMatrix);

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
                for (int j = 0; i < m_MatrixSize; i++)
                {
                    Button currentButton = m_Buttons[i, j];
                    if (i_LogicMatrix[i, j] == ' ')
                    {
                        currentButton.Text = string.Empty;
                    }
                    else
                    {
                        m_Buttons[i, j].Text = i_LogicMatrix[i, j].ToString();
                    }
                }
            }
        }

        private int calculateFlowLayoutWidth(int i_NumOfButtons)
        {
            return i_NumOfButtons * k_ButtonsHeightAndWidth + i_NumOfButtons;
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
