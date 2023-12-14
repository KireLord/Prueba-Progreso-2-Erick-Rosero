using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeAsteroidesController : MonoBehaviour
{
    public GameObject asteroide;

    float timer = 0.0f;
    float waitTime = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
        {

            // Durante todo el juego se va a instanciar un Prefab de tipo Asterioide en posiciones aleatorias.
            Instantiate(asteroide, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);

            timer = 0.0f;
        }
    }
}
