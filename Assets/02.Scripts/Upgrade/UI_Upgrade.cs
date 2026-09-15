using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Upgrade : MonoBehaviour
{
    [SerializeField] private int _index;

    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _valueText;
    [SerializeField] private TextMeshProUGUI _scoreCostText;

    public void OnClick()
    {
        // 골드 매니저에게 돈이 있는지 물어보고 돈이 있다면 차감후 업그레이드 호출
        // 버튼이 클릭되면 매니저에게 레벨업 해줘라고 요청한다.
        UpgradeManager.Instance.LevelUp(_index);
    }

    // 새로고침
    public void Refresh()
    {
        Upgrade upgrade = UpgradeManager.Instance.Upgrades[_index];

        _titleText.text = $"{upgrade.Name} Lv.{upgrade.Level}";
        _valueText.text = $"{upgrade.CurrentValue}->{upgrade.NextValue}";
        _scoreCostText.text = $"{upgrade.Cost:N0}";
    }
}