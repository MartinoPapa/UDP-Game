using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour {

    // Use this for initialization
    public Text txtIp;
    public Text txtName;

    static public int numRound = 2;
    static public Client client;
    static public  bool offline = true;

    public static string ipAddress = "0";
    public static string clientName = "a";

    public void OnClick()
    {
        ipAddress = txtIp.text;
        clientName = txtName.text;
        if(clientName=="")
        {
            clientName = "genericPlayer";
        }
        if (ipAddress == "")
        {
            ipAddress = "1";
        }
        try
        {
            client = new Client(ipAddress, 9050);
            Debug.Log("a");
            client.SendMessage("nome:"+clientName);
            Debug.Log("b");
            string data = client.Listen();
            numRound = Convert.ToInt32(data);
            Debug.Log("numRound: " + numRound);


            offline = false;
        }
        catch
        {
            offline = true;
        }
        SceneManager.LoadScene("Level01");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnClick();
        }
    }

    void Wait() { }
}
