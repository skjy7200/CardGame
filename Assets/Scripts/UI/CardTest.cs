using UnityEngine;

public class CardTest : MonoBehaviour
{
    public CardUI cardUI;       // ī�� UI ������
    public CardData cardData;   // ī�� ������

    void Start()
    {
        cardUI.SetCard(cardData);
    }
}
