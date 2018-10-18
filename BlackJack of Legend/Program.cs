using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BlackJack_of_Legend
{
    class Program
    {


        //resize
        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_RESIZE = 0xF000;
        public const int SC_MAXIMIZE = 0xF030;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        //marche pas
        public enum Sorte
        {
            Coeur = 1,
            Pique = 2,
            Careau = 3,
            Trefle = 4

        }

        public enum Categorie
        {
            Valet = 11,
            dame = 12,
            roi = 13

        }


        static int categorie;
        static int sorte;
        static int courtier1;
        static int courtier2;
        static int courtierTotal;
        static int carteSup1;
        static int carteSup2;
        static int choix = 0;
        static int mise = 0;
        static int jeton = 100;

        static List<int> cartePiger = new List<int>();


        public static void ShowGame(string lineToWrite = "", int offsetY = 0, int bob = 0, int categ = 0, int sort = 0, int chife = 0)
        {
            //jeuCentrer
            Console.SetCursorPosition(Console.WindowWidth / 2 - lineToWrite.Length / 2, Console.WindowHeight / 2 + offsetY);
            Console.WriteLine(lineToWrite);
            Console.SetCursorPosition(Console.WindowWidth / 2 - lineToWrite.Length / 2, Console.WindowHeight / 2 + offsetY + 1);
        }

        public static void FinJeu()
        {
            if (GetPlayerCarteTotal() == 21)
            {
                Console.Clear();
                ShowGame("Vous Avez gagnez !", -1);
                ShowGame("Vos jeton miser son doubler !", 1);
                jeton += (mise * 2);
            }

            else if (GetPlayerCarteTotal() < courtierTotal && GetPlayerCarteTotal() < 22)
            {
                Console.Clear();
                ShowGame("desoler vous avez perdu !", -1);
                ShowGame("Vous perdez votre mise :(", 1);
            }
            else if (GetPlayerCarteTotal() > courtierTotal && GetPlayerCarteTotal() < 22)
            {
                Console.Clear();
                ShowGame("Vous avez gagner !", -1);
                ShowGame("Votre mise sera doubler !", 1);
                jeton += (mise * 2);
            }
            else
            {
                Console.Clear();
                ShowGame("Desoler vous avez eu plus que 21 !", -1);
                ShowGame("Vous perdez votre mise !", 1);
            }

        }

        public static int GetPlayerCarteTotal()
        {
            int result = 0;
            for (int i = 0; i < cartePiger.Count; i++)
            {
                result += cartePiger[i];
                
            }

            return result;
        }


        static void Main(string[] args)
        {
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_RESIZE, MF_BYCOMMAND);
            DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);


            Random aleatoire = new Random();

            categorie = aleatoire.Next(0, 14);
            sorte = aleatoire.Next(0, 5);
            
            
            courtier1 = aleatoire.Next(0, 12);
            courtier2 = aleatoire.Next(0, 12);
            courtierTotal = courtier1 + courtier2;
            carteSup1 = aleatoire.Next(0, 12);
            carteSup2 = aleatoire.Next(0, 12);
            cartePiger.Add(aleatoire.Next(0, 12));
            cartePiger.Add(aleatoire.Next(0, 12));
            
            


            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();




            ShowGame("Bienvenu au BlackJack of Legend !", -1);
            ShowGame("Pour jouer appuyer sur la touche 1 ", 1);

            choix = Convert.ToInt32(Console.ReadLine());

            bool tryAgain = true;

            while (tryAgain == true)
            {
                cartePiger = new List<int>();

                cartePiger.Add(aleatoire.Next(0, 12));
                cartePiger.Add(aleatoire.Next(0, 12));

                Console.Clear();
                ShowGame("Votre total de jeton est de :  " + jeton, -1);
                ShowGame("pour commencer la mise appuyer sur 1", 1);


                choix = Convert.ToInt32(Console.ReadLine());

                Console.Clear();
                ShowGame("Veuillez entrez votre mise :", -1);

                mise = Convert.ToInt32(Console.ReadLine());

                jeton -= mise;

                //random ici + int
                Random rand = new Random();

               


                Console.Clear();
                ShowGame("Vous avez misez : " + mise, -3);
                ShowGame("   ...   ", -1);
                ShowGame("Le coutier distribut les cartes et vous donne deux carte qui ont la valeurs de : " + GetPlayerCarteTotal(), 1);
                ShowGame(" . . . ", 3);
                ShowGame("Pour continuer, appuyez sur la touche 1 ", 5);

                choix = Convert.ToInt32(Console.ReadLine());

                Console.Clear();
                ShowGame("Le courtier pour ca par a recu : " + courtier1 + " Plus une autre carte", -3);
                ShowGame(" ", -1);
                ShowGame("Pour continuer, appuyez sur une touche ", -1);

                Console.ReadKey();

                

                bool aChoisie = false;

                while (!aChoisie)
                {
                        Console.Clear();
                    ShowGame("Voulez vous piger une carte suplementaire ou arreter la ? ", -3);
                    ShowGame(" ", -1);
                    ShowGame("Pour une carte supplémentaire appuyez sur 1", 1);
                    ShowGame("Pour en rester la veillez appuyez sur 2", 3);

                     choix = Convert.ToInt32(Console.ReadLine());
                     


                    if (choix == 1)
                    {
                        cartePiger.Add(aleatoire.Next(0, 12));

                        ShowGame("Vous avez pigez une carte suplémenaire", -5);
                        ShowGame("Vous avez pigez une carte de la valeur de : " + cartePiger[2], -3);
                        ShowGame(" ", -1);
                        ShowGame("Vous avez alors un total de : " + GetPlayerCarteTotal(), 1);
                        ShowGame(" ", 3);
                        ShowGame("Pour piger une carte suplémentaire appuyer sur 1 ", 5);
                        ShowGame("Pour garder le jeu actuel appuyer sur 2", 7);

                        choix = Convert.ToInt32(Console.ReadLine());


                    }

                    else if (choix == 2)
                    {
                        aChoisie = true;
                    }


                }

                FinJeu();

                Console.ReadKey();

            }


            Console.ReadKey();
            Console.Clear();
        }



    }
}
