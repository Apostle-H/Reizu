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

            for (int i = 0; i < platfrom.encounters.Length; i++)
            {
                if (!(platfrom.encounters[i] is CombatEncounterSO))
                    continue;

                Queue<GameObject> tempQueue = new Queue<GameObject>();
                foreach (var enemy in (platfrom.encounters[i] as CombatEncounterSO).enemies)
                {
                    if (enemy == null)
                        continue;

                    GameObject tempEnenmy = Instantiate(enemy.gameObject);
                    string tempTittle = tempEnenmy.GetComponent<AutomaticFighter>().Title;
                    tempEnenmy.SetActive(false);

                    if (!pool.ContainsKey(tempTittle))
                        pool.Add(tempTittle, new Queue<GameObject>());

                    pool[tempTittle].Enqueue(tempEnenmy);
                }
            }
        }
    }

    public GameObject GetEnemy(string title) => pool[title]?.Dequeue();

    public void ReturnEnemy(string title, GameObject enemy)
    {
        if (!pool.ContainsKey(title))
            pool.Add(title, new Queue<GameObject>());

        pool[title].Enqueue(enemy);
    }
    
}
