using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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
    void Start()
    {
        ingredient_list = new List<Ingredient>();
        plate = -1;
        condiment = -1;
        grass = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }
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
    }
    public bool AddIngredient(Ingredient ingredient)
    {
        //���� üũ
        if (ingredient.idx < 3 && plate == -1 && grass == -1)
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


            return true;
        }
        //grass �߰�
        if (ingredient.idx < 3 && plate != -1 && grass == -1)
        {
            //ingredient_list[0] : plate
            if (ingredient_list[0].state == ingredient.state)
            {
                grass = ingredient.idx;
                ingredient_list.Add(ingredient);
                ingredient.transform.position = new Vector3(10, 10, 0);
                MixGrass(grass);
            }
            else if (ingredient_list[0].state == Ingredient_state.FRESH && ingredient.state == Ingredient_state.FRYED)
            {
                grass = ingredient.idx;
                ingredient_list.Add(ingredient);
                ingredient.transform.position = new Vector3(10, 10, 0);
                MixGrass(grass + 3); //3������ Ƣ��
            }
            else
            {
                //�߸��� �׸��� ��� �������� �׸��� 6���ε����� ������, �������� 3�� �ε����� ������
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

            }
<<<<<<< HEAD

=======
>>>>>>> e3f91faa4dee0c7da3142f7facc6e5c69b4f9e57
            return true;
        }//���̷� �߰�
        else if (ingredient.idx >= 3 && ingredient.idx <= 6 && grass != -1 && condiment == -1)
        {
            condiment = ingredient.idx;
            ingredient_list.Add(ingredient);

            ingredient.transform.position = new Vector3(10, 10, 0);
            return true;
        }// �׸� �߰�
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
