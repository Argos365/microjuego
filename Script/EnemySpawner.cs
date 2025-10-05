using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRatePerMinute = 30f;
    private float spawnNext = 0f;
    public float xLimit = 2000f;
    public float fallSpeed = 2.5f;

    void Update(){
        if (Time.time > spawnNext){
            spawnNext = Time.time + 60f / spawnRatePerMinute;

            float randX = Random.Range(-xLimit, xLimit);
            Vector3 spawnPos = new Vector3(randX, 10f, 0f);
            Vector3 direccion = Vector3.down;
            GameObject meteor = PiscinaMeteor.Instance.ObtenerMeteor(spawnPos, direccion, fallSpeed);
        }

    }

    public static void Dividir(Vector3 spawnPos){
        float offset = 1f;
        float impulso = 2f;
        float angle = 30f;

        Vector3 dir1 = Quaternion.Euler(0, 0, -angle) * Vector3.down;
        Vector3 dir2 = Quaternion.Euler(0, 0, angle) * Vector3.down;

        GameObject meteor1 = PiscinaMeteor.Instance.ObtenerMeteor(spawnPos + new Vector3(-offset, 0f, 0f), dir1, 2f);
        GameObject meteor2 = PiscinaMeteor.Instance.ObtenerMeteor(spawnPos + new Vector3(offset, 0f, 0f), dir2, 2f);

        if (meteor1 != null){
            meteor1.transform.localScale *= 0.5f;
            Rigidbody rb1 = meteor1.GetComponent<Rigidbody>();
            if (rb1 != null)
                rb1.AddForce(dir1.normalized * impulso, ForceMode.Impulse);
        }

        if (meteor2 != null){
            meteor2.transform.localScale *= 0.5f;
            Rigidbody rb2 = meteor2.GetComponent<Rigidbody>();
            if (rb2 != null)
                rb2.AddForce(dir2.normalized * impulso, ForceMode.Impulse);
        }
    }
}
