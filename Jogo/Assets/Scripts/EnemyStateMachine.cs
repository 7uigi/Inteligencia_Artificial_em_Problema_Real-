using UnityEngine;
using UnityEngine.InputSystem;
public class EnemyStateMachine : MonoBehaviour
{
    private float timer;
    public float range;
    public float maxDistance;
    public float movementSpeed = 1f;
    private float movement = 5f;
    [SerializeField] private float speed = 5f;

    private GameObject player;
    private Vector2 wayPoint;
    public EnemyFireBulletScript ProjectilePrefabFire;
    public EnemyBulletScript ProjectilePrefabIce;
    public Transform LaunchOffset;
    public int Action;
    private int lastAction = -1;

    public enum Enemy_STATE
    {
        Fire_Attack,
        Ice_Atack,
        Melee_Atack,
        Dodge,
    }
    public Enemy_STATE enemyState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();

        switch (enemyState)
        {
            case Enemy_STATE.Fire_Attack:
                {
                    //transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
                    timer += Time.deltaTime;

                    if (timer > 2)
                    {
                        timer = 0;
                        Instantiate(ProjectilePrefabFire, LaunchOffset.position, transform.rotation);
                    }

                    if (Keyboard.current.hKey.isPressed && Vector2.Distance(transform.position, player.transform.position) <= range)
                    {
                        enemyState = Enemy_STATE.Melee_Atack;
                    }
                    if (Keyboard.current.kKey.isPressed)
                    {
                        enemyState = Enemy_STATE.Dodge;
                    }
                    if (Keyboard.current.lKey.isPressed)
                    {
                        enemyState = Enemy_STATE.Ice_Atack;
                    }
                    break;
                }
            case Enemy_STATE.Ice_Atack:
                {
                    //transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * movementSpeed;
                    timer += Time.deltaTime;

                    if (timer > 2)
                    {
                        timer = 0;
                        Instantiate(ProjectilePrefabIce, LaunchOffset.position, transform.rotation);
                    }

                    if (Keyboard.current.hKey.isPressed && Vector2.Distance(transform.position, player.transform.position) <= range)
                    {
                        enemyState = Enemy_STATE.Melee_Atack;
                    }
                    if (Keyboard.current.kKey.isPressed)
                    {
                        enemyState = Enemy_STATE.Dodge;
                    }
                    if (Keyboard.current.jKey.isPressed)
                    {
                        enemyState = Enemy_STATE.Fire_Attack;
                    }
                    break;
                }
            case Enemy_STATE.Melee_Atack:
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, wayPoint) < range)
                    {
                        SetNewDestination();
                    }
                    if (Keyboard.current.jKey.isPressed)
                    {
                        enemyState = Enemy_STATE.Fire_Attack;
                    }
                    if (Keyboard.current.kKey.isPressed)
                    {
                        enemyState = Enemy_STATE.Dodge;
                    }
                    if (Keyboard.current.lKey.isPressed)
                    {
                        enemyState = Enemy_STATE.Ice_Atack;
                    }
                    break;
                }
            case Enemy_STATE.Dodge:
                {
                    Debug.Log("dodge");

                    // direção diagonal
                    Vector2 diagonalDir = new Vector2(1, 1).normalized;

                    // move na diagonal
                    transform.position += (Vector3)(diagonalDir * speed * Time.deltaTime);

                    // contador de distância
                    movement += speed * Time.deltaTime;

                    // quando atingir distância X
                    if (movement >= maxDistance)
                    {
                        movement = 10;

                        // move para lado oposto
                        Vector2 oppositeDir = Vector2.left;

                        transform.position += (Vector3)(oppositeDir * speed * Time.deltaTime);
            
                    }

                    if (Keyboard.current.hKey.isPressed)
                    {
                        enemyState = Enemy_STATE.Melee_Atack;
                    }
                    if (Keyboard.current.jKey.isPressed)
                    {
                        enemyState = Enemy_STATE.Fire_Attack;
                    }
                    if (Keyboard.current.lKey.isPressed)
                    {
                        enemyState = Enemy_STATE.Ice_Atack;
                   }
                    break;
                }

        }
    }
    void SetNewDestination()
    {
        wayPoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));
    }
    void ChangeState()
    {
        // só troca se Action mudou
        if (Action == lastAction)
            return;

        lastAction = Action;

        switch (Action)
        {
            case 0:
                enemyState = Enemy_STATE.Fire_Attack;
                break;

            case 1:
                enemyState = Enemy_STATE.Ice_Atack;
                break;

            case 2:
                // só entra em melee se estiver perto
                if (Vector2.Distance(transform.position, player.transform.position) <= range)
                {
                    enemyState = Enemy_STATE.Melee_Atack;
                }
                break;

            case 3:
                enemyState = Enemy_STATE.Dodge;
                break;
        }
    }
}
