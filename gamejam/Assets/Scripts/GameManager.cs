using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct clearInfo
{
    public bool _isGrassOne; //강아지풀이 필수인지 아닌지
    public bool _isGrassTwo; //민들레가 필수인지 아닌지
    public bool _isGrassThree; //고추가 필수인지 아닌지
    public bool _isSpiceOne; //간장이 필수인지 아닌지
    public bool _isSpiceTwo; //된장이 필수인지 아닌지
    public bool _isSpiceThree; //소금이 필수인지 아닌지
    public bool _isSpiceFour; //고춧가루가 필수인지 아닌지
    public bool _isToolOne; //냄비가 필수인지 아닌지
    public bool _isToolTwo; //믹서기가 필수인지 아닌지
    public bool _isToolThree; //튀김기가 필수인지 아닌지
}

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public float MaxTime;
    public float GameTime = 5.0f;
    public Slider TimeBar;
    //public int cuisine_num = 0;
    public bool _isOrdering = false;
    public Text orderText;
    public Board Doma; //도마
    //public Board Fryer; //튀김기
    //public Board Naembi; //냄비
    //public Board Blender; //믹서기
    // public GameObject Salad;
    // public GameObject Chicken;
    // public GameObject Soup;
    // public GameObject Jjigae;
    // public GameObject Wine;
    // public GameObject Smoothie;

    private string[] orderArray; //주문을 저장하는 배열
    private string[] goodReactionArray; //좋은 반응들을 저장하는 배열
    private string[] badReactionArray; //나쁜 반응들을 저장하는 배열
    private List<clearInfo> answerArray; //주문에 대한 정답을 저장하는 배열
    private bool toolFlag = false; //조리기는 한번에 한번씩만 사용
    private clearInfo currentSituation = new clearInfo()
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = false,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };

    //private bool[] repeatArray;
    //private Stack<int> indexStack;

    private clearInfo Answer1 = new clearInfo() //첫 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = false,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo Answer2 = new clearInfo() //두 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = true, //고추
        _isSpiceOne = false,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = true, //고춧가루
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo Answer3 = new clearInfo() //세 번째 주문에 대한 정답
    {
        _isGrassOne = true, //강아지풀
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = false,
        _isSpiceTwo = true, //된장
        _isSpiceThree = false,
        _isSpiceFour = true,
        _isToolOne = true, //냄비
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo Answer4 = new clearInfo() //네 번째 주문에 대한 정답
    {
        _isGrassOne = true, //전부
        _isGrassTwo = true,
        _isGrassThree = true,
        _isSpiceOne = false,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = true, //고춧가루
        _isToolOne = false,
        _isToolTwo = true, //믹서
        _isToolThree = false,
    };
    private clearInfo Answer5 = new clearInfo() //다섯 번째 주문에 대한 정답
    {
        _isGrassOne = true, //강아지풀
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true, //간장
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = true, //냄비
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo Answer6 = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = true, //강아지풀
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = false, //상관없음
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false, //상관없음
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo Answer7 = new clearInfo() //일곱 번째 주문에 대한 정답
    {
        _isGrassOne = true, //강아지풀
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true, //간장
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = true, //믹서기
        _isToolThree = false,
    };
    private clearInfo Answer8 = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false, //상관없음
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = false,
        _isSpiceTwo = true, //된장
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false, //상관없음
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo Answer9 = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = true, //민들레
        _isGrassThree = false,
        _isSpiceOne = false, //상관없음
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = true, //튀김기
    };
    private clearInfo Answer10 = new clearInfo() //구글독스 9번
    {
        _isGrassOne = false,
        _isGrassTwo = true, //민들레
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = true, //소금
        _isSpiceFour = false,
        _isToolOne = false, //상관없음
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo Answer11 = new clearInfo() //구글독스 11번
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = true, //고추
        _isSpiceOne = false,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = true, //고춧가루
        _isToolOne = false, //상관없음
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo Answer12 = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo sixthAnswer = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo sixthAnswer = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo sixthAnswer = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo sixthAnswer = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo sixthAnswer = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo sixthAnswer = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo sixthAnswer = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo sixthAnswer = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo sixthAnswer = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo sixthAnswer = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo sixthAnswer = new clearInfo() //여섯 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = true,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = false,
        _isToolOne = false,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private bool secondDish;
    private bool thirdDish;
    private int temp;

    void Awake()
    {
        MaxTime = GameTime;
        orderArray = new string[] {
            "안녕하세요, 샐러드 하나 주세요! 소스는 필요없어요.", //0번
             "저 오늘 다이어트 해야해요 완전 스트레스 받네.. 좀 화끈한 거 없어요?", //1번
             "날이 좀 으슬으슬하네.. 구수한 국수 하나 주세요.", //2번...
             "모든 걸 파괴해야 해요. 모든 걸, 화끈하게...",
             "간장 국수 하나 주세요.",
             "고양이들은 아무것도 몰라.",
             "전 클래식은 이제 질렸어요. 씁쓸한 스무디 한 잔이요, 근데 좀 다크하게 해주세요.",
             "할머니의 손맛이 그리워요…", //7번
             "예쁘고 바삭한 거 있나요? 그리고 최대한 심플하게요!",
             "노랗고..하얗고..짭짤한 거..",
             "혹시 사랑니 빼보셨어요? 진짜 아프네… 아 맵고 뜨겁고 자극적이지 않은 걸로 주세요.",
             "저 송지원인데요, 늘 먹던 걸로 주세요.", //11번,
             "Is there something salty and sweet? And also crispy, but without dark, you know what I am saying?",
             "그린 스무디 한 잔 주세요.",
             "구수하기만 하게 하면 돼요.",
             "蒲公英.", //15번
             "샐러드를 좋아하나봐?",
             "지옥은 어둡고 뜨겁겠지..",
             "원숭이 엉덩이는?",
             "겨울에도 널 볼수만 있다면..",
             "씁쓸하고 매콤한데 또 따뜻하게 주세요.",
             "핫도그 하나요!",//21번,
             "내가 만든 퍼즐은 늘 달콤하니까.",
             "Whatever you want.",
              };
        answerArray = new List<clearInfo>() { Answer1, Answer2, Answer3, Answer4, Answer5, Answer6 };
        //repeatArray = new bool[] { false, false, false, false, false, false };
        //indexStack = new Stack<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOrdering == false) //주문이 없는 경우 : 주문 받기
        {

            temp = Random.Range(0, 5); //어떤 주문을 할지 난수 생성
                                       // do
                                       // { //cuisineArray의 인덱스가 될 난수를 먼저 생성시킨 후 true인지 false인지 조건 판단
                                       //     temp = Random.Range(0, 5); //현재 구현하는 요리의 갯수 : 6개
                                       // } while (repeatArray[temp] == true); //중복되어 생성된 경우 다시 난수 생성시키기

            //repeatArray[temp] = true; // 난수 index의 요리를 true로 설정
            //indexStack.Push(temp);
            _isOrdering = true;
            orderText.text = orderArray[temp];
        }

        //요리하기 : 유저가 조리기구로 드래그한 재료들이 어떤 재료인지 파악 후 currentSituation변수의 플래그 변경하기
        //도마 - 냄비 - 믹서기 - 튀김기 순으로 체크
        if (Doma.ingredient_list.Count != 0)
        {
            temp = Doma.ingredient_list[^1].index;
            Debug.Log(temp);
            CheckIngredient(temp); //재료 플래그 함수
        }
        //장비 사용 여부는 각 조리기 Script에서 판정하기
        // if (Naembi.ingredient_list.Count != 0) //냄비의 ingredient_list에 재료가 있을 경우
        // {
        //     currentSituation._isToolOne = true; //냄비 사용 플래그 ON
        //     temp = Naembi.ingredient_list[^1].index; //냄비 ingredient_list의 마지막 원소의 index를 뽑아옴
        //     Debug.Log(temp);
        //     CheckIngredient(temp);
        // }
        // if (Blender.ingredient_list.Count != 0) //믹서기의 ingredient_list에 재료가 있을 경우
        // {
        //     currentSituation._isToolTwo = true; //믹서기 사용 플래그 ON
        //     temp = Blender.ingredient_list[^1].index; //믹서기의 ingredient_list의 마지막 원소의 index를 뽑아옴
        //     Debug.Log(temp);
        //     CheckIngredient(temp);
        // }
        // if (Fryer.ingredient_list.Count != 0) //튀김기의 ingredient_list에 재료가 있을 경우
        // {
        //     currentSituation._isToolThree = true; // 튀김기 사용 플래그 ON
        //     temp = Fryer.ingredient_list[^1].index; //튀김기의 ingredient_list의 마지막 원소의 index를 뽑아옴
        //     Debug.Log(temp);
        //     CheckIngredient(temp);
        // }


        GameTime -= Time.deltaTime;
        CheckTime();
        if (GameTime < 0)
        {
            Debug.Log("Time Over");
            Time.timeScale = 0f;
        }
    }

    void CheckTime()
    {
        if (TimeBar != null)
        {
            TimeBar.value = GameTime / MaxTime;
        }
    }

    public void CorrectDebug()
    {
        GameTime += 1.0f;
        if (GameTime > MaxTime)
        {
            MaxTime = GameTime;
        }
        _isOrdering = false; //맞췄으니까 다른 주문 받기
    }

    public void WrongDebug()
    {
        GameTime -= 1.0f;
        _isOrdering = false; //틀렸으니까 다른 주문 받기
    }

    public void CheckIngredient(int index)
    {
        switch (index)
        {
            case 1:
                currentSituation._isGrassOne = true;
                break;
            case 2:
                currentSituation._isGrassTwo = true;
                break;
            case 3:
                currentSituation._isGrassThree = true;
                break;
            case 4:
                currentSituation._isSpiceOne = true;
                break;
            case 5:
                currentSituation._isSpiceTwo = true;
                break;
            case 6:
                currentSituation._isSpiceThree = true;
                break;
            case 7:
                currentSituation._isSpiceFour = true;
                break;
            default:
                Debug.Log("재료 인덱싱 오류");
                break;
        }

    }
}