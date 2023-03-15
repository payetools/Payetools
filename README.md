# Paytools.Core
## .NET Library for UK payroll

--- **Preliminary Release** (March 2023) ---

This is the first public release of Paytools.Core; it contains support for calculating the following:

- UK Income Tax (all jurisdictions)
- National Insurance
- Student Loans
- Pensions using Qualifying Earnings and Pensionable Pay earnings bases (under both Relief at Source and Net Pay Arrangemnet tax treatments)
- Compliance with National Minimum/Living Wage regulations

It is a work in progress.  The code passes HMRC tests for income tax and National Insurance, with the exception of two-weekly payrolls, where there is a small mismatch.

Tests are based on 2022-2023 tax year rates and thresholds.  Tests for 2023-2024 will be added shortly.

### Documentation
The source code is annotated with XML comments which will be used as the source material for API documentation.  The plan is to generate a complete set of document using DocFx but there are some issues with that software at this time.  In any event documentation will be released during Q2 2023.

### Releases
The plan is to build and release the Paytools.Core libraries on a regular basis and to publish all supported modules via Nuget.  The first such publication is planned for April 2023.

### Roadmap
Support for running a complete set of payroll calculations is planned for release in Q2 2023.

### Contributing
Given the current pipeline of work, we are not quite ready to receive contributions at this stage but if you are interested in joining the project, please contact paytools-master via Github.

### License
This project is licensed under the Apache 2.0 open source license (see LICENSE.md).
