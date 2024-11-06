using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Animation_Points : MonoBehaviour
{
    public TextMeshProUGUI txt_points;
    private int score = 0;
    public void Score(int points)
    {
        score += points;
        txt_points.text = "Puntos: " + score.ToString();
    }
}
