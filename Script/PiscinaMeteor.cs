using UnityEngine;
using System.Collections.Generic;

public class PiscinaMeteor : MonoBehaviour
{
    [SerializeField] private GameObject prefabMeteor;
    [SerializeField] private int tamPiscina = 10;

    private List<GameObject> listaMeteor = new List<GameObject>();

    private static PiscinaMeteor instance;
    public static PiscinaMeteor Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        for (int i = 0; i < tamPiscina; i++)
        {
            GameObject meteor = Instantiate(prefabMeteor, transform.position, Quaternion.identity);
            meteor.SetActive(false);
            listaMeteor.Add(meteor);
        }
    }

    public GameObject ObtenerMeteor(Vector3 posicion, Vector3 direccion, float velocidad)
    {
        foreach (GameObject meteor in listaMeteor)
        {
            if (!meteor.activeInHierarchy)
            {
                meteor.transform.position = posicion;
                meteor.transform.rotation = Quaternion.identity;

                Rigidbody rb = meteor.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Resetear física antes de reutilizar
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;

                    // Asignar nueva velocidad
                    rb.linearVelocity = direccion.normalized * velocidad;
                }

                meteor.SetActive(true);
                return meteor;
            }
        }
        return null;
    }
}
