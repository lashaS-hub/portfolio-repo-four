using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    #region Private_Fields
    private Transform target;
    private GameObject acceleratorParent;
    private int current = 0;
    #endregion

    #region Public_Fields
    public GameObject loop;
    public Transform WorldAnchor;
    public List<Accelerator> Loops;
    public List<bool> perfectCircle;


    #endregion

    public static Spawner Instance;

    private void Awake()
    {
        Instance = this;
        PlayerController.OnChangeLevel += CreateWorld;
    }




    private void OnDestroy()
    {
        PlayerController.OnChangeLevel -= CreateWorld;
    }

    private void CreateWorld()
    {
        GameManager gm = GameManager.instance;

        DestroyWorld();


        target = Instantiate(gm.CurrentWorld.WorldPrefab, WorldAnchor).transform;
        acceleratorParent = new GameObject("Loop");
        acceleratorParent.transform.SetParent(target);
        acceleratorParent.transform.localPosition = Vector3.zero;

        loop = gm.CurrentWorld.AcceleratorPrefab;


        foreach (Models.Zone z in gm.CurrentWorld.zones)
        {
            z.material.color = z.possibleColors[Random.Range(0, z.possibleColors.Count )];
        }

    }

    private void DestroyWorld()
    {
        if (target)
        {
            Destroy(target.gameObject);
        }
    }


    public void DestroyLoop()
    {
        if (acceleratorParent)
        {
            Destroy(acceleratorParent);
        }
    }

    public void SpawnAccelerators(Transform player)
    {
        current = 0;
        LevelData currentLevel = GameManager.instance.CurrentLevel;

        Loops = new List<Accelerator>();
        Transform SpawnPosition = new GameObject().transform;
        SpawnPosition.position = this.transform.position;
        SpawnPosition.rotation = target.rotation;
        float spawnpos = currentLevel.offset;

        SpawnPosition.position = player.transform.position;
        SpawnPosition.rotation = player.transform.rotation;
        SpawnPosition.RotateAround(target.position, target.right, 20);

        for (int l = 0; l < currentLevel.loop; l++)
            for (int i = 0; i < currentLevel.Pattern.Length; i++)
            {
                // spawnpos += Random.Range(0, 3);
                SpawnPosition.RotateAround(target.position, target.right, spawnpos);//+ Random.Range(0, 5));

                if (!currentLevel.Pattern[i]) continue;
                GameObject LoopObj = Instantiate(loop, SpawnPosition.position, SpawnPosition.rotation);

                LoopObj.SetActive(l == 0 && i < 3);

                LoopObj.transform.parent = acceleratorParent.transform;
                LoopObj.name = "Obj " + i;
                Loops.Add(LoopObj.GetComponent<Accelerator>());
                perfectCircle.Add(false);
            }
        Destroy(SpawnPosition.gameObject);
    }


    public void nextLoop()
    {
        // Debug.LogError("Next");
        int c = current;
        if (c > 0)
            c -= 1;
        else
            c = Loops.Count - 1;

        Loops[c].HideAccelerator();
        current++;
        if (current == Loops.Count)
            current = 0;
        
        Loops[current].gameObject.SetActive(true);

        Loops[(current + 1) % Loops.Count].gameObject.SetActive(true);
        Accelerator.nextOne = Loops[current];
    }

    // public void Generate()
    // {
    //     Loops = new List<GameObject>();
    //     Transform SpawnPosition = new GameObject().transform;
    //     SpawnPosition.position = this.transform.position;
    //     SpawnPosition.rotation = target.rotation;
    //     float spawnpos = 30f;
    //     SpawnPosition.RotateAround(target.position, target.right, spawnpos);
    //     for (int i = 0; i < 10; i++)
    //     {
    //         // spawnpos += Random.Range(0, 3);
    //         SpawnPosition.RotateAround(target.position, target.right, spawnpos + Random.Range(0, 5));
    //         GameObject LoopObj = Instantiate(loop, SpawnPosition.position, SpawnPosition.rotation);
    //         LoopObj.transform.parent = target;
    //         LoopObj.name = "Obj " + i;
    //         Loops.Add(LoopObj);
    //         perfectCircle.Add(false);
    //     }
    //     Destroy(SpawnPosition.gameObject);
    // }

    // public bool PerfectCirlce(bool v)
    // {
    //     perfectCircle[current] = v;
    //     NextObject.transform.Find("Perfecto").gameObject.SetActive(v);
    //     return perfectCircle.TrueForAll((x) => x);
    // }
}
