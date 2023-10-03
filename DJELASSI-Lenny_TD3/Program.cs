using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace TD3
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Demande à l'utilisateur de saisir un choix
            Console.Write("Entrez votre choix (1, 2 ou 3) : ");
            string choixUtilisateur = Console.ReadLine();

            // Convertir l'entrée en un nombre entier
            if (int.TryParse(choixUtilisateur, out int choix))
            {
                switch (choix)
                {
                    case 1: //si le choix est 1 alors 
                        ReadFile("Etudiants.txt");
                        break;
                    case 2: //si le choix est 2 alors
                        ReadFile2("Etudiants.txt");
                        break;
                    case 3: //si le choix est 3 alors
                        ReadFile3("Etudiants.txt");
                        break;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez choisir entre 1, 2 ou 3.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrée invalide. Veuillez saisir un nombre valide (1, 2 ou 3).");
            }

            Console.ReadLine();
        }

        //Première fonction 
        private static void ReadFile(string fileName)
        {
            StreamReader lecteur = new StreamReader(fileName);
            String line = "";
            int numberOfChar = 0;

            while (line != null)
            {
                line = lecteur.ReadLine();
                if (line != null)
                {
                    numberOfChar += line.Length;
                    Console.WriteLine(line);
                }
            }
            lecteur.Close();
        }

        private static void ReadFile2(string fileName)
        {
            StreamReader lecteur = new StreamReader(fileName);
            string line;
            int lineNumber = 0;
            bool evenLine = false;

            while ((line = lecteur.ReadLine()) != null)
            {
                lineNumber++;
                if (lineNumber % 2 == 0)
                {
                    // Ligne paire, utilisez une couleur différente ici
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    // Ligne impaire, utilisez une autre couleur ici
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                string[] parts = line.Split('\t');

                if (parts.Length >= 3)
                {
                    string nom = parts[0].Trim();
                    string prenom = parts[1].Trim().ToUpper(); // Met le prénom en majuscule
                    string dateStr = parts[2].Trim();
                    string sexe = parts[3].Trim();
                    string bacOrigine = parts[4].Trim();

                    DateTime date;
                    if (DateTime.TryParse(dateStr, out date))
                    {
                        // Formate la date au format souhaité
                        string dateFormatted = date.ToString("dddd dd MMMM yyyy", new CultureInfo("fr-FR"));

                        // Affiche les informations dans l'ordre demandé
                        Console.WriteLine($"{nom} {prenom} {dateFormatted} {sexe} {bacOrigine}");
                    }
                    else
                    {
                        // En cas d'erreur de format de date, affiche la ligne telle quelle
                        Console.WriteLine(line);
                    }
                }
                else
                {
                    // Si la ligne ne contient pas les informations attendues, affiche la ligne telle quelle
                    Console.WriteLine(line);
                }

                evenLine = !evenLine;
            }
            lecteur.Close();
        }

        private static void ReadFile3(string fileName)
        {
            StreamReader lecteur = new StreamReader(fileName);
            string line;
            int total = 0;
            int hommes = 0;
            int femmes = 0;

            while ((line = lecteur.ReadLine()) != null)
            {
                string[] parts = line.Split('\t');

                if (parts.Length >= 4)
                {
                    string sexe = parts[3].Trim();
                    total++;

                    if (sexe.Equals("G", StringComparison.OrdinalIgnoreCase))
                    {
                        hommes++;
                    }
                    else if (sexe.Equals("F", StringComparison.OrdinalIgnoreCase))
                    {
                        femmes++;
                    }
                }
            }
            lecteur.Close();

            //Statistiques
            double hommesPourcentage = ((double)hommes / total) * 100;
            double femmesPourcentage = ((double)femmes / total) * 100;

            Console.WriteLine($"Nombre d'étudiants de sexe Masculin : {hommes} Hommes soit {hommesPourcentage:F2}% des étudiants");
            Console.WriteLine($"Nombre d'étudiants de sexe Féminin : {femmes} Femmes soit {femmesPourcentage:F2}% des étudiants");
            Console.WriteLine($"Nombre total d'étudiants : {total}");
        }
    }
}
