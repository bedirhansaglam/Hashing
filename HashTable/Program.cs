using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] hashtable = new int[100]; //int tipinde bir dizi tanımlanır.
            Random random = new Random(); //rastgele sayılar üretmek için random sınıfı kullanılır.
            int deger=0;//rastgele üretilen sayilar tutulsun diye deger değişkeni tanımlanır.

            int cakismasayisi=0;
            Console.WriteLine("-****************Rapor****************-");
                 for (int i = 0; i < 100; i++) //100 tane rastgele sayı üretiliyor ve hashtablosuna bu değerler gerekli kontrollerden sonra aktarılıyor.
                { 
                     deger = random.Next(1000);//0-1000 arasında rastgele sayılar üretilir.
                     if (hashtable[division(hashtable.Length, deger)] == 0) //hashtablosu division metodu ile kontrol ediliyor eğer indexs boşsa değeri hashtablosuna aktarılıyor.
                         hashtable[division(hashtable.Length, deger)] = deger;
                     else //indeks değeri doluysa (yani çakışma oluyorsa)
                    {
                    cakismasayisi++;
                    //burada kullanılmak istenen yöntem yorum satırından çıkartılmalıdır.
                      hashtable = linearprobing(hashtable.Length, hashtable, deger);  //linearprobing yöntemiyle çakışma önleniyor. Ve hashtablosu tamamen doluyor.
                   // hashtable = quadraticprobing(hashtable.Length, hashtable, deger);   //quadraticprobing yöntemiyle çakışma önleniyor.
                    
                    } 
                }
            
            Console.WriteLine("Toplam "+ cakismasayisi + "  çakışma olmuştur");
            Console.WriteLine();
            Console.WriteLine("*----------------------------------------------------------------------------*");
            Console.WriteLine("\t\t\t Hash Table");
            Console.WriteLine("*----------------------------------------------------------------------------*");
            Console.WriteLine("\t İndeks \t Değer");
            Console.WriteLine("*----------------------------------------------------------------------------*");
            for (int j = 0; j < 100; j++)
            {
                Console.WriteLine("\t "+j+"\t\t "+hashtable[j]);
            }
            
            Console.ReadKey();
        }

        public static int division(int tabloboyutu,int anahtardeger)//hashtablosunun boyutu alınıyor ve anahtar değer alınıyor.
        {
            int index=0;

            index = anahtardeger % tabloboyutu;//anahtar değer tablo boyutuyla mod işlemine tutuluyor
            return index;//sonuç indexe aktarılıyor böylelikle değere karşılık gelen indis ortaya çıkıyor.
        }
        
        public static int[] linearprobing(int hashboyutu,int[] hashtablosu,int eklenecekdeger)
        {
            int[] hash=new int[hashboyutu];
            hash = hashtablosu;
            int yeniindex = division(hash.Length, eklenecekdeger);//çakışma olan index değeri belirlenir.
            while (hash[yeniindex] != 0)//çakışma durumu kontrol edilir.
            {
                ++yeniindex; //index değeri arttrılarak bir sonraki indexe bakar
                if (yeniindex >= hash.Length) //hashtablosunun indeks değerinde taşma olursa baştan bütün alanlara bakar
                {
                    yeniindex = 0;
                }
            }
            hash[yeniindex] = eklenecekdeger;//boş index değeri bulunca tabloya yenideğeri ekler.
            return hash;
        }
        public static int[] quadraticprobing(int hashboyutu,int[] hashtablosu,int eklenecekdeger)  // h(x)=xmod100
        {
            int[] hash = new int[hashboyutu];

            hash = hashtablosu;
            int t = eklenecekdeger % 100; //t=h(x) 
            int x = 0;
            int fx = 0;
            bool hatakontrolu = false;

            if (hash[t] != 0)//tekrar bir çakışma varsa fx fonksiyonuna göre işlem yapılır ve boş bir indeks değeri aranır.
            {
                
                while (hash[fx] != 0)
                {
                    ++x;
                    fx = t + int.Parse(Math.Pow(x, 2).ToString()); //f(x)=t+x^2 işlemi yapılır.
                    if (fx >= hash.Length) //tablonun indeks değerinden daha büyük bir değer gelirse döngü sonlansın ve bir hata fırlatsın
                    {
                        Console.WriteLine("Eklenebilecek kapasite kalmadı...Eklenemeyen Değer : "+eklenecekdeger);
                        hatakontrolu = true;
                        break;
                    }
                }

                //while döngüsünden çıkıldığında eklenebilecek bir indeks değeri bulunmuştur.
                if(!hatakontrolu)//hata yoksa ekle
                hash[fx] = eklenecekdeger;

               
            }
            else //çakışma yoksa (t indeksi boş ise) değeri tabloya ekle
            {
                hash[t] = eklenecekdeger;
            }


            return hash;
        }
    }
}
