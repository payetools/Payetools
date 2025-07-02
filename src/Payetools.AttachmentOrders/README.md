# Payetools.AttachmentOrders

## Introduction

This library provides the facilites to calculate attachment order deductions.  The term ***attachment order*** is used here as a generic term for all types of deductions from net earnings mandated by law (often by the courts) and communicated externally to employers, regardless of source or type.  There are many distinct types of attachment order deductions that can be made from an employee's earnings; these are listed further below.

The legislation around attachment orders is complex and varies by jurisdiction. Although somewhat outdated, the HMCS document [Attachment Orders: A Guide for Employers](https://www.rbkc.gov.uk/pdf/aoehandbook-eng.pdf) provides a good overview of the process. Lewes and Eastbourne Councils also have a useful [Attachment of Earnings - Employers Guide](https://www.lewes-eastbourne.gov.uk/media/4064/Attachment-of-earnings-employers-guide/pdf/Attachment_of_Earnings_-Employer_Guide_1.pdf).

## Approach

The approach taken in the library is to provide a set of generic calculations that can be used to cover all the attachment order types prescribed by law across the UK.  Similar to Statutory Maternity Pay, the approach is to split the definition of specific attachment orders into a set of rules that can be applied to the generic calculations.  This allows for the addition of new attachment order types without needing to change the core calculations.  These rules must be implemented separately, typically in the user interface or application layer.  The following sections provide an overview of the different types of attachment orders and the rules that should be associated with them.

### Jurisdictional Variations

Due to the different legislators across the UK, different types of attachment orders apply in different jurisdictions, some across multiple jurisdictions, and some only in specific jurisdictions, as follows:

| Type                                       | Applicability            | Issued By                                                                           | Use                                                                                                                                    |
|--------------------------------------------|--------------------------|-------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------------------------------------|
| Direct Earnings Attachment (DEA)           | England, Scotland, Wales | DWP, Local Authority                                                                | Recovery of overpayment of benefits                                                                                                    |
| Deductions from Earnings Order (DEO)       | All of UK                | Child Maintenance Service (CMS) (Northern Ireland has a separate CMS, part of DfC)  | Child maintenance                                                                                                                      |
| Council Tax Attachment of Earnings (CTAOE) | England, Wales           | Magistrates Court                                                                   | Recovery of Council Tax arrears                                                                                                        |
| Attachment of Earnings order (AOE)         | England, Wales           | High Court, County Court, Magistrates Court                                         |                                                                                                                                        |
| Attachment of Earnings Order (AOE)         | Northern Ireland         | Northern Ireland Enforcement of Judgments Office                                    | Recovery of Rate arrears                                                                                                               |
| Earnings Arrestment (EA)                   | Scotland                 |                                                                                     | Recovery of Council Tax arrears, utility charges, unpaid fines, overpayment of benefits, unpaid debts, rent arrears, child maintenance |
| Current Maintenance Arrestment (CMA)       | Scotland                 |                                                                                     |                                                                                                                                        |                                         |                          |                                                                                     |                                                                                                                                        |

The following orders types are used when more than one order has been issued for a given employee and where the orders can be consolidated into a single order, replacing the original orders:

| Type                                       | Applicability            |
|--------------------------------------------|--------------------------|
| Consolidated Attachment of Earnings Order (CAEO) | England, Wales                 |                                                                                     |                                                                                                                                        |                                         |                          |                                                                                     |                                                                                                                                        |
| Combined Attachment of Earnings Order  | Northern Ireland                 |                                                                                     |                                                                                                                                        |                                         |                          |                                                                                     |                                                                                                                                        |
| Conjoined Arrestment Order (CAO)           | Scotland                 |                                                                                     |                                                                                                                                        |


There are a small number of specific attachment orders that are not included in the above tables, such as Income Payment Orders (IPO), Council Tax Attachment of Allowances Orders (CTAAO) and Income Support Deduction Notices (ISDN), but these are sufficiently rate that they are not covered here.  It should nevertheless be possible to describe these orders using the generic calculations provided by this library.


library
does not 




The term Attachment Order used here is a generic term for all types of deductions from earnings communicated externally to employers and mandated by law, regardless of source or type.  There are in fact distinct types of deductions that can be made from an employee's earnings:
- Direct Earnings Attachment (DEA) - for tax debts
- Attachment of Earnings Order (AOE) - for court debts
- Child Maintenance Deduction from Earnings Order (CMEO) - for child maintenance debts
- Deduction from Earnings Order (DEO) - for benefit overpayments
- Deduction from Earnings Request (DER) - for benefit overpayments
- 

### Direct Earnings Attachment (DEA)
The Welfare Reform Act 2012, which became law in March 2012, allows DWP to instruct an employer to make deductions directly from a employee’s earnings, and it does this by instructing the employer to operate a Direct Earnings Attachment (DEA). DWP does not have to go through the civil courts to do this, unlike for example the Attachment of Earnings Order (AOE) process. Within the Welfare Reform Act, the legislation covering DEAs, part of the Social Security (Overpayment and Recovery) Regulations 2013, came into force on 8th April 2013. Note that these regulations are only in force in England, Scotland and Wales and so excludes Northern Ireland (and Channel Islands and the Isle of Man).

  Further information on DEAs can be found at:
  - [Direct earnings attachment: a guide for employers](https://www.gov.uk/government/publications/direct-earnings-attachments-an-employers-guide/direct-earnings-attachment-a-guide-for-employers) - rates and thresholds are provided in Table A
  - https://www.gov.uk/government/publications/direct-earnings-attachments-an-employers-guide/direct-earnings-attachment-a-more-detailed-guide

### Attachment of Earnings Order (AOE)

https://www.gov.uk/debt-deductions-from-employee-pay
https://www.gov.uk/make-benefit-debt-deductions
https://www.gov.uk/child-maintenance-for-employers

## Prioritisation of Attachment Orders
When an employee has multiple Attachment Orders, the order in which they are processed is important. The prioritisation of these orders is typically as follows:

### Scottish Regulations

The Diligence against Earnings (Variation) (Scotland) Regulations 2024
https://www.legislation.gov.uk/ssi/2024/293/made

[The Diligence against Earnings (Variation) (Scotland) Regulations 2021](https://www.legislation.gov.uk/ssi/2021/409/made) provides the rates and thresholds for Attachment Orders in Scotland.

### Northern Ireland Regulations

In Northern Ireland, there is no exact equivalent to a Direct Earnings Attachment (DEA) as used in Great Britain for the recovery of benefit overpayments (via the Department for Work and Pensions, DWP).
[Benefit debt deductions from your pay](https://www.nidirect.gov.uk/articles/benefit-debt-deductions-your-pay)

## Welsh Regulations

CT-AEO - new rates from 1 April 2022, see [The Council Tax (Administration 
and Enforcement) (Amendment) 
(Wales) Regulations 2022](https://www.legislation.gov.uk/wsi/2022/107/made)

## Interaction with Student Loan Deductions

The following is not true:

Student loan deductions should NOT be made at all if making them would reduce the employee’s net pay below their Protected Earnings Rate (PER) set by an attachment order. This applies across all UK jurisdictions.

https://www.gov.uk/hmrc-internal-manuals/collection-of-student-loans-manual/cslm18010