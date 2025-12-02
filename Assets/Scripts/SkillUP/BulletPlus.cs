using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlus : SkillUp
{
    int addBullet = 3;
    public override void ApplySkillUp(Player player)
    {
       player.SkillUp(addBullet);
    }
}
