using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
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
=======

public class UIController : MonoBehaviour
{
    // --- Unity 에디터에서 연결할 UI 요소들 ---
    [Header("1. UI 요소 연결")]
    public Image backgroundImageComponent;
    public Button moveButton;
    public Button deckButton;       // 덱 버튼 추가
    public Button settingsButton;   // 설정 버튼 추가
    public GameObject leftArrowButton;
    public GameObject rightArrowButton;

    [Header("2. 배경 이미지 파일 연결")]
    public Sprite startSprite;      // 게임 시작 시 보일 기본 배경 (IMG_4008)
    public Sprite image4007Sprite;  // IMG_4007 이미지
    public Sprite image4009Sprite;  // IMG_4009 이미지

    void Start()
    {
        // 1. 초기 배경 설정
        if (backgroundImageComponent != null && startSprite != null)
        {
            backgroundImageComponent.sprite = startSprite;
            Debug.Log("초기 배경 이미지 할당 성공: " + startSprite.name);
        }
        else
        {
            Debug.LogWarning("초기 배경 이미지 할당 실패. backgroundImageComponent: " + (backgroundImageComponent != null) + ", startSprite: " + (startSprite != null));
        }

        // 2. 화살표 버튼 숨기기
        if (leftArrowButton != null) leftArrowButton.SetActive(false);
        if (rightArrowButton != null) rightArrowButton.SetActive(false);

        // 3. 각 버튼에 클릭 이벤트 연결
        if (moveButton != null) moveButton.onClick.AddListener(ToggleArrowButtons);
        if (deckButton != null) deckButton.onClick.AddListener(OnDeckButtonClick); // 덱 버튼 이벤트 연결
        if (settingsButton != null) settingsButton.onClick.AddListener(OnSettingsButtonClick); // 설정 버튼 이벤트 연결

        // Ensure Button component exists before adding listener
        if (leftArrowButton != null)
        {
            Button leftButton = leftArrowButton.GetComponent<Button>();
            if (leftButton != null)
            {
                leftButton.onClick.AddListener(() => ChangeBackground(-1));
            }
            else
            {
                Debug.LogError("LeftArrowButton에 Button 컴포넌트가 없습니다.");
            }
        }
        if (rightArrowButton != null)
        {
            Button rightButton = rightArrowButton.GetComponent<Button>();
            if (rightButton != null)
            {
                rightButton.onClick.AddListener(() => ChangeBackground(1));
            }
            else
            {
                Debug.LogError("RightArrowButton에 Button 컴포넌트가 없습니다.");
>>>>>>> 432645273c46d18a5cce9a5a646a067059fad06a
            }
        }
    }

<<<<<<< HEAD
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
=======
    // "이동" 버튼 기능
    void ToggleArrowButtons()
    {
        bool areArrowsActive = leftArrowButton.activeSelf;
        leftArrowButton.SetActive(!areArrowsActive);
        rightArrowButton.SetActive(!areArrowsActive);
    }

    // "덱" 버튼 기능
    void OnDeckButtonClick()
    {
        Debug.Log("덱 버튼이 클릭되었습니다.");
        // 여기에 나중에 덱 관련 기능을 추가할 수 있습니다.
    }

    // "설정" 버튼 기능
    void OnSettingsButtonClick()
    {
        Debug.Log("설정 버튼이 클릭되었습니다.");
        // 여기에 나중에 설정 관련 기능을 추가할 수 있습니다.
    }

    // 배경 변경 기능
    void ChangeBackground(int direction)
    {
        Sprite currentSprite = backgroundImageComponent.sprite;
        Sprite nextSprite = null;

        if (currentSprite.name == startSprite.name) // 현재 IMG_4008
        {
            if (direction == -1) // 왼쪽 화살표
            {
                nextSprite = image4007Sprite;
            }
            else if (direction == 1) // 오른쪽 화살표
            {
                nextSprite = image4009Sprite;
            }
        }
        else if (currentSprite.name == image4007Sprite.name) // 현재 IMG_4007
        {
            if (direction == -1) // 왼쪽 화살표
            {
                nextSprite = image4009Sprite;
            }
            else if (direction == 1) // 오른쪽 화살표
            {
                nextSprite = startSprite;
            }
        }
        else if (currentSprite.name == image4009Sprite.name) // 현재 IMG_4009
        {
            if (direction == -1) // 왼쪽 화살표
            {
                nextSprite = startSprite;
            }
            else if (direction == 1) // 오른쪽 화살표
            {
                nextSprite = image4007Sprite;
            }
        }

        if (backgroundImageComponent != null && nextSprite != null)
        {
            backgroundImageComponent.sprite = nextSprite;
            Debug.Log("배경 변경됨: " + nextSprite.name); // 이 줄 추가
        }
        else
        {
            Debug.LogWarning("배경 변경 실패: backgroundImageComponent 또는 nextSprite가 null입니다."); // 이 줄 추가
        }
    }
}
>>>>>>> 432645273c46d18a5cce9a5a646a067059fad06a
