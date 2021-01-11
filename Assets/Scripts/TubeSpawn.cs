using UnityEngine;

public class TubeSpawn : MonoBehaviour
{
    public float maxTime = 1f;
    public float height = 1.45f;

    private float _timer = 1;
    
    void Update()
    {
        if (!GameManager.S.GetIsActive() || GameManager.S.isGameOver) return;

        if (_timer > maxTime)
        {
            SpawnNewTupe();
            _timer = 0;
        }

        _timer += 1 * Time.deltaTime;
    }

    private void SpawnNewTupe(){
        GameObject newTupe = Instantiate(GameManager.S.biomList[GameManager.S.GetCurrentBiom()].tubeList[Random.Range(0, GameManager.S.biomList[GameManager.S.GetCurrentBiom()].tubeList.Count)]);
        newTupe.transform.position = transform.position + new Vector3(0, Random.Range(-height, height));
    }
}