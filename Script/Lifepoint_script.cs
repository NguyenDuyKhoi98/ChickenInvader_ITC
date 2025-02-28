using TMPro;
using UnityEngine;

public class Lifepoint_script : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textLP; // Tham chiếu đến text score trên màn hình (Canvas -> Score)
    public static Lifepoint_script instance; // Tham chiếu đến ScoreController để truy cập từ các script khác
    private int currentLP = 0;

    private void Awake()
    {
        // Đặt instance = this để truy cập đến ScoreController
        instance = this;
        currentLP = PlayerInfo.playerLP;
        textLP.text = currentLP.ToString();
    }

    // GetScore được gọi khi có sự kiện thu được điểm số
    public void SetLifepoint(int lp)
    {
        // Cập nhật text score trên màn hình
        currentLP = lp;
        textLP.text = currentLP.ToString();
    }

    public void increaseLP()
    {
        currentLP++;
        textLP.text = currentLP.ToString();
    }

    public int getPlayerHP()
    {
        return currentLP;
    }
}
