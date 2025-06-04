# Payetools
## Cross-platform Open-Source Library for UK payroll

**Payetools is the first library of its kind to bring open-source capabilities to UK payroll processing via a robust, cross-platform framework written in C#.**

Payetools contains support for calculating the following:

- UK Income Tax (all jurisdictions)
- National Insurance
- Student Loans
- Pensions using Qualifying Earnings and Pensionable Pay earnings bases (under both Relief at Source and Net Pay Arrangement tax treatments)
- Compliance with National Minimum/Living Wage regulations

See the [Program.cs](https://github.com/payetools/Payetools/blob/main/examples/Payroll/Program.cs) of the example project for step-by-step
instructions on getting your first pay run output.

The code passes HMRC tests for income tax, National Insurance, student loans and National Minimum Wage.

Separately, Payetools has developed libraries for HMRC RTI and DPS services that sit on top of the core Payetools open-source libraries
published here. The **Payetools.Hmrc** libraries enable the publishing of FPS and EPS submissions to HMRC and the retrieval of notifications
(e.g., tax code changes, student loan starts/stops, etc.) from HMRC's Data Provisioning Service (DPS).  Payetools.Hmrc is available as a
commercial offering - please contact info [at] payetools.com for further information.

Commercial support for the Payetools open-source libraries is also available - please contact info [at] payetools.com for further information.

### Releases
Releases are made via nuget - search for payetools to see all libraries for this repo or follow [this link](https://www.nuget.org/packages?q=payetools).
Packages are built with compatibility with .NET 8.0 and 9.0.

Release notes can be found in [releases.md](doc/releases.md).

### Reference Data
Payetools relies on static reference data to drive tax, NI, etc., calculations (the machine-readable equivalent of HMRC's
**[Rates and thresholds for employers](https://www.gov.uk/guidance/rates-and-thresholds-for-employers-2024-to-2025)** page.
A version of this data set for testing can be found at https://uk-rates-and-thresholds-data.netlify.app/index.json.
(2022-2023, 2023-2024, 2024-2025 and 2025-2026 reference data sets are currently published.)

### Documentation
API documentation is available at https://payetools.dev/api/Payetools.html.  (This is a work in progress.)
 
### Contributing
Please contact payetools via dev [at] payetools.com if you are interested in contributing to the project.

### License
This project is licensed under the MIT License.
