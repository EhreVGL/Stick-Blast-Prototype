using UnityEngine;

public class ClosedCellChecker : MonoBehaviour
{
    public static ClosedCellChecker Instance;

    public void Awake(){
        Instance = this;
    }

    public void CheckSurroundindCells(Edge newEdge){
        // newEdge'in etkileyebileceği hücreleri kontrol et.
    }
}
