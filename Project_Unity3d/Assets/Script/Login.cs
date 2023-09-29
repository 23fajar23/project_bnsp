using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    public GameObject username; 
    public GameObject password; 
    public GameObject alert; 
    public Button login;
    Config_Database config;
    Move_Scene move_side;

    public void validate()
    {
        string get_username = username.GetComponent<TMP_InputField>().text;
        string get_password = password.GetComponent<TMP_InputField>().text;

        if(get_username == "" || get_password == "")
        {
            enabled_alert();
        }else{
            config = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Config_Database>();
            string[] data = config.search_user(1);
            
            if(data[0] == get_username && data[1] == get_password)
            {
                print("SUCCESS");
                move_side = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Move_Scene>();
                move_side.to_homepage();
            }else{
                enabled_alert();
            }
        }
    }

    public void enabled_alert()
    {
        alert.SetActive(true);
    }

    public void disabled_alert()
    {
        alert.SetActive(false);
    }

}
