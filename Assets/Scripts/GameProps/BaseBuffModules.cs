using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseBuffModule : ScriptableObject
{
    //抽象类,具体逻辑由子类实现
    public abstract void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null);
}