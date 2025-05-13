using UnityEngine;

public struct Edge
{
    public Vector2Int A, B;

    public Edge(Vector2Int a, Vector2Int b){
        A = a;
        B = b;
    }

    public override bool Equals(object obj){
        if(!(obj is Edge)) return false;
        Edge other = (Edge)obj;

        return (A == other.A && B == other.B) || (A == other.B && B == other.A);
    }

    public override int GetHashCode(){
        return A.GetHashCode() ^ B.GetHashCode();
    }
}
