using UnityEngine;
using System.Collections.Generic;

public class ScriptPiscina : MonoBehaviour
{
    [SerializeField] private GameObject prefabBala;   // Asignar en el inspector
    [SerializeField] private int tamPiscina = 20;

    private List<GameObject> listaBalas = new List<GameObject>();

    private static ScriptPiscina instance;
    public static ScriptPiscina Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }
        for (int i = 0; i < tamPiscina; i++){
            GameObject bala = Instantiate(prefabBala, transform.position, Quaternion.identity);
            bala.SetActive(false);
            listaBalas.Add(bala);
        }
    }

    public GameObject ObtenerBala(Vector3 posicion, Vector3 direccion){
        foreach (GameObject bala in listaBalas){
            if (!bala.activeInHierarchy){
                bala.transform.position = posicion;
                bala.transform.rotation = Quaternion.identity;
                bala.GetComponent<Bullet>().targetVector = direccion;
                bala.SetActive(true);
                return bala;
            }
        }
        return null;
    }
}
