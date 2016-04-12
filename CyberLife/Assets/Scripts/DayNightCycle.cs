using System.Collections;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float day, hr, min;
    public float timeOfDay, daySpeedModifier;

    public float smooth = 2.0F;


    public int minMorning, maxMorning, 
               minNoon, maxNoon, 
               minEvening, maxEvening, 
               minNight, maxNight;


    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(timeOfDay, 0, 0), Time.deltaTime * smooth);


        /*
         5AM to 7AM - Dawn

        11AM - 1PM  -  Noon
        
        5pm to 7PM -   Duck and,
        
        8PM till 4AM is Night
         
         */



        if (hr >= minMorning && hr < maxMorning)
        {
            timeOfDay = 5;
        }
        else if (hr >= minNoon && hr < maxNoon)
        {
            timeOfDay = 90;
        }
        else if (hr >= minEvening && hr < maxEvening)
        {
            timeOfDay = 170;
        }
         else if (hr >= minNight)
        {
            timeOfDay = 290;
        }
        






        min += 0.35f + daySpeedModifier;
        if (min >= 60)
        {
            hr++;
            min = 0;

            if (hr >= 24)
            {
                hr = 0;
                day++;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            min = 59;
        }
    }
}