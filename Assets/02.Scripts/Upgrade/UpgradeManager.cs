using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // 업그레이드 관리자: 업그레이드들에 대한 무결성과 생성,조회,수정,삭제 등과 관련된 게임 로직
    private static UpgradeManager _instance = null;
    public static UpgradeManager Instance => _instance;


    // 업그레이드 도메인 클래스들
    [SerializeField] private Upgrade[] _upgrades;
    public Upgrade[] Upgrades => _upgrades;

    // 업그레이드 UI들
    [SerializeField] private UI_Upgrade[] _uiUpgrades;

    private const string UpgradeSaveDataKey = "UpgradeSaveData";

    private void Awake()
    {
        // 늦게 태어난 매니저는 나는 늦었네~ 하면서 삭제
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        Load();
        RefreshUI();
    }

    public void LevelUp(int index)
    {
        //TODO :
        Upgrade upgrade = _upgrades[index];

        if (ScoreManager.Instance.Score < upgrade.Cost)
        {
            return;
        }

        ScoreManager.Instance.Spend(upgrade.Cost);

        _upgrades[index].LevelUp();

        Save();

        RefreshUI();
    }

    // UI 갱신
    private void RefreshUI()
    {
        foreach (UI_Upgrade uiUpgrade in _uiUpgrades)
        {
            uiUpgrade.Refresh();
        }
    }

    private void Save()
    {
        // 데이터 저장은 유의미한 것만 저장.
        // 그래서 레벨만 저장

        UpgradeSaveData saveData = new UpgradeSaveData(_upgrades.Length);

        for (int i = 0; i < _upgrades.Length; i++)
        {
            saveData.Name[i] = _upgrades[i].Name;
            saveData.Level[i] = _upgrades[i].Level;
        }

        string json = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString(UpgradeSaveDataKey, json);
        PlayerPrefs.Save();

        PlayerPrefs.Save();
    }

    private void Load()
    {
        if (PlayerPrefs.HasKey(UpgradeSaveDataKey)) return;
        string json = PlayerPrefs.GetString(UpgradeSaveDataKey, string.Empty);

        UpgradeSaveData saveData = JsonUtility.FromJson<UpgradeSaveData>(json);

        for (int i = 0; i < _upgrades.Length; i++)
        {
            Debug.Log($"{_upgrades[i].Name} 로드완료~!");
            _upgrades[i].SetLevel(saveData.Level[i]);
        }
    }
}