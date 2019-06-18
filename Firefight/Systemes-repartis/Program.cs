using Parse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Systemes_repartis
{
    class Program
    {
        static void Main(string[] args)
        {
            List<long> temps = new List<long>();
            Stopwatch watch = new Stopwatch();
            
            FirebaseConnection db = new FirebaseConnection();
            Console.WriteLine("=====================================================================");
            Console.WriteLine("===================== Début du test Firebase ========================");
            Console.WriteLine("=====================================================================\n");

            //Test d'insertions de données
            Console.WriteLine("INSERTION d'une ligne avec 2 paramètres de la table Person \nTemps d'execution : ");
            for (int i = 0; i < 100; i++)
            {
                watch.Start();
                
                    Person person = new Person() { Firstname = "Louis", Lastname = "Blasselle" };
                    db.Insert(person, i.ToString()).Wait();
                
                watch.Stop();
                temps.Add(watch.ElapsedMilliseconds);
                Console.Write("{0}ms|", watch.ElapsedMilliseconds);
                watch.Reset();
            }
            
            //Affichage des résultats des insertions
            long moyenne = 0;
            foreach (long temp in temps)
            {
                moyenne += temp;
            }
            moyenne /= temps.Count;

            Console.WriteLine("\nMoyenne : {0}ms \n", moyenne);



            ///////////////////////////////////////////////////////////
            temps.Clear();
            moyenne = 0;
            //Test de lecture de données
            Console.WriteLine("LECTURE d'une ligne de la table Person \nTemps d'execution : ");
            for (int i = 0; i < 100; i++)
            {
                watch.Start();
                
                Task<Person> d = db.GetPersonFromKey(i.ToString());
                d.Wait();
                
                watch.Stop();
                temps.Add(watch.ElapsedMilliseconds);
                Console.Write("{0}ms|", watch.ElapsedMilliseconds);
                watch.Reset();
            }

            //Affichage des résultats des lectures
            moyenne = 0;
            foreach (long temp in temps)
            {
                moyenne += temp;
            }
            moyenne /= temps.Count;

            Console.WriteLine("\nMoyenne : {0}ms \n", moyenne);
            

            ///////////////////////////////////////////////////////////
            temps.Clear();
            moyenne = 0;
            //Test de suppression de données
            Console.WriteLine("SUPPRESSION d'une ligne de la table Person \nTemps d'execution : ");
            for (int i = 0; i < 100; i++)
            {
                watch.Start();


                db.DeletePersonFromKey(i.ToString()).Wait();
                

                watch.Stop();
                temps.Add(watch.ElapsedMilliseconds);
                Console.Write("{0}ms|", watch.ElapsedMilliseconds);
                watch.Reset();
            }

            //Affichage des résultats des lectures
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
