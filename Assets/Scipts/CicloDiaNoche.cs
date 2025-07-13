using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicloDiaNoche : MonoBehaviour
{
    public Material skyboxDia;
    public Material skyboxNoche;

    public float velocidadTransicion = 0.01f;
    public float duracionFade = 1f;

    private Material skyboxLerp;

    private float t = 0;

    private bool esNoche = false;
    private bool enTransicion = false;

    void Start()
    {
        skyboxLerp = new Material(skyboxDia.shader);
        skyboxLerp.CopyPropertiesFromMaterial(skyboxDia);
        RenderSettings.skybox = skyboxLerp;
    }

    void Update()
    {
        if (!enTransicion) return;
        t = Mathf.MoveTowards(t, esNoche ? 1f : 0f, Time.deltaTime * velocidadTransicion);
        skyboxLerp.Lerp(skyboxDia, skyboxNoche, t);
        DynamicGI.UpdateEnvironment();

        if (t == 0 || t == 1)
            enTransicion = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !enTransicion)
        {
            RenderSettings.skybox = esNoche ? skyboxDia : skyboxNoche;
            DynamicGI.UpdateEnvironment();
            esNoche = !esNoche;
            Destroy(gameObject);


            //StartCoroutine(cambiarCicloConFade(duracionFade));

        }
    }

    IEnumerator cambiarCicloConFade(float duracion)
    {
        for (float tiempo = 0; tiempo < duracion; tiempo += Time.deltaTime)
        {
            float exp = Mathf.Lerp(1f, 0f, tiempo / duracion);
            skyboxLerp.SetFloat("_Exposure", exp);
            yield return null;
        }
        skyboxLerp.SetFloat("_Exposure", 0f);
        esNoche = !esNoche;
        t = esNoche ? 0f : 1f;
        enTransicion = true;
        yield return new WaitUntil(()=> !enTransicion);
        skyboxLerp.CopyPropertiesFromMaterial(esNoche ? skyboxNoche : skyboxDia);
        RenderSettings.skybox = skyboxLerp;

        for (float tiempo = 0; tiempo < duracion; tiempo += Time.deltaTime)
        { float exp = Mathf.Lerp(0f, 1f, tiempo / duracion);
            skyboxLerp.SetFloat("_Exposure", exp);
            yield return null;
        }

        skyboxLerp.SetFloat("_Exposure", 1f);

    }
}


