﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 
namespace Paytools.Rti.Model {
    using Paytools.Rti.Model.Core;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.govtalk.gov.uk/CM/errorresponse")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.govtalk.gov.uk/CM/errorresponse", IsNullable=false)]
    public partial class ErrorResponse : IRtiResponse {
        
        private Application applicationField;
        
        private ErrorResponseError[] errorField;
        
        private string schemaVersionField;
        
        /// <remarks/>
        public Application Application {
            get {
                return this.applicationField;
            }
            set {
                this.applicationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Error")]
        public ErrorResponseError[] Error {
            get {
                return this.errorField;
            }
            set {
                this.errorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SchemaVersion {
            get {
                return this.schemaVersionField;
            }
            set {
                this.schemaVersionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.govtalk.gov.uk/CM/errorresponse")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.govtalk.gov.uk/CM/errorresponse", IsNullable=false)]
    public partial class Application {
        
        private System.Xml.XmlElement[] anyField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.govtalk.gov.uk/CM/errorresponse")]
    public partial class ErrorResponseError {
        
        private string raisedByField;
        
        private string numberField;
        
        private string typeField;
        
        private string[] textField;
        
        private string locationField;
        
        private Application applicationField;
        
        /// <remarks/>
        public string RaisedBy {
            get {
                return this.raisedByField;
            }
            set {
                this.raisedByField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer")]
        public string Number {
            get {
                return this.numberField;
            }
            set {
                this.numberField = value;
            }
        }
        
        /// <remarks/>
        public string Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Text")]
        public string[] Text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
            }
        }
        
        /// <remarks/>
        public string Location {
            get {
                return this.locationField;
            }
            set {
                this.locationField = value;
            }
        }
        
        /// <remarks/>
        public Application Application {
            get {
                return this.applicationField;
            }
            set {
                this.applicationField = value;
            }
        }
    }
}