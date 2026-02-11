using System;

namespace RpgCharacters
{
    public sealed class Character
    {
        private double xp;
        private int hp;

        public string Name { get; }
        public IRace Race { get; }
        public ICategory Category { get; }

        public int MaxHp => Race.MaxHp;

        public int Hp
        {
            get => hp;
            set
            {
                if (value < 0 )
                    hp = 0;
                else if (value > MaxHp)
                    hp = MaxHp;
                else        
                hp = value;

                //set => hp = Math.Clamp(value, 0, MaxHp); gör samma sak som koden över bara bättre skriven. 
            }
        }

        public double Xp
        {
            get => xp;
            set
            {
                if (value < 0)
                    xp = 0;
                else if (value > 10)
                    xp = 10;
                else
                    xp = value;
            }
        }

        public bool IsDead => Hp == 0;

        public Character(string name, IRace race, ICategory category)
        {
            Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Name required.", nameof(name)) : name;
            Race = race ?? throw new ArgumentNullException(nameof(race));
            Category = category ?? throw new ArgumentNullException(nameof(category));
            Hp = race.MaxHp;
            Xp = category.InitialXp;
        }

        public double OnAttack(Random? rng = null)
        {
            rng ??= Random.Shared;

            Console.WriteLine(Category.GetAttackMessage(Name));
            
            double baseAttack = Category.ComputeBaseAttack(Race);
            double factor = NextDouble(rng, 0, Xp);
            return baseAttack * factor;
        }

        public double OnDefend(Random? rng = null)
        {
            rng ??= Random.Shared;

            Console.WriteLine(Category.GetDefendMessage(Name));
            
            double baseDefense = Category.ComputeBaseDefense(Race);
            double factor = NextDouble(rng, 0, Xp);
            return baseDefense * factor;
        }

        public string OnWin(Random? rng = null)
        {
            rng ??= Random.Shared;
            return Race.GetVictoryMessage(rng);
        }

        public override string ToString()
        {
            return $"{Name} ({Race.Name} {Category.Name}) - HP {Hp}/{MaxHp}, XP {Xp:0.00}";
        }

        private static double NextDouble(Random rng, double minValue, double maxValue)
        {
            if (minValue > maxValue) throw new ArgumentOutOfRangeException($"Invalid range.");

            return minValue + rng.NextDouble() * (maxValue - minValue);
        }
    }
}
