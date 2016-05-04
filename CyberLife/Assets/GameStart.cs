using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    private float a = 255;
    public int smoothSpeed = 1;
    private float initWaitTime = 5f;

    private void Update()
    {
        if (Time.time < initWaitTime)
        {
            print((int)Time.time);
            return;
        }

        a -= 1 * Time.deltaTime * smoothSpeed;
        a = Mathf.Clamp(a, 0, 255);

        GetComponent<Image>().color = new Color32(20, 20, 20, (byte)a);
        if (a == 0)
        {
            Destroy(this.gameObject);
            // ClearLog();
            print("Complete initiate game state");
        }
    }

    public static void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.ActiveEditorTracker));
        var type = assembly.GetType("UnityEditorInternal.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}