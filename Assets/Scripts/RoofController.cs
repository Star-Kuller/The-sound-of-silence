using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoofController : MonoBehaviour
{
    private Collider2D _player;
    private TilemapRenderer _tilemap;
    private int _defaultLayer;
    private void Start()
    {
        _tilemap = GetComponent<TilemapRenderer>();
        _defaultLayer = _tilemap.sortingOrder;
        _player = GameObject.FindWithTag("Player").GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_player == other)
            _tilemap.sortingOrder = -10;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_player == other)
            _tilemap.sortingOrder = _defaultLayer;
    }
}
