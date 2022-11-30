using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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

    class ÖncelikliKuyruk // Generic List kullanarak ÖncelikliKuyruk sınıfı oluşturma.
    {
        private List<MilliPark> kuyruk;

        public ÖncelikliKuyruk()
        {
            kuyruk = new List<MilliPark>();
        }

        public void enqueue(MilliPark park) // Yeni elemanlar kuyruğun sonuna eklenir. O(1) ekleme zamanlı.
        {
            kuyruk.Add(park);
        }

        public MilliPark dequeue() // Kuyruktan eleman çıkarılırken , yüzölçümü en küçük olan milli park bulunur ve çıkarılır.
        {
            MilliPark temp = kuyruk[0];
            int index = 0;
            int enKüçükHektar = kuyruk[0].yüzÖlçümü;
            for (int i = 0; i < kuyruk.Count; i++)
            {
                if (enKüçükHektar > kuyruk[i].yüzÖlçümü) // Çıkarılacak milli parkın kuyruktaki konumu index değişkeni ile takip edilir.
                {
                    enKüçükHektar = kuyruk[i].yüzÖlçümü;
                    temp = kuyruk[i];
                    index = i;
                }
            }

            kuyruk.RemoveAt(index);
            return temp;
        }

        public bool isEmpty()
        {
            return kuyruk.Count == 0;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            ÖncelikliKuyruk öncelikliKuyruk = milliParkÖncelikliKuyrukOluştur(milliParkDosyasiOku());
            ekranaYaz(öncelikliKuyruk);
            Console.ReadLine();
        }

        static string[][] milliParkDosyasiOku()
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/milliParklar.txt");
            string[] dosyaOkuma = File.ReadAllLines(path);
            string[][] dataListe = new string[dosyaOkuma.Length][];
            for (int i = 0; i < dosyaOkuma.Length; i++)
            {
                dataListe[i] = new string[4];
                dataListe[i] = dosyaOkuma[i].Split('.');
            }

            return dataListe;
        }

        static ÖncelikliKuyruk milliParkÖncelikliKuyrukOluştur(string[][] dataDizisi) // Milli parkları öncelikli kuyruğa ekleme ve kuyruğu döndürme.
        {
            ÖncelikliKuyruk kuyruk = new ÖncelikliKuyruk();
            for (int i = 0; i < dataDizisi.Length; i++)
            {
                kuyruk.enqueue(new MilliPark(dataDizisi[i][0], dataDizisi[i][1], dataDizisi[i][2], Int32.Parse(dataDizisi[i][3])));
            }

            return kuyruk;
        }

        static void ekranaYaz(ÖncelikliKuyruk kuyruk) // Kuyruktaki milli parklar çıkarılır ve ekrana yazdırılır. Milli Parklar , yüz ölçümlerine göre küçükten büyüğe çıkarılırlar.
        {
            Console.WriteLine("Milli Parklar Öncelikli Kuyruk Yazımı\n\n");
            int yüzÖlçümToplamı = 0;
            Console.WriteLine(string.Format("{0,-65}{1,-46}{2,-41}{3,-30}", "Milli Park Adları", "Bulundukları İller", "İlan Tarihleri", "Yüzölçümleri"));
            Console.WriteLine(string.Format("{0,-65}{1,-45}{2,-41}{3,-30}", new string('-', 17), new string('-', 18), new string('-', 16), new string('-', 13)));
            while (!kuyruk.isEmpty())

            {
                MilliPark milliPark = kuyruk.dequeue();
                string[] tarih = milliPark.ilanYılı.Split(' ');
                string t = $"{tarih[0],-3} {tarih[1],-7}{tarih[2],5}";

                string hektar = $"{milliPark.yüzÖlçümü,7:n0} Hektar";
                Console.WriteLine(string.Format("{0,-65}{1,-45}{2,-40}{3,-30}", milliPark.milliPark_Adı, milliPark.ilAdları, t, hektar));
                yüzÖlçümToplamı += milliPark.yüzÖlçümü;
            }

            string y = $"{yüzÖlçümToplamı:n0} Hektar";
            Console.Write(string.Format("\nTüm YüzÖlçümlerin Toplamı: {0,-10}\n\n", y));
        }
    }
}