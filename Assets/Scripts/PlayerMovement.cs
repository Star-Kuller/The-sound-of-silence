using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f; // скорость передвижения
    private Rigidbody2D _rb; // компонент Rigidbody игрока

    private void Start()
    {
        // Получаем компонент Rigidbody
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Получаем ввод от пользователя
        var moveHorizontal = Input.GetAxisRaw("Horizontal");
        var moveVertical = Input.GetAxisRaw("Vertical") * 0.6;

        // Создаем вектор движения
        var movement = new Vector2(moveHorizontal, (float)moveVertical).normalized;

        // Применяем движение к Rigidbody
        _rb.velocity = movement * speed;
    }
}