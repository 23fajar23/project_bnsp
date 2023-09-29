using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;

public class Income : MonoBehaviour
{
    public Animator datepicker;
    public Dropdown day_option;
    public Dropdown month_option;
    public Dropdown year_option;
   
    public Text input_date; 
    public InputField input_nominal;
    public InputField input_desc; 

    public GameObject set_off;
    public GameObject alert; 

    private int month_select = 0;
    private int month_before = 0;
    private int year_select = 0;
    private int year_before = 0;
    DateControll date = new DateControll();

    Config_Database config;

    void Start()
    {
        int[] all_years = date.data_years();
        int[,,] all_month = date.data_month();

        year_option.options.Clear();
        for (var i = 0; i < all_years.Length; i++)
        {
            year_option.options.Add (new Dropdown.OptionData() {text = all_years[i].ToString()});
        }

        month_option.options.Clear();
        for (var i = 0; i < 12; i++)
        {
            month_option.options.Add (new Dropdown.OptionData() {text = all_month[0,i,0].ToString()});
        }

        day_option.options.Clear();
        for (var i = 0; i < all_month[year_select,month_select,1]; i++)
        {
            day_option.options.Add (new Dropdown.OptionData() {text = (i+1).ToString()});
        }

    }

    void Update()
    {
        int year_now = Int32.Parse(year_option.options[year_option.value].text);
        change_date_year();
        change_date_month();
    }

    public void change_date_year()
    {
        year_select = year_option.value;
        if(year_select != year_before)
        {
            year_before = year_select;
            day_option.value = 0;
        }
    }

    public void change_date_month()
    {
        month_select = month_option.value;
        if(month_select != month_before)
        {
            month_before = month_select;

            int[,,] all_month = date.data_month();
            day_option.value = 0;

            day_option.options.Clear();
            for (var i = 0; i < all_month[year_select,month_select,1]; i++)
            {
                day_option.options.Add (new Dropdown.OptionData() {text = (i+1).ToString()});
            }
        }
    }

    public void save_date()
    {
        string day_check = check_single_variable(day_option.options[day_option.value].text);
        string month_check = check_single_variable(month_option.options[month_option.value].text);
        string year_check = year_option.options[year_option.value].text;
        string output_text = day_check + "/" + month_check + "/" + year_check;
        input_date.text = output_text;
        close_datepicker();
    }

    public string check_single_variable(string data)
    {
        int convert = Int32.Parse(data);
        if(convert < 10)
        {
            return ("0" + data);
        }else{
            return data;
        }
    }

    public void save_form()
    {
        int value_nominal = Int32.Parse(input_nominal.text);
        config = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Config_Database>();
        config.insert_history("masuk",value_nominal,input_date.text,input_desc.text);
        open_alert();
    }

    public void save_form_spend()
    {
        int value_nominal = Int32.Parse(input_nominal.text);
        config = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Config_Database>();
        config.insert_history("keluar",value_nominal,input_date.text,input_desc.text);
        open_alert();
    }

    public void reset_form()
    {
        input_date.text = "01/01/2021";
        day_option.value = 0;
        month_option.value = 0;

        string fix_select_year= "";

        int count = 0;

        do
        {
            if (year_option.options[count].text == "2021")
            {
                fix_select_year = year_option.options[count].text;
            }else{
                count++;
            }
            
        } while (fix_select_year == "");

        year_option.value = count;

        input_nominal.text = "0";
        input_desc.text = "";
    }

    public void open_alert()
    {
        alert.SetActive(true);
    }

    public void close_alert()
    {
        alert.SetActive(false);
    }

    public void open_datepicker()
    {
        datepicker.SetBool("status", true);
        set_off.SetActive(false);
        close_alert();
    }

    public void close_datepicker()
    {
        datepicker.SetBool("status", false);
        set_off.SetActive(true);
    }
}
