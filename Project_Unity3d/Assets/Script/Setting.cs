using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Setting : MonoBehaviour
{
    public GameObject pwd_now; 
    public GameObject pwd_new; 
    public GameObject alert; 
    public GameObject alert2; 
    Config_Database config;

    public void save_user()
    {
        string get_pwd_now = pwd_now.GetComponent<TMP_InputField>().text;
        string get_pwd_new = pwd_new.GetComponent<TMP_InputField>().text;
        config = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Config_Database>();
        string[] data = config.search_user(1);

        if(get_pwd_now == data[1])
        {
            if(get_pwd_new == "")
            {
                enabled_alert2();
            }else{
                print("SUCCESS");
                config.update_password(1,get_pwd_new);
            }

        }else{
            enabled_alert();
        }

    }

    public void enabled_alert()
    {
        alert.SetActive(true);
    }

    public void enabled_alert2()
    {
        alert2.SetActive(true);
    }

    public void disabled_alert()
    {
        alert.SetActive(false);
        alert2.SetActive(false);
    }
}
