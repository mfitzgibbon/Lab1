using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InvoiceTotal
{
    // This is the starting point for exercise 8-1 from
    // "Murach's C# 2010" by Joel Murach
    // (c) 2010 by Mike Murach & Associates, Inc. 
    // www.murach.com

    public partial class frmInvoiceTotal : Form
	{
		public frmInvoiceTotal()
		{
			InitializeComponent();
		}

        // Declare class variables for array and list here
        List<string> stringList = new List<string>();
        string[,] stringArray = new string[3, 4];

        private void btnCalculate_Click(object sender, EventArgs e)
		{
            try
            {

                if (txtSubtotal.Text == "")
                {
                    MessageBox.Show(
                        "Subtotal is a required field.", "Entry Error");
                }
                else
                {
                    decimal subtotal = Decimal.Parse(txtSubtotal.Text);
                    if (subtotal > 0 && subtotal < 10000)
                    {
                        decimal discountPercent = 0m;
                        if (subtotal >= 500)
                            discountPercent = .2m;
                        else if (subtotal >= 250 && subtotal < 500)
                            discountPercent = .15m;
                        else if (subtotal >= 100 && subtotal < 250)
                            discountPercent = .1m;
                        decimal discountAmount = subtotal * discountPercent;
                        decimal invoiceTotal = subtotal - discountAmount;

                        discountAmount = Math.Round(discountAmount, 2);
                        invoiceTotal = Math.Round(invoiceTotal, 2);

                        txtDiscountPercent.Text = discountPercent.ToString("p1");
                        txtDiscountAmount.Text = discountAmount.ToString();
                        txtTotal.Text = invoiceTotal.ToString();

                        // Add invoice total to the array here
                        stringList.Add(subtotal.ToString());
                        stringArray[stringList.IndexOf(subtotal.ToString()), 0] = subtotal.ToString("c");
                        stringArray[stringList.IndexOf(subtotal.ToString()), 1] = discountPercent.ToString("n2");
                        stringArray[stringList.IndexOf(subtotal.ToString()), 2] = discountAmount.ToString("c");
                        stringArray[stringList.IndexOf(subtotal.ToString()), 3] = invoiceTotal.ToString("c");
                    }
                    else
                    {
                        MessageBox.Show(
                            "Subtotal must be greater than 0 and less than 10,000.",
                            "Entry Error");
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(
                    "Please enter a valid number for the Subtotal field.",
                    "Entry Error");
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show(
                    "You can only enter 3 values",
                    "Entry Error");
            }

            txtSubtotal.Focus();
        }

		private void btnExit_Click(object sender, EventArgs e)
		{
            // TODO: add code that displays dialog boxes here
            string message = "";
            int count = 0;

            foreach (string str in stringArray)
                if (str != null)
                {
                    message = message + str + "\n";
                    count++;
                    if(count == 4)
                    {
                        count = 0;
                        message = message + "\n";
                    }
                }
            MessageBox.Show(message);

            this.Close();
		}

	}
}