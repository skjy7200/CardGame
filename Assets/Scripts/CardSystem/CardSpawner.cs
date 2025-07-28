using UnityEngine;
using DG.Tweening;

public class CardSpawner : MonoBehaviour
{
    public Transform deckPosition;
    public Transform handPanel;
    public GameObject cardPrefab;

    public CardData sampleCardData;

    public void SpawnCard()
    {
        GameObject card = Instantiate(cardPrefab, deckPosition.position, Quaternion.identity, handPanel);

        card.transform.localScale = Vector3.zero;
        CanvasGroup canvasGroup = card.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = card.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;


        CardUI cardUI = card.GetComponent<CardUI>();
        if (cardUI != null)
        {
            cardUI.SetCard(sampleCardData);
        }

        Sequence seq = DOTween.Sequence();
        seq.Join(card.transform.DOMove(handPanel.position, 0.5f).SetEase(Ease.OutCubic));
        seq.Join(card.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack));
        seq.Append(canvasGroup.DOFade(1f, 0.3f));
        seq.Play();
    }
}