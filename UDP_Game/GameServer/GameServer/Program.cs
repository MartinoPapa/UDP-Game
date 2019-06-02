using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UDP;

class Program
{
    static void Main(string[] args)
    {
        List<Giocatore> players = new List<Giocatore>();

        Server server = new Server(9050);
        Console.WriteLine("SERVER ATTIVO\nserver ip = {0}\n", VisualizzaIp());

        string message, clientAddress, nome, c;
        int numGiocatori = 0;

        #region partita

        int index;
        double points;

        while (true)
        {
            message = server.Listen(out clientAddress);

            index = players.FindIndex(cl => cl._ip == clientAddress);
            if (index != -1)
            {
                points = Convert.ToDouble(message.Split('=')[1]);
                if(players[index].punteggio < points)
                {
                    players[index].punteggio = points;
                }

                Console.WriteLine(players[index]._nome + " ha terminato il round con punteggio: " + points);
                players.Sort((x, y) => y.punteggio.CompareTo(x.punteggio));
                string s = numGiocatori + "-";
                for (int a = 0; a < numGiocatori; a++)
                {
                    s += players[a]._nome + "," + players[a].punteggio + ";";
                }
                server.SendMessage(s);
            }
            else
            {
                if (Server.NewPlayer(message, out nome))
                {
                    players.Add(new Giocatore(nome, clientAddress));
                    Console.WriteLine("nuovo giocatore: {0}\n", nome);
                    server.SendMessage("99999");
                    numGiocatori++;
                }              
            }
            Console.WriteLine("\n**********************************************\n");
            c = "BEST:\n";
            for (int j = 0; j < numGiocatori; j++)
            {
                c += (j + 1) + ": " + players[j]._nome + "\t" + players[j].punteggio + "\n";
            }
            Console.WriteLine(c);
            // invia classifica
            Console.WriteLine("**********************************************\n");
        }
                
        #endregion

    }

    static void test()
    {
        Console.WriteLine("server ip = {0}\n", VisualizzaIp());
        Server server = new Server(9050);
        string playerA, dati;
        string s = server.Listen(out playerA);
        Console.WriteLine("player 1 connected\taddress= {0}\tname={1}", playerA, s);
        server.SendMessage("start game");

        while (true)
        {
            dati = server.Listen(out playerA);
            Console.WriteLine();
            string[] Vdata = dati.Split(';');
            Console.WriteLine("ROUND ENDED | player: {0} | score={1}", Vdata[0], Vdata[1]);
            server.SendMessage("restart");
        }
    }

    static void game()
    {


    }

    static private string VisualizzaIp()
    {
        IPHostEntry ipEntry = Dns.GetHostByName(Dns.GetHostName());
        IPAddress[] indirizzi = ipEntry.AddressList;
        return indirizzi[0].ToString();
    }

}

