using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float limiteY = -10f;
    public float minScale = 0.35f;

    void Update() //Este script comprueba constantemente si cada instancia de meteorito se ha salido de cámara, si sí, los mete a la piscina
    {
        if (transform.position.y < limiteY){
            if (transform.localScale.x < minScale){
                transform.localScale = Vector3.one * minScale;
            }

            gameObject.SetActive(false);
        }
    }
}
