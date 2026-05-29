using UnityEngine;

public class PlayerTestScript : MonoBehaviour
{
    public TrainingPlayerShoot ProjectilePrefab;
    private GameObject enemy;
    private float timer;
    private float timer2;
    public float velocidade = 3f;
    public float raioMovimento = 10f;
    public float tempoTrocaDestino = 3f;

    private Vector3 destino;
    public Transform launchOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1){
            timer = 0;
            Instantiate(ProjectilePrefab, launchOffset.position, transform.rotation);
        }

         transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidade * Time.deltaTime
        );

        // Conta o tempo
        timer2 += Time.deltaTime;

        // Escolhe novo destino após um tempo
        if (timer2 >= tempoTrocaDestino ||
            Vector3.Distance(transform.position, destino) < 0.5f)
        {
            EscolherNovoDestino();
            timer2 = 0f;
        }
    }

    void EscolherNovoDestino()
    {
        Vector2 areaAleatoria = Random.insideUnitCircle * raioMovimento;

        destino = new Vector3(
             areaAleatoria.x,
            areaAleatoria.y,
            0f
        );
    }

}

