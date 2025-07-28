using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    public TMP_Text cardNameText;
    public Image cardArtImage;
    public TMP_Text cardCostText;

    public Image backgroundImage;

    public void SetCard(CardData data)
    {
        if (data == null)
        {
            Debug.LogWarning("CardData is null");
            return;
        }
        cardNameText.text = data.cardName;
        cardArtImage.sprite = data.cardArt;
        cardCostText.text = data.cost.ToString();
    }
}