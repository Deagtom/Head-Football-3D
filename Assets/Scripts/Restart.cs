using UnityEngine;

public class Restart : MonoBehaviour
{
    public void Re()
    {
        DestroyWithTag("Object");
        Destroy(GameObject.FindGameObjectWithTag("Ball"));
        DestroyWithTag("Player");

        SpawnElements spawnElements = GameObject.FindGameObjectWithTag("Trigger").GetComponent<SpawnElements>();
        spawnElements.SpawnNewPlayers();
        spawnElements.SpawnNewBall();
        spawnElements.SpawnNewObjects();
        Countdown.Timer += 4;
    }

    private void DestroyWithTag(string destroyTag)
    {
        GameObject[] destroyObject;
        destroyObject = GameObject.FindGameObjectsWithTag(destroyTag);
        foreach (GameObject oneObject in destroyObject)
            Destroy(oneObject);
    }
}