using System;
using System.Collections.Generic;
using System.IO;

namespace DSProject2
{
    /* Milli Park sınıfında il adlarını ve ilan yılını tek bir string halinde tuttum.
     Program milli park bilgilerini kendim hazırladığım bir not defterinden alıyor.
     Milli Parkların her değişkeni birbirinden nokta ile ayrı. Birden fazla il ismi olsa da tek bir string şeklinde okunuyor.
    Ayrıca ilan yılı da tek bir string halinde okunabiliyor.*/
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


        static string[][] milliParkDosyasiOku() // Bu fonksiyon milliParklar.txt dosyasından milli parkların bilgilerini okuyor.
                                                // Not defterinde 48 satır ve her satırda birer milli park var. Her milli parkın bilgilerini, birbirinden nokta ile ayırdım.
                                                // Her elemanı ,4 elemanlı bir string dizisi olan bir diziye bilgiler arasındaki noktalar kullanılarak milli park bilgileri ekleniyor.


        {
            // Burdaki path stringi , ödevde verdiğim milliParklar.txt not defterinin bilgisayardaki yerini göstermelidir.
            // Default olarak, projenin solution dosyasındaki bin/debug/ klasörünü gösteriyor. 
            string path = Path.Combine(Directory.GetCurrentDirectory()) + "\\milliParklar.txt";

            string[] dosyaOkuma = File.ReadAllLines(path);
            string[][] dataListe = new string[dosyaOkuma.Length][];
            for (int i = 0; i < dosyaOkuma.Length; i++)
            {
                dataListe[i] = new string[4];
                dataListe[i] = dosyaOkuma[i].Split('.');
            }

            return dataListe; // dataListe 48 elemanlı ve her elemanı , 4 elemanlı bir string dizisi. 4 eleman sırasıyla: Milli Park adı, bulunduğu iller, ilan yılı ve yüzölçümü.
        }

        static List<List<MilliPark>> GenericListOluşturma(string[][] dataDizi) // Bu metot 2 Elemanlı Generic Listeyi oluşturmak için. Parametre olarak 48 milli parkın bilgilerini içeren diziyi alıyor.
        {
            List<List<MilliPark>> Liste = new List<List<MilliPark>>(2); // 2 Elemanlı Generic Liste. Elemanları da milli park nesneleri içeren Generic Listeler.

            for (int i = 0; i < 2; i++)
            {
                Liste.Add(new List<MilliPark>());
            }

            for (int i = 0; i < dataDizi.Length; i++)
            {
                int hektar = Int32.Parse(dataDizi[i][3]); // Dosyadan string olarak okuduğum için yüz ölçümünü int'e çevirdim. 15000 hektardan küçük olanlar 1. Elemana, büyükler 2. Elemana.
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

        static void ekranaYaz(List<List<MilliPark>> Liste) // Bu metot string.format fonksiyonu kullanarak, ekrana önce generic listenin 1. elemanını, yani yüzölçümü 15000'den küçük olanları yazıyor.
                                                           // Sonra Generic Listenin 2. elemanını, yüzölçümü 15000'den büyük olanları yazıyor.
        {
            string[] baslık = { "15.000 Hektardan Küçük Olanlar", "15.000 Hektardan Büyük Olanlar" };
            int k = 0;
            int yüzÖlçümToplamı = 0;
            int toplamYüzÖlçüm = 0;
            foreach (List<MilliPark> list in Liste) // 2 elemanlı Generic List'in for each döngüsü.
            {
                Console.Write(new string('-', 30) + "\n" + baslık[k] + "\n" + new string('-', 30) + "\n\n");

                Console.WriteLine(string.Format("{0,-65}{1,-46}{2,-41}{3,-30}", "Milli Park Adları", "Bulundukları İller", "İlan Tarihleri", "Yüzölçümleri"));
                Console.WriteLine(string.Format("{0,-65}{1,-45}{2,-41}{3,-30}", new string('-', 17), new string('-', 18), new string('-', 16), new string('-', 13)));
                foreach (MilliPark milliPark in list) // MilliPark nesnelerinden oluşan ,2 Generic List'in for each döngüsü.
                {
                    string[] tarih = milliPark.ilanYılı.Split(' ');
                    string t = $"{tarih[0],-3} {tarih[1],-7}{tarih[2],5}";

                    string hektar = $"{milliPark.yüzÖlçümü,7:n0} Hektar";
                    Console.WriteLine(string.Format("{0,-65}{1,-45}{2,-40}{3,-30}", milliPark.milliPark_Adı, milliPark.ilAdları, t, hektar));
                    yüzÖlçümToplamı += milliPark.yüzÖlçümü;
                }

                Console.Write("\n{0:n0}ın Toplam Yüzölçümü: {1:n0} Hektar\n\n", baslık[k++], yüzÖlçümToplamı);
                toplamYüzÖlçüm += yüzÖlçümToplamı;
                yüzÖlçümToplamı = 0;
            }

            Console.Write(string.Format("\nTüm YüzÖlçümlerin Toplamı: {0:n0} Hektar\n\n", toplamYüzÖlçüm));
        }
    }
}