using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Globalization;

public class HomePage : MonoBehaviour
{
    public Text income_text;
    public Text spend_text;
    Config_Database config;
    
    void Start()
    {
        config = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Config_Database>();
        income_text.text = "Rp " + config.income_this_month("masuk").ToString();
        spend_text.text = "Rp " + config.income_this_month("keluar").ToString();

    }

}
