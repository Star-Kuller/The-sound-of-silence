using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f; // скорость передвижения
    private Rigidbody2D _rb; // компонент Rigidbody игрока
    private Animator _animator; // компонент Animator игрока

    private void Start()
    {
        // Получаем компонент Rigidbody
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Получаем ввод от пользователя
        var moveHorizontal = Input.GetAxisRaw("Horizontal");
        _animator.SetInteger("MoveHorizontal",  (int)Math.Round(moveHorizontal));
        var moveVertical = Input.GetAxisRaw("Vertical") * 0.6;
        _animator.SetInteger("MoveVertical", (int)Math.Round(moveVertical));
        Debug.Log($"h = {moveVertical} v = {moveHorizontal}");

        // Создаем вектор движения
        var movement = new Vector2(moveHorizontal, (float)moveVertical).normalized;

        // Применяем движение к Rigidbody
        _rb.velocity = movement * speed;
    }
}