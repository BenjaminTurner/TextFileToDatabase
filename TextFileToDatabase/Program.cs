using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace TextFileToDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            //File Location
            string filePath = @"C:\Users\Cody\Documents\Visual Studio 2017\Projects\TextFileToDatabase\Data\Interview_Data.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();




            foreach (string line in lines)
            {

                String[] Data = line.Split('\t');

                SqlConnection myConnection = new SqlConnection(@"Data Source=ORION;Initial Catalog=Interview_Hospitals;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

                myConnection.Open();
                Console.WriteLine("Connected to DB.");

                try
                {
                    Console.WriteLine(line);
                    int Counter = 0;

                    for (int i = 0; i < lines.Count; i++)
                    {
                        Counter++;

                        SqlCommand myCommand = new SqlCommand("INSERT INTO Interview_Hospitals.dbo.Test (City, HospitalName, Address, StateCode) Values (@City, @HospitalName, @Address, @StateCode)", myConnection);

                        myCommand.Parameters.AddWithValue("@City", Data[i]);
                        myCommand.Parameters.AddWithValue("@HospitalName", Data[i + 1]);
                        myCommand.Parameters.AddWithValue("@Address", Data[i + 2]);
                        myCommand.Parameters.AddWithValue("@StateCode", Data[i + 3]);

                        myCommand.ExecuteNonQuery();
                        Console.WriteLine("Inserting data file " + Counter);
                    }

                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
                finally { myConnection.Close(); Console.WriteLine("Disconnected"); }
            }


            Console.ReadLine();
        }
    }
}

