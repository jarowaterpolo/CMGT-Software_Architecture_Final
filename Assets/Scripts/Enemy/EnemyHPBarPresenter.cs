using JJN.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace JJN.Enemy
{
    public class EnemyHPBarPresenter : MonoBehaviour
    {
        [SerializeField]
        private Enemy enemy;
        [SerializeField]
        private GameObject fill;
        [SerializeField]
        private IntValue enemyMaxHP;

        private Slider hpBar;

        void UpdateHPBar(int value)
        {
            hpBar.value = value;

            if (value <= 0)
            {
                fill.SetActive(false);
            }
            else
            {
                fill.SetActive(true);
            }
        }

        public void SetStartHPBar()
        {
            hpBar.maxValue = enemyMaxHP.InitialValue;
            hpBar.value = enemy.CurrentHP;

            fill.SetActive(enemy.CurrentHP > 0);
        }

        private void OnEnable()
        {
            hpBar = GetComponent<Slider>();
            SetStartHPBar();
            enemy.OnHPChanged += UpdateHPBar;
        }

        private void OnDisable()
        {
            enemy.OnHPChanged += UpdateHPBar;
        }
    }
}
