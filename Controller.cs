using System;
using System.Data.SqlClient;
using System.Xml;

namespace Stone__ft._R._De_Niro_
{
    class Controller
    {
        public static void GetTable(string ConnectionString)
        {
            using SqlConnection connection = new SqlConnection(ConnectionString);
            using XmlTextReader reader = new XmlTextReader("http://www.cbr.ru/scripts/XML_daily.asp");
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            { 
                connection.Open();

            (string date, string charcode, string value) task = ("", "", "");

                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Element)
                        continue;
                    switch (reader.Name)
                    {
                        case "ValCurs":
                            while (reader.MoveToNextAttribute())
                                if (reader.Name == "Date")
                                    task.date = reader.Value;
                            break;

                        case "CharCode":
                            reader.Read();
                            task.charcode = reader.Value;
                            break;

                        case "Value":
                            reader.Read();
                            task.value = reader.Value.Replace(',', '.');
                            try
                            {
                                SqlCommand command = new SqlCommand("insert into RATES (DATE1, CURRENCY, RATE) values (@DATE1, @CURRENCY, @RATE)", connection);
                                command.Parameters.AddWithValue("@DATE1", Convert.ToDateTime(task.date).ToString("MM-dd-yyyy"));
                                command.Parameters.AddWithValue("@CURRENCY", task.charcode);
                                command.Parameters.AddWithValue("@RATE", task.value);
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.ReadKey();
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public static void GetTable(string ConnectionString, string daydate)
        {
            using SqlConnection connection = new SqlConnection();
            using XmlTextReader reader = new XmlTextReader("http://www.cbr.ru/scripts/XML_daily.asp?date_req=" + daydate);
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();

                (string date, string charcode, string value) task = ("", "", "");

                while (reader.Read())
                {
                    if (reader.NodeType != XmlNodeType.Element)
                        continue;
                    switch (reader.Name)
                    {
                        case "ValCurs":
                            while (reader.MoveToNextAttribute())
                                if (reader.Name == "Date")
                                    task.date = reader.Value;
                            break;

                        case "CharCode":
                            reader.Read();
                            task.charcode = reader.Value;
                            break;

                        case "Value":
                            reader.Read();
                            task.value = reader.Value.Replace(',', '.');
                            try
                            {

                                SqlCommand command = new SqlCommand("insert into RATES (DATE1, CURRENCY, RATE) values (@DATE1, @CURRENCY, @RATE)", connection);
                                command.Parameters.AddWithValue("@DATE1", Convert.ToDateTime(task.date).ToString("MM-dd-yyyy"));
                                command.Parameters.AddWithValue("@CURRENCY", task.charcode);
                                command.Parameters.AddWithValue("@RATE", task.value);
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.ReadKey();
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public static decimal GetValue(string charcode, DateTime date)
        {
            decimal value;
            using SqlConnection connection = new SqlConnection("Server = localhost; Database = Курс; Trusted_Connection = True;");
            if (connection != null && connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT RATE FROM RATES WHERE CURRENCY = '{charcode}' AND DATE1 = '{date.Date.ToString("MM/dd/yyyy")}'", connection);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                value = (decimal)reader["RATE"];                
                return value;
            }
            else
                return 0;
        }       
    }
}
