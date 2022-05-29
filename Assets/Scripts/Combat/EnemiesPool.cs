using System.Collections.Generic;
using UnityEngine;

public class EnemiesPool : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;

    private Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();

    public void Awake()
    {
        foreach (var platfrom in levelManager.platformsForRead)
        {
            if (!(platfrom.encounter is CombatEncounterSO))
                continue;

            Queue<GameObject> tempQueue = new Queue<GameObject>();
            foreach (var enemy in (platfrom.encounter as CombatEncounterSO).enemies)
            {
                if (enemy == null)
                    continue;

                GameObject tempEnenmy = Instantiate(enemy.gameObject);
                string tempTittle = tempEnenmy.GetComponent<Enemy>().stats.title;
                tempEnenmy.SetActive(false);

                if (!pool.ContainsKey(tempTittle))
                    pool.Add(tempTittle, new Queue<GameObject>());

                pool.TryGetValue(tempTittle, out tempQueue);
                tempQueue.Enqueue(tempEnenmy);
            }
        }
    }

    public GameObject GetEnemy(string title)
    {
        Queue<GameObject> tempQueue = new Queue<GameObject>();
        pool.TryGetValue(title, out tempQueue);

        return tempQueue.Dequeue();
    }

    public void ReturnEnemy(string title, GameObject enemy)
    {
        Queue<GameObject> tempQueue = new Queue<GameObject>();
        if (!pool.TryGetValue(title, out tempQueue))
        {
            pool.Add(title, tempQueue);
        }

        tempQueue.Enqueue(enemy);
    }
}
