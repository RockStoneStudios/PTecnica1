using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    private Queue<GameObject> pool = new Queue<GameObject>();
    private int numSphere = 0;
    private int poolSize = 10;
    private float xRange = 15;
    private float zRange = 15;
    
    [SerializeField] TextMeshProUGUI counterText;
    private Coroutine createCoroutine;

    void Start()
    {
        AddToPool(poolSize);
        createCoroutine = StartCoroutine(CreateSphere());
        UpdateCounter();
    }

    void AddToPool(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = Vector3.zero;
            sphere.transform.SetParent(transform);
            sphere.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            sphere.SetActive(false);
            pool.Enqueue(sphere);
        }
    }

    void SpawnerSphere()
    {
        if (numSphere >= 10) return;

        if (pool.Count > 0)
        {
            GameObject sphere = pool.Dequeue();
            sphere.transform.position = new Vector3(Random.Range(-xRange, xRange), 0.51f, Random.Range(-zRange, zRange));
            sphere.SetActive(true);

            
            if (!sphere.TryGetComponent<Sphere>(out _))
                sphere.AddComponent<Sphere>();

            numSphere++;
            UpdateCounter();
        }
        else
        {
            AddToPool(1);
            SpawnerSphere();
        }
    }

    public void EnqueueSphere(GameObject sphere)
    {
        if (sphere.activeSelf)
        {
            sphere.SetActive(false);
            pool.Enqueue(sphere);

            numSphere--;
            UpdateCounter();

            if (numSphere < 10 && createCoroutine == null)
            {
                createCoroutine = StartCoroutine(CreateSphere());
            }
        }
    }

    void UpdateCounter()
    {
        counterText.text = numSphere.ToString();
    }

    private IEnumerator CreateSphere()
    {
        while (numSphere < 10)
        {
            yield return new WaitForSeconds(2f);
            SpawnerSphere();
        }
        createCoroutine = null;
    }
}
