using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MovingFortressAI : MonoBehaviour
{
   public bool startSpawning = false;

    [Header("spawnStats")]


    [Range(1f, 10f)]
    public float FirstSpawnTime;


    [Range(2f, 60f)]
    public float SpawnSpeed;
    float spawnS;
    


    BossSpawnEnemy BossSpawnEnemy;


    private void Start()
    {

        BossSpawnEnemy = GetComponent<BossSpawnEnemy>();

        spawnS = FirstSpawnTime;
    }


    private void Update()
    {
        if (PauseManager.Instance != null)
        {
            if (PauseManager.Instance.IsPaused == false)
            {

                if (startSpawning)
                {
                    spawnS -= Time.deltaTime;

                    if (spawnS <= 0)
                    {
                        spawnS = SpawnSpeed;
                        BossSpawnEnemy.SetSTrigger(true);
                    }

                }
            }
        }
    }


    // Start is called before the first frame update




}
