using UnityEngine;
using UnityEngine.UI;


public class ScoreView : MonoBehaviour
{
    // ==============================================================================================
    public int CurrentScoreCount;
    public Text ScoreCountText;


    // ==============================================================================================
    public void UpdateUI()
    {
        ScoreCountText.text = CurrentScoreCount.ToString("0000");
    }
}
