using UnityEngine;

public class MenuButtonPlay : MonoBehaviour
{
    public void StartGameButton(int index)
    {
        Application.LoadLevel(1);
        Time.timeScale = 1;
    }

    public void NextLevelButton(string Patchingwordsprototype)
    {
        Application.LoadLevel(1);
        Time.timeScale = 1;
    }
}