using OnBoardingDigital.Domain.Common;
using System.Data;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldType : Enumeration
{
    public static readonly FieldType Choice = new((int)AttributeFieldTypes.Choice, "Choice");
    public static readonly FieldType File = new((int)AttributeFieldTypes.File, "File");
    public static readonly FieldType Number = new((int)AttributeFieldTypes.Number, "Number Manager");
    public static readonly FieldType Options = new((int)AttributeFieldTypes.Options, "Options");
    public static readonly FieldType Text = new((int)AttributeFieldTypes.Text, "Text");
    public static readonly FieldType Information = new((int)AttributeFieldTypes.Information, "Information");
    public static readonly FieldType DateTime = new((int)AttributeFieldTypes.DateTime, "DateTime");
    
    public FieldType(int id, string name)
        : base(id, name)
    {
    }

    public static IEnumerable<FieldType> List()
    {
        return new[] {  Choice, File, Number, Options, Text, Information, DateTime };
    }

    public static FieldType FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new ArgumentException($"Possible values for FieldType: {String.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static FieldType From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new ArgumentException($"Possible values for FieldType: {String.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

#pragma warning disable CS8618
    private FieldType()
    {
    }
#pragma warning restore CS8618
}

public enum AttributeFieldTypes
{
    Choice = 67,
    DateTime = 68,
    File = 70,
    Information = 73,
    Number = 78,
    Options = 79,
    Text = 84
}