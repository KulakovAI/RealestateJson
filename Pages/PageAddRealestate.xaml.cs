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
using Realestate.Classes;

namespace Realestate.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddRealestate.xaml
    /// </summary>
    public partial class PageAddRealestate : Page
    {
        private RealEstate _currentRealestate = new RealEstate();
        public PageAddRealestate(RealEstate selectedrealestate)
        {
            InitializeComponent();
            if (selectedrealestate != null)
            {
                _currentRealestate = selectedrealestate;
                TitleRealestate.Text = "Изменение собственника";
                BtnAddRealestate.Content = "Изменить";
            }
            DataContext = _currentRealestate;
        }

        private void BtnCancelRealestate_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageRealestate());
        }

        private void BtnAddRealestate_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentRealestate.Address)) error.AppendLine("Укажите адрес");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentRealestate.Prescribed))) error.AppendLine("Укажите число прописанных");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentRealestate.HousingArea))) error.AppendLine("Укажите площадь ");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentRealestate.Rent))) error.AppendLine("Укажите квартплату ");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentRealestate.IDestate == 0)
            {
                CommunalPaymentsEntities.GetContext().RealEstate.Add(_currentRealestate);
                try
                {
                    CommunalPaymentsEntities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new PageRealestate());
                    MessageBox.Show("Новая недвижимость успешно добавлена!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                try
                {
                    CommunalPaymentsEntities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new PageRealestate());
                    MessageBox.Show("недвижимость успешна изменёна!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
