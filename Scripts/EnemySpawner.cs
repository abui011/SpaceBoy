using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public List<WaveConfig> WaveConfigs;
    // Use this for initialization
    int startingwave = 0;


    void Start () {
        StartCoroutine(SpawnAllWaves());
	}
	

    private IEnumerator SpawnAllWaves()
    {
        for (int i = startingwave; i < WaveConfigs.Count; i++)
        {
            Debug.Log("start");
            var currentWave = WaveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesinWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesinWave(WaveConfig wave)
    {
        for (int i = 0; i < wave.GetNoE(); i++)
        {
            var newEnemy = Instantiate(
                wave.GetEnemyPrefab(),
                wave.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(wave);            
            yield return new WaitForSeconds(wave.GetTimeBS());
        }

    }
}
