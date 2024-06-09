using static System.Net.Mime.MediaTypeNames;

namespace DesignPatternC_.ChainOfResponsibility;

public class CreatureModifier
{
    protected readonly Creature _creature;
    protected CreatureModifier? _nextModifier;

    public CreatureModifier(Creature creature)
    {
        _creature = creature;
    }

    public void SetNextModifier(CreatureModifier nextModifier)
    {
        if (this._nextModifier != null)
        {
            _nextModifier.SetNextModifier(nextModifier);
        }
        else
        {
            _nextModifier = nextModifier;
        }
    }

    public virtual void Handle() => _nextModifier?.Handle();
}