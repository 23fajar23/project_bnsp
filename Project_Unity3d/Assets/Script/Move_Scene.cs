using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_Scene : MonoBehaviour
{
    public void to_login()
    {
        SceneManager.LoadScene("Login");
    }
    
    public void to_homepage()
    {
        SceneManager.LoadScene("HomePage");
    }

    public void to_setting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void to_Detail()
    {
        SceneManager.LoadScene("Details_cash");
    }

    public void to_income()
    {
        SceneManager.LoadScene("Income");
    }

    public void to_spend()
    {
        SceneManager.LoadScene("Spend");
    }

}
