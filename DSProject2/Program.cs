using System;
using System.Collections.Generic;

namespace DSProject2
{
    class Kuyruk // Normal kuyruk sınıfı. Yeni eleman sona eklenir. Eleman çıkarılırken baştan çıkarılır.
    {
        private List<int> kuyruk;

        public Kuyruk()
        {
            kuyruk = new List<int>();
        }

        public void enqueue(int ürünAdedi)
        {
            kuyruk.Add(ürünAdedi);
        }

        public int dequeue()
        {
            int temp = kuyruk[0];
            kuyruk.RemoveAt(0);
            return temp;
        }

        public bool isEmpty()
        {
            return kuyruk.Count == 0;
        }

        public int elemanSayisi()
        {
            return kuyruk.Count;
        }
    }

    class ÖncelikliKuyruk // ÖncelikliKuyruk sınıfı.
    {
        private List<int> kuyruk;

        public ÖncelikliKuyruk()
        {
            kuyruk = new List<int>();
        }

        public void enqueue(int ürünAdedi) // Eleman eklenirken kuyruğun sonuna eklenir.
        {
            kuyruk.Add(ürünAdedi);
        }

        public int dequeue() // Eleman çıkarılırken sayı değeri en küçük eleman bulunur ve çıkarılır.
        {
            int enKüçükSayi = kuyruk[0];
            int index = 0;
            for (int i = 0; i < kuyruk.Count; i++)
            {
                if (enKüçükSayi > kuyruk[i])
                {
                    enKüçükSayi = kuyruk[i];
                    index = i;
                }
            }

            kuyruk.RemoveAt(index);
            return enKüçükSayi;
        }

        public bool isEmpty()
        {
            return kuyruk.Count == 0;
        }

        public int elemanSayisi()
        {
            return kuyruk.Count;
        }

        internal class Program
        {
            static void Main(string[] args)
            {
                Kuyruk kuyruk = new Kuyruk();
                ÖncelikliKuyruk öncelikliKuyruk = new ÖncelikliKuyruk();
                int[] müşteriler = { 8, 9, 6, 7, 10, 1, 11, 5, 3, 4, 2 };
                for (int i = 0; i < müşteriler.Length; i++) // İki kuyruğa da müşterilerin ürün miktarları eklenir.
                {
                    kuyruk.enqueue(müşteriler[i]);
                    öncelikliKuyruk.enqueue(müşteriler[i]);
                }

                ürünleriOkut(kuyruk, öncelikliKuyruk);
                Console.ReadLine();
            }

            static void ürünleriOkut(Kuyruk kuyruk, ÖncelikliKuyruk öncelikliKuyruk) // 2 Kuyruktaki müşterilerin işlem sıralarını hesaplayıp ekrana yazan metot.
            {
                int beklenenSüre = 0;
                int ürünAdedi;
                double ortalamaBeklemeSüresi = 0;
                int müşteriAdedi = kuyruk.elemanSayisi();
                Console.WriteLine(new string('-', 23));

                Console.Write("Normal Kuyruk İşlemleri\n");
                Console.WriteLine(new string('-', 23));

                while (!kuyruk.isEmpty()) // Her bir müşterinin bekleme süresi ,kendisinden önceki müşterilerin bekleme süresi ile kendi ürününün işlem süresinin toplamıdır.
                {
                    ürünAdedi = kuyruk.dequeue();
                    beklenenSüre += (ürünAdedi * 3);
                    Console.WriteLine(string.Format("{0,-3} Adet ürünlü müşterinin bekleme süresi: {1,5} Saniye", ürünAdedi, beklenenSüre));
                    ortalamaBeklemeSüresi += beklenenSüre; // Ortalama bekleme süresine , bakılan müşterinin kuyrukta toplam harcadığı süre eklenir.
                }

                ortalamaBeklemeSüresi /= müşteriAdedi; // Ortalama beklenen süre , her müşterinin kuyrukta beklediği sürelerin toplamının, müşteri sayısına bölümüdür.
                Console.Write("\nToplam Geçen Süre: " + beklenenSüre + " Saniye");

                Console.Write(string.Format("\nNormal Kuyrukta Müşterilerin Ortalama İşlem Tamamlanma Süresi: {0} Saniye\n\n", Math.Round(ortalamaBeklemeSüresi, 2)));
                ortalamaBeklemeSüresi = 0;
                beklenenSüre = 0;
                Console.WriteLine(new string('-', 26));

                Console.Write("Öncelikli Kuyruk İşlemleri\n");
                Console.WriteLine(new string('-', 26));
                while (!öncelikliKuyruk.isEmpty())
                {
                    ürünAdedi = öncelikliKuyruk.dequeue();
                    beklenenSüre += (ürünAdedi * 3);
                    Console.WriteLine(string.Format("{0,-3} Adet ürünlü müşterinin bekleme süresi: {1,5} Saniye", ürünAdedi, beklenenSüre));
                    ortalamaBeklemeSüresi += beklenenSüre;
                }

                ortalamaBeklemeSüresi /= müşteriAdedi;
                Console.Write("\nToplam Geçen Süre: " + beklenenSüre + " Saniye");

                Console.Write(string.Format("\nÖncelikli Kuyrukta Müşterilerin Ortalama İşlem Tamamlanma Süresi: {0} Saniye\n\n", Math.Round(ortalamaBeklemeSüresi, 2)));
            }
        }
    }
}