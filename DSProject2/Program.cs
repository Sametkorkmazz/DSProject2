using System;
using System.Collections.Generic;
using System.IO;

namespace DSProject2
{
    class MilliPark
    {
        public string milliPark_Adı;
        public int yüzÖlçümü;
        public string ilAdları;
        public string ilanYılı;

        public MilliPark(string milliPark_Adı, string ilAdları, string ilanYılı, int yüzÖlçümü)
        {
            this.ilAdları = ilAdları;
            this.milliPark_Adı = milliPark_Adı;
            this.yüzÖlçümü = yüzÖlçümü;
            this.ilanYılı = ilanYılı;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<List<MilliPark>> MilliParklar = GenericListOluşturma(milliParkDosyasiOku());


            ekranaYaz(MilliParklar);
        }


        static string[][] milliParkDosyasiOku()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory()) + "\\milliParklar.txt";
            string[] dosyaOkuma = File.ReadAllLines(path);
            string[][] dataListe = new string[dosyaOkuma.Length][];
            for (int i = 0; i < dosyaOkuma.Length; i++)
            {
                dataListe[i] = new string[4];
                dataListe[i] = dosyaOkuma[i].Split('.');
            }

            return dataListe;
        }

        static List<List<MilliPark>> GenericListOluşturma(string[][] dataDizi)
        {
            List<List<MilliPark>> Liste = new List<List<MilliPark>>(2);

            for (int i = 0; i < 2; i++)
            {
                Liste.Add(new List<MilliPark>());
            }

            for (int i = 0; i < dataDizi.Length; i++)
            {
                int hektar = Int32.Parse(dataDizi[i][3]);
                if (hektar < 15000)
                {
                    Liste[0].Add(new MilliPark(dataDizi[i][0], dataDizi[i][1], dataDizi[i][2], Int32.Parse(dataDizi[i][3])));
                }
                else
                {
                    Liste[1].Add(new MilliPark(dataDizi[i][0], dataDizi[i][1], dataDizi[i][2], Int32.Parse(dataDizi[i][3])));
                }
            }

            return Liste;
        }

        static void ekranaYaz(List<List<MilliPark>> Liste)
        {
            string[] baslık = { "15.000 Hektardan Küçük Olanlar:", "15.000 Hektardan Büyük Olanlar:" };
            int k = -1;
            int yüzÖlçümToplamı = 0;
            foreach (List<MilliPark> list in Liste)
            {
                Console.Write(baslık[++k] + "\n\n");
                Console.WriteLine(string.Format("{0,-65}{1,-47}{2,-40}{3,-30}", "Milli Park Adları", "Bulundukları İller", "İlan Tarihleri", "Yüzölçümleri"));
                Console.WriteLine(string.Format("{0,-65}{1,-46}{2,-40}{3,-30}", new string('-', 17), new string('-', 18), new string('-', 15), new string('-', 13)));
                foreach (MilliPark milliPark in list)
                {
                    string[] tarih = milliPark.ilanYılı.Split(' ');
                    string t = $"{tarih[0],3} {tarih[1],7}{tarih[2],5}";

                    string hektar = $"{milliPark.yüzÖlçümü,7:n0} Hektar";
                    Console.WriteLine(string.Format("{0,-65}{1,-45}{2,-40}{3,-30}", milliPark.milliPark_Adı, milliPark.ilAdları, t, hektar));
                    yüzÖlçümToplamı += milliPark.yüzÖlçümü;
                }

                Console.WriteLine();
            }

            string y = $"{yüzÖlçümToplamı:n0} Hektar";
            Console.Write(string.Format("\nTüm YüzÖlçümlerin Toplamı: {0,-10}\n\n", y));
        }
    }
}