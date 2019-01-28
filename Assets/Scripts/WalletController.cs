using UnityEngine;
using UnityEngine.UI;

public class WalletController : MonoBehaviour
{
    public Text coinCountText;
    private int coinCount = 0;

    public void Start()
    {
        coinCountText.text = "Coins: 0";
    }

    public void Add(int amount)
    {
        coinCount += amount;
        coinCountText.text = "Coins: " + coinCount;
    }
}
