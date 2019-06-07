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
        private const int k_ImageWidthAndHeight = 50;
        private const int k_ButtonsHeightAndWidth = 55;
        private int m_MatrixSize;
        private PictureBox[,] m_Buttons;
        readonly Bitmap r_RedImage = new Bitmap(Properties.Resources.CoinRed, new Size(k_ImageWidthAndHeight, k_ImageWidthAndHeight));
        readonly Bitmap r_YellowImage = new Bitmap(Properties.Resources.CoinYellow, new Size(k_ImageWidthAndHeight, k_ImageWidthAndHeight));

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
            m_Buttons = new PictureBox[i_MatrixSize, i_MatrixSize];
            this.Size = new Size(i_MatrixSize * 55 + 200, i_MatrixSize * 55 + 200);
            this.ClientSize = new Size(i_MatrixSize * 55 + 15, i_MatrixSize * 55 + 15);

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
                    m_Buttons[i, j].Top = 3 + i * 55;
                    m_Buttons[i, j].Left = 3 + j * 55;
                    this.Controls.Add(newPic);
                }
            }
        }

        private void picBox_Click(object sender, EventArgs e)
        {
            Point coords = getPointFromButton(sender as PictureBox);
            onClick(coords);
        }

        private Point getPointFromButton(PictureBox currentButton)
        {
            string[] coordinates = currentButton.Name.Split(',');

            int x = int.Parse(coordinates[0]);
            int y = int.Parse(coordinates[1]);

            return new Point(x, y);
        }

        private PictureBox createPicBox(Point i_Indx)
        {
            PictureBox picBox = new PictureBox();
            picBox.Location = new System.Drawing.Point(218, 70);
            picBox.Name = i_Indx.X.ToString() + "," + i_Indx.Y.ToString();
            picBox.Size = new System.Drawing.Size(55, 55);
            picBox.TabIndex = 1;
            picBox.TabStop = false;
            picBox.BackgroundImageLayout = ImageLayout.Center;
            return picBox;
        }
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            ControlPaint.DrawBorder(e.Graphics, pic.ClientRectangle,
                Color.Black, 1, ButtonBorderStyle.Solid, // left border
                Color.Black, 1, ButtonBorderStyle.Solid,// top border
                Color.Black, 1, ButtonBorderStyle.Solid,// right border
                Color.Black, 1, ButtonBorderStyle.Solid);// bottom border
        }

        public void UpdateBoard(List<Point> i_Optionals, char[,] i_LogicMatrix, string i_CurrentPlayer)
        {
            // 1.chagne the text to X/O due to to logic matrix .
            updateMatrix(i_LogicMatrix);

            // 2. change enable-- true /fasle 
            updateOptional(i_Optionals);

            // 3chagne title of form 
            this.Text = i_CurrentPlayer;
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

        private void updateMatrix(char[,] i_LogicMatrix)
        {
            for (int i = 0; i < m_MatrixSize; i++)
            {
                for (int j = 0; j < m_MatrixSize; j++)
                {
                    PictureBox currentButton = m_Buttons[i, j];
                    if (i_LogicMatrix[i, j] == ' ')
                    {
                        currentButton.Text = string.Empty;
                    }
                    else if (i_LogicMatrix[i, j] == 'X')
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
    }
}
