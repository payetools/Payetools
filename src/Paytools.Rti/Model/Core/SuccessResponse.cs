// Copyright (c) 2022-2023 Paytools Ltd
//
// Licensed under the Apache License, Version 2.0 (the "License")~
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#nullable disable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paytools.Rti.Model.Core;


// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.inlandrevenue.gov.uk/SuccessResponse")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.inlandrevenue.gov.uk/SuccessResponse", IsNullable = false)]
public partial class SuccessResponse : IRtiResponse
{

    private SuccessResponseIRmarkReceipt iRmarkReceiptField;

    private SuccessResponseMessage messageField;

    private System.DateTime acceptedTimeField;

    /// <remarks/>
    public SuccessResponseIRmarkReceipt IRmarkReceipt
    {
        get
        {
            return this.iRmarkReceiptField;
        }
        set
        {
            this.iRmarkReceiptField = value;
        }
    }

    /// <remarks/>
    public SuccessResponseMessage Message
    {
        get
        {
            return this.messageField;
        }
        set
        {
            this.messageField = value;
        }
    }

    /// <remarks/>
    public System.DateTime AcceptedTime
    {
        get
        {
            return this.acceptedTimeField;
        }
        set
        {
            this.acceptedTimeField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.inlandrevenue.gov.uk/SuccessResponse")]
public partial class SuccessResponseIRmarkReceipt
{

    private Signature signatureField;

    private SuccessResponseIRmarkReceiptMessage messageField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public Signature Signature
    {
        get
        {
            return this.signatureField;
        }
        set
        {
            this.signatureField = value;
        }
    }

    /// <remarks/>
    public SuccessResponseIRmarkReceiptMessage Message
    {
        get
        {
            return this.messageField;
        }
        set
        {
            this.messageField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2000/09/xmldsig#", IsNullable = false)]
public partial class Signature
{

    private SignatureSignedInfo signedInfoField;

    private string signatureValueField;

    private SignatureKeyInfo keyInfoField;

    /// <remarks/>
    public SignatureSignedInfo SignedInfo
    {
        get
        {
            return this.signedInfoField;
        }
        set
        {
            this.signedInfoField = value;
        }
    }

    /// <remarks/>
    public string SignatureValue
    {
        get
        {
            return this.signatureValueField;
        }
        set
        {
            this.signatureValueField = value;
        }
    }

    /// <remarks/>
    public SignatureKeyInfo KeyInfo
    {
        get
        {
            return this.keyInfoField;
        }
        set
        {
            this.keyInfoField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfo
{

    private SignatureSignedInfoCanonicalizationMethod canonicalizationMethodField;

    private SignatureSignedInfoSignatureMethod signatureMethodField;

    private SignatureSignedInfoReference referenceField;

    /// <remarks/>
    public SignatureSignedInfoCanonicalizationMethod CanonicalizationMethod
    {
        get
        {
            return this.canonicalizationMethodField;
        }
        set
        {
            this.canonicalizationMethodField = value;
        }
    }

    /// <remarks/>
    public SignatureSignedInfoSignatureMethod SignatureMethod
    {
        get
        {
            return this.signatureMethodField;
        }
        set
        {
            this.signatureMethodField = value;
        }
    }

    /// <remarks/>
    public SignatureSignedInfoReference Reference
    {
        get
        {
            return this.referenceField;
        }
        set
        {
            this.referenceField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoCanonicalizationMethod
{

    private string algorithmField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Algorithm
    {
        get
        {
            return this.algorithmField;
        }
        set
        {
            this.algorithmField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoSignatureMethod
{

    private string algorithmField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Algorithm
    {
        get
        {
            return this.algorithmField;
        }
        set
        {
            this.algorithmField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoReference
{

    private SignatureSignedInfoReferenceTransform[] transformsField;

    private SignatureSignedInfoReferenceDigestMethod digestMethodField;

    private string digestValueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Transform", IsNullable = false)]
    public SignatureSignedInfoReferenceTransform[] Transforms
    {
        get
        {
            return this.transformsField;
        }
        set
        {
            this.transformsField = value;
        }
    }

    /// <remarks/>
    public SignatureSignedInfoReferenceDigestMethod DigestMethod
    {
        get
        {
            return this.digestMethodField;
        }
        set
        {
            this.digestMethodField = value;
        }
    }

    /// <remarks/>
    public string DigestValue
    {
        get
        {
            return this.digestValueField;
        }
        set
        {
            this.digestValueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoReferenceTransform
{

    private string xPathField;

    private string algorithmField;

    /// <remarks/>
    public string XPath
    {
        get
        {
            return this.xPathField;
        }
        set
        {
            this.xPathField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Algorithm
    {
        get
        {
            return this.algorithmField;
        }
        set
        {
            this.algorithmField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureSignedInfoReferenceDigestMethod
{

    private string algorithmField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Algorithm
    {
        get
        {
            return this.algorithmField;
        }
        set
        {
            this.algorithmField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureKeyInfo
{

    private SignatureKeyInfoX509Data x509DataField;

    /// <remarks/>
    public SignatureKeyInfoX509Data X509Data
    {
        get
        {
            return this.x509DataField;
        }
        set
        {
            this.x509DataField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2000/09/xmldsig#")]
public partial class SignatureKeyInfoX509Data
{

    private string x509CertificateField;

    /// <remarks/>
    public string X509Certificate
    {
        get
        {
            return this.x509CertificateField;
        }
        set
        {
            this.x509CertificateField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.inlandrevenue.gov.uk/SuccessResponse")]
public partial class SuccessResponseIRmarkReceiptMessage
{

    private byte codeField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte code
    {
        get
        {
            return this.codeField;
        }
        set
        {
            this.codeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.inlandrevenue.gov.uk/SuccessResponse")]
public partial class SuccessResponseMessage
{

    private uint codeField;

    private string valueField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint code
    {
        get
        {
            return this.codeField;
        }
        set
        {
            this.codeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value
    {
        get
        {
            return this.valueField;
        }
        set
        {
            this.valueField = value;
        }
    }
}

