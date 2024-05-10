# Payetools
## Cross-platform Open-Source Library for UK payroll

**Payetools is the first library of its kind to bring open-source capabilities to UK payroll processing via a robust, cross-platform framework written in C#.**

Payetools contains support for calculating the following:

- UK Income Tax (all jurisdictions)
- National Insurance
- Student Loans
- Pensions using Qualifying Earnings and Pensionable Pay earnings bases (under both Relief at Source and Net Pay Arrangement tax treatments)
- Compliance with National Minimum/Living Wage regulations

The code passes HMRC tests for income tax, National Insurance, student loans and National Minimum Wage.

Tests are included for 2022-2023 and 2023-2024 tax year rates and thresholds.

### Releases
Releases are made via nuget - search for payetools to see all libraries for this repo or follow [this link](https://www.nuget.org/packages?q=payetools).  Packages are built with compatibility with .NET 6.0, 7.0 and 8.0.

### Reference Data
Payetools relies on static reference data to drive tax, NI, etc., calculations (the machine-readable equivalent of
https://www.gov.uk/guidance/rates-and-thresholds-for-employers-2024-to-2025).  There is currently no high availability
source of this data, but a version for testing can be found at https://uk-rates-and-thresholds-data.netlify.app/index.json.
(Only 2022-2024 and 2023-2024 are published but 2024-2025 should be available before end May 2024.)

### Documentation
Limited API documentation is available at https://payetools.dev/api/Payetools.html.  This is very much a work in progress.
 
### Roadmap
To follow.

### Contributing
Given the current pipeline of work, we are not quite ready to receive contributions at this stage but if you are interested in joining the project, please contact payetools via dev [at] payetools.com.  Thanks in advance for your interest.

**It isn't recommended to fork this repo at this time, on the basis that there are a number of imminent changes in the pipeline.**  Once again, if there is a feature or capability you require, please contact us in the first instance.

### License
This project is licensed under the MIT License.
