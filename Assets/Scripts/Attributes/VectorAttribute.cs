using UnityEngine;

public class VectorAttribute : PropertyAttribute
{
    public readonly string[] Labels;

    public VectorAttribute(params string[] labels)
    {
        Labels = labels;
    }
}