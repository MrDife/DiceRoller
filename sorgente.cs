using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DiceRoller
{
    class DiceRoller
    {
        static string usage = "\nErrore!\n\n" + //come usare il programma via CLI,usato in caso di errori
                "COME UTILIZZARE :\n\n" +
                "Il programma richiede in input quanti" +
                " e quali dadi lanciare e restituisce il risultato finale, accettando " +
                "anche eventuali valori da sommare alla fine\n\n" +
                "Si accettano più lanci conteporaneamente, separati da uno spazio\n\n" +
                "SINTASSI: (NUMERODADI)d(TIPODIDADO)+(SOMMA)\n\n" +
                "Dadi accettabili : D4,D6,D8,D10,D12 e D20\n\nES:\n\n1d6\n1d6+2\n2d10\n1d10 2d20+10 3d10+2 1d4";

        private static int Convert(string value) //converte string del parametro in int, con gestione eccezione
        {
            int number = 0;
            try
            {
                number = Int32.Parse(value);
            }
            catch (FormatException)
            {
                Console.WriteLine("\n\nERRORE DI CONVERSIONE\n\n");
                Console.WriteLine(usage);
            }
            catch (OverflowException)
            {
                Console.WriteLine("\n\nERRORE DI CONVERSIONE\n\n");
                Console.WriteLine(usage);
            }
            return number;
        }


        static void Main(string[] args)
        {
            if (args.Length == 0) //Al momento per forza via CLI con args
            {
                Console.WriteLine(usage);
                return;
            }
            else
            {
                string[] rollata; //vettore di stringhe con le rollate, un elemento per ogni della rollata
                                  //0 = quanti dadi 
                                  // 1 = che tipo
                                  // 2 = somma
                Random rnd = new Random();                  //per generare la rollata di dadi para randomica
                foreach (string i in args)
                {
                    rollata = Regex.Split(i, @"[d\+]");          //con regex : separo dentro rollata i 3 elementi di una rollata
                    bool flag = false;                          // un flag che ci servirà per gestire il caso in cui dopo il + non ci sia nulla
                    if (Regex.IsMatch(i, @"\+$")) flag = true; //true == finisce con il + ,ergo dopo salto la somma
                    int dado = Convert(rollata[1]);             //dado selezionato, in una variabile perchè la uso anche dopo
                    int[] allowedDice = { 4, 6, 8, 10, 12, 20 }; //dadi selezionabili, dado percentuale da aggiungere (00)

                    if (allowedDice.Contains(dado) == false)
                    {
                        Console.WriteLine(usage);
                        return;
                    }
                    int finalResult = 0; //risultato finale della rollata

                    for (int j = 0; j < Convert(rollata[0]); j++) //rollata[0] = quante volte tiro il dado = quante iterazioni
                    {
                        finalResult += rnd.Next(1, dado + 1); //numero random fra 1 e il numero del dado (senza il +1 l'ultimo elemento era escluso)
                    }

                    if (rollata.Length == 3 && flag == false) finalResult += Convert(rollata[2]); //SE l'elemento somma c'è ed non è vuoto, parsalo e sommalo
                    Console.WriteLine(finalResult); //siamo void main, altrimenti return final result

                }
            }
        }
    }
}
