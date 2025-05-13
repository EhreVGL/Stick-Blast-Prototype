using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShapeSpawner : MonoBehaviour
{
    [Header("Veritabanı")]
    public ShapeDatabase shapeDatabase;
    public ShapeVisualLibrary visualLibrary;

    [Header("Prefab")]
    public GameObject shapePrefab;
    public static ShapeSpawner Instance;
    private void Awake() { Instance = this; }
    public GameObject SpawnShape(Vector3 spawnPosition, ShapeSlot slot)
    {
        List<ShapeData> pool = GetWeightedShapePool();
        int shapeIndex = Random.Range(0, pool.Count);

        ShapeData shape = pool[shapeIndex];
        int rotationIndex = Random.Range(0, shape.rotations.Count);

        Sprite sprite = visualLibrary.GetSprite(shape.shapeName, rotationIndex);

        // 1️⃣ Sağ ekran dışı başlangıç pozisyonu
        Vector3 startPosition = spawnPosition + new Vector3(10f, 0f, 0f); // sağdan 10 birim dışarıda başlasın
        GameObject shapeObj = Instantiate(shapePrefab, startPosition, Quaternion.identity);
        shapeObj.transform.localScale = new Vector3(0.25f, 0.25f, 1);

        // 2️⃣ Hedef pozisyona kayarak gelsin
        shapeObj.transform.DOMove(spawnPosition, 0.5f).SetEase(Ease.OutCubic);

        // 3️⃣ Sprite ve collider tanımı
        SpriteRenderer sr = shapeObj.GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sprite = sprite;

        BoxCollider2D collider = shapeObj.GetComponent<BoxCollider2D>();
        if (collider != null && sr != null && sr.sprite != null)
        {
            collider.size = sr.sprite.bounds.size;
            collider.offset = sr.sprite.bounds.center;
        }

        ShapeDragHandler drag = shapeObj.GetComponent<ShapeDragHandler>();
        if (drag != null)
            drag.Initialize(shape, rotationIndex, slot);
        
        slot.AssignShape(shapeObj);

        return shapeObj;
    }

    private List<ShapeData> GetWeightedShapePool()
    {
        List<ShapeData> pool = new List<ShapeData>();

        foreach (ShapeData shape in shapeDatabase.shapes)
        {
            int weight = GetWeightForShape(shape.shapeName);
            for (int i = 0; i < weight; i++)
            {
                pool.Add(shape);
            }
        }

        if (pool.Count == 0)
        {
            return null;
        }

        return pool;
    }

    private int GetWeightForShape(string shapeName)
    {
        switch (shapeName)
        {
            case "I": return 3;
            case "L": return 2;
            case "U": return 1;
            default:
                return 1;
        }
    }

    public GameObject SpawnSpecificShape(ShapeData shape, int rotationIndex)
    {
        ShapeSlot freeSlot = ShapeSlotManager.Instance.GetFirstEmptySlot();
        if (freeSlot == null)
        {
            return null;
        }

        Vector3 spawnPos = freeSlot.transform.position;

        Sprite sprite = visualLibrary.GetSprite(shape.shapeName, rotationIndex);

        GameObject shapeObj = Instantiate(shapePrefab, spawnPos, Quaternion.identity);
        shapeObj.transform.localScale = new Vector3(0.25f, 0.25f, 1);

        SpriteRenderer sr = shapeObj.GetComponent<SpriteRenderer>();
        if (sr != null) sr.sprite = sprite;

        BoxCollider2D col = shapeObj.GetComponent<BoxCollider2D>();
        if (col != null && sr.sprite != null)
        {
            col.size = sr.sprite.bounds.size;
            col.offset = sr.sprite.bounds.center;
        }

        ShapeDragHandler drag = shapeObj.GetComponent<ShapeDragHandler>();
        if (drag != null)
            drag.Initialize(shape, rotationIndex, freeSlot);

        freeSlot.AssignShape(shapeObj);

        return shapeObj;
    }

}
