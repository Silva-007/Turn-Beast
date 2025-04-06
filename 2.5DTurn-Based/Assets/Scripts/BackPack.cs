using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackPack : MonoBehaviour
{
    public static BackPack  Instance;
    //public string mainMenuSceneName = "OverworldScene";
    private bool isPaused = false;
    private PartyManager partyManager;

 

    [Header("UI")]
    [SerializeField] private GameObject pauseUI; // 拖入PauseUI父物体
    [SerializeField] private GameObject itemquantity;
    [SerializeField] private TextMeshProUGUI Propeffect;
    [SerializeField] private TextMeshProUGUI CurrentQuantity;
    [SerializeField] private TextMeshProUGUI MaxQuantity;

    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Level;
    [SerializeField] private TextMeshProUGUI CurrrentHealth;
    [SerializeField] private TextMeshProUGUI MaxHealth;
    [SerializeField] private TextMeshProUGUI Atk;
    [SerializeField] private TextMeshProUGUI Spe;




    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            // 初始化时强制隐藏
            if (pauseUI != null)
            {
                pauseUI.SetActive(false);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        partyManager = GameObject.FindAnyObjectByType<PartyManager>();

        BackpackTable backpacktable = Resources.Load<BackpackTable>("DataTable/packageTable");


    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 每次加载场景都强制关闭暂停状态
        //ForceResume();
        if (pauseUI != null)
        {
            pauseUI.SetActive(false);
        }
    }

    void Update()
    {
        
        //Debug.Log("按键:"+Input.GetKeyDown(KeyCode.R));
        //若当前的活动场景为xxx并且按下ESC按钮


        if (SceneManager.GetActiveScene().name == "OverworldScene" && Input.GetKeyDown(KeyCode.R))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseUI.SetActive(isPaused); // 根据状态显示/隐藏
        List<PartyMember> CurrentParty = new List<PartyMember>();
        CurrentParty = partyManager.GetAliveParty();


        // Debug.Log("名字:"+CurrentParty[0].MemberName+"等级:"+CurrentParty[0].Level+"当前体力:"+ CurrentParty[0].CurrHealth+ "最大体力:" + CurrentParty[0].MaxHealth);
        // CurrentParty[0].Level += 1;

        DataUpdate(CurrentParty);
        CurrentQuantity.text = "2";
        
        /*
        BackpackTable backpacktable = Resources.Load<BackpackTable>("DataTable/packageTable");
        PackageTableItem pt = new PackageTableItem();
         pt.id = 3;
         backpacktable.DataList.Add(pt);
        
        foreach (PackageTableItem packageItem in backpacktable.DataList)
        {
            Debug.Log(string.Format("id:{0}, name:{1}", packageItem.id, packageItem.name));
        }
*/
       

        
    }
    public void DataUpdate(List<PartyMember> CurrentParty)
    {
          Name.text = CurrentParty[0].MemberName;
          Level.text = ""+CurrentParty[0].Level;
        CurrrentHealth.text = "" + CurrentParty[0].CurrHealth;
        MaxHealth.text = "" + CurrentParty[0].MaxHealth;
        Spe.text = "" + CurrentParty[0].Initiative;
        Atk.text = "" + CurrentParty[0].Strength;
    }

    // 其他方法保持原样...

    public void ResumeGame()
    {
        TogglePause();
    }
}
