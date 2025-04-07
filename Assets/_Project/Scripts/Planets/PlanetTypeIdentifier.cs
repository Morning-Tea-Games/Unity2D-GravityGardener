using Core;

namespace Planets
{
    public class PlanetTypeIdentifier
    {
        private readonly GameRules _rules;

        public PlanetTypeIdentifier(GameRules rules)
        {
            _rules = rules;
        }

        public bool TryDefine(PlanetBehaviour target, out PlanetType type)
        {
            float size = target.transform.localScale.y;

            for (int i = 0; i < _rules.PlanetSizes.Length; i++)
            {
                if (size <= _rules.PlanetSizes[i].MaxSize)
                {
                    type = _rules.PlanetSizes[i].Type;
                    return true;
                }
            }

            type = PlanetType.BlackHole;
            return false;
        }
    }
}