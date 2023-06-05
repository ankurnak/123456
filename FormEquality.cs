using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ArrayKursova.FormMath_operations;

namespace ArrayKursova
{
    public partial class FormEquality : Form
    {
        private List<Fraction> fractionArray;
        public FormEquality()
        {
            InitializeComponent();
            fractionArray = new List<Fraction>();
        }
        public class Fraction : IComparable<Fraction>
        {
            public int Numerator { get; set; }
            public int Denominator { get; set; }

            public Fraction(int numerator, int denominator)
            {
                Numerator = numerator;
                Denominator = denominator;
            }

            public int CompareTo(Fraction other)
            {
                double value1 = (double)Numerator / Denominator;
                double value2 = (double)other.Numerator / other.Denominator;
                return value1.CompareTo(value2);
            }

            public void Add(Fraction other)
            {
                Numerator = Numerator * other.Denominator + other.Numerator * Denominator;
                Denominator *= other.Denominator;
                Simplify();
            }

            public void Subtract(Fraction other)
            {
                Numerator = Numerator * other.Denominator - other.Numerator * Denominator;
                Denominator *= other.Denominator;
                Simplify();
            }

            public void Multiply(Fraction other)
            {
                Numerator *= other.Numerator;
                Denominator *= other.Denominator;
                Simplify();
            }

            public void Divide(Fraction other)
            {
                if (other.Numerator == 0)
                    throw new DivideByZeroException();

                Numerator *= other.Denominator;
                Denominator *= other.Numerator;
                Simplify();
            }

            public void Simplify()
            {
                int gcd = GCD(Numerator, Denominator);
                Numerator /= gcd;
                Denominator /= gcd;

                if (Denominator < 0)
                {
                    Numerator = -Numerator;
                    Denominator = -Denominator;
                }
            }

            public void Exponentiate(int exponent)
            {
                Numerator = (int)Math.Pow(Numerator, exponent);
                Denominator = (int)Math.Pow(Denominator, exponent);
                Simplify();
            }

            public override string ToString()
            {
                return $"{Numerator}/{Denominator}";
            }

            public override bool Equals(object obj)
            {
                if (obj is Fraction other)
                {
                    Simplify();
                    other.Simplify();
                    return Numerator == other.Numerator && Denominator == other.Denominator;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return Numerator.GetHashCode() ^ Denominator.GetHashCode();
            }

            private int GCD(int a, int b)
            {
                while (b != 0)
                {
                    int temp = b;
                    b = a % b;
                    a = temp;
                }
                return a;
            }

            public static bool operator ==(Fraction fraction1, Fraction fraction2)
            {
                return fraction1.Equals(fraction2);
            }

            public static bool operator !=(Fraction fraction1, Fraction fraction2)
            {
                return !fraction1.Equals(fraction2);
            }

            public static bool operator >(Fraction fraction1, Fraction fraction2)
            {
                double value1 = (double)fraction1.Numerator / fraction1.Denominator;
                double value2 = (double)fraction2.Numerator / fraction2.Denominator;
                return value1 > value2;
            }

            public static bool operator <(Fraction fraction1, Fraction fraction2)
            {
                double value1 = (double)fraction1.Numerator / fraction1.Denominator;
                double value2 = (double)fraction2.Numerator / fraction2.Denominator;
                return value1 < value2;
            }

            public static bool operator >=(Fraction fraction1, Fraction fraction2)
            {
                double value1 = (double)fraction1.Numerator / fraction1.Denominator;
                double value2 = (double)fraction2.Numerator / fraction2.Denominator;
                return value1 >= value2;
            }

            public static bool operator <=(Fraction fraction1, Fraction fraction2)
            {
                double value1 = (double)fraction1.Numerator / fraction1.Denominator;
                double value2 = (double)fraction2.Numerator / fraction2.Denominator;
                return value1 <= value2;
            }
        }
        private void equalButton_Click(object sender, EventArgs e)
        {
            if (fractionListBox.SelectedIndex != -1)
            {
                try
                {
                    int numerator = int.Parse(relationNumeratorTextBox.Text);
                    int denominator = int.Parse(relationDenominatorTextBox.Text);
                    Fraction fraction = fractionArray[fractionListBox.SelectedIndex];
                    bool isEqual = fraction.Equals(new Fraction(numerator, denominator));

                    relationResultLabel.Text = isEqual.ToString();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input. Please enter integers for numerator and denominator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a fraction from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void botEqualButton_Click(object sender, EventArgs e)
        {
            if (fractionListBox.SelectedIndex != -1)
            {
                try
                {
                    int numerator = int.Parse(relationNumeratorTextBox.Text);
                    int denominator = int.Parse(relationDenominatorTextBox.Text);
                    Fraction fraction = fractionArray[fractionListBox.SelectedIndex];
                    bool isNotEqual = !fraction.Equals(new Fraction(numerator, denominator));

                    relationResultLabel.Text = isNotEqual.ToString();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input. Please enter integers for numerator and denominator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a fraction from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void greaterOrEqualButton_Click(object sender, EventArgs e)
        {
            if (fractionListBox.SelectedIndex != -1)
            {
                try
                {
                    int numerator = int.Parse(relationNumeratorTextBox.Text);
                    int denominator = int.Parse(relationDenominatorTextBox.Text);
                    Fraction fraction = fractionArray[fractionListBox.SelectedIndex];
                    bool isGreaterOrEqual = fraction >= new Fraction(numerator, denominator);

                    relationResultLabel.Text = isGreaterOrEqual.ToString();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input. Please enter integers for numerator and denominator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a fraction from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lesserOrEqualButton_Click(object sender, EventArgs e)
        {
            if (fractionListBox.SelectedIndex != -1)
            {
                try
                {
                    int numerator = int.Parse(relationNumeratorTextBox.Text);
                    int denominator = int.Parse(relationDenominatorTextBox.Text);
                    Fraction fraction = fractionArray[fractionListBox.SelectedIndex];
                    bool isLesserOrEqual = fraction <= new Fraction(numerator, denominator);

                    relationResultLabel.Text = isLesserOrEqual.ToString();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input. Please enter integers for numerator and denominator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a fraction from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void greaterButton_Click(object sender, EventArgs e)
        {
            if (fractionListBox.SelectedIndex != -1)
            {
                try
                {
                    int numerator = int.Parse(relationNumeratorTextBox.Text);
                    int denominator = int.Parse(relationDenominatorTextBox.Text);
                    Fraction fraction = fractionArray[fractionListBox.SelectedIndex];
                    bool isGreater = fraction > new Fraction(numerator, denominator);

                    relationResultLabel.Text = isGreater.ToString();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input. Please enter integers for numerator and denominator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a fraction from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lesserButton_Click(object sender, EventArgs e)
        {
            if (fractionListBox.SelectedIndex != -1)
            {
                try
                {
                    int numerator = int.Parse(relationNumeratorTextBox.Text);
                    int denominator = int.Parse(relationDenominatorTextBox.Text);
                    Fraction fraction = fractionArray[fractionListBox.SelectedIndex];
                    bool isLesser = fraction < new Fraction(numerator, denominator);

                    relationResultLabel.Text = isLesser.ToString();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input. Please enter integers for numerator and denominator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a fraction from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateFractionListBox()
        {
            fractionListBox.Items.Clear();
            foreach (Fraction fraction in fractionArray)
            {
                fractionListBox.Items.Add(fraction);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                int numerator = int.Parse(numeratorTextBox.Text);
                int denominator = int.Parse(denominatorTextBox.Text);
                fractionArray.Add(new Fraction(numerator, denominator));

                numeratorTextBox.Clear();
                denominatorTextBox.Clear();

                UpdateFractionListBox();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid input. Please enter integers for numerator and denominator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            fractionArray.Sort();
            UpdateFractionListBox();
        }
        private void FormMath_operations_Load(object sender, EventArgs e)
        {
            // Set the design for the text fields
            numeratorTextBox.BorderStyle = BorderStyle.FixedSingle;
            denominatorTextBox.BorderStyle = BorderStyle.FixedSingle;
            relationNumeratorTextBox.BorderStyle = BorderStyle.FixedSingle;
            relationDenominatorTextBox.BorderStyle = BorderStyle.FixedSingle;
            relationResultLabel.BorderStyle = BorderStyle.FixedSingle;
            
            // Set the design for the list box
            fractionListBox.BorderStyle = BorderStyle.FixedSingle;

            // Set the design for the buttons
            addButton.FlatStyle = FlatStyle.Flat;
            sortButton.FlatStyle = FlatStyle.Flat;
            equalButton.FlatStyle = FlatStyle.Flat;
            botEqualButton.FlatStyle = FlatStyle.Flat;
            greaterOrEqualButton.FlatStyle = FlatStyle.Flat;
            lesserOrEqualButton.FlatStyle = FlatStyle.Flat;
            greaterButton.FlatStyle = FlatStyle.Flat;
            lesserButton.FlatStyle = FlatStyle.Flat;
            cancel.FlatStyle = FlatStyle.Flat;

            // Set the design for the button text color
            addButton.ForeColor = Color.White;
            sortButton.ForeColor = Color.White;
            equalButton.ForeColor = Color.White;
            botEqualButton.ForeColor = Color.White;
            greaterOrEqualButton.ForeColor = Color.White;
            lesserOrEqualButton.ForeColor = Color.White;
            greaterButton.ForeColor = Color.White;
            lesserButton.ForeColor = Color.White;
            cancel.ForeColor = Color.White;

            // Set the design for the button background color
            addButton.BackColor = Color.DodgerBlue;
            sortButton.BackColor = Color.DodgerBlue;
            equalButton.BackColor = Color.DodgerBlue;
            botEqualButton.BackColor = Color.DodgerBlue;
            greaterOrEqualButton.BackColor = Color.DodgerBlue;
            lesserOrEqualButton.BackColor = Color.DodgerBlue;
            greaterButton.BackColor = Color.DodgerBlue;
            lesserButton.BackColor = Color.DodgerBlue;
            cancel.BackColor = Color.DodgerBlue;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
