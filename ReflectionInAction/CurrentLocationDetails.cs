using System.Linq.Expressions;
using System.Reflection;

namespace ReflectionInAction;

public class CurrentLocationDetails
{
    private CurrentLocationDetails()
    {
    }


    /// <summary>
    /// The location that the train is travelling from. This shall be provided if the location state is
    /// <see cref="MovementStatus.OnRoute"/> or <see cref="MovementStatus.Departed"/>.
    /// </summary>

    public string? OriginParentLocation { get; private set; }

    /// <summary>
    /// The child location that the train is travelling from. This shall be provided if the location state is
    /// <see cref="MovementStatus.OnRoute"/> or <see cref="MovementStatus.Departed"/>.
    /// </summary>

    public string? OriginChildLocation { get; private set; }

    /// <summary>
    /// The location that the train is travelling towards or is currently at. 
    /// This will not be provided if the location state is <see cref="MovementStatus.Unknown"/>.
    /// </summary>

    public string? DestinationParentLocation { get; private set; }

    /// <summary>
    /// The child location that the train is travelling towards or is currently at. 
    /// This will not be provided if the location state is <see cref="MovementStatus.Unknown"/>.
    /// </summary>
    public string? DestinationChildLocation { get; private set; }


    /// <summary>
    /// The direction that the train is travelling.
    /// </summary>
    /// <summary>
    /// A reason from the train disappearance.
    /// 
    /// Note: This shall be populated only when the state of the train is set to disappear.
    /// </summary>
    public string? DisappearReason { get; private set; }

    /// <summary>
    /// The date and time that the train last occupied a track.
    /// </summary>

    public DateTime LastOccupyDateTime { get; private set; }

    /// <summary>
    /// The date and time that the train last vacated a track.
    /// </summary>

    public DateTime LastVacancyDateTime { get; private set; }


    public static CurrentLocationDetails CreateNew()
    {
        return new CurrentLocationDetails();
    }

    public CurrentLocationDetails Set<T>(Expression<Func<CurrentLocationDetails, T>> propertyExpression, T value)
    {
        var memberExpression = propertyExpression.Body as MemberExpression;
        if (memberExpression == null)
        {
            throw new ArgumentException("Invalid property expression");
        }

        var propertyInfo = memberExpression.Member as PropertyInfo;
        if (propertyInfo == null)
        {
            throw new ArgumentException("Invalid property expression");
        }

        propertyInfo.SetValue(this, value);
        return this;
    }
}