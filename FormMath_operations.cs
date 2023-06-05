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
    public partial class FormMath_operations : Form
    {
        private List<Fraction> fractionArray;
        public FormMath_operations()
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

        private void additionButton_Click(object sender, EventArgs e)
        {
            if (fractionListBox.SelectedIndex != -1)
            {
                try
                {
                    int numerator = int.Parse(additionNumeratorTextBox.Text);
                    int denominator = int.Parse(additionDenominatorTextBox.Text);
                    Fraction fraction = fractionArray[fractionListBox.SelectedIndex];
                    fraction.Add(new Fraction(numerator, denominator));

                    additionNumeratorTextBox.Clear();
                    additionDenominatorTextBox.Clear();

                    UpdateFractionListBox();
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

        private void substractionButton_Click(object sender, EventArgs e)
        {
            if (fractionListBox.SelectedIndex != -1)
            {
                try
                {
                    int numerator = int.Parse(subtractionNumeratorTextBox.Text);
                    int denominator = int.Parse(subtractionDenominatorTextBox.Text);
                    Fraction fraction = fractionArray[fractionListBox.SelectedIndex];
                    fraction.Subtract(new Fraction(numerator, denominator));

                    subtractionNumeratorTextBox.Clear();
                    subtractionDenominatorTextBox.Clear();

                    UpdateFractionListBox();
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

        private void multiplicationButton_Click(object sender, EventArgs e)
        {
            if (fractionListBox.SelectedIndex != -1)
            {
                try
                {
                    int numerator = int.Parse(multiplicationNumeratorTextBox.Text);
                    int denominator = int.Parse(multiplicationDenominatorTextBox.Text);
                    Fraction fraction = fractionArray[fractionListBox.SelectedIndex];
                    fraction.Multiply(new Fraction(numerator, denominator));

                    multiplicationNumeratorTextBox.Clear();
                    multiplicationDenominatorTextBox.Clear();

                    UpdateFractionListBox();
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

        private void divisionButton_Click(object sender, EventArgs e)
        {
            if (fractionListBox.SelectedIndex != -1)
            {
                try
                {
                    int numerator = int.Parse(divisionNumeratorTextBox.Text);
                    int denominator = int.Parse(divisionDenominatorTextBox.Text);
                    Fraction fraction = fractionArray[fractionListBox.SelectedIndex];
                    fraction.Divide(new Fraction(numerator, denominator));

                    divisionNumeratorTextBox.Clear();
                    divisionDenominatorTextBox.Clear();

                    UpdateFractionListBox();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input. Please enter integers for numerator and denominator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (DivideByZeroException)
                {
                    MessageBox.Show("Division by zero is not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a fraction from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simplifyButton_Click(object sender, EventArgs e)
        {
            if (fractionListBox.SelectedIndex != -1)
            {
                Fraction fraction = fractionArray[fractionListBox.SelectedIndex];
                fraction.Simplify();

                UpdateFractionListBox();
            }
            else
            {
                MessageBox.Show("Please select a fraction from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exponentiationButton_Click(object sender, EventArgs e)
        {
            if (fractionListBox.SelectedIndex != -1)
            {
                try
                {
                    int exponent = int.Parse(exponentTextBox.Text);
                    Fraction fraction = fractionArray[fractionListBox.SelectedIndex];
                    fraction.Exponentiate(exponent);

                    exponentTextBox.Clear();

                    UpdateFractionListBox();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid input. Please enter an integer for the exponent.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void FormMath_operations_Load(object sender, EventArgs e)
        {
            // Set the design for the text fields
            numeratorTextBox.BorderStyle = BorderStyle.FixedSingle;
            denominatorTextBox.BorderStyle = BorderStyle.FixedSingle;
            additionNumeratorTextBox.BorderStyle = BorderStyle.FixedSingle;
            additionDenominatorTextBox.BorderStyle = BorderStyle.FixedSingle;
            subtractionNumeratorTextBox.BorderStyle = BorderStyle.FixedSingle;
            subtractionDenominatorTextBox.BorderStyle = BorderStyle.FixedSingle;
            multiplicationNumeratorTextBox.BorderStyle = BorderStyle.FixedSingle;
            multiplicationDenominatorTextBox.BorderStyle = BorderStyle.FixedSingle;
            divisionNumeratorTextBox.BorderStyle = BorderStyle.FixedSingle;
            divisionDenominatorTextBox.BorderStyle = BorderStyle.FixedSingle;
            exponentTextBox.BorderStyle = BorderStyle.FixedSingle;
            // Set the design for the list box
            fractionListBox.BorderStyle = BorderStyle.FixedSingle;

            // Set the design for the buttons
            addButton.FlatStyle = FlatStyle.Flat;
            sortButton.FlatStyle = FlatStyle.Flat;
            additionButton.FlatStyle = FlatStyle.Flat;
            substractionButton.FlatStyle = FlatStyle.Flat;
            multiplicationButton.FlatStyle = FlatStyle.Flat;
            divisionButton.FlatStyle = FlatStyle.Flat;
            simplifyButton.FlatStyle = FlatStyle.Flat;
            exponentiationButton.FlatStyle = FlatStyle.Flat;
            cancel.FlatStyle = FlatStyle.Flat;

            // Set the design for the button text color
            addButton.ForeColor = Color.White;
            sortButton.ForeColor = Color.White;
            additionButton.ForeColor = Color.White;
            substractionButton.ForeColor = Color.White;
            multiplicationButton.ForeColor = Color.White;
            divisionButton.ForeColor = Color.White;
            simplifyButton.ForeColor = Color.White;
            exponentiationButton.ForeColor = Color.White;
            cancel.ForeColor = Color.White;

            // Set the design for the button background color
            addButton.BackColor = Color.DodgerBlue;
            sortButton.BackColor = Color.DodgerBlue;
            additionButton.BackColor = Color.DodgerBlue;
            substractionButton.BackColor = Color.DodgerBlue;
            multiplicationButton.BackColor = Color.DodgerBlue;
            divisionButton.BackColor = Color.DodgerBlue;
            simplifyButton.BackColor = Color.DodgerBlue;
            exponentiationButton.BackColor = Color.DodgerBlue;
            cancel.BackColor = Color.DodgerBlue;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
    }
