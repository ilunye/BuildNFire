using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Property_", menuName = "BuffSystem/Property", order = 1)]
public class ChangeProperty : BaseBuffModule
{
    [SerializeField]
    private Property property;

    public Property Property
    {
        get { return property; }
        set { property = value; }
    } //序列化，以便可以在unity窗口中显示


    public override void Apply(BuffInfo buffInfo, DamageInfo damageInfo = null)
    {
        var character = buffInfo.target.GetComponent<Character>();
        if (character)
        {
             character.buffproperty.attack += property.attack;
             character.buffproperty.speed += property.speed;
             character.buffproperty.sleep += property.sleep;
        }
    }
}
