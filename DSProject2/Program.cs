using System;
using System.IO;

namespace DSProject2
{
    class MilliPark // Ödevin ilk üç şıkkı için MilliPark sınıfı ve dosyadan okuma yapan fonksiyon aynıdır.
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

    class Yığıt // Yığıt sınıfı. MilliPark elemanlarından oluşan array kullanılarak hazırlandı.
    {
        private int boyut;
        private MilliPark[] yığıtDizisi;
        private int top;

        public Yığıt(int maxBoyut)
        {
            boyut = maxBoyut;
            yığıtDizisi = new MilliPark[boyut];
            top = -1;
        }

        public void push(MilliPark park)
        {
            yığıtDizisi[++top] = park;
        }

        public MilliPark pop()
        {
            return yığıtDizisi[top--];
        }

        public MilliPark peek()
        {
            return yığıtDizisi[top];
        }

        public bool isEmpty()
        {
            return (top == -1);
        }

        public bool isFull()
        {
            return (top == boyut - 1);
        }
    }

    class Kuyruk // Kuyruk sınıfı. MilliPark elemanlarından oluşan array kullanılarak hazırlandı.
    {
        private int boyut;
        private MilliPark[] kuyrukDizisi;
        private int baş;
        private int son;
        private int elemanSayisi;

        public Kuyruk(int maxboyut)
        {
            boyut = maxboyut;
            kuyrukDizisi = new MilliPark[boyut];
            baş = 0;
            son = -1;
            elemanSayisi = 0;
        }

        public void enqueue(MilliPark park)
        {
            if (son == boyut - 1)
            {
                son = -1;
            }

            kuyrukDizisi[++son] = park;
            elemanSayisi++;
        }

        public MilliPark dequeue()
        {
            MilliPark park = kuyrukDizisi[baş++];
            if (baş == boyut)
            {
                baş = 0;
            }

            elemanSayisi--;
            return park;
        }

        public MilliPark peekFront()
        {
            return kuyrukDizisi[baş];
        }

        public bool isEmpty()
        {
            return elemanSayisi == 0;
        }

        public int kuyruktakiElemanSayisi()
        {
            return elemanSayisi;
        }

        public bool isFull()
        {
            return elemanSayisi == boyut;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string[][] MilliParkListesi = milliParkDosyasiOku();
            Yığıt yığıt = MilliParklarıYığıtaEkle(MilliParkListesi);
            Kuyruk kuyruk = MilliParklarıKuyruğaEkle(MilliParkListesi);
            ekranaYaz(yığıt, kuyruk);
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

        static Yığıt MilliParklarıYığıtaEkle(string[][] dataDizisi) // Milli Park bilgilerinin bulunduğu diziyi parametre olarak alıp , Yığıt sınıfından bir nesneye ekleme ve geri döndürme.
        {
            Yığıt yığıt = new Yığıt(dataDizisi.Length);
            for (int i = 0; i < dataDizisi.Length; i++)
            {
                yığıt.push(new MilliPark(dataDizisi[i][0], dataDizisi[i][1], dataDizisi[i][2], Int32.Parse(dataDizisi[i][3])));
            }

            return yığıt;
        }

        static Kuyruk MilliParklarıKuyruğaEkle(string[][] dataDizisi) // Kuyruk nesnesini oluşturma.
        {
            Kuyruk kuyruk = new Kuyruk(dataDizisi.Length);
            for (int i = 0; i < dataDizisi.Length; i++)
            {
                kuyruk.enqueue(new MilliPark(dataDizisi[i][0], dataDizisi[i][1], dataDizisi[i][2], Int32.Parse(dataDizisi[i][3])));
            }

            return kuyruk;
        }

        static void ekranaYaz(Yığıt yığıt, Kuyruk kuyruk) // String.format fonksiyonları kullanarak alt alta ekrana yazma.
        {
            int yüzÖlçümToplamı = 0;
            Console.Write("Milli Park Yığıtı:\n\n");
            Console.WriteLine(string.Format("{0,-65}{1,-46}{2,-41}{3,-30}", "Milli Park Adları", "Bulundukları İller", "İlan Tarihleri", "Yüzölçümleri"));
            Console.WriteLine(string.Format("{0,-65}{1,-45}{2,-41}{3,-30}", new string('-', 17), new string('-', 18), new string('-', 16), new string('-', 13)));
            while (!yığıt.isEmpty())
            {
                MilliPark park = yığıt.pop();
                string[] tarih = park.ilanYılı.Split(' ');
                string t = $"{tarih[0],-3} {tarih[1],-7}{tarih[2],5}";
                string hektar = $"{park.yüzÖlçümü,7:n0} Hektar";
                Console.WriteLine(string.Format("{0,-65}{1,-45}{2,-40}{3,-30}", park.milliPark_Adı, park.ilAdları, t, hektar));
                yüzÖlçümToplamı += park.yüzÖlçümü;
            }

            Console.Write("\n\nMilli Park Kuyruğu:\n\n");
            Console.WriteLine(string.Format("{0,-65}{1,-46}{2,-41}{3,-30}", "Milli Park Adları", "Bulundukları İller", "İlan Tarihleri", "Yüzölçümleri"));
            Console.WriteLine(string.Format("{0,-65}{1,-45}{2,-41}{3,-30}", new string('-', 17), new string('-', 18), new string('-', 16), new string('-', 13)));
            while (!kuyruk.isEmpty())
            {
                MilliPark park = kuyruk.dequeue();
                string[] tarih = park.ilanYılı.Split(' ');
                string t = $"{tarih[0],-3} {tarih[1],-7}{tarih[2],5}";
                string hektar = $"{park.yüzÖlçümü,7:n0} Hektar";
                Console.WriteLine(string.Format("{0,-65}{1,-45}{2,-40}{3,-30}", park.milliPark_Adı, park.ilAdları, t, hektar));
            }


            string y = $"{yüzÖlçümToplamı:n0} Hektar";
            Console.Write(string.Format("\nTüm YüzÖlçümlerin Toplamı: {0,-10}\n\n", y));
        }
    }
}