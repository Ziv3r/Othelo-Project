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
        private const char k_FirstPlayerSign = 'X';
        private const char k_SecPlayerSign = 'O';
        private const char k_EmptyPointSign = ' ';
        private const int k_ImageWidthAndHeight = 50;
        private const int k_ButtonsHeightAndWidth = 55;
        public const string k_TopBarMessage = "Othello - {0}'s Turn";
        private readonly Bitmap r_RedImage = new Bitmap(Properties.Resources.CoinRed, new Size(k_ImageWidthAndHeight, k_ImageWidthAndHeight));
        private readonly Bitmap r_YellowImage = new Bitmap(Properties.Resources.CoinYellow, new Size(k_ImageWidthAndHeight, k_ImageWidthAndHeight));
        private int m_MatrixSize;
        private PictureBox[,] m_Buttons;

        public event Action<Point> OnButtonClick;

        public BoardForm()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
        }

        public void AddButtons(int i_MatrixSize)
        {
            m_Buttons = new PictureBox[i_MatrixSize, i_MatrixSize];
            this.ClientSize = new Size((i_MatrixSize * k_ButtonsHeightAndWidth) + 50, (i_MatrixSize * k_ButtonsHeightAndWidth) + 50);

            m_MatrixSize = i_MatrixSize;

            for (int i = 0; i < i_MatrixSize; i++)
            {
                for (int j = 0; j < i_MatrixSize; j++)
                {
                    Point indexes = new Point(i, j);
                    PictureBox newPic = createPicBox(indexes);
                    newPic.Click += new EventHandler(picBox_Click);
                    newPic.Paint += new PaintEventHandler(pictureBox_Paint);
                    m_Buttons[i, j] = newPic;
                    m_Buttons[i, j].Top = 25 + (i * k_ButtonsHeightAndWidth);
                    m_Buttons[i, j].Left = 25 + (j * k_ButtonsHeightAndWidth);
                    this.Controls.Add(newPic);
                }
            }
        }

        private void picBox_Click(object sender, EventArgs e)
        {
            Point coords = getPointFromButton(sender as PictureBox);
            OnButtonClick(coords);
        }

        private Point getPointFromButton(PictureBox i_CurrentButton)
        {
            string[] coordinates = i_CurrentButton.Name.Split(',');

            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);

            return new Point(x, y);
        }

        private PictureBox createPicBox(Point i_Indx)
        {
            PictureBox picBox = new PictureBox();
            picBox.Name = i_Indx.X.ToString() + "," + i_Indx.Y.ToString();
            picBox.Size = new System.Drawing.Size(k_ButtonsHeightAndWidth, k_ButtonsHeightAndWidth);
            picBox.BackgroundImageLayout = ImageLayout.Center;
            return picBox;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            ControlPaint.DrawBorder(e.Graphics, pic.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        public void UpdateBoard(List<Point> i_Optionals, char[,] i_LogicMatrix, string i_CurrentPlayer)
        {
            updateMatrix(i_LogicMatrix);
            updateOptional(i_Optionals);
            this.Text = string.Format(k_TopBarMessage, i_CurrentPlayer);
        }

        private void updateOptional(List<Point> i_Optionals)
        {
            foreach (Point currentPoint in i_Optionals)
            {
                PictureBox currentButton = m_Buttons[currentPoint.X, currentPoint.Y];
                currentButton.BackColor = Color.Green;
                currentButton.Enabled = true;
            }
        }

        public void OnUpdateCell(Point i_ToUpdate, char i_Sign)
        {
            int col = i_ToUpdate.X;
            int row = i_ToUpdate.Y;
            PictureBox currentButton = m_Buttons[col, row];
            if (i_Sign == k_EmptyPointSign)
            {
                currentButton.Text = string.Empty;
                currentButton.BackgroundImage = null;
            }
            else if (i_Sign == k_FirstPlayerSign)
            {
                m_Buttons[col, row].BackgroundImage = r_RedImage;
            }
            else
            {
                m_Buttons[col, row].BackgroundImage = r_YellowImage;
            }

            currentButton.BackColor = default(Color);
            currentButton.Enabled = false;
        }

        private void updateMatrix(char[,] i_LogicMatrix)
        {
            for (int i = 0; i < m_MatrixSize; i++)
            {
                for (int j = 0; j < m_MatrixSize; j++)
                {
                    PictureBox currentButton = m_Buttons[i, j];
                    if (i_LogicMatrix[i, j] == k_EmptyPointSign)
                    {
                        currentButton.Text = string.Empty;
                        currentButton.BackgroundImage = null;
                    }
                    else if (i_LogicMatrix[i, j] == k_FirstPlayerSign)
                    {
                        m_Buttons[i, j].BackgroundImage = r_RedImage;
                    }
                    else
                    {
                        m_Buttons[i, j].BackgroundImage = r_YellowImage;
                    }

                    currentButton.BackColor = default(Color);
                    currentButton.Enabled = false;
                }
            }
        }

        private void BoardForm_Load(object sender, EventArgs e)
        {
        }
    }
}
