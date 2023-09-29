using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateControll  
{
    DateData data = new DateData();

    public int first_years()
    {
        
        return data.years[0];
    }

    public int last_years()
    {
        return data.years[data.years.Length-1];
    }

    public int[] data_years()
    {
        return data.years;
    }

    public int[,,] data_month()
    {
        return data.month;
    }
}
