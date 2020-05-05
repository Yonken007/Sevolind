using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sevolind
{
    public class StreamreaderAndWriter
    {

        List<string> highscore10 = new List<string>(); // en lista för de senaste 10 highscoren
        List<double> bestHighscores = new List<double>(); // en lista för bästa highscore


        public void LoadBestHighscore() // den här metoden läser in värden från en textfil med alla bästa highscores och gör om dom doubels och lägger in dom i listan
        {
            StreamReader sr = new StreamReader("BestHighscore.txt");

            using (sr)
            {

                string line;

                while ((line = sr.ReadLine()) != null)
                {

                    bestHighscores.Add(double.Parse(line));

                }

            }

        }


        public void StremWriterbesthighscore(double time) // ordnar listan och lägger till och tar bort värden som är bättre/ sämre
        {

            StreamWriter sw = new StreamWriter("BestHighscore.txt", false); 
            bestHighscores.Sort();// soterar listan först

            if(bestHighscores.Count >= 0 && bestHighscores.Count < 10) // lägger endast till tider i highscore listan om de finns färre än 10 tider och sorterar listan igen
            {
                bestHighscores.Add(time);
                bestHighscores.Sort();

            }
          
            int lastindex = bestHighscores.Count - 1; // beräknar sista indexet på listan
            double lastValue = bestHighscores[lastindex]; // beräknar sista värdet i listan som är sämst

       
            if (time < lastValue && bestHighscores.Count == 10) // om de finns fler än 10 tider i lsitan och det sista värdet är sämre än den nya tiden byts tiden bort
            {
                bestHighscores.Sort();
                bestHighscores[lastindex] = time;
                


            }

            using (sw) // skriver in listan i text dokumenetet så den sparas. 
            {
                foreach (double n in bestHighscores)
                {

                    sw.WriteLine(n);
                }

            }

        }

        public void LoadHighscore10() // Den här lsitan läser in 10 highscores från de senaste gången man spelade
        {
           StreamReader sr = new StreamReader("Highscore10.txt");

            using (sr)
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {

                    highscore10.Add(line);

                }

            }

        }      
        
        public void StreamWriterhighscore10(double time) // Den här listan lägger till den nya tiden du fick och tar bort den älsta om de är fler än 10 tider i listan
        {

            StreamWriter sw = new StreamWriter("Highscore10.txt", false);

            highscore10.Add(time.ToString());

            using (sw)
            {

               if(Highscore10.Count > 10)
                {
                    Highscore10.RemoveAt(9);

                }

               foreach(string n in Highscore10)
                {

                    sw.WriteLine(n);
                }


            }

        }


      

        public int[,] StreamreaderMap() { // Den här metoden läser in kartan som en textfil 

            int[,] maparray = new int[14, 32]; // textflien har för tillfället ett speciellt värde så om kartan ska ändras måste värderna ändras
            int temp = 0;
           
                using (StreamReader sr = new StreamReader("Map.txt")) // läser in filen
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            maparray[temp, i] = int.Parse(line[i]. // läser in filen som en två dimesionell array 
                                ToString());

                        }
                        temp++;

                    }

                }
                               
            return maparray; // retunerar kartan till GameElements

        }
        
        public List<string> Highscore10
        {
            get { return highscore10; }

        }

        public List<double> BestHighscore
        {
            get { return bestHighscores; }

        }

    }
}
