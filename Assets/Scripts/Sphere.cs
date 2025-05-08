using UnityEngine;

public class Sphere : MonoBehaviour
{
    private Spawner spawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawner = FindAnyObjectByType<Spawner>();
    }

    void OnMouseDown()
    {
        spawner.EnqueueSphere(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
