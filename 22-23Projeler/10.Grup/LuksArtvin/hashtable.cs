using System;
using System.Drawing;
using System.IO;
using System.Net.Http.Headers;
using System.Text;

namespace personeller
{
    public class HashTable
    {
        private const int TABLE_SIZE = 10; // hash tablosu uzunluğu
        private string[] table = new string[TABLE_SIZE];

        private int Hash(int key)
        {
            return key % TABLE_SIZE; // hash fonksiyonumuz
        }

        public void Add(MyObject obj)
        {
            int key = obj.GetId(); // Nesneden anahtar değerini al
            string valStr = obj.GetId() + "," + obj.GetName() + "," + obj.GetSurname() + "," + obj.GetAge() + "," + obj.GetTel() + "," + obj.GetGorev(); // Değeri string olarak al

            int hash = Hash(key);
            int index = hash;
            while (table[index] != null)
            {
                index = (index + 1) % TABLE_SIZE; // Boş adres bulana kadar ilerle
                if (index == hash)
                {
                    // Tablo dolu, hata mesajı ver ve çık
                    Console.WriteLine("Hash tablosu dolu. Yeni kayıt eklenemedi.");
                    return;
                }
            }

            table[index] = valStr;


        }
        public void Remove(int key)
        {
            int hash = Hash(key);
            int index = hash;
            while (table[index] != null)
            {
                if (table[index].StartsWith(key + ","))
                {
                    table[index] = null;
                    return;
                }
                index = (index + 1) % TABLE_SIZE; // Boş adres bulana kadar ilerle
                if (index == hash)
                {
                    // Tablo dolu, hata mesajı ver ve çık
                    Console.WriteLine("Aranan kayıt bulunamadı.");
                    return;
                }
            }
            Console.WriteLine("Aranan kayıt bulunamadı.");
        }

        public string Search(int key)
        {
            int hash = Hash(key);
            string record = table[hash];
            if (record != null)
            {
                return record;
            }
            else
            {
                return "Aranan kayıt bulunamadı.";
            }

        }



        private void SaveToCsv(string fileName)
        {
            string fullPath = Path.Combine("C:\\Users\\songu\\OneDrive\\Masaüstü", fileName);
            StringBuilder sb = new StringBuilder();

            // Başlık (header) satırını ekle
            sb.AppendLine("Id;Name;Surname;Age;Telephone;Position");

            for (int i = 0; i < TABLE_SIZE; i++)
            {
                string record = table[i];
                if (!string.IsNullOrEmpty(record))
                {
                    string[] fields = record.Split(',');
                    bool isEmpty = true;

                    // Değerlerin boş olup olmadığını kontrol et hee
                    foreach (string field in fields)
                    {
                        if (!string.IsNullOrEmpty(field.Trim()))
                        {
                            isEmpty = false;
                            break;
                        }
                    }

                    if (isEmpty)
                    {
                        sb.AppendLine("       ");
                    }
                    else
                    {
                        sb.AppendLine(record.Replace(",", ";"));
                    }
                }
                else
                {
                    sb.AppendLine("         ");
                }
            }

            File.WriteAllText(fullPath, sb.ToString());
        }


        public static void Main(string[] args)
        {
            HashTable table = new HashTable();

            MyObject obj1 = new MyObject(2, "meral", "coban", 30, "tel1", "muavin");
            MyObject obj2 = new MyObject(5, "songul", "aba", 38, "tel2", "sofor");
            MyObject obj3 = new MyObject(7, "kader", "sal", 37, "tel3", "tur rehberi");
            MyObject obj4 = new MyObject(1, "mehmet", "yilmaz", 59, "tel4", "musteri temsilcisi");
            MyObject obj5 = new MyObject(3, "ayse", "kaya", 25, "tel5", "satis gorevlisi");

            string fileName = "hash_table.csv";

            table.Add(obj1);
            table.Add(obj2);
            table.Add(obj3);
            table.Add(obj4);
            table.Add(obj5);
            //table.Remove(5);
            table.SaveToCsv(fileName);
            Console.WriteLine("Hash tablosu CSV dosyasına kaydedildi: " + fileName);
            //Console.WriteLine(table.Search(9));


        }
    }

    public class MyObject
    {
        private int id;
        private string name;
        private string surname;
        private int yas;
        private string tel;
        private string gorev;

        public MyObject(int id, string name, string surname, int yas, string tel, string gorev)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.yas = yas;
            this.tel = tel;
            this.gorev = gorev;
        }

        public int GetId()
        {
            return id;
        }

        public string GetName()
        {
            return name;
        }

        public string GetSurname()
        {
            return surname;
        }

        public int GetAge()
        {
            return yas;
        }
        public string GetTel()
        {
            return tel;
        }
        public string GetGorev()
        {
            return gorev;
        }
    }
}
