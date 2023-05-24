using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tracks
{
    internal class Program
    {
        static Hashtable data = new Hashtable(); // { artist: {album: {track1,track2,...,trackN}}}
        static void Main(string[] args)
        {
            Run();
        }
        static void Run()
        {
            bool inWork = true;
            while (inWork)
            {
                Console.WriteLine("Выберите опцию");
                Console.WriteLine("1) Добавить композицию");
                Console.WriteLine("2) Вывести всех музыкантов");
                Console.WriteLine("3) Вывести все альбомы музыканта");
                Console.WriteLine("4) Вывести все композиции альбома");
                Console.WriteLine("5) Вывести все композиции музыканта");
                Console.WriteLine("6) Удалить композицию");
                Console.WriteLine("7) Удалить альбом");
                Console.WriteLine("8) Удалить музыканта");
                Console.WriteLine("9) Завершить работу");
                switch (Console.ReadLine())
                {
                    case "1":
                        AddTrack();
                        break;
                    case "2":
                        {
                            Console.WriteLine("Музыканты:");                     
                            foreach (var artist in data.Keys) { Console.WriteLine(artist); }
                            break;
                        }
                    case "3":
                        { 
                            Console.WriteLine("Введите желаемого музыканта");
                            string artist = Console.ReadLine();
                            if (data.ContainsKey(artist))
                            {
                                Console.WriteLine($"Все альбомы {artist}:");
                                foreach (var album in (data[artist] as Hashtable).Keys) { Console.WriteLine(album); }
                            }
                            else
                            {
                                Console.WriteLine("Такой музыкант не найден");
                            }
                            break; 
                        }
                    case "4":
                        {
                            Console.WriteLine("Введите желаемого музыканта");
                            string artist = Console.ReadLine();
                            if (data.ContainsKey(artist))
                            {
                                Console.WriteLine("Введите желаемый альбом");
                                string album = Console.ReadLine();
                                if ((data[artist] as Hashtable).ContainsKey(album))
                                {
                                    Console.WriteLine($"Все композиции {artist} с альбома {album}:");
                                    foreach (var song in (data[artist] as Hashtable)[album] as List<string>) 
                                    Console.WriteLine(song);
                                }
                                else
                                {
                                    Console.WriteLine("Такой альбом не найден");
                                }
                                    
                            }
                            else
                            {
                                Console.WriteLine("Такой музыкант не найден");
                            }
                        }
                        break;
                    case "5":
                        {
                            Console.WriteLine("Введите желаемого музыканта");
                            string artist = Console.ReadLine();
                            if (data.ContainsKey(artist))
                            {
                                Console.WriteLine($"Все композиции {artist}:");
                                foreach (var album in (data[artist] as Hashtable).Keys) 
                                {
                                    foreach (var song in ((data[artist] as Hashtable)[album] as List<string>))
                                        Console.WriteLine(song);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Такой музыкант не найден");
                            }
                            break;
                        }
                    case "6":
                        {
                            Console.WriteLine("Введите желаемого музыканта");
                            string artist = Console.ReadLine();
                            if (data.ContainsKey(artist))
                            {
                                Console.WriteLine("Введите желаемый альбом");
                                string album = Console.ReadLine();
                                if ((data[artist] as Hashtable).ContainsKey(album))
                                {
                                    Console.WriteLine("Введите желаемую песню");
                                    string song = Console.ReadLine();
                                    if (((data[artist] as Hashtable)[album] as List<string>).Contains(song))
                                    {
                                        ((data[artist] as Hashtable)[album] as List<string>).Remove(song);
                                    }
                                    else Console.WriteLine("Такой трек не найден");
                                }
                                else Console.WriteLine("Такой альбом не найден");
                            }
                            else
                            {
                                Console.WriteLine("Такой музыкант не найден");
                            }
                            break;
                        }
                    case "7":
                        {
                            Console.WriteLine("Введите желаемого музыканта");
                            string artist = Console.ReadLine();
                            if (data.ContainsKey(artist))
                            {
                                Console.WriteLine("Введите желаемый альбом");
                                string album = Console.ReadLine();
                                if ((data[artist] as Hashtable).ContainsKey(album))
                                    (data[artist] as Hashtable).Remove(album);
                                else Console.WriteLine("Такой альбом не найден");
                            }
                            else
                            {
                                Console.WriteLine("Такой музыкант не найден");
                            }
                            break;
                        }
                    case "8":
                        {
                            Console.WriteLine("Введите желаемого музыканта");
                            string artist = Console.ReadLine();
                            if (data.ContainsKey(artist))
                            {
                                data.Remove(artist);
                            }
                            else
                            {
                                Console.WriteLine("Такой музыкант не найден");
                            }
                            break;
                        }
                    case "9":
                        inWork = false;
                        break;
                    default:
                        Console.WriteLine("Ошибка ввода");
                        break;
                }
                Console.WriteLine("Нажмите любую кнопку");
                Console.ReadLine();
                Console.Clear();
            }

        }
        static void AddTrack()
        {
            Console.WriteLine("Введите имя исполнителя");
            string artist = Console.ReadLine();
            Console.WriteLine("Введите название альбома");
            string album = Console.ReadLine();
            Console.WriteLine("Введите название песни");
            string song = Console.ReadLine();

            if (data.ContainsKey(artist))
            {
                if ((data[artist] as Hashtable).ContainsKey(album))
                {
                    if (((data[artist] as Hashtable)[album] as List<string>).Contains(song))
                    {
                        Console.WriteLine("Данный трек уже в базе");
                    }
                    else
                    {
                        ((data[artist] as Hashtable)[album] as List<string>).Add(song);
                    }
                }
                else
                {
                    List<string> songs = new List<string>() { song };
                    (data[artist] as Hashtable).Add(album,songs);
                }
            }
            else 
            {
                Hashtable newAlbum = new Hashtable();
                List<string> songs = new List<string>() {song };
                newAlbum.Add(album, songs);
                data.Add(artist, newAlbum);
            }
        }
    }
}
