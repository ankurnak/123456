using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ArrayKursova
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panelMenu.BackColor = Color.DarkGray;

            // Оформление panelLogo
            panelLogo.BackColor = Color.LightGray;

            panelLogo.BackgroundImageLayout = ImageLayout.Zoom;

            // Оформление panelTitle
            panelTitle.BackColor = Color.Khaki;
            panelTitle.ForeColor = Color.White;
            textBox1.Font = new Font("Arial", 40, FontStyle.Bold);
            textBox1.Text = "Array and Fractions";
        }

        private void Math_operations_Click(object sender, EventArgs e)
        {
            FormMath_operations formMath_operations = new FormMath_operations();
            formMath_operations.Show();
            Math_operations.BackColor = Color.LightGreen;
        }
        private void Math_operations_MouseUp(object sender, MouseEventArgs e)
        {
            // Восстановление цвета фона кнопки после отпускания
            Math_operations.BackColor = Color.Transparent;
        }
        private void Equality_Click(object sender, EventArgs e)
        {
            FormEquality formEquality = new FormEquality();
            formEquality.Show();
            Math_operations.BackColor = Color.LightGreen;
        }
        private void Equality_MouseUp(object sender, MouseEventArgs e)
        {
            // Восстановление цвета фона кнопки после отпускания
            Equality.BackColor = Color.Transparent;
        }

    }
}
