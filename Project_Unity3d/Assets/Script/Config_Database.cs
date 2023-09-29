using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using UnityEngine.UI;
using System.Globalization;

public class Config_Database : MonoBehaviour
{
    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    private IDataReader reader;
    string DatabaseName = "transaksi.db";

    public string[] search_user(int id_user)
    {
        string filepath = Application.persistentDataPath + "/" + DatabaseName;

        string username = "";
        string password = "";
        
        new_connection();
        
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT username,password " + "FROM account where id = " + id_user;
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                username = reader.GetString(0);
                password = reader.GetString(1);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
        }

        string[] data = new string[2];
        data[0] = username;
        data[1] = password;
        
        return data;
    }

    public void new_connection()
    {
        string filepath = Application.persistentDataPath + "/" + DatabaseName;
        conn = "URI=file:" + filepath;

        Debug.Log("Stablishing connection to: " + conn);
        dbconn = new SqliteConnection(conn);
        dbconn.Open();
    }

    public void insert_history(string jenis,int nominal, string date, string desc)
    {
        new_connection();        
        string select_type = "";
        if(jenis == "masuk")
        {
            select_type = "masuk";
        }else{
            select_type = "keluar";
        }

        int true_nominal = 0;
        if(nominal > 0)
        {
            true_nominal = nominal;
        }

        string true_desc = "-";
        if(desc != ""){
            true_desc = desc;
        }


        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into history (jenis, nominal, tanggal, keterangan) values ('" +  
                        select_type + "','" + true_nominal + "','" + date + "','" + true_desc + "')");
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }

        Debug.Log("Insert Done  ");
    }

    public void update_password(int id, string pwd_new)
    {
        new_connection();

        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE account set password = '" + pwd_new +"' where ID = " + id);

            SqliteParameter update_password = new SqliteParameter("password", pwd_new);

            dbcmd.Parameters.Add(update_password);

            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }
    }

    public List<string[]> all_details_data()
    {
        new_connection();
        int value_nominal = 0;
        string value_type = "";
        string value_date = "";
        string value_desc = "";
        List<string[]> data_sql = new List<string[]>();
        
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT jenis, nominal, tanggal, keterangan " + "FROM history ";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                string[] nilai = new string[4];
                nilai[0] = reader.GetString(0);
                nilai[1] = reader.GetInt32(1).ToString();
                nilai[2] = reader.GetString(2);
                nilai[3] = reader.GetString(3);
                data_sql.Add(nilai);
            }

            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
        }
        
        return data_sql;
    }

    public int income_this_month(string type)
    {
        DateTime currentDateTime = DateTime.Now;
        string month_now = currentDateTime.ToString("MM"); // 09
        string year_now = currentDateTime.ToString("yyyy");

        new_connection();
        int value_money = 0;
        
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT tanggal, nominal " + "FROM history where jenis = '" + type + "' ";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {
                DateTime dt = DateTime.ParseExact(reader.GetString(0), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                string year_search = dt.ToString("yyyy");
                string month_search = dt.ToString("MM");
                
                if(year_now == year_search && month_now == month_search)
                {
                    value_money = value_money + reader.GetInt32(1);
                }
                
                // value_income = value_income + reader.GetInt32(0);
                
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
        }

        return value_money;

    }
}
