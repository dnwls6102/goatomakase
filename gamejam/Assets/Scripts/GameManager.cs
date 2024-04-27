using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

struct clearInfo //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
{
    public int _isGrassOne; //강아지풀이 필수인지 아닌지
    public int _isGrassTwo; //민들레가 필수인지 아닌지
    public int _isGrassThree; //고추가 필수인지 아닌지
    public int _isSpiceOne; //간장이 필수인지 아닌지
    public int _isSpiceTwo; //된장이 필수인지 아닌지
    public int _isSpiceThree; //소금이 필수인지 아닌지
    public int _isSpiceFour; //고춧가루가 필수인지 아닌지
    public int _isToolOne; //냄비가 필수인지 아닌지
    public int _isToolTwo; //믹서기가 필수인지 아닌지
    public int _isToolThree; //튀김기가 필수인지 아닌지
}

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public float MaxTime;
    public float GameTime = 5.0f;
    public Slider TimeBar;
    //public int cuisine_num = 0;
    public bool _isOrdering = false; // 주문을 받는 중인지 아닌지 판단하는 플래그
    public Text orderText;
    public bool _isFinished = false; // 요리를 완성했는지 못했는지 판단하는 플래그
    public bool _isCorrect = false; // 완성한 요리가 정답인지 아닌지 판단하는 플래그

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
        _isGrassOne = -1,
        _isGrassTwo = -1,
        _isGrassThree = -1,
        _isSpiceOne = -1,
        _isSpiceTwo = -1,
        _isSpiceThree = -1,
        _isSpiceFour = -1,
        _isToolOne = -1,
        _isToolTwo = -1,
        _isToolThree = -1,
    };

    //private bool[] repeatArray;
    //private Stack<int> indexStack;

    private clearInfo Answer1 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = -1, //간장이 필수인지 아닌지
        _isSpiceTwo = -1, //된장이 필수인지 아닌지
        _isSpiceThree = -1, //소금이 필수인지 아닌지
        _isSpiceFour = -1, //고춧가루가 필수인지 아닌지
        _isToolOne = -1, //냄비가 필수인지 아닌지
        _isToolTwo = -1, //믹서기가 필수인지 아닌지
        _isToolThree = -1, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer2 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 1, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 1, //고춧가루가 필수인지 아닌지
        _isToolOne = -1, //냄비가 필수인지 아닌지
        _isToolTwo = -1, //믹서기가 필수인지 아닌지
        _isToolThree = -1, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer3 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 1, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 1, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 1, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer4 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 1, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 1, //민들레가 필수인지 아닌지
        _isGrassThree = 1, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 1, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 1, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer5 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 1, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 1, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 1, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer6 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 1, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer7 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 1, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 1, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 1, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer8 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 1, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer9 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 1, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = -1, //간장이 필수인지 아닌지
        _isSpiceTwo = -1, //된장이 필수인지 아닌지
        _isSpiceThree = -1, //소금이 필수인지 아닌지
        _isSpiceFour = -1, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 1, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer10 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 1, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 1, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer11 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 1, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 1, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer12 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 1, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 1, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 1, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer13 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 1, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 1, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer14 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = -1, //강아지풀이 필수인지 아닌지
        _isGrassTwo = -1, //민들레가 필수인지 아닌지
        _isGrassThree = -1, //고추가 필수인지 아닌지
        _isSpiceOne = -1, //간장이 필수인지 아닌지
        _isSpiceTwo = 1, //된장이 필수인지 아닌지
        _isSpiceThree = -1, //소금이 필수인지 아닌지
        _isSpiceFour = -1, //고춧가루가 필수인지 아닌지
        _isToolOne = -1, //냄비가 필수인지 아닌지
        _isToolTwo = -1, //믹서기가 필수인지 아닌지
        _isToolThree = -1, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer15 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 1, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = -1, //간장이 필수인지 아닌지
        _isSpiceTwo = -1, //된장이 필수인지 아닌지
        _isSpiceThree = -1, //소금이 필수인지 아닌지
        _isSpiceFour = -1, //고춧가루가 필수인지 아닌지
        _isToolOne = -1, //냄비가 필수인지 아닌지
        _isToolTwo = -1, //믹서기가 필수인지 아닌지
        _isToolThree = -1, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer16 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = -1, //냄비가 필수인지 아닌지
        _isToolTwo = -1, //믹서기가 필수인지 아닌지
        _isToolThree = -1, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer17 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 1, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 1, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer18 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 1, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 1, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer19 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 1, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer20 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 1, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 1, //고춧가루가 필수인지 아닌지
        _isToolOne = 1, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer21 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 1, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 1, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer22 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 1, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private clearInfo Answer23 = new clearInfo() //-1 : 없음(넣으면 안됨) 0 : 상관없음 1 : 필요(넣어야 함)
    {
        _isGrassOne = 0, //강아지풀이 필수인지 아닌지
        _isGrassTwo = 0, //민들레가 필수인지 아닌지
        _isGrassThree = 0, //고추가 필수인지 아닌지
        _isSpiceOne = 0, //간장이 필수인지 아닌지
        _isSpiceTwo = 0, //된장이 필수인지 아닌지
        _isSpiceThree = 0, //소금이 필수인지 아닌지
        _isSpiceFour = 0, //고춧가루가 필수인지 아닌지
        _isToolOne = 0, //냄비가 필수인지 아닌지
        _isToolTwo = 0, //믹서기가 필수인지 아닌지
        _isToolThree = 0, //튀김기가 필수인지 아닌지
    };
    private int temp; //재료의 인덱스를 확인하는 변수
    private int orderIndex; //주문 index를 저장하는 변수

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

            orderIndex = Random.Range(0, 5); //어떤 주문을 할지 난수 생성 (temp = Random.Range(0, 22))
                                             // do
                                             // { //cuisineArray의 인덱스가 될 난수를 먼저 생성시킨 후 true인지 false인지 조건 판단
                                             //     temp = Random.Range(0, 5); //현재 구현하는 요리의 갯수 : 6개
                                             // } while (repeatArray[temp] == true); //중복되어 생성된 경우 다시 난수 생성시키기

            //repeatArray[temp] = true; // 난수 index의 요리를 true로 설정
            //indexStack.Push(temp);
            _isOrdering = true;
            orderText.text = orderArray[orderIndex];
        }

        //요리하기 : 유저가 조리기구로 드래그한 재료들이 어떤 재료인지 파악 후 currentSituation변수의 플래그 변경하기
        //도마 - 냄비 - 믹서기 - 튀김기 순으로 체크 -> 냄비 / 믹서기 / 튀김기 사용 여부는 각자 script에서 판정.
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

        // 요리한 재료들 합친 후 완성본 보여주기

        // 클리어 여부 판정
        if (_isFinished) //만약 요리를 완성했다면
        {
            _isCorrect = CheckCorrect(orderIndex);
            if (_isCorrect) //정답일 경우
            {
                Debug.Log("정답");
                GameTime += 1.0f;
                if (GameTime > MaxTime)
                {
                    MaxTime = GameTime;
                }
                _isOrdering = false;
                _isCorrect = false;
            }
            else //오답일 경우
            {
                Debug.Log("오답");
                GameTime -= 1.0f;
                _isOrdering = false;
            }
            _isFinished = false; //완성 여부 플래그의 초기화
        }





        GameTime -= Time.deltaTime;
        CheckTime();
        if (GameTime < 0) //게임 오버 여부 : 코루틴으로 만들기(시간되면)
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

    public void FinishDebug()
    {
        _isFinished = true;
    }

    public void CheckIngredient(int index)
    {
        switch (index)
        {
            case 1:
                currentSituation._isGrassOne = 1;
                break;
            case 2:
                currentSituation._isGrassTwo = 1;
                break;
            case 3:
                currentSituation._isGrassThree = 1;
                break;
            case 4:
                currentSituation._isSpiceOne = 1;
                break;
            case 5:
                currentSituation._isSpiceTwo = 1;
                break;
            case 6:
                currentSituation._isSpiceThree = 1;
                break;
            case 7:
                currentSituation._isSpiceFour = 1;
                break;
            default: //조리한 재료들이 올라올 경우의 분기
                Debug.Log("재료 인덱싱 오류");
                break;
        }
    }

    // 클리어 여부 판정
    public bool CheckCorrect(int orderindex)
    {
        if (currentSituation._isGrassOne != answerArray[orderindex]._isGrassOne) //현재 주문의 Grass1 플래그가 정답의 Grass1 플래그와 다를 경우
        {
            if (answerArray[orderindex]._isGrassOne != 0) //0(상관없음)의 경우는 배제
            {
                return false;
            }
        }
        if (currentSituation._isGrassTwo != answerArray[orderindex]._isGrassTwo) //현재 주문의 Grass2 플래그가 정답의 Grass2 플래그와 다를 경우
        {
            if (answerArray[orderindex]._isGrassTwo != 0) //0(상관없음)의 경우는 배제
            {
                return false;
            }
        }
        if (currentSituation._isGrassThree != answerArray[orderindex]._isGrassThree) //현재 주문의 Grass3 플래그가 정답의 Grass3 플래그와 다를 경우
        {
            if (answerArray[orderindex]._isGrassThree != 0) //0(상관없음)의 경우는 배제
            {
                return false;
            }
        }
        if (currentSituation._isSpiceOne != answerArray[orderindex]._isSpiceOne) //현재 주문의 Spice1 플래그가 정답의 Spice1 플래그와 다를 경우
        {
            if (answerArray[orderindex]._isSpiceOne != 0) //0(상관없음)의 경우는 배제
            {
                return false;
            }
        }
        if (currentSituation._isSpiceTwo != answerArray[orderindex]._isSpiceTwo) //현재 주문의 Spice2 플래그가 정답의 Spice2 플래그와 다를 경우
        {
            if (answerArray[orderindex]._isSpiceTwo != 0) //0(상관없음)의 경우는 배제
            {
                return false;
            }
        }
        if (currentSituation._isSpiceThree != answerArray[orderindex]._isSpiceThree) //현재 주문의 Spice3 플래그가 정답의 Spice3 플래그와 다를 경우
        {
            if (answerArray[orderindex]._isSpiceThree != 0) //0(상관없음)의 경우는 배제
            {
                return false;
            }
        }
        if (currentSituation._isSpiceFour != answerArray[orderindex]._isSpiceFour) //현재 주문의 Spice4 플래그가 정답의 Spice4 플래그와 다를 경우
        {
            if (answerArray[orderindex]._isSpiceFour != 0) //0(상관없음)의 경우는 배제
            {
                return false;
            }
        }
        if (currentSituation._isToolOne != answerArray[orderindex]._isToolOne) //현재 주문의 Tool1 플래그가 정답의 Tool1 플래그와 다를 경우
        {
            if (answerArray[orderindex]._isToolOne != 0) //0(상관없음)의 경우는 배제
            {
                return false;
            }
        }
        if (currentSituation._isToolTwo != answerArray[orderindex]._isToolTwo) //현재 주문의 Tool2 플래그가 정답의 Tool2 플래그와 다를 경우
        {
            if (answerArray[orderindex]._isToolTwo != 0) //0(상관없음)의 경우는 배제
            {
                return false;
            }
        }
        if (currentSituation._isToolThree != answerArray[orderindex]._isToolThree) //현재 주문의 Tool3 플래그가 정답의 Tool3 플래그와 다를 경우
        {
            if (answerArray[orderindex]._isToolThree != 0) //0(상관없음)의 경우는 배제
            {
                return false;
            }
        }
        return true;
    }
}