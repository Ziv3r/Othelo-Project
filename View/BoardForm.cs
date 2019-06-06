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
        private Dictionary<Point, Button> m_buttons;
        public BoardForm()
        {
            InitializeComponent();
            m_buttons = new Dictionary<Point, Button>();
            
        }

        private void BoardForm_Load(object sender, EventArgs e)
        {

        }

        public void AddButtons(int i_MatrixSize)
        {
            for (int i = 0; i < i_MatrixSize; i++)
            {
                for (int j = 0; j < i_MatrixSize; j++)
                {
                    Point indexes = new Point(i, j);
                    Button newButton = createButton(indexes);
                    m_buttons.Add(indexes, newButton);
                    flowLayoutPanel.Controls.Add(newButton);
                }
            }

            int flowLayOutwidthHeight = calculateFlowLayoutWidth(i_MatrixSize);

            flowLayoutPanel.Width = flowLayOutwidthHeight;
            flowLayoutPanel.Height = flowLayOutwidthHeight;

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

  
        private int calculateFlowLayoutWidth(int i_NumOfButtons)
        {
            return i_NumOfButtons * k_ButtonsHeightAndWidth + i_NumOfButtons;
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
