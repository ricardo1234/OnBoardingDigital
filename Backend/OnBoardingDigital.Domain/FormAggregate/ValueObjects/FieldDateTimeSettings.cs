using OnBoardingDigital.Domain.Common;

namespace OnBoardingDigital.Domain.FormAggregate.ValueObjects;

public class FieldDateTimeSettings : ValueObject
{
    public bool HasTime { get; private set; }
    public bool HasDate { get; private set; }
    public bool IsMinimumToday { get; private set; }
    public bool IsMaximumToday { get; private set; }
    public DateTime? Minimum { get; private set; }
    public DateTime? Maximum { get; private set; }


    private FieldDateTimeSettings(bool hasTime, bool hasDate, bool isMinimumToday, bool isMaximumToday, DateTime? minimum, DateTime? maximum)
    {
        //Todo: Validations
        HasTime = hasTime;
        HasDate = hasDate;
        IsMinimumToday = isMinimumToday;
        IsMaximumToday = isMaximumToday;
        Minimum = minimum;
        Maximum = maximum;
    }

    public static FieldDateTimeSettings Create(bool hasTime, bool hasDate, bool isMinimumToday, bool isMaximumToday, DateTime? minimum, DateTime? maximum) 
        => new(hasTime, hasDate, isMinimumToday, isMaximumToday, minimum, maximum);
    public static FieldDateTimeSettings CreateDateTime(bool isMinimumToday = false, bool isMaximumToday = false, DateTime? minimum = null, DateTime? maximum = null)
        => new(true, true, isMinimumToday, isMaximumToday, minimum, maximum);
    public static FieldDateTimeSettings CreateDate(bool isMinimumToday = false, bool isMaximumToday = false, DateTime? minimum = null, DateTime? maximum = null)
     => new(false, true, isMinimumToday, isMaximumToday, minimum, maximum);
    public static FieldDateTimeSettings CreateTime(bool isMinimumToday = false, bool isMaximumToday = false, DateTime? minimum = null, DateTime? maximum = null)
     => new(true, false, isMinimumToday, isMaximumToday, minimum, maximum);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return HasTime;
        yield return HasDate;
        yield return IsMinimumToday;
        yield return IsMaximumToday;
        yield return Minimum;
        yield return Maximum;
    }
#pragma warning disable CS8618
    private FieldDateTimeSettings()
    {
    }
#pragma warning restore CS8618
}