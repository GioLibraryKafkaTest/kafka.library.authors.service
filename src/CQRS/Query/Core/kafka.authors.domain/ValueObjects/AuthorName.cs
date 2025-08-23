using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kafka.authors.domain.ValueObjects;
public class AuthorName
{
  public AuthorName()
  {
    
  }
  [SetsRequiredMembers]
  private AuthorName(string firstName, string lastName) => (FirstName, LastName) = (firstName, lastName);

  public required string FirstName { get; set; }
  public required string LastName { get; set; }

  public static AuthorName Of(string firstName, string lastName)
  {
    if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
      return null;

    return new AuthorName(firstName, lastName);
  }
}