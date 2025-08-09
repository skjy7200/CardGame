
using UnityEngine;

// 이 스크립트는 UI 버튼들의 클릭 이벤트를 처리
public class ButtonHandler : MonoBehaviour
{
    // "이동" 버튼을 클릭했을 때 호출될 함수
    public void OnMoveButtonClick()
    {
        Debug.Log("이동!");
    }

    // "덱" 버튼을 클릭했을 때 호출될 함수
    public void OnDeckButtonClick()
    {
        Debug.Log("덱 목록!");
    }

    // "설정" 버튼을 클릭했을 때 호출될 함수
    public void OnSettingsButtonClick()
    {
        Debug.Log("설정!");
    }
}
