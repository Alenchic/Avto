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
  
        DataTable avtomobil;
        DataTable Klient;
        DataTable Sklad;
        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAvtomobil();
        }

        private void UpdateAvtomobil()
        {
      
            string sql = "SELECT         "
               + "* FROM dbo.Automobile INNER JOIN dbo.Komplektacia  ON dbo.Automobile.Komplektacia_ID = dbo.Komplektacia.id_komplektacia "
               + "INNER JOIN dbo.Kategoria_automobilya  ON dbo.Automobile.Kategotia_ID = dbo.Kategoria_automobilya.id_kategoria";
         
            string sql1 = "SELECT * FROM dbo.Kategoria_automobilya ";
            string sql2 = "SELECT * FROM dbo.Komplektacia ";
            avtomobil = new DataTable();
            DataTable tip_avto = new DataTable();
            DataTable komplect = new DataTable();
            SqlConnection connection = null;
            try
            {
               
                ConnectionClass ConCheck = new ConnectionClass();
                RegistryKey DataBase_Connection = Registry.CurrentConfig;
                RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                
                connection = new SqlConnection(ConCheck.ConnectString);

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                SqlCommand command1 = new SqlCommand(sql1, connection);
                SqlDataAdapter adapter1 = new SqlDataAdapter(command1);

                SqlCommand command2 = new SqlCommand(sql2, connection);
                SqlDataAdapter adapter2 = new SqlDataAdapter(command2);

                connection.Open();
                           
                adapter1.Fill(tip_avto);
                comboBox1tip.SelectedValuePath = "id_kategoria";
                comboBox1tip.DisplayMemberPath = "Tip_avto";
                comboBox1tip.ItemsSource = tip_avto.DefaultView;
                adapter1.Update(tip_avto);

                adapter2.Fill(komplect);
                comboBoxkorl.SelectedValuePath = "id_komplektacia";
                comboBoxkorl.DisplayMemberPath = "Tip_Komplektacii";
                comboBoxkorl.ItemsSource = komplect.DefaultView;
                adapter2.Update(komplect);

                adapter.Fill(avtomobil);
                dataGrid2.ItemsSource = avtomobil.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }


        private void UpdateKlient()
        {
            string sql = "SELECT         "
               + "* FROM dbo.Klient INNER JOIN dbo.Automobile  ON dbo.Klient.Auto_ID = dbo.Automobile.id_auto "
               + "INNER JOIN dbo.Pasport  ON dbo.Klient.Pasport_ID = dbo.Pasport.id_pasport";
            Klient = new DataTable();
          

            SqlConnection connection = null;
            try
            {
                ConnectionClass ConCheck = new ConnectionClass();
                RegistryKey DataBase_Connection = Registry.CurrentConfig;
                RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));


                connection = new SqlConnection(ConCheck.ConnectString);
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(Klient);

               

                dataGrid1.ItemsSource = Klient.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void UpdateZay()
        {
            string sql = "SELECT         "
               + "* FROM dbo.Zyavka INNER JOIN dbo.Zapchast  ON dbo.Zyavka.Zapch_ID = dbo.Zapchast.ID_Zapch ";
               
            Sklad = new DataTable();
          

            SqlConnection connection = null;
            try
            {
                ConnectionClass ConCheck = new ConnectionClass();
                RegistryKey DataBase_Connection = Registry.CurrentConfig;
                RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));

                connection = new SqlConnection(ConCheck.ConnectString);

                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(Sklad);

              
                dataGridzay.ItemsSource = Sklad.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void UpdateSklad()
        {
            string sql = "SELECT         "
               + "* FROM dbo.Zapchast INNER JOIN dbo.Sklad  ON dbo.Zapchast.Sklad_ID = dbo.Sklad.ID_Sklad "
            + "INNER JOIN dbo.Postavhic  ON dbo.Zapchast.PS_ID = dbo.Postavhic.ID_PS";
            Sklad = new DataTable();

            SqlConnection connection = null;
            try
            {
                ConnectionClass ConCheck = new ConnectionClass();
                RegistryKey DataBase_Connection = Registry.CurrentConfig;
                RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));

                connection = new SqlConnection(ConCheck.ConnectString);
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(Sklad);

                dataGrid.ItemsSource = Sklad.DefaultView;
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
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
                    command.Parameters.AddWithValue("@Zapch_ID", tbZaynam.Text);
                    try
                    {

                        command.ExecuteNonQuery();
                        UpdateZay();
                    }
                    catch  
                    {
                        MessageBox.Show("Введите коректный номер детали");
                    }
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
                    
                    ConnectionClass ConCheck =  new ConnectionClass();
                    RegistryKey DataBase_Connection = Registry.CurrentConfig;
                    RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                    ConCheck.Connection_Options(Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString()),
                       Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString()));
                    SqlConnection connection = new SqlConnection(ConCheck.ConnectString);

                    connection.Open();
                   
                    

                    SqlCommand Select_USID = new SqlCommand("select Pasport.id_pasport from Pasport where Pasport.Seria ='" 
                        + klientseriya.Text + "'", connection);
                   
                    try
                    {
                        SqlCommand Select_ID = new SqlCommand("select Automobile.id_auto from Automobile where Automobile.Gos_nomer = '" + klientgosnomerav.Text + "'", connection);

                        AVID = Select_ID.ExecuteScalar().ToString();
                        MessageBox.Show(AVID);
                    }
                    catch
                    {
                        MessageBox.Show("Не зарегистрированный номер автомобиля");
                        return;
                    }


                    //SqlCommand command = new SqlCommand(sql1, connection);
                    //SqlDataAdapter adapter = new SqlDataAdapter(Select_USID);
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
                    try
                    {

                        USID = Select_USID.ExecuteScalar().ToString();
                        MessageBox.Show(USID);
                    }
                    catch
                    {
                        MessageBox.Show("ошибка 3");
                    }
                    try
                    {
                        SqlCommand command = new SqlCommand("dbo.add_Klient", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Fam_kl", klientfam.Text);
                        command.Parameters.AddWithValue("@Im_kl", klientim.Text);
                        command.Parameters.AddWithValue("@Otch_kl", klientoth.Text);
                        command.Parameters.AddWithValue("@Auto_ID", AVID);
                        command.Parameters.AddWithValue("@Pasport_ID", USID);

                        command.ExecuteNonQuery();
                    }
                    catch {
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
                    var zaph = ((DataRowView)dataGridzay.SelectedItems[0]).Row["Zapch_ID"].ToString();
                    var IDZay = ((DataRowView)dataGridzay.SelectedItems[0]).Row["ID_Zyavka"].ToString();
                    DataZay.Text = data;
                    tbZaykolvo.Text = kolvo;
                    tbZaynam.Text = zaph;

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
                            command.Parameters.AddWithValue("@Zapch_ID", tbZaynam.Text);
                            command.Parameters.AddWithValue("@ID_Zyavka", IDZay1);
                            command.ExecuteNonQuery();
                            UpdateZay();

                            connection.Close();
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show("Введите коректный номер детали");


                        }


                        break;
                    }
            }
        
        }

        private void btnclearzay_Click(object sender, RoutedEventArgs e)
        {
            
            tbZaykolvo.Clear();
            tbZaynam.Clear();
        }

        private void klientdell_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItems.Count == 0) return;
            var IDPR = ((DataRowView)dataGrid1.SelectedItems[0]).Row["ID_Klient"].ToString();
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
                    SqlCommand command = new SqlCommand("dbo.dell_Klient", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id_klient", IDPR);
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    UpdateKlient();
                    connection.Close();
                    break;
                case MessageBoxResult.No:
                    break;
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
    }
}
