using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page_Details : MonoBehaviour
{
    Config_Database config;
    public GameObject cardOriginal;
    public Button btn_next;
    public Button btn_prev;
    public int page = 1;

    void Start()
    {
        view_data(page);
    }

    void Update()
    {
        Config_Database access_data = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Config_Database>();
        List<string[]> data_sql = access_data.all_details_data();
        int new_count = (page * 4);

        if((data_sql.Count-1) < new_count)
        {
            btn_next.interactable = false;
        }
        
        if(page == 1)
        {
            btn_prev.interactable = false;
        }
    }

    public void next_page()
    {
        btn_prev.interactable = true;
        config = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Config_Database>();
        List<string[]> data_sql = config.all_details_data();
        
        int new_count = (page * 4);

        if((data_sql.Count-1) < new_count)
        {
            new_count = data_sql.Count;
        }

        for (var i = (page * 4 - 4); i < new_count; i++)
        {
            GameObject off_object = GameObject.Find("pengujian" + i);
            Destroy(off_object);
        }

        page = page + 1;
        view_data(page);

    }

    public void prev_page()
    {   
        btn_next.interactable = true;
        config = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Config_Database>();
        List<string[]> data_sql = config.all_details_data();
        
        int new_count = (page * 4);

        if((data_sql.Count-1) < new_count)
        {
            new_count = data_sql.Count;
        }

        for (var i = (page * 4 - 4); i < new_count; i++)
        {
            GameObject off_object = GameObject.Find("pengujian" + i);
            Destroy(off_object);
        }

        page = page - 1;
        view_data(page);

    }

    public void view_data(int page_number)
    {
        config = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<Config_Database>();
        List<string[]> data_sql = config.all_details_data();
        float default_dotY = 3.2f;

        int new_count = (page_number * 4);

        if((data_sql.Count-1) < (new_count))
        {
            new_count = data_sql.Count;
        }

        for (var i = (page_number * 4 - 4); i < new_count; i++)
        {
            GameObject cardClone = Instantiate(cardOriginal);
            cardClone.name = "pengujian" + i;
            
            object_card data_object = GameObject.Find("pengujian" + i).GetComponent<object_card>();
            data_object.set_data_position(0f,default_dotY,0f);
            data_object.set_type_card(data_sql[i][0],data_sql[i][1]);
            data_object.set_desc_card(data_sql[i][3]);
            data_object.set_date_card(replace_slash(data_sql[i][2]));
            data_object.set_move();

            default_dotY = default_dotY - 1.35f;
            
        }

    }

    public string replace_slash(string value)
    {
        return value.Replace("/", "-");
    }
}
