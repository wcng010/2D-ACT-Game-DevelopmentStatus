using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace C_Script.Player.Skill.Dash
{
    public class AfterImageDash : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer; 
        private Transform _player;
        private Transform _mytrans;
        private float _timer;
        [SerializeField] private float aliveTime;
        [SerializeField] private float alpha;

        private void Awake()
        {
            _player = GameObject.FindWithTag("Player").transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _mytrans = transform;
        }

        private void OnEnable()
        {
            _timer = 0;
            _spriteRenderer.sprite = _player.GetComponent<SpriteRenderer>().sprite;
            _mytrans.position = _player.position;
            _mytrans.localScale = _player.localScale;
            Invoke(nameof(SetFalse),aliveTime);
            InvokeRepeating(nameof(UpdateAlpha),0,0.1f);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
        }
        
        private void SetFalse() => gameObject.SetActive(false);
        private void UpdateAlpha() => _spriteRenderer.color =new Color( 1,1,1,alpha-(_timer / aliveTime));

    }
}
