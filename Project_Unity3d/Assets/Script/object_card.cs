using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class object_card : MonoBehaviour
{
    public Text type_card;
    public Text desc_card;
    public Text date_card;
    public SpriteRenderer arrow;
    public float speed = 1000;
    public bool open_data = false;
    
    public float dotX = 0f;
    public float dotY = 0f;
    public float dotZ = 0f;

    public Color out_color;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update() 
    {
        Vector3 target = new Vector3(dotX, dotY,dotZ);
        if(open_data == true)
        {
            float step = speed * Time.deltaTime * 10f;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }

    public void set_data_position(float valueX, float valueY, float valueZ)
    {
        dotX = valueX;
        dotY = valueY;
        dotZ = valueZ;
    }

    public void set_move()
    {
        open_data = true;
    }

    public void set_type_card(string value, string nominal)
    {
        string logo = "";
        if(value == "masuk")
        {
            logo = "+";
        }else if(value == "keluar")
        {
            arrow.flipY = Physics2D.gravity.y < 180;
            arrow.color = out_color;
            logo = "-";
        }

        type_card.text = "[ " + logo + " ] Rp. " + nominal;
    }

    public void set_desc_card(string value)
    {
        desc_card.text = value;
    }

    public void set_date_card(string value)
    {
        date_card.text = value;
    }
}
