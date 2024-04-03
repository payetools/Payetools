// Copyright (c) 2023-2024, Payetools Foundation.
//
// Payetools Foundation licenses this file to you under the following license(s):
//
//   * The MIT License, see https://opensource.org/license/mit/

using Payetools.Common.Model;

namespace Payetools.Payroll.Model;

/// <summary>
/// Entity that provides access to information about an employee's partner, typically used in
/// Shared Parental Leave/Pay scenarios.
/// </summary>
public class EmployeePartnerDetails : IEmployeePartnerDetails
{
    private class NamedPerson : INamedPerson
    {
        public Title? Title { get; }

        public string? FirstName { get; }

        public char[]? Initials { get; }

        public string? MiddleNames { get; }

        public string LastName { get; }

        public string? KnownAsName { get; }

        public bool HasMiddleName => MiddleNames != null;

        public string? InitialsAsString => Initials != null ? string.Join(", ", Initials) : null;

        public NamedPerson(
            Title? title,
            string? firstName,
            char[]? initials,
            string? middleNames,
            string lastName,
            string? knownAsName)
        {
            Title = title;
            FirstName = firstName;
            Initials = initials;
            MiddleNames = middleNames;
            LastName = lastName;
            KnownAsName = knownAsName;
        }
    }

    /// <summary>
    /// Gets the name information (first name, last name, etc.) of the employee's partner.
    /// </summary>
    public INamedPerson NameInfo { get; }

    /// <summary>
    /// Gets the National Insurance number of the employee.
    /// </summary>
    public NiNumber NiNumber { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeePartnerDetails"/> class.
    /// </summary>
    /// <param name="title">Partner title.</param>
    /// <param name="forenames">Partner forename(s). At least one name must be supplied.</param>
    /// <param name="lastName">Partner last name.</param>
    /// <param name="niNumber">Partner National Insurance number.</param>
    /// <remarks>Initialising this type with partner initials rather than forenames is not currently supported.</remarks>
    /// <exception cref="ArgumentException">Thrown if less than one forename is supplied.</exception>
    public EmployeePartnerDetails(
        Title title,
        string[] forenames,
        string lastName,
        NiNumber niNumber)
    {
        if (forenames.Length < 1)
            throw new ArgumentException("EmployeePartnerDetails constructor requires at least one forename", nameof(forenames));

        NameInfo = new NamedPerson(
            title,
            forenames[0],
            null,
            forenames.Length > 1 ? string.Join(' ', forenames[..1]) : null,
            lastName,
            niNumber);
        NiNumber = niNumber;
    }
}
