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
using System.Windows.Shapes;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;
using Word = Microsoft.Office.Interop.Word;

namespace avto
{
    /// <summary>
    /// Логика взаимодействия для Remont.xaml
    /// </summary>
    public partial class Remont : Window
    {
        public Remont()
        {
            InitializeComponent();

        }


        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAvtomobil();
        }

        private void UpdateAvtomobil()
        {
            Podkl avto = new Podkl("SELECT * FROM dbo.Automobile INNER JOIN dbo.Komplektacia  ON dbo.Automobile.Komplektacia_ID = dbo.Komplektacia.id_komplektacia "
               + "INNER JOIN dbo.Kategoria_automobilya  ON dbo.Automobile.Kategotia_ID = dbo.Kategoria_automobilya.id_kategoria");
            Podkl kategoriya = new Podkl("SELECT * FROM dbo.Kategoria_automobilya ");
            Podkl komplektaciya = new Podkl("SELECT * FROM dbo.Komplektacia");

            comboBox1tip.SelectedValuePath = "id_kategoria";
            comboBox1tip.DisplayMemberPath = "Tip_avto";
            comboBox1tip.ItemsSource = kategoriya.Table.DefaultView;

            comboBoxkorl.SelectedValuePath = "id_komplektacia";
            comboBoxkorl.DisplayMemberPath = "Tip_Komplektacii";
            comboBoxkorl.ItemsSource = komplektaciya.Table.DefaultView;
            dataGrid2.ItemsSource = avto.Table.DefaultView;

        }


        private void UpdateKlient()
        {
            Podkl Klient = new Podkl("SELECT         "
               + "* FROM dbo.Klient INNER JOIN dbo.Automobile  ON dbo.Klient.Auto_ID = dbo.Automobile.id_auto "
               + "INNER JOIN dbo.Pasport  ON dbo.Klient.Pasport_ID = dbo.Pasport.id_pasport");
            dataGrid1.ItemsSource = Klient.Table.DefaultView;
            Podkl gosnomer = new Podkl("SELECT * FROM dbo.Automobile ");

            klientgosnomerav.SelectedValuePath = "id_auto";
            klientgosnomerav.DisplayMemberPath = "Gos_nomer";
            klientgosnomerav.ItemsSource = gosnomer.Table.DefaultView;

        }

        private void UpdateZay()
        {
            Podkl Zay = new Podkl("SELECT * FROM dbo.Zyavka INNER JOIN dbo.Zapchast  ON dbo.Zyavka.Zapch_ID = dbo.Zapchast.ID_Zapch ");
            dataGridzay.ItemsSource = Zay.Table.DefaultView;
            Podkl detl = new Podkl("SELECT * FROM dbo.Zapchast ");

            tbZaynam.SelectedValuePath = "ID_Zapch";
            tbZaynam.DisplayMemberPath = "Naimenovanie";
            tbZaynam.ItemsSource = detl.Table.DefaultView;
            
        }

        private void UpdateSklad()
        {

            Podkl sklad = new Podkl("SELECT * FROM dbo.Zapchast INNER JOIN dbo.Sklad  ON dbo.Zapchast.Sklad_ID = dbo.Sklad.ID_Sklad "
            + "INNER JOIN dbo.Postavhic  ON dbo.Zapchast.PS_ID = dbo.Postavhic.ID_PS");
            dataGrid.ItemsSource = sklad.Table.DefaultView;

        }



        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            UpdateKlient();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //switch (MessageBox.Show("Завершить работу приложения?", "Выход", MessageBoxButton.YesNo))
            //{
            //    case MessageBoxResult.Yes:

            //        MainWindow sda = new MainWindow();
            //        sda.Visibility = Visibility.Visible;
            //        UpdateLayout();
            //        Remont rem = new Remont();
            //        rem.Close();
            //        //Application.Current.Shutdown();
            //        break;
            //    case MessageBoxResult.No:

            //        break;
            //}
        }

        private void TabItem_Loaded_2(object sender, RoutedEventArgs e)
        {
            UpdateSklad();
            UpdateZay();
        }

        

        private void btndelzay_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridzay.SelectedItems.Count == 0) return;
            var IDPR = ((DataRowView)dataGridzay.SelectedItems[0]).Row["ID_Zyavka"].ToString();
            MessageBox.Show("Удалить:" + IDPR);
            switch (MessageBox.Show("Вы действительно хотите удалить данные?", "Удаление", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:

                    ConnectionClass ConCheck = new ConnectionClass();
                    RegistryKey DataBase_Connection = Registry.CurrentConfig;
                    RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                    ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                    SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                    connection.Open();
                    SqlCommand command = new SqlCommand("dbo.dell_Zyavka", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID_Zyavka", IDPR);
                    command.ExecuteNonQuery();
                    UpdateZay();
                    connection.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        private void btnaddzay_Click(object sender, RoutedEventArgs e)
        {
            switch (DataZay.Text == "" || tbZaykolvo.Text == "" || tbZaynam.Text == "")
            {
                case (false):

                    ConnectionClass ConCheck = new ConnectionClass();
                    RegistryKey DataBase_Connection = Registry.CurrentConfig;
                    RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                    ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                    SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                    connection.Open();

                    SqlCommand command = new SqlCommand("dbo.add_Zyavka", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Data_zayavki", DataZay.Text);
                    command.Parameters.AddWithValue("@Kol_stvo", tbZaykolvo.Text);
                    command.Parameters.AddWithValue("@Zapch_ID", tbZaynam.SelectedValue.ToString());

                        command.ExecuteNonQuery();
                        UpdateZay();
                        connection.Close();
                    break;

                case (true):
                    MessageBox.Show("Заполните все пустые поля");
                    break;
            }
        }

        private void avtoadd_Click(object sender, RoutedEventArgs e)
        {
            switch (avtogos.Text == "" || avtomarka.Text == "" || avtomodel.Text == "" || avtodata.Text == "")
            {
                case (false):
                    try
                    {
                        ConnectionClass ConCheck = new ConnectionClass();
                        RegistryKey DataBase_Connection = Registry.CurrentConfig;
                        RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                        ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                           Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                           Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                           Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                        SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                        connection.Open();
                        SqlCommand command = new SqlCommand("dbo.add_Automobile", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Gos_nomer", avtogos.Text);
                        command.Parameters.AddWithValue("@Data_vypuska", avtodata.Text);
                        command.Parameters.AddWithValue("@Marka", avtomarka.Text);
                        command.Parameters.AddWithValue("@Model", avtomodel.Text);
                        command.Parameters.AddWithValue("@Kategotia_ID", comboBox1tip.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Komplektacia_ID", comboBoxkorl.SelectedValue.ToString());

                        command.ExecuteNonQuery();
                        UpdateAvtomobil();

                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    break;

                case (true):

                    MessageBox.Show("Заполните все пустые поля");
                    break;
            }
        }




        public string USID;
        public string AVID;

        private void klientadd_Click(object sender, RoutedEventArgs e)
        {

            switch (klientseriya.Text == "" || klientnomer.Text == "" || klientim.Text == "" || klientfam.Text == "" || klientoth.Text == "")
            {

                case (false):

                    ConnectionClass ConCheck = new ConnectionClass();
                    RegistryKey DataBase_Connection = Registry.CurrentConfig;
                    RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                    ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                    SqlConnection connection = new SqlConnection(ConCheck.ConnectString);

                    connection.Open();
                    
                    try
                    {
                        SqlCommand Pasport = new SqlCommand("add_Pasport", connection);
                        Pasport.CommandType = CommandType.StoredProcedure;
                        Pasport.Parameters.AddWithValue("@Seria", klientseriya.Text);
                        Pasport.Parameters.AddWithValue("@Nomer", klientnomer.Text);
                        Pasport.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка 2");
                    }
                    
                        SqlCommand Select_USID = new SqlCommand("select Pasport.id_pasport from Pasport where Pasport.Seria ='"
                      + klientseriya.Text + "'", connection);
                        USID = Select_USID.ExecuteScalar().ToString();
                      
                    try
                    {
                        SqlCommand command = new SqlCommand("dbo.add_Klient", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Fam_kl", klientfam.Text);
                        command.Parameters.AddWithValue("@Im_kl", klientim.Text);
                        command.Parameters.AddWithValue("@Otch_kl", klientoth.Text);
                        command.Parameters.AddWithValue("@Auto_ID", klientgosnomerav.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Pasport_ID", USID);

                        command.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("ошибка 4");
                    }

                    UpdateKlient();

                    connection.Close();
                    break;



                case (true):

                    MessageBox.Show("Заполните все пустые поля");
                    break;
            }
        }




        private void btnupdzay_Click(object sender, RoutedEventArgs e)
        {

            switch (DataZay.Text == "" || tbZaykolvo.Text == "" || tbZaynam.Text == "")
            {
                case (true):

                    if (dataGridzay.SelectedItems.Count == 0) return;
                    var data = ((DataRowView)dataGridzay.SelectedItems[0]).Row["Data_zayavki"].ToString();
                    var kolvo = ((DataRowView)dataGridzay.SelectedItems[0]).Row["Kol_stvo"].ToString();
                    var IDZay = ((DataRowView)dataGridzay.SelectedItems[0]).Row["ID_Zyavka"].ToString();
                    DataZay.Text = data;
                    tbZaykolvo.Text = kolvo;

                    dataGridzay.IsEnabled = false;
                    btnaddzay.IsEnabled = false;
                    //string ID = IDZay;


                    break;

                case (false):

                    var IDZay1 = ((DataRowView)dataGridzay.SelectedItems[0]).Row["ID_Zyavka"].ToString();

                    MessageBoxResult result = MessageBox.Show("Изменить " + IDZay1 + " значение ?", "Изменить", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.No) return;
                    {



                        try
                        {
                            ConnectionClass ConCheck = new ConnectionClass();
                            RegistryKey DataBase_Connection = Registry.CurrentConfig;
                            RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                            ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                               Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                               Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                               Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                            SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                            connection.Open();
                            SqlCommand command = new SqlCommand("dbo.upd_Zyavka", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Data_zayavki", DataZay.Text);
                            command.Parameters.AddWithValue("@Kol_stvo", tbZaykolvo.Text);
                            command.Parameters.AddWithValue("@Zapch_ID", tbZaynam.SelectedValue.ToString());
                            command.Parameters.AddWithValue("@ID_Zyavka", IDZay1);
                            command.ExecuteNonQuery();
                            UpdateZay();
                            dataGridzay.IsEnabled = true;
                            btnaddzay.IsEnabled = true;
                            connection.Close();
                            tbZaykolvo.Clear();
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("Введите коректный номер детали");


                        }


                        break;
                    }
            }

        }

      

        

        private void DataZay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
        }





        private void button_Click(object sender, RoutedEventArgs e)
        {
            //otdelkadrow1 = new OtdelKadrov();

            this.Close();
            MainWindow sda = new MainWindow();
            sda.Visibility = Visibility.Visible;
            //UpdateLayout();

        }

        private void klientseriya_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.Text, 0);
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void avtoupd_Click(object sender, RoutedEventArgs e)
        {
            switch (  avtogos.Text == "" || avtomarka.Text == ""||avtomodel.Text == "")
            {
                case (true):

                    if (dataGrid2.SelectedItems.Count == 0) return;
                    var gos = ((DataRowView)dataGrid2.SelectedItems[0]).Row["Gos_nomer"].ToString();
                    var data = ((DataRowView)dataGrid2.SelectedItems[0]).Row["Data_vypuska"].ToString();
                    var marka = ((DataRowView)dataGrid2.SelectedItems[0]).Row["Marka"].ToString();
                    var model = ((DataRowView)dataGrid2.SelectedItems[0]).Row["Model"].ToString();
                    avtodata.Text = data;
                    avtogos.Text = gos;
                    avtomarka.Text = marka;
                    avtomodel.Text = model;
                    dataGrid2.IsEnabled = false;
                    avtoadd.IsEnabled = false;
                    break;

                case (false):

                    var IDavto = ((DataRowView)dataGrid2.SelectedItems[0]).Row["id_auto"].ToString();
                   
                    MessageBoxResult result = MessageBox.Show("Изменить " + IDavto + " значение ?", "Изменить", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.No) return;
                    {
                        try
                        {
                            
                             ConnectionClass ConCheck = new ConnectionClass();
                            RegistryKey DataBase_Connection = Registry.CurrentConfig;
                            RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                            ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                               Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                               Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                               Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                            SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                            connection.Open();
                            SqlCommand command = new SqlCommand("dbo.upd_Automobile", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Gos_nomer", avtogos.Text);
                            command.Parameters.AddWithValue("@Data_vypuska", avtodata.Text);
                                command.Parameters.AddWithValue("@Marka", avtomarka.Text);
                            command.Parameters.AddWithValue("@Model", avtomodel.Text);
                            command.Parameters.AddWithValue("@Kategotia_ID", comboBox1tip.SelectedValue.ToString());
                            command.Parameters.AddWithValue("@Komplektacia_ID", comboBoxkorl.SelectedValue.ToString());
                            command.Parameters.AddWithValue("@id_auto", IDavto);
                            command.ExecuteNonQuery();

                            connection.Close();
                            dataGrid2.IsEnabled = true;
                            avtoadd.IsEnabled = true;
                            UpdateAvtomobil();
                            
                            avtogos.Clear();
                            avtomarka.Clear();
                            avtomodel.Clear();
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("Введите коректный номер детали");
                        }
                        break;
                    }
            }
        }

        private void klientupd_Click(object sender, RoutedEventArgs e)
        {
            switch (klientfam.Text == "" || klientim.Text == "" || klientoth.Text == "" || klientseriya.Text == "" || klientnomer.Text == "")
            {
                case (true):

                    if (dataGrid1.SelectedItems.Count == 0) return;
                    var fam = ((DataRowView)dataGrid1.SelectedItems[0]).Row["Fam_kl"].ToString();
                    var im = ((DataRowView)dataGrid1.SelectedItems[0]).Row["Im_kl"].ToString();
                    var oth = ((DataRowView)dataGrid1.SelectedItems[0]).Row["Otch_kl"].ToString();
                    var seriya = ((DataRowView)dataGrid1.SelectedItems[0]).Row["Seria"].ToString();
                    var nomer = ((DataRowView)dataGrid1.SelectedItems[0]).Row["Nomer"].ToString();
                    klientfam.Text = fam;
                    klientim.Text = im;
                    klientoth.Text = oth;
                    klientseriya.Text = seriya;
                    klientnomer.Text = nomer;
                    dataGrid1.IsEnabled = false;
                    klientadd.IsEnabled = false;
                    break;

                case (false):

                    var IDklient = ((DataRowView)dataGrid1.SelectedItems[0]).Row["ID_Klient"].ToString();

                    MessageBoxResult result = MessageBox.Show("Изменить " + IDklient + " значение ?", "Изменить", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.No) return;
                    {
                        try
                        {

                            ConnectionClass ConCheck = new ConnectionClass();
                            RegistryKey DataBase_Connection = Registry.CurrentConfig;
                            RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                            ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                               Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                               Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                               Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                            SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                            connection.Open();
                            SqlCommand IDPas = new SqlCommand("select Pasport.id_pasport from Pasport where Pasport.Seria ='"
                             + klientseriya.Text + "'", connection);
                            var IDPasprt = IDPas.ExecuteScalar().ToString();

                            SqlCommand command1 = new SqlCommand("dbo.upd_Pasport", connection);
                            command1.CommandType = CommandType.StoredProcedure;
                            command1.Parameters.AddWithValue("@Nomer", klientnomer.Text);
                            command1.Parameters.AddWithValue("@Seria", klientseriya.Text);
                            command1.Parameters.AddWithValue("@id_Pasport", IDPasprt);
                            command1.ExecuteNonQuery();

                            
                            SqlCommand command = new SqlCommand("dbo.upd_Klient", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Fam_kl", klientfam.Text);
                            command.Parameters.AddWithValue("@Im_kl", klientim.Text);
                            command.Parameters.AddWithValue("@Otch_kl", klientoth.Text);
                            command.Parameters.AddWithValue("@Auto_ID", klientgosnomerav.SelectedValue.ToString());
                            command.Parameters.AddWithValue("@id_klient", IDklient);
                            command.ExecuteNonQuery();

                            connection.Close();
                            dataGrid1.IsEnabled = true;
                            klientadd.IsEnabled = true;
                            UpdateKlient();
                         
                            klientfam.Clear();
                            klientim.Clear();
                            klientoth.Clear();
                            klientseriya.Clear();
                            klientnomer.Clear();
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("Введите коректный номер детали");
                        }
                        break;
                    }
            }
        }
    }    
}
