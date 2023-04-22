using OnBoardingDigital.Domain.Common;
using System.Data;

namespace OnBoardingDigital.Domain.Form.ValueObjects;

public class FieldType : Enumeration
{
    public static FieldType Choice = new(((int)AttributeFieldTypes.Choice), "Choice");
    public static FieldType File = new(((int)AttributeFieldTypes.File), "File");
    public static FieldType Number = new(((int)AttributeFieldTypes.Number), "Number Manager");
    public static FieldType Options = new(((int)AttributeFieldTypes.Options), "Options");
    public static FieldType Text = new(((int)AttributeFieldTypes.Text), "Text");

    protected FieldType() { }

    public FieldType(int id, string name)
        : base(id, name)
    {
    }

    public static IEnumerable<FieldType> List()
    {
        return new[] {  Choice, File, Number, Options, Text };
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
}

public enum AttributeFieldTypes
{
    Choice = 67,
    File = 70,
    Number = 78,
    Options = 79,
    Text = 84
}