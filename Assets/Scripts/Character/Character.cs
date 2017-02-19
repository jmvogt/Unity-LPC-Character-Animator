using Assets.Scripts.Character.CharacterTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Character
{
    public abstract class Character
    {
        string name;
        ICharacterType characterType;
    }
}
