using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stos_i_kolejka
{
    class Program
    {
        static void Main(string[] args)
        {
            Stos<Int32> stos = new Stos<Int32>();
            stos.Dodaj(1);
            stos.Dodaj(2);
            stos.Dodaj(3);

            Console.WriteLine(stos.Zabierz());
            Console.WriteLine(stos.Zabierz());
            Console.WriteLine(stos.Zabierz());
            //Console.WriteLine(stos.Zabierz()); -> dostalibyśmy wyjątek

            Kolejka<String> kolejka = new Kolejka<String>();
            kolejka.Dodaj("pierwszy");
            kolejka.Dodaj("drugi");
            kolejka.Dodaj("trzeci");
            kolejka.Dodaj("czwarty");

            Console.WriteLine(kolejka.Zabierz());
            Console.WriteLine(kolejka.Zabierz());
            Console.WriteLine(kolejka.Zabierz());
            Console.WriteLine(kolejka.Zabierz());
            try
            {
                Console.WriteLine(kolejka.Zabierz());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Spodziewany wyjątek \"{ex.Message}...\" bo skończyły się elementy w kolejce");
            }

            Console.ReadLine();
        }
    }

    public class Stos<T>
    {
        T[] _coll;

        public Stos()
        {
            _coll = new T[0];
        }

        public T Zabierz()
        {
            if (_coll.Length > 0)
            {
                T toPop = _coll[_coll.Length - 1];
                T[] newColl = new T[_coll.Length - 1];
                if (newColl.Length > 0)
                    for (Int32 i = 0; i < newColl.Length; i++)
                    {
                        newColl[i] = _coll[i];
                    }
                _coll = newColl;
                return toPop;
            }
            else throw new ArgumentException("Stos jest pusty");
        }

        public void Dodaj(T toPush)
        {
            _coll = _coll.Append(toPush).ToArray();
        }

    }

    public class Kolejka<T>
    {
        T[] _coll;

        public Kolejka()
        {
            _coll = new T[0];
        }

        public T Zabierz()
        {
            if (_coll.Length > 0)
            {
                T toDequeue = _coll[0];
                T[] newColl = new T[_coll.Length - 1];
                if (newColl.Length > 0)
                    for (Int32 i = 0; i < newColl.Length; i++)
                    {
                        newColl[i] = _coll[i + 1];
                    }
                _coll = newColl;
                return toDequeue;
            }
            else throw new ArgumentException("Kolejka jest pusta");
        }

        public void Dodaj(T toEnqueue)
        {
            _coll = _coll.Append(toEnqueue).ToArray();
        }

    }
}
