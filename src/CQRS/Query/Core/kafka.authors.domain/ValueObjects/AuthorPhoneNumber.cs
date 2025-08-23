using System.Text.RegularExpressions;

namespace kafka.authors.domain.ValueObjects;
public partial record AuthorPhoneNumber
{
  private const int DefaultValue = 6;
  private const string Pattern = @"^(?:-*\d-*){6}$";

  public string Value { get; set; }

  public AuthorPhoneNumber(string value) => Value = value;

  public static AuthorPhoneNumber? Of(string value)
  {
    if(string.IsNullOrEmpty(value) || !PhoneNumberRegex().IsMatch(value) || value.Length != DefaultValue)
      return null;

    return new(value);
  }

  [GeneratedRegex(Pattern)]
  private static partial Regex PhoneNumberRegex();
}