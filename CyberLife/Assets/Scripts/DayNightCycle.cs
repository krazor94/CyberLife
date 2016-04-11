using System.Collections;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public float day, hr, min, sec, nextTime;
    public float timeOfDay, daySpeed;

    public float smooth = 2.0F;

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(timeOfDay, 0, 0), Time.deltaTime * smooth);

        if (Time.time > nextTime)
        {
            sec += 1 + daySpeed;
            nextTime = Time.time + 1;
        }

        if (sec >= 60)
        {
            min++;
            sec = 0;

            if (min >= 60)
            {
                hr++;
                min = 0;

                if (hr >= 12 && hr <= 6)
                    timeOfDay += 7.5f;
                else
                    timeOfDay += 22;

                if (hr > 24)
                {
                    hr = 0;
                    timeOfDay = 0;
                    day++;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            min = 59;
        }
    }
}