using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawManager : MonoBehaviour {

    private Vector3 beginPos;
    private Vector3 currentPos;

    public GameObject arrowTipObj;

    private bool isBegin = false;
	
	void Update () {

        if (GameManager.instance.CurrentBottle.IsThrowed || GameManager.instance.IsGameOver)
            return;

#if UNITY_EDITOR || UNITY_STANDALONE

        if(Input.GetMouseButtonDown(0) && !isBegin) {                           // 마우스 버튼을 처음 눌렀을 때
            beginPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);     // 마우스의 위치를 월드 좌표로변환
            beginPos.z = 1f;                                                    // 2D 게임이기 때문에 z축을 1로 수정
            isBegin = true;                                                     // 마우스 버튼이 눌러져있다는 flag 설정
        }

        if (Input.GetMouseButton(0) && isBegin) {                               // 마우스 버튼이 눌러지고 있을 때
            currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);   // 마우스위 위치를 월드 좌표로 변환
            currentPos.z = 1f;                                                  // z축을 1로 수정
            
            GetComponent<LineRenderer>().SetPositions(new Vector3[] {           // LineRenderer의 포지션을 설정
                beginPos,                                                       // 선의 시작 위치
                currentPos                                                      // 선의 끝 위치
            });

            GetComponent<LineRenderer>().positionCount = 2;                     // 위치의 갯수를 2개로 설정

            arrowTipObj.transform.position = currentPos;                        // 화살표 머리를 현재 마우스 위치로 이동
            
            arrowTipObj.transform.rotation = Quaternion.Euler(0f, 0f,           // 화살표 머리 회전
                Vector3.Angle(currentPos - beginPos, Vector3.up));
            arrowTipObj.SetActive(true);                                        // 화살표 머리 표시
        }

        if (Input.GetMouseButtonUp(0) && isBegin) {                             // 마우스 버튼을 뗐을 때
            GetComponent<LineRenderer>().positionCount = 0;                     // 위치의 갯수를 0개로 설정 (선 지우기)
            arrowTipObj.SetActive(false);                                       // 화살표 머리 숨기기
            isBegin = false;                                                    // flag를 false로 설정

            GameManager.instance.ThrowLaundry((currentPos - beginPos));         // 빨랫감을 던지는 함수 호출
        }

#elif UNITY_ANDROID || UNITY_IOS

        if(Input.touchCount > 0) {
            for(int i = 0; i < Input.touchCount; i++) {
                Touch tempTouchs = Input.GetTouch(i);

                if(tempTouchs.phase == TouchPhase.Began && !isBegin) {                  // 터치 시작했을 때
                    beginPos = Camera.main.ScreenToWorldPoint(tempTouchs.position);     // 터치 좌표를 월드 좌표로변환
                    beginPos.z = 1f;                                                    // 2D 게임이기 때문에 z축을 1로 수정
                    isBegin = true;                                                     // 터치를 시작했다는 flag 설정

                } else if(tempTouchs.phase == TouchPhase.Moved && isBegin) {            // 터치 중일 때
                    currentPos = Camera.main.ScreenToWorldPoint(tempTouchs.position);   // 터치 좌표를 월드 좌표로 변환
                    currentPos.z = 1f;                                                  // z축을 1로 수정

                    GetComponent<LineRenderer>().SetPositions(new Vector3[] {           // LineRenderer의 포지션을 설정
                        beginPos,                                                       // 선의 시작 위치
                        currentPos                                                      // 선의 끝 위치
                    });

                    GetComponent<LineRenderer>().positionCount = 2;                     // 위치의 갯수를 2개로 설정

                    arrowTipObj.transform.position = currentPos;                        // 화살표 머리를 현재 마우스 위치로 이동

                    arrowTipObj.transform.rotation = Quaternion.Euler(0f, 0f,           // 화살표 머리 회전
                        Vector3.Angle(currentPos - beginPos, Vector3.up));
                    arrowTipObj.SetActive(true);                                        // 화살표 머리 표시

                } else if(tempTouchs.phase == TouchPhase.Ended && isBegin) {            // 터치가 끝났을 때
                    GetComponent<LineRenderer>().positionCount = 0;                     // 위치의 갯수를 0개로 설정 (선 지우기)
                    arrowTipObj.SetActive(false);                                       // 화살표 머리 숨기기
                    isBegin = false;                                                    // flag를 false로 설정

                    GameManager.instance.ThrowLaundry((currentPos - beginPos));         // 빨랫감을 던지는 함수 호출
                }
                
                break;

            }
        }

#endif
    }
}
