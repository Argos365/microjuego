using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour{
    private Rigidbody _rigid;              // Rigid body de Player
    public float thrustForce = 60f;        // Fuerza de empuje
    private float rotationSpeed = 120f;    // Velocidad de rotación
    public GameObject gun;                 // Salida de balas
    public static int SCORE = 0;           // Puntuación 
    private float xBorderLimit = 12;       // Límite horizontal 
    private float yBorderLimit = 8;        // Límite vertical
    // Variables para pausar
    public GameObject menuPausa;
    public bool enPausa = false;

    void Start(){
        _rigid = GetComponent<Rigidbody>();
        menuPausa.SetActive(false);

    }

    void Update(){
        //Pausa al pulsar Escape
        if (Input.GetKeyDown(KeyCode.Escape)){
            if(enPausa){
                Reanudar();
            }
            else{
                Pausar();
            }
        }

        // Movimiento del jugador
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime; // Saber el eje de empuje
        Vector3 thrustDirection = transform.right;              
        _rigid.AddForce(thrustDirection * thrust * thrustForce); // Aplica la fuerza al Rigidbody

        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;   // Saber el eje de rotación
        transform.Rotate(Vector3.forward, -rotation * rotationSpeed); // Aplica la rotación del jugador

        //  Disparo de balas
        if (Input.GetKeyDown(KeyCode.Space)){
            GameObject bullet = ScriptPiscina.Instance.ObtenerBala(gun.transform.position, transform.right);
        }

        //Teletransporte en los bordes de la pantalla ---
        var newPos = transform.position;

        if (newPos.x > xBorderLimit)         
            newPos.x = -xBorderLimit + 1;    
        else if (newPos.x < -xBorderLimit)   
            newPos.x = xBorderLimit - 1;    
        else if (newPos.y > yBorderLimit)    
            newPos.y = -yBorderLimit;        
        else if (newPos.y < -yBorderLimit)  
            newPos.y = yBorderLimit;         

        transform.position = newPos;  
    }

    public void Reanudar(){
        menuPausa.SetActive(false);
        Time.timeScale = 1;
        enPausa = false;
    }

    public void Pausar(){
        menuPausa.SetActive(true);
        Time.timeScale = 0;
        enPausa = true;
    }


    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Enemy"){
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
