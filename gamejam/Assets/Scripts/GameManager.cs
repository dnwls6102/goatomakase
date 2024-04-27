using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct clearInfo
{
    public bool _isGrassOne; //1번 풀이 필수인지 아닌지
    public bool _isGrassTwo; //2번 풀이 필수인지 아닌지
    public bool _isGrassThree; //3번 풀이 필수인지 아닌지
    public bool _isSpiceOne; //1번 조미료가 필수인지 아닌지
    public bool _isSpiceTwo; //2번 조미료가 필수인지 아닌지
    public bool _isSpiceThree; //3번 조미료가 필수인지 아닌지
    public bool _isSpiceFour; //4번 조미료가 필수인지 아닌지
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

    private clearInfo firstAnswer = new clearInfo() //첫 번째 주문에 대한 정답
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
    private clearInfo secondAnswer = new clearInfo() //두 번째 주문에 대한 정답
    {
        _isGrassOne = true,
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
    private clearInfo thirdAnswer = new clearInfo() //세 번째 주문에 대한 정답
    {
        _isGrassOne = false,
        _isGrassTwo = false,
        _isGrassThree = false,
        _isSpiceOne = false,
        _isSpiceTwo = false,
        _isSpiceThree = false,
        _isSpiceFour = true,
        _isToolOne = true,
        _isToolTwo = false,
        _isToolThree = false,
    };
    private clearInfo fourthAnswer = new clearInfo() //네 번째 주문에 대한 정답
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
    private clearInfo fifthAnswer = new clearInfo() //다섯 번째 주문에 대한 정답
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
        orderArray = new string[] { "밥줘", "배고파", "물줘", "추워", "입이 근질근질", "난 비건이야" };
        answerArray = new List<clearInfo>() { firstAnswer, secondAnswer, thirdAnswer, fourthAnswer, fifthAnswer, sixthAnswer };
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