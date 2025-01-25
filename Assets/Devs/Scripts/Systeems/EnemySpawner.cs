using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject[] enemyTypes;
    private Vector3 batoffset;


    private float minimumCD;
    private float maximumCD;

    private float countdown;

    private void Start()
    {
        SetSpawnTimer();

        batoffset.y = 5f;

        batoffset = new Vector2(this.transform.position.x, transform.position.y + batoffset.y);
        minimumCD = 5f;
        maximumCD = 10f;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            for (int i = 0; i < enemyTypes.Length; i++)
            {


                if (enemyTypes[1])
                {
                    Instantiate(enemyTypes[0], batoffset, Quaternion.identity);
                    SetSpawnTimer();
                }
                Instantiate(enemyTypes[Random.Range(0, i)], spawnPoint.transform.position, Quaternion.identity);
                SetSpawnTimer();
            }
        }
    }

    private void SetSpawnTimer()
    {
        countdown = Random.Range(minimumCD, maximumCD);
    }
}
