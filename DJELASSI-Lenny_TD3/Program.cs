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
                        ReadFile("Etudiants.txt"); //Appel de la fonction ReadFile
                        break;
                    case 2: //si le choix est 2 alors
                        ReadFile2("Etudiants.txt"); //Appel de la fonction ReadFile2
                        break;
                    case 3: //si le choix est 3 alors
                        ReadFile3("Etudiants.txt"); //Appel de la fonction ReadFile3
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
            StreamReader lecteur = new StreamReader(fileName); //Instanciation 
            String line = ""; //Initialisation de line une chaine de caractères vide
            int numberOfChar = 0; //Initialisation de la variable numberOfChar un entier égal à 0

            while (line != null) //tant que la ligne lu n'est pas nulle
            {
                line = lecteur.ReadLine(); //lis la ligne
                if (line != null) //si la ligne n'est pas nulle
                {
                    numberOfChar += line.Length; //Calcul le nombre de caractères dans la ligne
                    Console.WriteLine(line); //Affiche la ligne
                }
            }
            lecteur.Close();
        }
        
        //Deuxième fonction
        private static void ReadFile2(string fileName)
        {
            StreamReader lecteur = new StreamReader(fileName); //Instanciation de lecteur un nouvel objet
            string line; //Déclaration de line une chaine de caractère
            int lineNumber = 0; //Initialisation de lineNumber en entier égal à 0
            bool evenLine = false; //Initialisation de evenLine un booléen égal à false

            while ((line = lecteur.ReadLine()) != null) //tant que les lignes lues ne sont pas égal à nulle
            {
                lineNumber++; //ajoute 1 à lineNumber
                if (lineNumber % 2 == 0) //Si la ligne est paire
                {
                    // Ligne paire, utilisez une couleur différente ici
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else //sinon elle est impaire
                {
                    // Ligne impaire, utilisez une autre couleur ici
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                string[] parts = line.Split('\t'); //initalisation de parts un tableau de chaine de caractères qui met chaque partie séparée par une tabulation dans une colonne du tableau

                if (parts.Length >= 3) //si la longueur de parts est supérieure ou égale à 3 alors
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

        //Troisième fonction
        private static void ReadFile3(string fileName)
        {
            StreamReader lecteur = new StreamReader(fileName); //Instanciation de lecteur un nouvel objet
            string line; //Déclaration de line une chaine de caractère
            int total = 0; //Initialisation de total un entier égal à 0
            int hommes = 0; //Initialisation de hommes un entier égal à 0
            int femmes = 0; //Initialisation de femmes un entier égal à 0

            while ((line = lecteur.ReadLine()) != null) //tant que les lignes lues ne sont pas égal à nulle
            {
                string[] parts = line.Split('\t'); //initalisation de parts un tableau de chaine de caractères qui met chaque partie séparée par une tabulation dans une colonne du tableau

                if (parts.Length >= 4) //si la longueur de parts est supérieure ou égale à 4 alors
                {
                    string sexe = parts[3].Trim(); //On va directement dans la colonne qui correspond au sexe de l'étudiant
                    total++; //On ajoute 1 au total

                    if (sexe.Equals("G", StringComparison.OrdinalIgnoreCase)) //Si le sexe est G
                    {
                        hommes++; //Ajoute 1 à l'entier hommes
                    }
                    else if (sexe.Equals("F", StringComparison.OrdinalIgnoreCase)) //Si le sexe est F
                    {
                        femmes++; //Ajoute 1 à l'entier femmes
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
