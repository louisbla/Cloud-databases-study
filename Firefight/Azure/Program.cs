using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace sqltest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<long> temps = new List<long>();
            Stopwatch watch = new Stopwatch();

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = "firefightprojet.database.windows.net",
                    UserID = "louisbla",
                    Password = "@Zerty123",
                    InitialCatalog = "azuredb"
                };
                Console.WriteLine("=====================================================================");
                Console.WriteLine("================= Début du test Azure SQL Server ====================");
                Console.WriteLine("=====================================================================\n");
                
                //Test d'insertions de données
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("INSERTION d'une ligne avec 2 paramètres de la table Person \nTemps d'execution : ");

                    connection.Open();

                    //Test d'insertions de données
                    for (int i = 0; i < 100; i++)
                    {
                            StringBuilder sb = new StringBuilder();
                        sb.Append("INSERT INTO person (NOM, AGE) ");
                        sb.Append("VALUES ('Louis', "+ i + ");");
                        String sql = sb.ToString();

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                                watch.Start();

                                command.ExecuteNonQuery();

                                watch.Stop();
                                temps.Add(watch.ElapsedMilliseconds);
                                Console.Write("{0}ms|", watch.ElapsedMilliseconds);
                                watch.Reset();
                            }
                    }
                }
                //Affichage des résultats des lectures
                long moyenne = 0;
                foreach (long temp in temps)
                {
                    moyenne += temp;
                }
                moyenne /= temps.Count;

                Console.WriteLine("\nMoyenne : {0}ms \n", moyenne);


                //////////////////////////////////////////////////////////////////
                moyenne = 0;
                temps.Clear();
                //Test de lecture de données
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("LECTURE d'une ligne de la table Person \nTemps d'execution : ");

                    connection.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT *");
                    sb.Append("FROM person");
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            watch.Start();
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                reader.Read();
                            }
                            watch.Stop();
                            temps.Add(watch.ElapsedMilliseconds);
                            Console.Write("{0}ms|", watch.ElapsedMilliseconds);
                            watch.Reset();
                        }
                    }
                }
                //Affichage des résultats des lectures
                foreach (long temp in temps)
                {
                    moyenne += temp;
                }
                moyenne /= temps.Count;

                Console.WriteLine("\nMoyenne : {0}ms \n", moyenne);

                //////////////////////////////////////////////////////////////////////////

                temps.Clear();
                moyenne = 0;
                //Test de suppression de données
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("SUPPRESSION d'une ligne de la table Person \nTemps d'execution : ");

                    connection.Open();
                    for (int i = 0; i < 100; i++)
                    {
                        StringBuilder sb = new StringBuilder();
                    sb.Append("DELETE FROM person ");
                    sb.Append("WHERE AGE="+i);
                    String sql = sb.ToString();

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                            watch.Start();
                            command.ExecuteNonQuery();
                            watch.Stop();
                            temps.Add(watch.ElapsedMilliseconds);
                            Console.Write("{0}ms|", watch.ElapsedMilliseconds);
                            watch.Reset();
                        }
                    }
                }

                //Affichage des résultats des suppressions
                moyenne = 0;
                foreach (long temp in temps)
                {
                    moyenne += temp;
                }
                moyenne /= temps.Count;

                Console.WriteLine("\nMoyenne : {0}ms \n", moyenne);

                Console.WriteLine("=====================================================================");
                Console.WriteLine("=========================== Fin du test =============================");
                Console.WriteLine("=====================================================================\n");
                Console.WriteLine("Appuyez sur Entrer pour fermer la fenêtre...");
                Console.ReadLine();
                
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}