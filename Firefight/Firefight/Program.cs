using Parse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Firefight
{
    class Program
    {
        
        static void Main(string[] args)
        {
            List<long> temps = new List<long>();

            Stopwatch watch = new Stopwatch();

            ParseClient.Initialize(new ParseClient.Configuration
            {
                ApplicationId = "NP26N2nAa6JLyasVy9BawQVg1AhbUmHjOC1I354T",
                WindowsKey = "yNVbMUwcgRpRkulYyGcqUQSWdY0mFSIzEO2MOxGz",
                Server = "https://parseapi.back4app.com/classes"
            });

            Console.WriteLine("=====================================================================");
            Console.WriteLine("===================== Début du test Back4App ========================");
            Console.WriteLine("=====================================================================\n");

            Console.WriteLine("INSERTION d'une ligne avec 2 paramètres de la table Person \nTemps d'execution : ");

            //Test d'insertions de données
            for (int i = 0; i < 10; i++)
            {
                watch.Start();
                
                    ParseObject myObject = new ParseObject("Person");
                    myObject["Name"] = "Louis";
                    myObject["Age"] = 25;
                    myObject.SaveAsync().Wait();
                
                watch.Stop();
                temps.Add(watch.ElapsedMilliseconds);
                Console.Write("{0}ms|", watch.ElapsedMilliseconds);
                watch.Reset();
            }

            //Affichage des résultats des insertions
            long moyenne = 0;
            foreach(long temp in temps)
            {
                moyenne += temp;
            }
            moyenne /= temps.Count;

            Console.WriteLine("\nMoyenne : {0}ms \n", moyenne);


            ////////////////////////////////////////////////////////
            temps.Clear();
            moyenne = 0;
            //Test de lecture de données
            Console.WriteLine("LECTURE d'une ligne de la table Person \nTemps d'execution : ");

            for (int i = 0; i < 10; i++)
            {
                watch.Start();

                ParseQuery<ParseObject> query = ParseObject.GetQuery("Person");
                query.FirstAsync().Wait();
                
                watch.Stop();
                temps.Add(watch.ElapsedMilliseconds);
                Console.Write("{0}ms|", watch.ElapsedMilliseconds);
                watch.Reset();
            }

            //Affichage des résultats des insertions
            moyenne = 0;
            foreach (long temp in temps)
            {
                moyenne += temp;
            }
            moyenne /= temps.Count;

            Console.WriteLine("\nMoyenne : {0}ms \n", moyenne);


            ////////////////////////////////////////////////////////
            temps.Clear();
            moyenne = 0;
            //Test de suppression de données
            Console.WriteLine("SUPPRESSION d'une ligne de la table Person \nTemps d'execution : ");
            for (int i = 0; i < 10; i++)
            {
                watch.Start();

                ParseQuery<ParseObject> query = ParseObject.GetQuery("Person");
                query.FirstAsync().Result.DeleteAsync().Wait();

                watch.Stop();
                temps.Add(watch.ElapsedMilliseconds);
                Console.Write("{0}ms|", watch.ElapsedMilliseconds);
                watch.Reset();
            }

            //Affichage des résultats des insertions
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
    }
}
