using System;

namespace RpgCharacters
{
    // Race
    public interface IRace
    {
        string Name { get; }
        int Strength { get; }
        int Agility { get; }
        int Intelligence { get; }
        int MaxHp { get; }
        string GetVictoryMessage(Random rng);
    }

    public abstract class RaceBase : IRace
    {
        private readonly string[] victoryM;

        public string Name {get; }
        public int Strength { get; }
        public int Agility { get; }
        public int Intelligence { get; }
        public int MaxHp { get; }

        protected RaceBase(string name, int strength, int agility, int intelligence, int maxHp, params string[] victoryMessage)
        {
            Name = name;
            Strength = strength;
            Agility = agility;
            Intelligence = intelligence;
            MaxHp = maxHp;

            victoryM = victoryMessage?.Length >0 ? victoryMessage : [$"{Name} celebrates!"];
        }

        public string GetVictoryMessage(Random rng)
        {
            return victoryM[rng.Next(victoryM.Length)];
        }
    }

    public sealed class FairyRace : RaceBase
    {
        public FairyRace() : base(
            "Fairy",
            2,
            5,
            9,
            20,
            " Fairy laughs triumphantly!"," Fairy sprinkles victory dust!", " Fairy dances in joy!")
        { }
    }

    public sealed class OrcRace : RaceBase
    {
        public OrcRace() : base(
            "Orc",
            10,
            3,
            2,
            40,
            " Orc roars victoriously!", " Orc pounds chest in triumph!", " Orc howls in victory!")
        { }
    }

    public sealed class ElfRace : RaceBase
    {
        public ElfRace() : base(
            "Elf",
            4,
            7,
            6,
            30,
            " Elf bows gracefully in victory!", " Elf smiles with satisfaction!", " Elf sings a victory song!")
        { }
    }

    public sealed class HumanRace : RaceBase
    {
        public HumanRace() : base(
            "Human",
            6,
            6,
            6,
            35,
            " Human raises a fist in triumph!", " Human shouts in victory!", " Human celebrates with a dance!")
        { }
    }
}
