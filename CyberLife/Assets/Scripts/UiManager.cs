using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text hygiene, hunger, bladder, energy;

    public Slider hygieneSlider, hungerSlider, bladderSlider, energySlider;

    private void Update()

    {
        hygiene.text = "Hygiene";// + GetComponent<PlayerController>().hygiene.ToString();
        hunger.text = "Hunger";// + GetComponent<PlayerController>().hunger.ToString();
        bladder.text = "Bladder";// + GetComponent<PlayerController>().bladder.ToString();
        energy.text = "Energy";// + GetComponent<PlayerController>().energy.ToString();

        hygieneSlider.value = GetComponent<PlayerController>().hygiene;
        hungerSlider.value = GetComponent<PlayerController>().hunger;
        bladderSlider.value = GetComponent<PlayerController>().bladder;
        energySlider.value = GetComponent<PlayerController>().energy;
    }
}