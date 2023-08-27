# Portfolio1

It's a copy from the original.  
When I copied it, it was 2023.08.24

본 포트폴리오는 원본에서 복사한 사본입니다.  
복사한 시점은 2023.08.24

따로 사본을 만든 이유는 개인라이브러리등 리포지토리가 3개이상되서  
한꺼번에 올리기 위해 사본을 만들었습니다.

### 커밋내용
-GamePortfolio1
![Commit1](commit1.png)
-StandardQualityControlLibary
![Commit2](commit2.png)



## 목적
뉴럴클라우드 시스템을 가지고 와서 2차 세계대전(에셋은 좀 다르게 처리) 전투게임을 만들어보자

### 개발인원 1명
이동호 : 개발, 기획, 모델링, 애니메이션, 자료조사

### 자기소개서
준비중

### 총 개발기간 6개월(코로나, 컨디션악화로 1달 쉼)

[![YouTube Video](https://img.youtube.com/vi/zgutF4z9UNU/0.jpg)](https://www.youtube.com/watch?v=zgutF4z9UNU)

### 미완성
계속 보완중



## 제작된 기능

1   카메라기능, 시네머신 + 에디터  
[![YouTube Video](https://img.youtube.com/vi/QLLJy-3b-Kw/0.jpg)](https://www.youtube.com/watch?v=QLLJy-3b-Kw)  

[![YouTube Video](https://img.youtube.com/vi/25pvZrS9cCg/0.jpg)](https://www.youtube.com/watch?v=25pvZrS9cCg)  

[![YouTube Video](https://img.youtube.com/vi/xxAvBKvxWfs/0.jpg)](https://www.youtube.com/watch?v=xxAvBKvxWfs)  

[![YouTube Video](https://img.youtube.com/vi/_J7pW63nOkQ/0.jpg)](https://www.youtube.com/watch?v=_J7pW63nOkQ)  

[![YouTube Video](https://img.youtube.com/vi/Dk8ywJSsFWk/0.jpg)](https://www.youtube.com/watch?v=Dk8ywJSsFWk)

2   헥스타일맵  
[![YouTube Video](https://img.youtube.com/vi/oVlLTpQl9vk/0.jpg)](https://www.youtube.com/watch?v=oVlLTpQl9vk)

3   유닛배치에디터  
[![YouTube Video](https://img.youtube.com/vi/irBt2UnBwsQ/0.jpg)](https://www.youtube.com/watch?v=irBt2UnBwsQ)

4   무한스크롤 + 에디터  
5   업적시스템  
[![YouTube Video](https://img.youtube.com/vi/116Wf5N0w8M/0.jpg)](https://www.youtube.com/watch?v=116Wf5N0w8M)

6   CSV 임포터  
CSV 링크 : https://docs.google.com/spreadsheets/d/1aBeyNg035A2VciVUj92WgP-APTC5R40tXyKSZeJv5qk/edit?usp=sharing  
[![YouTube Video](https://img.youtube.com/vi/t5yjD9iq2mU/0.jpg)](https://www.youtube.com/watch?v=t5yjD9iq2mU)

7  스탯&상태 시스템 + 에디터  
[![YouTube Video](https://img.youtube.com/vi/G-L4EyhbumM/0.jpg)](https://www.youtube.com/watch?v=G-L4EyhbumM)

8  절차애니메이션 + 에디터  
[![YouTube Video](https://img.youtube.com/vi/47A27fet85E/0.jpg)](https://www.youtube.com/watch?v=47A27fet85E)

9  알림 디스플레이 + 에디터  
[![YouTube Video](https://img.youtube.com/vi/o9FNsFZkSmI/0.jpg)](https://www.youtube.com/watch?v=o9FNsFZkSmI)

10  특정 컴포넌트 리무브 컴포넌트    
[![YouTube Video](https://img.youtube.com/vi/EaZu0esWjaU/0.jpg)](https://www.youtube.com/watch?v=EaZu0esWjaU)


11  카메라쫒아가는 UI (모드마다 대응)  
[![YouTube Video](https://img.youtube.com/vi/TXht9loURVA/0.jpg)](https://www.youtube.com/watch?v=TXht9loURVA)

12  A* 길찾기  
13  점수판, MVP판  
15  다이나믹오브젝트폴
16  게임플레이규칙관리자  
17  세션매니저, 씬로드매니저 



# 작업내역
## 기획설계 2월27일 ~ 2월28일(2일)

## 작업 1달차 2023년 3월1일 ~ 3월25일(25일)
0. 2D작업만하다고 3D작업을 처음해봐서 고생하는데 위치는 별상관없지만 회전에서 고생.
1. 시네머신 기능 + 툴(완)
2. 디렉터카메라 (3D용 카메라)(완)
3. 육각타일맵을 위한 타일맵과 기능 (완)
4. 에이스타제작 (에이스타 제작후 인터페이스로 변경후 노드맵, 육각타일, 사각타일등 노드가 있는건 다 사용할수 있게 변경)(완)
5. 모델뷰 룸 (특정오브젝트를 주변에서 감상할수 있는 구역)(완)
6. 인게임 컨트롤러 : 육각타일맵과 상호작용까지만 
7. 유닛베이스 & ai베이스 : 말그대로 베이스만 작업
8. 노드맵 베이스 : 말그대로 베이스만 작업
9. 유닛배치 시스템 : 가진카드로 유닛을 배치하는 기능(완)
10. 커스텀폴바이블 : 다이나믹 폴바이블 생성. 컴포넌트로 접근 소환됨. 제작하면서 기존 폴 검색이 느려지는걸 딕하나 더 추가시킴

## 작업 2달차 3월26일 ~ 5월2일(38일) (4월20일날(26일) 공개카톡방에 공개. 호응은 좋았다. 완성은 못했고)
0. 타이머매니저 버그수정 : 어플종료했을시 계속접근하는걸 막음
1. 카드배치 버그수정 : 바로앞에 있는 카드와 변경이 안되는걸 수정
2. 게임씬로드 매니저(완) : 씬전환에 사용하는 매니저 + 어트리튜트
3. 시네머신 기능변경 : 여러 오브젝트를 컨트롤 할 수 있게 제작
4. 시네머신에디터 기능추가 : 복사 붙여넣기 기능, 루프(반복플레이)기능
5. 업적시스템 (완) : 자료조사 + CSV 파일틀제작후 데이터입력 + 업적시스템 + 어트리뷰트 + 자동해금
6. 알람디스플레이박스 (완) : 특정 메세지를 받았을시 설정에 의해 일정하게 보여주는 역할을 가짐. 에디터제작하여 비쥬얼적으로 설정할떄 도움을 줄수 있게 제작(기존 알람시스템을 대체)
7. 메인화면에 있는 요소들을 각각의 UI로 분리 : AchivementUI, BasementUI, ChangeMainScreenSoliderUI, ETC
8. 무한스크롤 (완) : 수직, 수평지원, 에디터제작하여 비쥬얼적으로 설정할떄 도움을 줄수 있게 제작 + scrollSmooth제작하여 클릭드래그로도 스무스하게 제작
9. 옵션 추가기능 제작 : 음소거기능, 퀄리티기능선택 추가
10. 여러 CSV데이터들을 임포트시켜주는 심플한 에디터제작 : 지정된 CSV파일들을 설정에 따라 Scriptable 오브젝트로 생성
11. 에디터유틸 버그수정 + 함수추가 + 제네릭된 몇몇 에디터들 직관적으로 보기좋게 수정 + 전처리기추가
12. 레벨모듈제작 : 커브를 사용하여 레벨링처리를 자동화하기 위핸 레벨모듈 + 데이터 + 에디터
13. 세이브시스템 기초 제작 : 플레이어프리팹으로 할려다가 Json으로 하는게 현명하다고 판단. 
14. 뽑기시스템 기초제작 : 시네머신과 연동하고 맛깔나게 만드는게 남음
15. 보급캐쉬창고제작 : 일정시간이 지나면 자원이 모이게하는 기능. 자원수집했을때 이팩트작동 기능
16. 암호화 유틸추가 : 현재 DES만 작동, AES, ProtectedData는 작동이 이상하거나 요소추가를 안해서 작동이 안됨
17. 유닛생성데이터에디터 제작 : 유닛을 생성하기위한 에디터제작. 유닛스탯, 유닛카드, 유닛오브젝트등을 설정
18. 상점UI 탭 기능추가 : 탭누르면 UI변경기능
19. 대미지모듈제작 : 유닛에 대미지를 줄수 있게하는 모듈
20. 씬에 커스텀시네머신을 활용한 요소 추가 : 날라다니는 비행기 2대, 전장에 진입할때 연출
21. 배틀매니저 수정 : 전장에 진입했을시 전투를 관리해주는 역할을 함. 배치기능만 제작완료. 전투시 기능은 아직 대기
22. 헥스타일맵 제작 : 유니티타일맵은 2D전용이라 3D에서는 사용이 불가능하여 제작함. 대신 유니티타일맵에서 쓰던 오프셋을 그대로
활용하여 언제든지 유니티타일맵과 연동이 가능함. 
23-1. 헥스타일오브젝트 (완) : 더링(정점직접 배치후 그려줌), 배치타겟요소로 사용
23-2. 헥스타일맵 (완) : XY XZ축 지원. PointyTop, FlatTop 지원. 헥스타일을 높이 넓이 조절 타일크기조절
유니티타일맵에서 지원하던 함수들중에서 많이쓰던 함수들을 헥스타일맵에서도 제작. WorldToCell, hasTile, SetTile, ETC..
헥스타일맵용 일정거리에 있는 타일들을 가져오는 함수, 일정범위에 있는 타일들을 가져오는 함수 제작
24. 타일맵 배치윈도우에디터를 만들기 위한 빌드업중 : 타일맵배치오브젝트 에디터 + 데이터제작, 타일맵 배치정보를 저장할 데이터제작. 에디터는 기초만 제작. 설계가 필요.
25. 트랜스폼정렬을 위한 윈도우에디터 제작 (완) : 에디터를 키고 원하는 대상들을 클릭한다음 수정하기 누르면 작동

## 작업 x달차 5월3일 ~ (5월7일~5월14일 코로나 격리기간)
작업 불능

## 작업 5월 15일 ~ 8월 25일
0.에이스타 길찾기 제작 
1. 프레임워크 구조를 위한 데이터 구조변경 
2. 커브데이터추가, 레벨기능제작 
3. 어빌리티시스템 설계 및 제작 : 매 다른게임마다 스탯을 대응해야되는 문제점을 해결하고자 반자동화까지 제작  
4. 절차적애니메이션 제작 (탱크바디, 주퇴복좌기)  
5. 애니멘서 에셋 한번 덮어서 사용되게 애니메이터컨트롤러 제작 : 애니멘서에셋 메뉴얼을 읽고 애니멘서를 컨트롤하기 위한 기능을 제작  
6. 유닛 AI작업
7. 배틀매니저와 다른 기능들 연동 : 시네머신, 씬들끼리의 통신, 점수판, MVP판등 연동
8. 시네머신 이벤트기능 추가 
9. 육각타일맵 문제발생 발견후 수정 : 짝수마다의 각칸에 대한 대응이 안되는 문제를 해결
10. 씬제작 (참호, 해변, 마을)
11. 카메라기능 추가
12. 참고자료 정리
13. CSV, 구글스프레드 제작 (CSV 링크 : https://docs.google.com/spreadsheets/d/1aBeyNg035A2VciVUj92WgP-APTC5R40tXyKSZeJv5qk/edit?usp=sharing)
14. 데이터들을 임포트할떄 규칙정하기 : 데이터들을 임포트할떄 폴더구조를 어덯게 짜고 어디로 연결할지 정함.
15. 루틴이 돌아갈정도로 연동


