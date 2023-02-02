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
using Newtonsoft.Json;
using System.IO;

namespace Realestate.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageRealestate.xaml
    /// </summary>
    public partial class PageRealestate : Page
    {
        public PageRealestate()
        {
            InitializeComponent();
            Dtgstate.ItemsSource = CommunalPaymentsEntities.GetContext().RealEstate.ToList();
        }


        private void MenuAddRealestate_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageAddRealestate(null));
        }

        private void MenuSortClear_Click(object sender, RoutedEventArgs e)
        {
            Dtgstate.ItemsSource = CommunalPaymentsEntities.GetContext().RealEstate.ToList();
        }

        private void MenuEditRealestate_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageAddRealestate((RealEstate)Dtgstate.SelectedItem));
        }

        private void MenuDelRealestate_Click(object sender, RoutedEventArgs e)
        {
            var RealeStateForRemoving = Dtgstate.SelectedItems.Cast<RealEstate>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {RealeStateForRemoving.Count()} записи?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CommunalPaymentsEntities.GetContext().RealEstate.RemoveRange(RealeStateForRemoving);
                    CommunalPaymentsEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    Dtgstate.ItemsSource = CommunalPaymentsEntities.GetContext().RealEstate.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }


        private void MenuSortRealestate_Click(object sender, RoutedEventArgs e)
        {
            Dtgstate.ItemsSource = CommunalPaymentsEntities.GetContext().RealEstate.OrderBy(x => x.Address).ToList();
        }

        private void MenuSortRealestate2_Click(object sender, RoutedEventArgs e)
        {
            Dtgstate.ItemsSource = CommunalPaymentsEntities.GetContext().RealEstate.OrderByDescending(x => x.Address).ToList();
        }

        private void MenuSortRealestate4_Click(object sender, RoutedEventArgs e)
        {
            Dtgstate.ItemsSource = CommunalPaymentsEntities.GetContext().RealEstate.Where(x => x.Prescribed >= 3).ToList();
        }

        private void MenuSortRealestate3_Click(object sender, RoutedEventArgs e)
        {
            Dtgstate.ItemsSource = CommunalPaymentsEntities.GetContext().RealEstate.Where(x => x.Prescribed >= 0 && x.Prescribed < 3).ToList();
        }

        private void MenuFilterClear_Click(object sender, RoutedEventArgs e)
        {
            Dtgstate.ItemsSource = CommunalPaymentsEntities.GetContext().RealEstate.ToList();
        }

        private void MenuFilterClear2_Click(object sender, RoutedEventArgs e)
        {
            Dtgstate.ItemsSource = CommunalPaymentsEntities.GetContext().RealEstate.ToList();
        }

        private void MenuFilterRent_Click(object sender, RoutedEventArgs e)
        {
            Dtgstate.ItemsSource = CommunalPaymentsEntities.GetContext().RealEstate.Where(x => x.Rent <= 20000).ToList();
        }

        private void MenuFilterRent2_Click(object sender, RoutedEventArgs e)
        {
            Dtgstate.ItemsSource = CommunalPaymentsEntities.GetContext().RealEstate.Where(x => x.Rent >= 20000 && x.Rent < 30000).ToList();
        }

        private void MenuFilterRent3_Click(object sender, RoutedEventArgs e)
        {
            Dtgstate.ItemsSource = CommunalPaymentsEntities.GetContext().RealEstate.Where(x => x.Rent >= 30000).ToList();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            Classes.ClassFrame.frmObj.Navigate(new PageMain());
        }

        private void BtnJsonSave_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("input.json", string.Empty);
            foreach (var sv in CommunalPaymentsEntities.GetContext().RealEstate)
            {
                RealEstate s = new RealEstate()
                {
                    IDestate = sv.IDestate,
                    Address = sv.Address,
                    Prescribed = sv.Prescribed,
                    HousingArea = sv.HousingArea,
                    Rent = sv.Rent,
                    IDtype = sv.IDestate,
                    CostForMeter = sv.CostForMeter
                };
                File.AppendAllText("input.json", JsonConvert.SerializeObject(s)); //Сохранение в json файл//
            }
        MessageBox.Show("Данные записаны", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void BtnJsonSearch_Click(object sender, RoutedEventArgs e)
        {
            List<RealEstate> ntttt = new List<RealEstate>();
            JsonTextReader reader = new JsonTextReader(new StreamReader("input.json"));
            reader.SupportMultipleContent = true;
            while (reader.Read())
            {
                JsonSerializer serializer = new JsonSerializer();
                RealEstate temp_point = serializer.Deserialize<RealEstate>(reader);
                if (temp_point.Address.Contains(tbSeacrh.Text))
                    ntttt.Add(temp_point);
            }
            string s = "";
            foreach (RealEstate p in ntttt)
            {
                s += p.Address + " - квартплата: " + p.Rent + "\n";
           }
            MessageBox.Show(s);
        }
    }
}
