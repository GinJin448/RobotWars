﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour
{
    //敵のMaxHP
    [SerializeField]
    private int maxHp = 100;
    //敵のHP
    [SerializeField]
    private int hp;
    //敵の攻撃力
    [SerializeField]
    private int attackPower = 5;
    //敵の防御力
    [SerializeField]
    private int defensePower = 5;
    //HP表示用UI
    [SerializeField]
    private GameObject HPUI;
    //次にHPを減らすまでの時間
    [SerializeField]
    private float nextCountTime = 0f;
    
    private Slider hpSlider;                    //HP表示用スライダー
    private float countTime = 0f;               //HPを一度減らしてからの経過時間
    private int damage = 0;                     //現在のダメージ量
    private EnemyController enemyController;    //敵の制御スクリプト

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        hp = maxHp;
        hpSlider = HPUI.transform.Find("HPBar").GetComponent<Slider>();
        hpSlider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //　ダメージなければ何もしない
        if (damage == 0)
        {
            return;
        }
        //　次に減らす時間がきたら
        if (countTime >= nextCountTime)
        {
            //　ダメージ量を10で割った商をHPから減らす
            var tempDamage = damage / 10;
            //　商が0になったら余りを減らす
            if (tempDamage == 0)
            {
                tempDamage = damage % 10;
            }
            hp -= tempDamage;
            damage -= tempDamage;

            countTime = 0f;
        }

        UpdateHPValue();
        countTime += Time.deltaTime;
    }

    public void SetDamage(int opponentAttackPower)
    {
        var tempDamage = opponentAttackPower * 5 - defensePower * 2;

        if (tempDamage > 0)
        {
            damage += tempDamage;
        }
        else
        {
            damage += 1;
        }
        
        countTime = 0f;
    }

    public int GetHp()
    {
        return hp;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }

    public void UpdateHPValue()
    {
        hpSlider.value = (float)GetHp() / (float)GetMaxHp();

        if (GetHp() <= 0)
        {
            enemyController.SetState(EnemyController.EnemyState.Dead);
        }
    }

    public int GetAttackPower()
    {
        return attackPower;
    }

}