using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;

public class Make_default_db : MonoBehaviour
{
    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    private IDataReader reader;
    string DatabaseName = "transaksi.db";

    // Start is called before the first frame update
    void Start()
    {
        string filepath = Application.persistentDataPath + "/" + DatabaseName;
        
        if(!File.Exists(filepath))
        {
            File.WriteAllText(filepath, "");
            make_table(filepath);
            set_default_data();
        }else{
            print("File Exists");
        }
    }

    public void make_table(string path)
    {
        conn = "URI=file:" + path;

        Debug.Log("Stablishing connection to: " + conn);
        dbconn = new SqliteConnection(conn);
        dbconn.Open();

        string query1 = "CREATE TABLE account (ID INTEGER PRIMARY KEY AUTOINCREMENT, username VARCHAR(255), password VARCHAR(255))";
        string query2 = "CREATE TABLE history (ID INTEGER PRIMARY KEY AUTOINCREMENT, jenis VARCHAR(255), nominal INTEGER(255), tanggal DATE, keterangan TEXT)";
        
        try
        {
            dbcmd = dbconn.CreateCommand(); 
            dbcmd.CommandText = query1; 
            reader = dbcmd.ExecuteReader(); 
            print("Success Make Table 1");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            print("Failed Make Table 1");
        }

        try
        {
            dbcmd = dbconn.CreateCommand(); // create empty command
            dbcmd.CommandText = query2; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader

            print("Success Make Table 2");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            print("Failed Make Table 2");
        }
    }

    public void set_default_data()
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open(); //Open connection to the database.
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into account (username, password) values ('user','user')");
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }

        Debug.Log("Insert Done  ");
    }

}
