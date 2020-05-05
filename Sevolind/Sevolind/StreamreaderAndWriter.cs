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

        Player player;
        List<string> highscore10 = new List<string>();



        public void LoadHighscore10()
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
        
        public void StreamWriterhighscore10(double time)
        {

            StreamWriter sw = new StreamWriter("Highscore10.txt", false);

            highscore10.Add(time.ToString());

            using (sw)
            {

               if(Highscore10.Count > 10)
                {
                    Highscore10.RemoveAt(0);

                }

               foreach(string n in Highscore10)
                {

                    sw.WriteLine(n);
                }


            }

        }


      

        public int[,] StreamreaderMap() { // den här metoden hämtar värden från textfilen

            int[,] maparray = new int[14, 32];
            int temp = 0;
           
                using (StreamReader sr = new StreamReader("Map.txt"))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        for (int i = 0; i < line.Length; i++)
                        {
                            maparray[temp, i] = int.Parse(line[i].
                                ToString());

                        }
                        temp++;

                    }

                }
                               
            return maparray;

        }
        
        public List<string> Highscore10
        {
            get { return highscore10; }

        }

    }
}
