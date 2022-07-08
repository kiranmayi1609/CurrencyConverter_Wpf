using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Text.RegularExpressions;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindCurrency();
        }
        private void BindCurrency()
        {
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");
            // Add rows in the Datatable with text and value 
            dtCurrency.Rows.Add("--SELECT--", 0);
            dtCurrency.Rows.Add("INR",1);
            dtCurrency.Rows.Add("DKK", 13);
            dtCurrency.Rows.Add("USD", 75);
            dtCurrency.Rows.Add("EUR", 85);
            dtCurrency.Rows.Add("POUND",5); 

            CmbFromCurrency.ItemsSource = dtCurrency.DefaultView;
            CmbFromCurrency.DisplayMemberPath = "Text";
            CmbFromCurrency.SelectedValuePath = "Value";
            CmbFromCurrency.SelectedIndex = 0;   

            CmbToCurrency.ItemsSource = dtCurrency.DefaultView;
            CmbToCurrency.DisplayMemberPath = "Text";
            CmbToCurrency.SelectedValuePath = "Value";
            CmbToCurrency.SelectedIndex = 0;


        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
           
            // Create the variable as ConvertedValue with double datatype to store currency converted value 
            double ConvertedValue;
            //Check if the amount textbox is NULL or blank 
            if( txtCurrency.Text==null || txtCurrency.Text.Trim()=="")
            {
                //If amount textbox is Null or Blank it will show this message BOX 
                MessageBox.Show("Please Enter Currency", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    //After Clicking on messagebox ok set focus on amount textbox 
                    txtCurrency.Focus();
                return;
            }
            //Else if Currency From is not selected or select default text--SELECT--
            else if(CmbFromCurrency.SelectedValue==null || CmbFromCurrency.SelectedIndex==0)
            {
                //Show the message 
                MessageBox.Show("Please Select Currency From", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //Set focus on the From COMBOBOX
                CmbFromCurrency.Focus();
                return;
            }
            //Else if currency to is not selected or select default text--Select --
            else if (CmbToCurrency.SelectedValue==null || CmbToCurrency.SelectedIndex==0)
            {
                //Show the message 
                MessageBox.Show("Please Select Currency TO", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                CmbToCurrency.Focus();
                return;
            }
            //Check if From and TO cOMBOBOX selected values are same 
            if(CmbFromCurrency.Text==CmbToCurrency.Text)
            {
                //Amount textbox value set in ConvertedValue 
                //Double.parse is used for Converting the datatype string To double 
                //Textbox text have string and convertedValue is double Datatype
                ConvertedValue = Double.Parse(txtCurrency.Text);
                //Show the label converted currency and converted currency name and to string  is used 
                lblCurrency.Content = CmbToCurrency.Text + "" + ConvertedValue.ToString("N3");
            }
            else
            {
                //Calculation for currency converter is from Currency VALUE MULTIPLY()
                //With the amount textBOX VALUE AND THEN THAT tOTAL DIVEDERDD WITH  TO Currency value 
                ConvertedValue = (Double.Parse(CmbFromCurrency.SelectedValue.ToString()) * double.Parse(txtCurrency.Text)) / double.Parse(CmbToCurrency.SelectedValue.ToString());

                    //show the label converted currency and converted currency name
                    lblCurrency.Content = CmbToCurrency.Text + "" + ConvertedValue.ToString("N3");

            }





        }



        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void ClearControls()
        {
            txtCurrency.Text = string.Empty;
            if (CmbFromCurrency.Items.Count > 0)
                CmbFromCurrency.SelectedIndex = 0;
            if (CmbToCurrency.Items.Count > 0)
                CmbToCurrency.SelectedIndex = 0;
            lblCurrency.Content = "";
            txtCurrency.Focus();

        }

    }
}
