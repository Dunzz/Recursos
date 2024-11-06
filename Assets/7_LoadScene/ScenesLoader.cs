using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    public Slider barraDeProgreso;
    public TextMeshProUGUI textoDeCarga;

    private void Start()
    {
        CargarEscena("Sc_Animations_Weapon_2_V1");
    }

    public void CargarEscena(string nombreEscena)
    {
        StartCoroutine(CargarEscenaConProgreso(nombreEscena));
    }

    private IEnumerator CargarEscenaConProgreso(string nombreEscena)
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);

        while (!operacion.isDone)
        {
            float progreso = Mathf.Clamp01(operacion.progress / 0.9f);

            if (barraDeProgreso != null)
                barraDeProgreso.value = progreso;

            if (textoDeCarga != null)
                textoDeCarga.text = "Cargando... " + (progreso * 100f).ToString("F0") + "%";

            yield return null;
        }
    }
}
