using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    public List<Ingredient> ingredient_list;
    public int PLATE_MIX_ERROR_IDX = 6;
    public int OTHER_MIX_ERROR_IDX = 3;

    public FoodSpriteChanger food_sprite_changer;

    public int plate;
    public int condiment;
    public int grass;
    public bool can_add;
    private void Awake()
    {
        ingredient_list = new List<Ingredient>();
    }
    void Start()
    {
        
        plate = -1;
        condiment = -1;
        grass = -1;
        can_add = true;
    }

    // Update is called once per frame
    public void clear_board()
    {
        for (int i = 0; i < ingredient_list.Count; i++)
        {/*
            ingredient_list[i].spriteChanger.ChangeToDefualtSprite()
            ingredient_list[i].gameObject.SetActive(false);*/

            Destroy(ingredient_list[i].gameObject);
        }
        ingredient_list.Clear();
        plate = -1;
        condiment = -1;
        grass = -1;
        can_add = true;
    }
    public bool AddIngredient(Ingredient ingredient)
    {
        // 추가 불가능한 경우 빠르게 false
        if (can_add == false) return false;

        if (ingredient.idx < 3 && plate == -1 && grass == -1)// 도마에 아무것도없는데 음식 올릴때 fail + cant_add
        {
            //������
            if (ingredient.state == Ingredient_state.BOILED)
            {
                ingredient.spriteRenderer.enabled = true;
                ingredient.spriteChanger.ChangeToBoilFail();
                ingredient.state = Ingredient_state.FAILD;

            }
            else if (ingredient.state == Ingredient_state.MIXED)
            {
                ingredient.spriteRenderer.enabled = true;
                ingredient.spriteChanger.ChangeToMixedFail();
                ingredient.state = Ingredient_state.FAILD;

            }

            ingredient_list.Add(ingredient);
            ingredient.state = Ingredient_state.FAILD;
            can_add = false; // 초기화 전까지 음식 추가 불가능

            return true;
        }
        //grass �߰�
        if (ingredient.idx < 3 && plate != -1 && grass == -1)  //정상적으로 그릇 추가 후 정상적으로 풀 넣은것 => 합치고 flag 설정하기
        {
            //ingredient_list[0] : plate
            if (ingredient_list[0].state == ingredient.state)
            {
                grass = ingredient.idx;
                ingredient_list.Add(ingredient);
                ingredient.transform.position = new Vector3(10, 10, 0);
                MixGrass(grass);
                print("state ::::::::::::: " + ingredient_list[1].state);
                if (ingredient_list[1].state == Ingredient_state.BOILED)
                    GameManager.gm.SetToolFlag(1, -1, -1);
                else if (ingredient_list[1].state == Ingredient_state.MIXED)
                    GameManager.gm.SetToolFlag(-1, 1, -1);

            }
            else if (ingredient_list[0].state == Ingredient_state.FRESH && ingredient.state == Ingredient_state.FRYED) //마찬가지로 합치고 flag 설정하기
            {
                grass = ingredient.idx;
                ingredient_list.Add(ingredient);
                ingredient.transform.position = new Vector3(10, 10, 0);
                MixGrass(grass + 3); //3������ Ƣ��
                print("state ::::::::::::: " + ingredient_list[1].state);

                GameManager.gm.SetToolFlag(-1, -1, 1);
            }
            else // 그릇과 재료의 state가 맞지않은 경우 => fail + cant_add
            {
                //에러지만 표현해야하는것들.. => can_add false
                ingredient.transform.position = new Vector3(10, 10, 0);
                if (ingredient_list[0].state == Ingredient_state.FRESH)
                {
                    if (ingredient.state == Ingredient_state.BOILED)
                    {
                        MixGrass(PLATE_MIX_ERROR_IDX);
                    }
                    else if (ingredient.state == Ingredient_state.MIXED)
                    {
                        MixGrass(PLATE_MIX_ERROR_IDX + 1);
                    }


                }


                else if (ingredient_list[0].state == Ingredient_state.BOILED)
                {
                    if (ingredient.state == Ingredient_state.FRESH)
                    {
                        MixGrass(OTHER_MIX_ERROR_IDX);
                    }
                    else if (ingredient.state == Ingredient_state.MIXED)
                    {
                        MixGrass(OTHER_MIX_ERROR_IDX + 1);
                    }
                    else if (ingredient.state == Ingredient_state.FRYED)
                    {
                        MixGrass(OTHER_MIX_ERROR_IDX + 2);
                    }
                }

                else if (ingredient_list[0].state == Ingredient_state.MIXED)
                {
                    if (ingredient.state == Ingredient_state.BOILED)
                    {
                        MixGrass(OTHER_MIX_ERROR_IDX);
                    }
                    else if (ingredient.state == Ingredient_state.FRESH)
                    {
                        MixGrass(OTHER_MIX_ERROR_IDX + 1);
                    }
                    else if (ingredient.state == Ingredient_state.FRYED)
                    {
                        MixGrass(OTHER_MIX_ERROR_IDX + 2);
                    }
                }


                ingredient_list.Add(ingredient);
                ingredient_list[0].state = Ingredient_state.FAILD;
                can_add = false;// 초기화 전까지 이제 추가 불가능

            }
            return true;
        }//���̷� �߰�
        else if (ingredient.idx >= 3 && ingredient.idx <= 6 && grass != -1 && condiment == -1) // 풀 추가 완료햇고 조미료 넣을 수 있는 경우(정상)
        {
            condiment = ingredient.idx;
            ingredient_list.Add(ingredient);

            ingredient.transform.position = new Vector3(10, 10, 0);
            return true;
        }// �׸� �߰�   플레이트를 놓을 수 있는경우(정상)
        else if (ingredient.idx > 6 && plate == -1)
        {

            plate = ingredient.idx;
            ingredient_list.Add(ingredient);
            food_sprite_changer = ingredient.gameObject.GetComponent<FoodSpriteChanger>();

            return true;
        }


        return false;
    }

    void MixGrass(int idx)
    {
        food_sprite_changer.ChangeSprite(idx);
    }

}
