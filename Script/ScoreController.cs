using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textScore; // Tham chiếu đến text score trên màn hình (Canvas -> Score)
    private int score=0; // Biến lưu trữ điểm số hiện tại
    public static ScoreController instance; // Tham chiếu đến ScoreController để truy cập từ các script khác

    public int nextExtraLifeScore = 1000;
    private bool extraLifeGiven = false;

    private void Awake()
    {
        instance = this;
        float tam = PlayerInfo.playerScore;
        if(tam == 0 || tam % 1000 == 0)
        {
            tam++;
        }
        nextExtraLifeScore = Mathf.CeilToInt(tam / 1000) * 1000;
        score = PlayerInfo.playerScore;
        textScore.text = "Score: " + score.ToString();
    }

    private void Update()
    {
        if (score >= nextExtraLifeScore && !extraLifeGiven)
        {
            Debug.Log("Player tăng máu: " + (PlayerInfo.playerLP + 1));
            GameObject.FindGameObjectWithTag("lpController").GetComponent<Lifepoint_script>().increaseLP(); // Cộng thêm 1 mạng
            extraLifeGiven = true; // Đánh dấu rằng một mạng đã được cộng thêm
            nextExtraLifeScore += 1000; // Cập nhật điểm mốc cho mạng tiếp theo
        }
        extraLifeGiven = !extraLifeGiven;
    }

    public void GetScore(int newExtraScore)
    {
        this.score += newExtraScore;
        textScore.text = "Score: " +this.score.ToString();
    }

    public int scoreGetter()
    {
        return score;
    }
}

