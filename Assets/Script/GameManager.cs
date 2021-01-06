using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public Color[] ShapeColors;
    public GameObject[] Cells;
    const int SIZE = 8;
    public int[,] Array = new int[SIZE, SIZE];

    static public Vector3[] spawnPoint = new Vector3[3];
    public GameObject[] prefab;
    GameObject[] chkBlock = new GameObject[3];
    int[] chkRand = new int[3];
    int random;
    int count;
    bool isEmpty;
    private void Awake()
    {
        spawnPoint[0] = new Vector3(-0.49f, -1.5f, 0);
        spawnPoint[1] = new Vector3(2.51f, -1.5f, 0);
        spawnPoint[2] = new Vector3(5.01f, -1.5f, 0);
    }
    private void Start()
    {
        AddBlock();
    }
    GameObject GetCell(int x, int y)
    {
        return Cells[y * SIZE + x];
    }
    bool InRange(int x, int y)
    {
        if (x < 0 || y < 0 || x >= SIZE || y >= SIZE) return false;
        return true;
    }
    bool InPossible(int x, int y)
    {
        if (Array[x, y] != 0) return false;
        return true;
    }
    public void BlockInput(BlockScript blockScript, int ColorIndex, Vector3 lastPos, Vector3[] blockPos)
    {
        for (int i = 0; i < blockPos.Length; i++)
        {
            Vector3 SumPos = blockPos[i] + lastPos;
            if (!InRange((int)SumPos.x, (int)SumPos.y)) return;
            if (!InPossible((int)SumPos.x, (int)SumPos.y)) return;
        }
        for (int i = 0; i < blockPos.Length; i++)
        {
            Vector3 SumPos = blockPos[i] + lastPos;
            Array[(int)SumPos.x, (int)SumPos.y] = ColorIndex;
            GetCell((int)SumPos.x, (int)SumPos.y).GetComponent<SpriteRenderer>().color = ShapeColors[ColorIndex];
        }
        blockScript.ForceDestroy();
        LineLogic();
        count++;
    }
    void Update()
    {
        if (count == 3)
        {
            count = 0;
            isEmpty = true;
        }
        if (isEmpty)
        {
            AddBlock();
        }
    }

    void LineLogic()
    {
        int oneLine = 0;
        for (int i = 0; i < SIZE; i++)
        {
            int horizontalCount = 0;
            int verticalCount = 0;
            for (int j = 0; j < SIZE; j++)
            {
                if (Array[j, i] != 0) ++horizontalCount;
                if (Array[i, j] != 0) ++verticalCount;
            }
            if (horizontalCount == SIZE)
            {
                ++oneLine;
                for (int j = 0; j < SIZE; j++) Array[j, i] = -1;
            }
            if (verticalCount == SIZE)
            {
                ++oneLine;
                for (int j = 0; j < SIZE; j++) Array[i, j] = -1;
            }
        }

        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (Array[i, j] == -1)
                {
                    Array[i, j] = 0;
                    GameObject CurCell = GetCell(i, j);
                    var doScale = CurCell.transform.DOScale(Vector3.zero, 0.5f);
                    doScale.OnComplete(() =>
                    {
                        CurCell.GetComponent<SpriteRenderer>().color = ShapeColors[0];
                        CurCell.transform.localScale = new Vector3(0.23f, 0.23f, 1);
                    });
                }
            }
        }
    }
    void AddBlock() // 블럭 생성
    {
        int index = 0;
        for (int i = 0; i < 3; i++)
        {
            random = Random.Range(0, prefab.Length);
            chkRand[i] = random; // 랜덤 정수 3개 chkRand 배열에 저장
        }
        if (chkRand[0] == chkRand[1] || chkRand[0] == chkRand[2] || chkRand[1] == chkRand[2]) // 숫자가 같다면 (같은 블럭 방지)
        {
            AddBlock();
            return;
        }
        for (int i = 0; i < 3; i++)
        {
            chkBlock[i] = Instantiate(prefab[chkRand[i]], spawnPoint[i], Quaternion.identity);
            chkBlock[i].tag = index.ToString();
            index++;
            isEmpty = false;
        }
    }
}