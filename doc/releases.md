# Releases

# 1.1.20

Full changelog: [1.1.17 -> 1.1.20](https://github.com/payetools/Payetools/compare/1.1.17...1.1.20)

## What's Changed

### Misc
- Fix: Corrected issue with non-cumulative tax codes generating negative tax amounts when the 
  employee's pay was less than the tax-free allowance
- Misc: Updated dependencies

# 1.1.17

Full changelog: [1.1.16 -> 1.1.17](https://github.com/payetools/Payetools/compare/1.1.16...1.1.17)

## What's Changed

### Misc
- Misc: Allowed for enum values to be converted in case-insensitive fashion when parsing from string

# 1.1.16

Full changelog: [1.1.15 -> 1.1.16](https://github.com/payetools/Payetools/compare/1.1.15...1.1.16)

## What's Changed

### Misc
- Feat: Added IsWIthinRange method to DateRange class
- Feat: Added IsWithin method to TaxYear class

# 1.1.15

Full changelog: [1.1.14 -> 1.1.15](https://github.com/payetools/Payetools/compare/1.1.14...1.1.15)

## What's Changed

### Misc
- Chore: Updated dependency (microsoft.extensions.http)
- Misc: Removed #ifdefs for pre- .NET 8.0
- Test: Replaced all references to FluentAssertions with Shouldly

# 1.1.14

Full changelog: [1.1.13 -> 1.1.14](https://github.com/payetools/Payetools/compare/1.1.13...1.1.14)

## What's Changed

### Misc
- Chore: Updated dependencies (nuke.common, fluentassertions, coverlet.collector, microsoft.extensions.logging.console, microsoft.extensions.http)

# 1.1.13

Full changelog: [1.1.12 -> 1.1.13](https://github.com/payetools/Payetools/compare/1.1.12...1.1.13)

## What's Changed

### Misc
- Chore: Updated dependencies (xunit, xunit.runner.visualstudio, coverlet.collector)

# 1.1.12

Full changelog: [1.1.11 -> 1.1.12](https://github.com/payetools/Payetools/compare/1.1.11...1.1.12)

## What's Changed

### Misc
- Feat: Added YTD calculation for Statutory Neonatal Care Pay

# 1.1.11

Full changelog: [1.1.10 -> 1.1.11](https://github.com/payetools/Payetools/compare/1.1.10...1.1.11)

## What's Changed

### Misc
- Chore: Updated dependency for xunit.runner.visualstudio

# 1.1.10

Full changelog: [1.1.9 -> 1.1.10](https://github.com/payetools/Payetools/compare/1.1.9...1.1.10)

## What's Changed

### Misc
- Feat: Added Statutory Neonatal Care Pay (applicable from April 2025) to employee payroll history
and to payroll output

# 1.1.9

Full changelog: [1.1.8 -> 1.1.9](https://github.com/payetools/Payetools/compare/1.1.8...1.1.9)

## What's Changed

### Misc
- Feat: Added support for recording and reclaiming Statutory Neonatal Care Pay (applicable from
April 2025)

# 1.1.8

Full changelog: [1.1.7 -> 1.1.8](https://github.com/payetools/Payetools/compare/1.1.7...1.1.8)

## What's Changed

### Misc
- Feat: Added field to Employment record for employee's workplace postcode (required from April 2025 for employees
working in Freeport and Investment Zone locations)

# 1.1.7

Full changelog: [1.1.6 -> 1.1.7](https://github.com/payetools/Payetools/compare/1.1.6...1.1.7)

## What's Changed

### Misc
- Feat: Added enumeration for tax year 2025-2026