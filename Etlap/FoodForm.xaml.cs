using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Etlap
{
    /// <summary>
    /// Interaction logic for FoodForm.xaml
    /// </summary>
    public partial class FoodForm : Window
    {
        private FoodService service;
        public FoodForm(FoodService foodService)
        {
            InitializeComponent();
            this.service = foodService;
        }

        private void buttonAddFood_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text.Trim();
            string description = textBoxDescription.Text.Trim();
            string category = "";
            if ((bool)radioButtonAppetizer.IsChecked)
            {
                category = "előétel";
            }
            else if ((bool)radioButtonMain.IsChecked)
            {
                category = "főétel";
            }
            else if ((bool)radioButtonDessert.IsChecked)
            {
                category = "desszert";
            }

            string priceString = textBoxPrice.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Étel nevének megadása kötelező!");
                return;
            }
            if (string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Étel leírásának megadása kötelező!");
                return;
            }
            if (!int.TryParse(priceString, out int price))
            {
                MessageBox.Show("Az ár csak szám lehet!");
                return;
            }
            if (price < 1)
            {
                MessageBox.Show("Az ár csak pozitív szám lehet!");
                return;
            }
            if (!(bool)radioButtonAppetizer.IsChecked && !(bool)radioButtonMain.IsChecked && !(bool)radioButtonDessert.IsChecked)
            {
                MessageBox.Show("Kategória megadása kötelező!");
                return;
            }

            Food newFood = new Food();
            newFood.Name = name;
            newFood.Description = description;
            newFood.Price = price;
            newFood.Category = category;

            if (this.service.Create(newFood))
            {
                MessageBox.Show("Sikeres hozzáadás!");
                textBoxName.Text = "";
                textBoxDescription.Text = "";
                textBoxPrice.Text = "";
                radioButtonAppetizer.IsChecked = false;
                radioButtonMain.IsChecked = false;
                radioButtonDessert.IsChecked = false;
            }
            else
            {
                MessageBox.Show("Hiba történt a Hozzáadás során!");
            }
        }
    }
}
