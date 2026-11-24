using JJN.FSM;
using JJN.ScriptableObjects;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace JJN.PlayerSystem.Presenters
{
    public class HPVFXPresenter : MonoBehaviour
    {
        [SerializeField]
        private PlayerHP hp;

        private Image _hurtEffect;
        private int lastSavedHP;

        private void Start()
        {
            if (_hurtEffect == null)
            {
                _hurtEffect = GetComponent<Image>();
            }
            _hurtEffect.enabled = false;
            lastSavedHP = hp.HP_value;
        }
        void CheckHPUpdate(int num)
        {
            if (_hurtEffect == null)
            {
                _hurtEffect = GetComponent<Image>();
            }

            if (hp.HP_value < lastSavedHP)
            {
                _hurtEffect.enabled = true;
                StopAllCoroutines();
                StartCoroutine(DisableHurtEffect());
            }
            lastSavedHP = hp.HP_value;
        }
        IEnumerator DisableHurtEffect()
        {
            yield return new WaitForSeconds(.2f);
            if (!PlayerDeathController.IsDead)
            {
                _hurtEffect.enabled = false;
            }
        }
        void SetIsDead(bool isDead)
        {
            if (isDead)
            {
                StopAllCoroutines();
                _hurtEffect.enabled = true;
            }
            else
            {
                _hurtEffect.enabled = false;
                lastSavedHP = hp.HP_value;
            }
        }

        private void OnEnable()
        {
            PlayerHP.onDeath += SetIsDead;
            PlayerHP.onHPValueChanged += CheckHPUpdate;
        }

        private void OnDisable()
        {
            PlayerHP.onDeath -= SetIsDead;
            PlayerHP.onHPValueChanged -= CheckHPUpdate;
        }
    }
}
