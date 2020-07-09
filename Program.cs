using System;
using System.Data.SqlClient;

namespace Stone__ft._R._De_Niro_
{
    class Program
    {
        static void Main(string[] args)
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string ConnectionString = "Server = localhost; Database = Курс; Trusted_Connection = True;";

            using SqlConnection connection = new SqlConnection("Server = localhost; Database = Курс; Trusted_Connection = True;");            

            //string URLString = "http://www.cbr.ru/scripts/XML_daily.asp";

            Controller.GetTable(ConnectionString);//На сегодня

            //Controller.GetTable(ConnectionString, "18.01.2020");//На конкретную дату "день.месяц.год"

            Console.WriteLine(Controller.GetValue("AZN", Convert.ToDateTime("17.10.2019")));//Валюта, дата            

            Console.ReadKey();            
        }
    }
}
