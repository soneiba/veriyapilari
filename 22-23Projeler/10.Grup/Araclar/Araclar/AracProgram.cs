﻿using System;
using System.Collections.Generic;
using LINQtoCSV;

namespace Araclar
{
    class Program
    {
        
        static void Main(string[] args)
        {

            İkiYönlüListe liste = new İkiYönlüListe();//liste olustur

            ReadCsvFile(liste);//csvokuyorum listeye ekliyorum

            WriteCsvFile(liste);//listedekileri csvye yazıyorumyazıyorum

            Console.ReadLine();
        }
        public static void ReadCsvFile(İkiYönlüListe liste)
        {
            var csvFileDescription = new CsvFileDescription// csv okunma kurallarını tanımlar
            {
                FirstLineHasColumnNames = true,
                IgnoreUnknownColumns = true,
                SeparatorChar=',',
                UseFieldIndexForReadingData=false,
            };

            var csvContext = new CsvContext();
            var araclar = csvContext.Read<Vehicle>("C:/Users/ejder/source/repos/Araclar/Araclar/araclarStogu.csv", csvFileDescription);
            foreach (var arac in araclar)
            {
                liste.Ekle(arac);
            }     
           
        }
        public static void WriteCsvFile(İkiYönlüListe liste)
        {
            var aracList = new List<Vehicle>();
            İkiYönlüListe tempList = new İkiYönlüListe();
            tempList = liste;
            int kontrol = tempList.bas.Arac.id;
            do
            {
                Console.WriteLine("temp id: " + tempList.bas.Arac.id + "listeid: " + liste.bas.Arac.id);
                aracList.Add(tempList.bas.Arac);
                tempList.bas = tempList.bas.Sonraki;
                
            } while (tempList.bas.Arac.id != kontrol);

            var csvFileDescription = new CsvFileDescription
            {
                FirstLineHasColumnNames = true,               
                SeparatorChar = ',',      
            };
            var csvContext = new CsvContext();
            csvContext.Write(aracList, "C:/Users/ejder/source/repos/Araclar/Araclar/araclarStogu.csv", csvFileDescription);
            Console.WriteLine("Basarili");

        }
    }

    public class İkiYönlüListe
    {
        public static int idCounter = 0;
        public class Node
        {
            public Vehicle Arac;
            public Node Onceki;
            public Node Sonraki;

            public Node(Vehicle arac)
            {
                Arac = arac;
                Onceki = null;
                Sonraki = null;
            }
        }

        public Node bas;
        //string aracPlakasi, string marka, string model, string aracturu, int uretimYili, int koltukSayisi, bool klima, bool wifi
        public void Ekle(Vehicle arac)
        {

            //Vehicle arac = new Vehicle(aracPlakasi, marka, model, aracturu, uretimYili, koltukSayisi, klima, wifi);
            Node yeniNode = new Node(arac);
            arac.id = idCounter++;
            if (bas == null)
            {

                bas = yeniNode;
                bas.Sonraki = bas;
                bas.Onceki = yeniNode;
            }
            else
            {
                Node lastNode = bas.Onceki;
                yeniNode.Sonraki = bas;
                yeniNode.Onceki = lastNode;
                bas.Onceki = yeniNode;
                lastNode.Sonraki = yeniNode;
            }
        }
        public void Ekle(string aracPlakasi, string marka, string model, string aracturu, int uretimYili, int koltukSayisi, bool klima, bool wifi)
        {

            Vehicle arac = new Vehicle(aracPlakasi, marka, model, aracturu, uretimYili, koltukSayisi, klima, wifi);
            Node yeniNode = new Node(arac);
            arac.id = idCounter++;
            if (bas == null)
            {

                bas = yeniNode;
                bas.Sonraki = bas;
                bas.Onceki = yeniNode;
            }
            else
            {
                Node lastNode = bas.Onceki;
                yeniNode.Sonraki = bas;
                yeniNode.Onceki = lastNode;
                bas.Onceki = yeniNode;
                lastNode.Sonraki = yeniNode;
            }

        }


        public void Sil(İkiYönlüListe liste ,int id)
        {
            if (bas == null)
                return;
            
            Node currentNode = liste.bas;

            do
            {
                if (currentNode.Arac.id ==id )
                {
                    if (currentNode == bas)
                    {
                        bas = bas.Sonraki;
                        bas.Onceki = currentNode.Onceki;
                        currentNode.Onceki.Sonraki = bas;
                    }
                    else
                    {
                        currentNode.Onceki.Sonraki = currentNode.Sonraki;
                        currentNode.Sonraki.Onceki = currentNode.Onceki;
                    }

                    return;
                }

                currentNode = currentNode.Sonraki;
            } while (currentNode != bas);
        }
        public void Say(İkiYönlüListe liste)
        {
            Node currentNode = bas;
            int count = 0;
            do
            {
                count++;
                currentNode = currentNode.Sonraki;
            } while (currentNode != bas);
            Console.WriteLine("listedeki eleman sayisi: "+count);
        }
        public void Yazdır(İkiYönlüListe liste)
        {
            Node node = liste.bas;

            Console.WriteLine("Id\tArac Plakası\tMarka\tModel\tArac turu\tUretim Yılı\tKoltuk Sayısı\tKlima\tWifi\n");
            do
            {
                Console.WriteLine(node.Arac.id+"\t"+node.Arac.aracPlakasi + "\t" + node.Arac.marka + "\t" + node.Arac.model
                    + "\t" + node.Arac.aracturu + "\t\t" + node.Arac.uretimYili + "\t\t" + node.Arac.koltukSayisi
                    + "\t\t" + (node.Arac.klima ? "var" : "yok") + "\t" + (node.Arac.wifi ? "var" : "yok") + "\n");
                node = node.Sonraki;
            } while (node != bas);


                Console.WriteLine();
        }
    }
    [Serializable]
    public class Vehicle
    {


        [CsvColumn(Name = "Id", FieldIndex =1)]
        public int id { get; set; }
        [CsvColumn(Name = "Arac-Plakası", FieldIndex = 2)]
        public string aracPlakasi { get; set; }
        [CsvColumn(Name = "Marka", FieldIndex = 3)]
        public string marka { get; set; }
        [CsvColumn(Name = "Model", FieldIndex = 4)]
        public string model { get; set; }
        [CsvColumn(Name = "Arac-Turu", FieldIndex = 5)]
        public string aracturu { get; set; }
        [CsvColumn(Name = "Uretim-Yılı", FieldIndex = 6)]
        public int uretimYili { get; set; }
        [CsvColumn(Name = "Koltuk-Sayısı", FieldIndex = 7)]
        public int koltukSayisi { get; set; }
        [CsvColumn(Name = "Klima", FieldIndex = 8)]
        public bool klima { get; set; }
        [CsvColumn(Name = "Wifi", FieldIndex = 9)]
        public bool wifi { get; set; }

        public Vehicle()
        {

        }
        public Vehicle(string aracPlakasi, string marka, string model, string aracturu, int uretimYili, int koltukSayisi, bool klima, bool wifi)
        {
            this.aracPlakasi = aracPlakasi;
            this.marka = marka;
            this.model = model;
            this.aracturu = aracturu;
            this.uretimYili = uretimYili;
            this.koltukSayisi = koltukSayisi;
            this.klima = klima;
            this.wifi = wifi;
        }

    }
}
