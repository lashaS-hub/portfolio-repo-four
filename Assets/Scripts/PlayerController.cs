using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float score;
    private float lastDistance = 10000;
    private float maxDistance = 10000;
    private AccelType lastState;

    private Animator animator;
    private Camera cam;
    private float fieldOfView;

    private LevelData CurrentLevel;

    private float trailValue;

    private float degPerSec;
    private float rotateSpeed;
    private float speedAccelerator;
    private float speedModifier;
    private float MediumDist;
    private float PerfectDist;
    private float goal;

    public int goodCombo;
    public int count;
    // badCombo,

    #region Public_Fields
    public Color[] IndicatorColors;
    public Material IndicatorMaterial;
    public ParticleSystem Warp;
    public ParticleSystem AccelSparks;
    public Transform WorldAnchor;
    public Transform SkinContainer;

    public Spawner sp;
    public CamShaker cameraShaker;
    // public Indicator indicator;

    bool clicked = false;
    bool canPlay = false;
    bool canTouch = false;
    bool isTraveling = false;



    #region delegates_and_events

    public delegate void Click(AccelType i);
    public static event Click OnClick;

    public delegate void UpdateScore(int s);
    public delegate void UpdateProgress(float progress);
    // public delegate void GameOver(float score);
    public static event UpdateScore OnUpdateScore;
    // public static event UpdateScore OnAddScore;
    public static event UpdateScore OnCombo;
    public static event UpdateProgress OnUpdateProgress;

    public static event System.Action OnChangeLevel;

    public static event UpdateProgress OnGameOver;

    #endregion

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        animator = GetComponent<Animator>();
        GameUiController.OnStartGame += StartGame;
        SkinUI.OnChangeSkin += ChangeSkin;
        OnGameOver += GameOver;
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // Debug.Log("Start");
        cam = Camera.main;
        fieldOfView = cam.fieldOfView;
        OnChangeLevel();
        SetStats();
    }


    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        GameUiController.OnStartGame -= StartGame;
        SkinUI.OnChangeSkin -= ChangeSkin;
        OnGameOver -= GameOver;
    }

    

    public void SetStats()
    {
        
        isTraveling = false;
        goodCombo = 0;
        lastDistance = 10000;
        CurrentLevel = GameManager.instance.CurrentLevel;

        IndicatorMaterial.SetColor("_TintColor", IndicatorColors[0]);

        PerfectDist = CurrentLevel.perfectDist;
        speedModifier = CurrentLevel.speedModifier;
        MediumDist = CurrentLevel.mediumDist;

        goal = CurrentLevel.goal;
        degPerSec = CurrentLevel.startDPS;
        
        score = Mathf.Max(score, CurrentLevel.minScore);

        clicked = false;

        count = 0;
        OnUpdateProgress(count / (float)goal);
        // Progress();
    }

    public void StartGame()
    {
        StartCoroutine(StartPlaying());
    }

    IEnumerator StartPlaying(){


        // Debug.Log("WAAA");
        // CurrentLevel = GameManager.instance.CurrentLevel;

        sp = Spawner.Instance;


        fieldOfView = 120;
        // world = sp.target.GetComponent<WorldBehaviour>();
        // PlayerObject = world.Train;

        // Camera.main.gameObject.AddComponent<Follower>().target = transform;
        // float startUp = 0;
        // while (startUp < degPerSec)
        // {
        //     // Orbit(WorldAnchor, startUp);
        //     startUp += 0.1f;
        //     yield return null;
        // }
        canPlay = true;

        yield return 0;
        canTouch = true;
        sp.SpawnAccelerators(this.transform);
        UpdateMaxDistance();



    }

    private void ChangeSkin(int i)
    {
        var skin = GameManager.instance.GetSkinByIndex(i);
        foreach (Transform child in SkinContainer)
        {
            Destroy(child.gameObject);
        }
        Instantiate(skin.prefab, SkinContainer);
    }

    private void UpdateMaxDistance()
    {
        maxDistance = Vector3.Distance(transform.position, Accelerator.nextOne.transform.position);
    }

    void Update()
    {
        // if (!canPlay) return;
        Orbit(WorldAnchor, degPerSec);

        cam.fieldOfView = Vector3.Slerp(Vector3.right * cam.fieldOfView, Vector3.right * fieldOfView, Time.deltaTime * 3).x;
        if (canTouch)
        {
            Looper();

            if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Space))
                Touch();
        }
    }

    #endregion

    public AccelType CheckLoop()
    {
        if (lastDistance > MediumDist)
        {
            return AccelType.BAD;
        }
        if (lastDistance <= PerfectDist)
        {
            return AccelType.PERFECT;
        }
        return AccelType.GOOD;
    }

    void Touch()
    {
        if (!clicked)
        {
            Debug.LogWarning(lastState);
            Accelerator.nextOne.Activate(lastState, degPerSec / 10);
            OnClick(lastState);
            switch (lastState)
            {
                case AccelType.BAD:
                    // if (degPerSec > speedModifier + badCombo)
                    //     degPerSec -= speedModifier + (.3f * badCombo);
                    // // degPerSec = Mathf.Min(degPerSec, 0);
                    // badCombo++;
                    // goodCombo = 0;
                    OnGameOver(score);
                    AdManager.Instance.ShowAnything();

                    

                    //world.FailSparks.Play(true);
                    break;
                case AccelType.GOOD:
                    goodCombo = 0;
                    // badCombo = 0;
                    // degPerSec = CurrentLevel.minSpeed;
                    score += 100;
                    Boost();
                    Progress();
                    

                    // OnAddScore((int)AccelType.GOOD);
                    // degPerSec += speedModifier;
                    break;
                case AccelType.PERFECT:
                    Progress();

                    score += 150*goodCombo;
                    // OnAddScore(2);
                    goodCombo++;
                    // badCombo = 0;
                    // Debug.LogError("WAT");
                    speedAccelerator = 30 / degPerSec;
                    degPerSec += speedAccelerator * speedModifier + (0.1f * goodCombo);
                    // sp.NextObject.GetComponent<Animator>().SetTrigger("Accel");
                    if (goodCombo >= 2)
                    {
                        OnCombo(goodCombo);
                        
                        Boost();
                        
                    }
                    break;
            }

            // rotateSpeed += 1f;

            // trailValue = Mathf.Max(lastState*2,0);

            // float norm = CurrentLevel.NormalizeSpeed(degPerSec);

            // if(!(norm > 0 && norm < 1))
            //     canTouch = false;


            // (lastState == 1);            
            // bool perfect = sp.PerfectCirlce(lastState == 1);
            // if(perfect)
            // {
            //     canTouch = false;
            //     world.FirstPersonCam.SetActive(true);
            //     world.FirstPersonCam.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
            //     degPerSec = CurrentLevel.goalSpeed;
            //     Camera.main.gameObject.SetActive(false);
            // }
            OnClick(lastState);
        }
        // GameManager.instance.CheckPortal();
        clicked = true;
    }
    
    void Boost()
    {
        cameraShaker.Shake();
        fieldOfView = Mathf.Min(fieldOfView + 3f, 140f);
        PlayWarp(count / 15f);
        // if(count % 2 == 0){

        // }
    }

    void Progress()
    {
        count++;
        OnUpdateProgress(count / (float)goal);
        Travel();
    }


    void Travel()
    {
        if (count == goal && !isTraveling)
        {
            canTouch = false;
            Accelerator.nextOne = null;
            animator.SetTrigger("Travel");
            sp.DestroyLoop();
            
            isTraveling = true;
        }
    }




    void Orbit(Transform target, float degree)
    {
        transform.RotateAround(target.position, target.right, degree * Time.deltaTime);
        transform.eulerAngles += Vector3.forward * Time.deltaTime * rotateSpeed;

        // foreach (var item in world.trailRenderers)
        // {
        //     item.widthMultiplier = Mathf.Lerp(item.widthMultiplier, trailValue, Time.deltaTime);
        // }
    }

    void Looper()
    {
        if (score > 0)
        {
            // if (degPerSec < goal)
            //     degPerSec -= degPerSec * Time.deltaTime * 0.05f;
            score -= .3f;
            OnUpdateScore((int)score);
        }else{
            OnGameOver(score);
            return;
        }

        float dis = Vector3.Distance(transform.position, Accelerator.nextOne.transform.position);
        if (dis > lastDistance)
        {
            if (!clicked)
            {
                score = Mathf.Max( score - 200, 0);
            }
            // Debug.LogError("Next");
            clicked = false;
            dis = 10000;
            // Accelerator.nextOne.indicator.ResetIndicator();
            sp.nextLoop();
            UpdateMaxDistance();
        }

        lastDistance = dis;
        var currentState = CheckLoop();
        if (currentState != lastState)
        {
            lastState = currentState;
            IndicatorMaterial.
            SetColor("_TintColor", IndicatorColors[(int)currentState]);
        }
        if(!clicked)
            Accelerator.nextOne.indicator.FillImage(lastDistance / maxDistance, IndicatorColors[(int)currentState]);
    }

    void GameOver(float score)
    {
        // degPerSec = 10f;
        canTouch = false;
        // sp.DestroyLoop();
    }
    public void PlayWarp(float power)
    {
        Debug.Log(power);
        var a = Warp.main;
        a.startSizeMultiplier = power;
        if (Warp != null)
            Warp.Play();
        if(AccelSparks != null)
            AccelSparks.Play();
    }

    public void ChangeWorld()
    {
        if (OnChangeLevel != null)
        {
            OnChangeLevel();
            SetStats();
            StartGame();
        }
    }
    
}
