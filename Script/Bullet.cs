using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;

    private float lifeTimer;
    private Text scoreText;
    private float escalaInicial = 0.35f;

    private void Awake(){
        // Cacheamos el texto de puntuación
        GameObject go = GameObject.FindWithTag("UI");
        if (go != null)
            scoreText = go.GetComponent<Text>();
    }

    private void OnEnable(){
        lifeTimer = 0f;
    }

    void Update(){
        transform.Translate(targetVector.normalized * speed * Time.deltaTime, Space.World);

        lifeTimer += Time.deltaTime;
        if (lifeTimer >= maxLifeTime)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Enemy")){
            // Actualizar puntuación
            PlayerScript.SCORE++;
            if (scoreText != null)
                scoreText.text = "Puntos: " + PlayerScript.SCORE;

            // Dividir meteorito si es suficientemente grande
            if (collision.gameObject.transform.localScale.x >= 0.35f){
                EnemySpawner.Dividir(collision.gameObject.transform.position);
            }

            // Desactivar meteoro y bala
            collision.gameObject.transform.localScale = Vector3.one * escalaInicial;
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
