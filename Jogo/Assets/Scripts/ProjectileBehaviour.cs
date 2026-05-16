using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float Speed = 4.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * Speed;
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        Destroy(gameObject);
    }


}
