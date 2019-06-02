using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Giocatore
{
    public string _nome;
    public string _ip;

    public double punteggio = 0;

    public Giocatore(string nome, string ip)
    {
        _nome = nome;
        _ip = ip;
    }

    public Giocatore()
    {

    }

}
