using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallLayersController : MonoBehaviour
{
    private SpriteRenderer _sprite;
    [SerializeField] private List<Collider2D> colliders = new List<Collider2D>();
    [SerializeField] private List<int> collidersOrder = new List<int>();
    private readonly Dictionary<Collider2D, int> _layers = new Dictionary<Collider2D, int>();
    private readonly Dictionary<Collider2D, int> _contacts = new Dictionary<Collider2D, int>();
    private const int DefaultLayer = 3;
    
    public void Start()
    {
        for (var i = 0; i < colliders.Count && i < collidersOrder.Count; i++)
        {
            _layers.Add(colliders[i], collidersOrder[i]);
        }
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_layers.TryGetValue(other, out var current)) return;

        if (_contacts.TryGetValue(other, out var contact))
        {
            Debug.Log("1 "+_contacts
                .Select(keyVal => keyVal.Value).Prepend(DefaultLayer).Max());
            _sprite.sortingOrder = _contacts
                .Select(keyVal => keyVal.Value).Prepend(DefaultLayer).Max();
        }
        else
        {
            _contacts.Add(other, current);
            Debug.Log("2 "+_contacts
                .Select(keyVal => keyVal.Value).Prepend(DefaultLayer).Max());
            _sprite.sortingOrder = _contacts
                .Select(keyVal => keyVal.Value).Prepend(DefaultLayer).Max();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_layers.TryGetValue(other, out var current)) return;

        _contacts.Remove(other);
        
        var layer = _contacts
                .Select(keyVal => keyVal.Value).Prepend(DefaultLayer).Max();

        _sprite.sortingOrder = layer;
    }
}
