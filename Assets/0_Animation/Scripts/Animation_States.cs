using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Animation_States : MonoBehaviour
{
    public Animator weapon;
    public Rig Rig_Aim_Body_1;
    public Rig Rig_Aim_Body_2;
    public MultiAimConstraint Rig_Head;

    public bool isOnRun;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isOnRun = true;
            weapon.Play("Weapon_OnRun");
            StartCoroutine(CambiarValor(Rig_Aim_Body_1, 1, 0, 0.2f));
            StartCoroutine(CambiarValor(Rig_Aim_Body_2, 1, 0, 0.2f));
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isOnRun = false;
            weapon.Play("Weapon_OnWalk");
            StartCoroutine(CambiarValor(Rig_Aim_Body_1, 0, 1, 0.2f));
            StartCoroutine(CambiarValor(Rig_Aim_Body_2, 0, 1, 0.2f));
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(CambiarValorHead(Rig_Head, 0.2f, 1, 0.2f));
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StartCoroutine(CambiarValorHead(Rig_Head, 1, 0.2f, 0.2f));
        }
    }

    IEnumerator CambiarValor(Rig rig, float inicio, float fin, float duracion)
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;

            rig.weight = Mathf.Lerp(inicio, fin, tiempo / duracion);

            yield return null;
        }
    }

    IEnumerator CambiarValorHead(MultiAimConstraint rig, float inicio, float fin, float duracion)
    {
        float tiempo = 0f;
        var source = rig.data.sourceObjects;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;

            source.SetWeight(0, Mathf.Lerp(inicio, fin, tiempo / duracion));

            yield return null;
        }

        rig.data.sourceObjects = source;
    }
}
