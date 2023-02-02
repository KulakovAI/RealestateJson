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
    /// Логика взаимодействия для PageAddCitezen.xaml
    /// </summary>
    public partial class PageAddCitezen : Page
    {
        private Citizen _currentCitizen = new Citizen();
        public PageAddCitezen(Citizen selectedCitizen)
        {
            InitializeComponent();
            if (selectedCitizen != null)
            {
                _currentCitizen = selectedCitizen;
                TitleCitizen.Text = "Изменение собственника";
                BtnAddCitizen.Content = "Изменить";
            }
            DataContext = _currentCitizen;
        }

        private void BtnCancelCitizen_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageCitizen());
        }

        private void BtnAddCitizen_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentCitizen.FIO)) error.AppendLine("Укажите ФИО");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentCitizen.Passport))) error.AppendLine("Укажите паспорт");
            if (string.IsNullOrWhiteSpace(Convert.ToString(_currentCitizen.Telephone))) error.AppendLine("Укажите телефон ");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_currentCitizen.IDcitizen == 0)
            {
                CommunalPaymentsEntities.GetContext().Citizen.Add(_currentCitizen);
                try
                {
                    CommunalPaymentsEntities.GetContext().SaveChanges();
                    Classes.ClassFrame.frmObj.Navigate(new PageCitizen());
                    MessageBox.Show("Новый собственник успешно добавлен!");
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
                    Classes.ClassFrame.frmObj.Navigate(new PageCitizen());
                    MessageBox.Show("собственник успешно изменён!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
    }
}
