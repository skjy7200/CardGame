using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI; // 새로운 Input System UI를 사용하기 위해 추가!

public class UIController : MonoBehaviour
{
    void Start()
    {
        // --- 새로운 Input System에 맞는 EventSystem 설정 ---
        SetupEventSystem();

        // UI 요소들을 담을 Canvas를 찾거나, 없으면 새로 생성합니다.
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObject = new GameObject("UICanvas");
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObject.AddComponent<CanvasScaler>();
            CanvasScaler canvasScaler = canvasObject.GetComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080); // 기준 해상도 설정 (예: Full HD)
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.5f; // 너비와 높이의 중간에 맞춤
            canvasObject.AddComponent<GraphicRaycaster>();
        }

        Transform canvasTransform = canvas.transform;

        // --- 작고 동그란 검은색 버튼 3개 생성 ---
        CreateCircleButton("이동", new Vector2(-50, -50), canvasTransform, () => { Debug.Log("이동!"); });
        CreateCircleButton("덱", new Vector2(-50, -110), canvasTransform, () => { Debug.Log("카드 덱!"); });
        CreateCircleButton("설정", new Vector2(-50, -170), canvasTransform, () => { Debug.Log("설정!"); });
    }

    // EventSystem을 새로운 Input System에 맞게 설정하는 함수
    void SetupEventSystem()
    {
        EventSystem eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null)
        {
            // EventSystem이 없으면 새로 만들고, 새 Input System에 맞는 모듈을 추가합니다.
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystemObj.AddComponent<EventSystem>();
            eventSystemObj.AddComponent<InputSystemUIInputModule>(); // << 중요! 이 부분이 변경되었습니다.
        }
        else
        {
            // 이미 EventSystem이 있다면, 구버전 모듈이 있는지 확인합니다.
            if (eventSystem.GetComponent<StandaloneInputModule>() != null)
            {
                // 구버전 모듈이 있다면 파괴하고...
                Destroy(eventSystem.GetComponent<StandaloneInputModule>());
                // 새 버전에 맞는 모듈을 추가합니다.
                eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
            }
        }
    }

    void CreateCircleButton(string buttonText, Vector2 position, Transform parentCanvas, UnityEngine.Events.UnityAction clickAction)
    {
        GameObject buttonObj = new GameObject(buttonText + " Button");
        buttonObj.transform.SetParent(parentCanvas, false);

        Image buttonImage = buttonObj.AddComponent<Image>();
        buttonImage.sprite = Resources.GetBuiltinResource<Sprite>("UI/Skin/Knob.psd");
        buttonImage.color = Color.black;

        Button button = buttonObj.AddComponent<Button>();
        button.onClick.AddListener(clickAction);

        RectTransform rectTransform = buttonObj.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(1, 1);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(1, 1);
        rectTransform.anchoredPosition = position;
        

        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform, false);
        Text textComponent = textObj.AddComponent<Text>();
        textComponent.text = buttonText;
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComponent.color = Color.white;
        textComponent.alignment = TextAnchor.MiddleCenter;
        textComponent.fontSize = 18;
    }
}