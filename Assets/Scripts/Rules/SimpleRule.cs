using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rules/SimpleRule")]
public class SimpleRule : Rule
{
    public override void Init()
    {
        IsCompleted = false;
    }
}
