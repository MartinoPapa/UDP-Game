using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataReader : MonoBehaviour
{

    public Text classifica;
    public Text timer;

    private bool end;
    private int tempo = 5;
    private int N;
    private List<string> nomi = new List<string>();
    private List<string> punteggi = new List<string>();

    static private int Round = 0;
    // roundTerminato
    void Start()
    {
        timer.text = "";
        ServerManager.client.SendMessage("P=" + GameManager.score);
        string s = ServerManager.client.Listen(); //N-nome,punteggio;
        string[] v = s.Split('-');
        N = Convert.ToInt32(v[0]);
        v = v[1].Split(';');
        string punteggio, nome;
        for (int i = 0; i < N; i++)
        {
            nome = v[i].Split(',')[0];
            punteggio = v[i].Split(',')[1];
            Debug.Log(nome + "," + punteggio);
            nomi.Add(nome);
            punteggi.Add(punteggio);

        }
        CreaClassifica();
        Round++;

        Debug.Log(ServerManager.numRound);
        if (Round == ServerManager.numRound)
        {
            timer.text = "PARTITA COMPLETATA!\npremere ESC per tornare alla home";
            end = true;
        }
        else
        {
            timer.text = "la partita continuerà a breve";
            Invoke("Reload", tempo);            
        }


    }

    void Reload()
    {
        SceneManager.LoadScene("Level01");
    }

    // Update is called once per frame
    void Update()
    {
        if (end && Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("loadScene");
        }
    }


    void CreaClassifica()
    {

        classifica.text = "";
        int i = 0;
        while(i<N && i < 10)
        {
            classifica.text += nomi[i] + ": " + punteggi[i] + "\n";
            i++;
        }
    }
}
