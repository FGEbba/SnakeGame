using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetText : MonoBehaviour
{
    private Text textBox;

    private void Awake()
    {
        textBox = GetComponent<Text>();
    }

    public void SetTextFromInt(int value)
    {
        textBox.text = "Score: " + value.ToString().PadLeft(2, '0');
    }
}
